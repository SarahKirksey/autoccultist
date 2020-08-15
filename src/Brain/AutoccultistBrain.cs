namespace Autoccultist.Brain
{
    using System.Collections.Generic;
    using System.Linq;
    using Autoccultist.Brain.Config;

    // TODO: This should not implement IGameState, as currently it just forwards to static classes.

    /// <summary>
    /// The AutoccultistBrain takes a BrainConfig and executes it against the game.
    /// </summary>
    public class AutoccultistBrain : IGameState
    {
        private BrainConfig config;

        private Goal currentGoal;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoccultistBrain"/> class.
        /// </summary>
        /// <param name="config">The config to use.</param>
        public AutoccultistBrain(BrainConfig config)
        {
            this.config = config;
        }

        /// <summary>
        /// Gets a value indicating whether the brain is running.
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>
        /// Starts the brain executing the configured plan.
        /// </summary>
        public void Start()
        {
            if (this.IsRunning)
            {
                return;
            }

            this.IsRunning = true;
            AutoccultistPlugin.Instance.LogInfo("Starting brain");
            this.ResetGoalIfSatisfiedOrNull();
        }

        /// <summary>
        /// Stops the brain executing the configured plan.
        /// </summary>
        public void Stop()
        {
            AutoccultistPlugin.Instance.LogInfo("Stopping brain");
            this.IsRunning = false;
        }

        /// <summary>
        /// Clears the current goal, resets progress tracking, and tries to obtain the first possible goal.
        /// </summary>
        /// <param name="configIn">The replacement config file to use, if desired.</param>
        public void Reset(BrainConfig configIn = null)
        {
            AutoccultistPlugin.Instance.LogInfo($"Resetting brain [new config: {configIn != null}]");

            if (!this.CanGoalActivate() || configIn != null)
            {
                this.currentGoal = null;
            }

            this.config = configIn ?? this.config;

            if (this.IsRunning && this.currentGoal == null)
            {
                this.ObtainNextGoal();
            }
        }

        /// <summary>
        /// Checks to see if the goal is still valid, and if any goal imperatives should be triggered.
        /// </summary>
        public void Update()
        {
            if (!this.IsRunning)
            {
                return;
            }

            this.ResetGoalIfSatisfiedOrNull();
            if (this.currentGoal != null)
            {
                this.TryStartImperatives();
            }
        }

        /// <summary>
        /// Dumps information on the state of the brain to the console.
        /// </summary>
        public void LogStatus()
        {
            AutoccultistPlugin.Instance.LogInfo(string.Format("My goal is {0}", this.currentGoal != null ? this.currentGoal.Name : "<none>"));
            AutoccultistPlugin.Instance.LogInfo(string.Format("I have {0} satisfiable imperatives", this.GetSatisfiableImperatives().Count));
            if (this.currentGoal != null)
            {
                foreach (var imperative in this.currentGoal.Imperatives.OrderByDescending(x => x.Priority))
                {
                    AutoccultistPlugin.Instance.LogInfo($"Imperative - {imperative.Name}");
                    AutoccultistPlugin.Instance.LogInfo($"Requirements satisfied: {imperative.Requirements.IsConditionMet(this)}");
                    AutoccultistPlugin.Instance.LogInfo($"-- Situation {imperative.Operation.Situation} available: {this.IsSituationAvailable(imperative.Operation.Situation)}");
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
                }
            }
        }

        /// <inheritdoc/>
        public bool IsSituationAvailable(string situationId)
        {
            return SituationOrchestrator.IsSituationAvailable(situationId);
        }

        /// <inheritdoc/>
        public bool CardsCanBeSatisfied(IReadOnlyCollection<ICardMatcher> choices)
        {
            return CardManager.CardsCanBeSatisfied(choices);
        }

        private void TryStartImperatives()
        {
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

                if (!SituationOrchestrator.IsSituationAvailable(operation.Situation))
                {
                    continue;
                }

                SituationOrchestrator.ExecuteOperation(operation);
            }
        }

        private void ResetGoalIfSatisfiedOrNull()
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

        private bool CanGoalActivate()
        {
            if (this.currentGoal == null)
            {
                return false;
            }

            return this.currentGoal.CanActivate(this);
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
