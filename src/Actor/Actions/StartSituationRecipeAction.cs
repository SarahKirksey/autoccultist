namespace Autoccultist.Actor.Actions
{
    class StartSituationRecipeAction : IAutoccultistAction
    {
        public string SituationId { get; private set; }


        public StartSituationRecipeAction(string situationId)
        {
            this.SituationId = situationId;
        }

        public void Execute()
        {
            if (!GameAPI.IsInteractable)
            {
                throw new ActionFailureException(this, "Game is not interactable at this moment.");
            }

            var situation = GameAPI.GetSituation(this.SituationId);
            if (situation == null)
            {
                throw new ActionFailureException(this, "Situation is not available.");
            }

            situation.AttemptActivateRecipe();
            if (situation.SituationClock.State == SituationState.Unstarted)
            {
                throw new ActionFailureException(this, "Failed to start situation.");
            }
        }
    }
}