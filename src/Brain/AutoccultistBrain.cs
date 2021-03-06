using System.Linq;
using System.Collections.Generic;
using Autoccultist.Brain.Config;

namespace Autoccultist.Brain
{
    public class AutoccultistBrain : IGameState
    {
        private BrainConfig config;

        private Goal currentGoal;

        public AutoccultistBrain(BrainConfig config)
        {
            this.config = config;
        }

        public void Start()
        {
            if (this.IsGoalSatisfied())
            {
                this.currentGoal = null;
            }

            if (this.currentGoal == null)
            {
                this.ObtainNextGoal();
            }
        }

        public void Stop()
        {
        }

        public void Update()
        {
            if (this.IsGoalSatisfied())
            {
                this.currentGoal = null;
            }

            if (this.currentGoal == null)
            {
                this.ObtainNextGoal();
                if (this.currentGoal == null)
                {
                    return;
                }
            }

            // Scan through all possible imperatives and invoke the ones that can start.
            //  Where multiple imperatives try for the same verb, invoke the highest priority
            var candidateGroups =
                from imperative in this.GetSatisfiableImperatives()
                orderby imperative.Priority descending
                group imperative.Operation by imperative.Operation.Situation into situationGroup
                select situationGroup;

            foreach (var group in candidateGroups)
            {
                var operation = group.FirstOrDefault();
                if (operation == null)
                {
                    continue;
                }

                if (!SituationOrchestrator.SituationIsAvailable(operation.Situation))
                {
                    continue;
                }

                SituationOrchestrator.ExecuteOperation(operation);
            }
        }

        public void LogStatus()
        {
            AutoccultistPlugin.Instance.LogInfo(string.Format("My goal is {0}", this.currentGoal != null ? this.currentGoal.Name : "<none>"));
            AutoccultistPlugin.Instance.LogInfo(string.Format("I have {0} satisfiable imperatives", this.GetSatisfiableImperatives().Count));
            if (this.currentGoal != null)
            {
                foreach (var imperative in this.currentGoal.Imperatives.OrderByDescending(x => x.Priority))
                {
                    AutoccultistPlugin.Instance.LogInfo($"Imperative - {imperative.Name}");
                    AutoccultistPlugin.Instance.LogInfo($"-- Situation {imperative.Operation.Situation} available: {this.SituationIsAvailable(imperative.Operation.Situation)}");
                    foreach (var choice in imperative.Operation.StartingRecipe.Slots)
                    {
                        AutoccultistPlugin.Instance.LogInfo($"-- Slot {choice.Key} satisfied: {this.CardsCanBeSatisfied(new[] { choice.Value })}");
                    }
                }
            }
            else
            {
                foreach (var goal in this.config.Goals)
                {
                    AutoccultistPlugin.Instance.LogInfo("Goal " + goal.Name);
                    if (goal.CompletedByCards.mode == ConditionMode.ALL_OF && goal.CompletedByCards.Requirements != null)
                    {
                        AutoccultistPlugin.Instance.LogInfo("-- Required cards (allof):");
                        foreach (CardChoice card in goal.RequiredCards.Requirements)
                        {
                            AutoccultistPlugin.Instance.LogInfo("-- -- " + card + " satisfied " + this.CardsCanBeSatisfied(new[] { card }));
                        }
                    }
                    if (goal.CompletedByCards.mode == ConditionMode.ANY_OF && goal.CompletedByCards.Requirements != null)
                    {
                        AutoccultistPlugin.Instance.LogInfo("-- Required cards (anyof):");
                        foreach (CardChoice card in goal.RequiredCards.Requirements)
                        {
                            AutoccultistPlugin.Instance.LogInfo("-- -- " + card + " satisfied " + this.CardsCanBeSatisfied(new[] { card }));
                        }
                    }

                    if (goal.CompletedByCards.mode == ConditionMode.ALL_OF && goal.CompletedByCards.Requirements != null)
                    {
                        AutoccultistPlugin.Instance.LogInfo("-- CompletedByCards cards (allof):");
                        foreach (CardChoice card in goal.CompletedByCards.Requirements)
                        {
                            AutoccultistPlugin.Instance.LogInfo("-- -- " + card + " satisfied " + this.CardsCanBeSatisfied(new[] { card }));
                        }
                    }

                    if (goal.CompletedByCards.mode == ConditionMode.ANY_OF && goal.CompletedByCards.Requirements != null)
                    {
                        AutoccultistPlugin.Instance.LogInfo("-- CompletedByCards cards (anyof):");
                        foreach (CardChoice card in goal.CompletedByCards.Requirements)
                        {
                            AutoccultistPlugin.Instance.LogInfo("-- -- " + card + " satisfied " + this.CardsCanBeSatisfied(new[] { card }));
                        }
                    }
                }
            }
        }

        public bool SituationIsAvailable(string situationId)
        {
            return SituationOrchestrator.SituationIsAvailable(situationId);
        }

        public bool CardsCanBeSatisfied(IReadOnlyCollection<ICardMatcher> choices)
        {
            return CardManager.CardsCanBeSatisfied(choices);
        }

        private IList<Imperative> GetSatisfiableImperatives()
        {
            if (this.currentGoal == null)
            {
                return new Imperative[0];
            }

            var imperatives =
                from imperative in this.currentGoal.Imperatives
                where imperative.CanExecute(this)
                select imperative;
            return imperatives.ToList();
        }

        private bool IsGoalSatisfied()
        {
            if (this.currentGoal == null)
            {
                return true;
            }
            return this.currentGoal.IsSatisfied(this);
        }

        private void ObtainNextGoal()
        {
            var goals =
                from goal in this.config.Goals
                where goal.CanActivate(this)
                select goal;
            this.currentGoal = goals.FirstOrDefault();
            AutoccultistPlugin.Instance.LogTrace($"Next goal is {this.currentGoal?.Name ?? "[none]"}");
        }
    }
}