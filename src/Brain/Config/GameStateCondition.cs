using System.Collections.Generic;

namespace Autoccultist.Brain.Config
{
    public class GameStateCondition
    {
        // The trend in yaml is to use properties for mode selectors.
        //  See kubernetes and docker compose files.

        public ConditionMode mode { get; }
        public List<CardChoice> Requirements { get; set; }

        private List<GameStateCondition> metaChildren;
        private bool IsMeta;

        public GameStateCondition(ConditionMode Mode, params CardChoice[] requirements)
        {
            mode = Mode;
            IsMeta = false;
            Requirements = new List<CardChoice>(requirements);
        }

        /*
        public GameStateCondition(ConditionMode Mode, params GameStateCondition[] requirements)
        {
            mode = Mode;
            IsMeta = true;
            metaChildren = new List<GameStateCondition>(requirements);
        }
        */


        public bool IsConditionMet(IGameState state)
        {
            if(IsMeta)
            {
                // If the mode is ANY, we want to return false only if all are false.
                // If the mode is ALL, we want to return true only if all are true.
                // We use the variable "AnyOf" to represent "true" for AnyOf cases, and to represent "false" in AllOf cases.
                bool AnyOf = mode == ConditionMode.ANY_OF;
                foreach (var condition in metaChildren)
                {
                    if (AnyOf == condition.IsConditionMet(state)) return AnyOf;
                }
                return !AnyOf;
            }

            if (mode == ConditionMode.ALL_OF)
            {
                return state.CardsCanBeSatisfied(this.Requirements);
            }
            else if (mode == ConditionMode.ANY_OF)
            {
                foreach (var card in this.Requirements)
                {
                    if (state.CardsCanBeSatisfied(new[] { card }))
                    {
                        return true;
                    }
                }
                return false;
            }

            return false;
        }

        public static GameStateCondition NeedsAllOf(params CardChoice[] requirements)
        {
            return new GameStateCondition(ConditionMode.ALL_OF, requirements);
        }

        public static GameStateCondition NeedsAnyOf(params CardChoice[] requirements)
        {
            return new GameStateCondition(ConditionMode.ANY_OF, requirements);
        }
    }

    public enum ConditionMode
    {
        ANY_OF,
        ALL_OF
    }
}