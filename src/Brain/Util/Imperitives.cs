using Autoccultist.Brain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoccultist.Brain.Util
{
    class Imperatives
    {
        /*
        public static Imperative HardLabor = new Imperative("Work for a living")
        {
            Priority = TaskPriority.Maintenance,
            Verb = "work",
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                    {"work", new CardChoice { ElementId = "health"}}
                }
            }
        };

        public static Imperative RefreshHealth = new Imperative("Refresh health")
        {
            Priority = TaskPriority.Maintenance,
            Verb = "dream",
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                    {"dream", new CardChoice{ElementId = "fatigue"}}
                }
            }
        };

        public static Imperative PaintAwayRestlessness = new Imperative("Paint away Restlessness")
        {
            Priority = TaskPriority.Critical,
            Verb = "work",
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                    {"work", new CardChoice {ElementId = "passion"}}
                }
            },
            OngoingRecipes = new Dictionary<string, RecipeSolution> {
                {"paintbasic", new RecipeSolution {
                    Slots = new Dictionary<string, CardChoice> {
                        {"Yearning", new CardChoice {ElementId = "restlessness"}}
                    }
                }}
            }
        };

        public static Imperative HealtAfflictionWithFunds = new Imperative("Heal Affliction with Money")
        {
            Priority = TaskPriority.Critical,
            Verb = "dream",
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                    {"dream", new CardChoice { ElementId = "affliction"}},
                    {"medicine", new CardChoice { ElementId = "funds"}}
                }
            }
        };

        public static Imperative ExerciseC = new Imperative("exerciseC")
        {
            Priority = TaskPriority.Maintenance,
            Verb = "study",
            ForbidWhenCardsPresent = GameStateCondition.NeedsAllOf(
                                            // Do not monopolize our study verb if we have two vitality waiting to be upgraded.
                                            new CardChoice { ElementId = "vitalityplus"},
                                            new CardChoice { ElementId = "vitalityplus"},
                                            new CardChoice { ElementId = "vitalityplus"}
                                        ),
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                                            { "study", new CardChoice{ElementId = "health"}}
                                        }
            }
        };

        public static Imperative ExerciseD = new Imperative("exerciseD")
        {
            Priority = TaskPriority.Maintenance,
            Verb = "study",
            ForbidWhenCardsPresent = GameStateCondition.NeedsAllOf(
                                            // Do not monopolize our study verb if we have two vitality waiting to be upgraded.
                                            new CardChoice { ElementId = "vitalityplus" },
                                            new CardChoice { ElementId = "vitalityplus" },
                                            new CardChoice { ElementId = "vitalityplus" },
                                            new CardChoice { ElementId = "vitalityplus" }
                                        ),
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                                            { "study", new CardChoice{ElementId = "health"}}
                                        }
            }
        };

        public static Imperative UpgradeVitalityC = new Imperative("Learn a lesson from Vitality")
        {
            Priority = TaskPriority.Maintenance,
            Verb = "study",
            ForbidWhenCardsPresent = GameStateCondition.NeedsAllOf(
                                            // Do not monopolize our study verb if we have two vitality waiting to be upgraded.
                                            new CardChoice { ElementId = "vitalityplus"},
                                            new CardChoice { ElementId = "vitalityplus"},
                                            new CardChoice { ElementId = "vitalityplus"}
                                        ),
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                                            { "study", new CardChoice{ElementId = "health"}}
                                        }
            }
        };

        public static Imperative GetSwoleC = new Imperative("Get Swole 3")
        {
            Priority = TaskPriority.Goal,
            Verb = "study",
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                                            {"study", new CardChoice { ElementId = "skillhealthb"}},
                                            {"V1", new CardChoice {ElementId="vitalityplus"}},
                                            {"V2", new CardChoice {ElementId="vitalityplus"}},
                                            {"V3", new CardChoice {ElementId="vitalityplus"}}
                                        }
            }
        };

        public static Imperative WorkPhysiqueC = new Imperative("Hardened Physique work")
        {
            Priority = TaskPriority.Maintenance,
            Verb = "work",
            ForbidWhenCardsPresent = GameStateCondition.NeedsAllOf(
                                            new CardChoice { ElementId = "vitalityplus" },
                                            new CardChoice { ElementId = "vitalityplus" },
                                            new CardChoice { ElementId = "vitalityplus" }
                                            ),
            StartingRecipe = new RecipeSolution
            {
                Slots = new Dictionary<string, CardChoice> {
                                            {"work", new CardChoice { ElementId = "skillhealthb"}},
                                            {"Health", new CardChoice { ElementId = "health"}}
                                        }
            }
        };*/
    }
}
