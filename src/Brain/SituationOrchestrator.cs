using System;
using System.Collections.Generic;
using System.Linq;
using Autoccultist.Brain.Config;

namespace Autoccultist.Brain
{
    public static class SituationOrchestrator
    {
        private static IDictionary<string, ISituationOrchestration> executingOperationsBySituation = new Dictionary<string, ISituationOrchestration>();

        public static void Update()
        {
            foreach (var executor in executingOperationsBySituation.Values.ToArray())
            {
                executor.Update();
            }

            // After we update the existing situation handlers, dump any completed ones if any remain.
            //  This is because some situations operate on their own volition, without an executor associated with them.
            foreach (var situation in GameAPI.GetAllSituations())
            {
                var situationId = situation.GetTokenId();

                if (executingOperationsBySituation.ContainsKey(situationId))
                {
                    // Already orchestrating this
                    continue;
                }

                if (situation.SituationClock.State != SituationState.Complete)
                {
                    // Situation is ongoing, no need to dump it.
                    continue;
                }

                // We still want to dump even if the situation has nothing in it
                //  This is particulary the case for situations that end with no output,
                //  such as suspicion-free suspicion

                DumpSituation(situationId);
            }
        }

        public static void LogStatus()
        {
            AutoccultistPlugin.Instance.LogInfo("We are orchestrating:");
            foreach (var entry in executingOperationsBySituation)
            {
                AutoccultistPlugin.Instance.LogInfo($"-- {entry.Key}: {entry.Value.GetType().Name}");
            }
        }

        public static bool SituationIsAvailable(string situationId)
        {
            var controller = GameAPI.GetSituation(situationId);
            if (controller == null)
            {
                // Not present on the board.
                return false;
            }

            if (controller.SituationClock.State != SituationState.Unstarted && controller.SituationClock.State != SituationState.Complete)
            {
                // Busy doing something.
                return false;
            }

            // Not busy, but we are still orchestrating it.
            return !executingOperationsBySituation.ContainsKey(situationId);
        }

        public static void ExecuteOperation(Operation operation)
        {
            if (!SituationIsAvailable(operation.Situation))
            {
                throw new OperationFailedException($"Cannot execute operation for situation {operation.Situation} because the situation is not available.");
            }

            if (executingOperationsBySituation.ContainsKey(operation.Situation))
            {
                return;
            }

            var orchestration = new OperationOrchestration(operation);
            executingOperationsBySituation[operation.Situation] = orchestration;
            orchestration.Completed += OnOrchestrationCompleted;
            orchestration.Start();
        }


        private static void DumpSituation(string situationId)
        {
            if (executingOperationsBySituation.ContainsKey(situationId))
            {
                throw new OperationFailedException($"Cannot dump situation {situationId} because the situation already has an orchestration running.");
            }

            var orchestration = new DumpSituationOrchestration(situationId);
            executingOperationsBySituation[situationId] = orchestration;
            orchestration.Completed += OnOrchestrationCompleted;
            orchestration.Start();
        }

        private static void OnOrchestrationCompleted(object sender, EventArgs e)
        {
            var orchestration = sender as ISituationOrchestration;
            orchestration.Completed -= OnOrchestrationCompleted;
            executingOperationsBySituation.Remove(orchestration.SituationId);
        }
    }
}