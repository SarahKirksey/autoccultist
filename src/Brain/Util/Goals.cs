using Autoccultist.Brain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoccultist.Brain.Config
{
    class Goals
    {
        /*
        public static Goal GetSwole4 = new Goal("Get Swole 4")
        {
            RequiredCards = new GameStateCondition(ConditionMode.ANY_OF, new CardChoice { ElementId = "skillhealthc" }),
            CompletedByCards = new GameStateCondition(ConditionMode.ANY_OF, new CardChoice { ElementId = "skillhealthd_strength" }, new CardChoice { ElementId = "skillhealthd_grace" }),

            Imperatives = new List<Imperative>
            {
                Imperatives.RefreshHealth,
                Imperatives.PaintAwayRestlessness,
                Imperatives.HealtAfflictionWithFunds,
                Imperatives.ExerciseD,
                new Imperative ("Learn a lesson from Vitality") {
                    Priority = TaskPriority.Goal,
                    Verb = "study",
                    ForbidWhenCardsPresent = new GameStateCondition (
                        ConditionMode.ALL_OF,
                        // Do not monopolize our study verb if we have enough vitality.
                        new CardChoice { ElementId = "vitalityplus"},
                        new CardChoice { ElementId = "vitalityplus"},
                        new CardChoice { ElementId = "vitalityplus"},
                        new CardChoice { ElementId = "vitalityplus"}
                    ),
                    StartingRecipe = new RecipeSolution
                    {
                        Slots = new Dictionary<string, CardChoice> {
                            {"study", new CardChoice{ElementId = "vitality"}},
                            {"morevitality", new CardChoice {ElementId = "vitality"}}
                        }
                    }
                },
                new Imperative ("Steely Physique work") {
                    Priority = TaskPriority.Maintenance,
                    Verb = "work",
                    ForbidWhenCardsPresent = new GameStateCondition (
                        ConditionMode.ALL_OF,
                            // Do not monopolize our skill card if we have enough vitality.
                            new CardChoice { ElementId = "vitalityplus"},
                            new CardChoice { ElementId = "vitalityplus"},
                            new CardChoice { ElementId = "vitalityplus"},
                            new CardChoice { ElementId = "vitalityplus"}
                    ),
                    StartingRecipe = new RecipeSolution
                    {
                        Slots = new Dictionary<string, CardChoice> {
                            {"work", new CardChoice { ElementId = "skillhealthc"}},
                            {"Health", new CardChoice { ElementId = "health"}}
                        }
                    }
                },
                new Imperative ("Get Swole 4") {
                    Priority = TaskPriority.Goal,
                    Verb = "study",
                    StartingRecipe = new RecipeSolution {
                        Slots = new Dictionary<string, CardChoice> {
                            {"study", new CardChoice { ElementId = "skillhealthc"}},
                            {"somethingmore", new CardChoice {ElementId="fragmentforge"}},
                            {"V1", new CardChoice {ElementId="vitalityplus"}},
                            {"V2", new CardChoice {ElementId="vitalityplus"}},
                            {"V3", new CardChoice {ElementId="vitalityplus"}},
                            {"V4", new CardChoice {ElementId="vitalityplus"}}
                        }
                    }
                }
            }
        };
        */
    }
}
