using System.Collections.Generic;
using Autoccultist.Brain.Config;
using Autoccultist.Brain.Util;

namespace Autoccultist.Preconfigured
{
    public class Aspirant
    {
        /*
        public static BrainConfig Config
        {
            get
            {
                return new BrainConfig
                {
                    Goals = new List<Goal> {
                        new Goal ("Begin Aspirant Intro") {
                            RequiredCards = new GameStateCondition (
                                ConditionMode.ALL_OF,
                                new CardChoice { ElementId = "introjob"}
                            ),
                            CompletedByCards = new GameStateCondition (
                                ConditionMode.ALL_OF,
                                    new CardChoice { ElementId = "bequestintro"}
                            ),
                            Imperatives = new List<Imperative> {
                                new Imperative ("Work the intro job") {
                                    Priority = TaskPriority.Goal,
                                    Operation = new Operation {
                                        Situation = "work",
                                        StartingRecipe = new RecipeSolution {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"work", new CardChoice { ElementId = "introjob"}}
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new Goal ("Study Bequest") {
                            RequiredCards = new GameStateCondition (
                                ConditionMode.ALL_OF,
                                    new CardChoice { ElementId = "bequestintro"}
                            ),
                            CompletedByCards = new GameStateCondition (
                                ConditionMode.ANY_OF,
                                    new CardChoice { ElementId = "ascensionpowera"},
                                    new CardChoice { ElementId = "ascensionenlightenmenta"},
                                    new CardChoice { ElementId = "ascensionsensationa"}
                            ),
                            Imperatives = new List<Imperative> {
                                HardLabor,
                                new Imperative ("Study the Bequest") {
                                    Priority = TaskPriority.Goal,
                                    Operation = new Operation {
                                        Situation = "study",
                                        StartingRecipe = new RecipeSolution {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"study", new CardChoice { ElementId = "bequestintro"}},
                                                {"Approach", new CardChoice { ElementId = "passion"}}
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new Goal("Find a Collaborator") {
                            RequiredCards = new GameStateCondition (
                                ConditionMode.ALL_OF,
                                    new CardChoice { ElementId = "contactintro"}
                            ),
                            CompletedByCards = new GameStateCondition (
                                ConditionMode.ANY_OF,
                                    new CardChoice {
                                        Aspects = new Dictionary<string, int> {
                                            {"acquaintance", 1}
                                        }
                                    }
                            ),
                            Imperatives = new List<Imperative> {
                                HardLabor,
                                PaintAwayRestlessness,
                                new Imperative ("Contact Colaborator") {
                                    Priority = TaskPriority.Goal,
                                    Operation = new Operation {
                                        Situation = "study",
                                        StartingRecipe = new RecipeSolution {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"study", new CardChoice { ElementId = "contactintro"}}
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new Goal ("Get Swole 1") {
                            RequiredCards = new GameStateCondition (
                                ConditionMode.ALL_OF,
                                    new CardChoice { ElementId = "health"}
                            ),
                            CompletedByCards = new GameStateCondition (
                                ConditionMode.ANY_OF,
                                    new CardChoice { ElementId = "skillhealtha"},
                                    new CardChoice { ElementId = "skillhealthb"},
                                    new CardChoice { ElementId = "skillhealthc"},
                                    new CardChoice { ElementId = "skillhealthd_strength" },
                                    new CardChoice { ElementId = "skillhealthd_grace" }
                            ),
                            Imperatives = new List<Imperative> {
                                HardLabor,
                                PaintAwayRestlessness,
                                new Imperative ("Increase health skill from Vitality") {
                                    Priority = TaskPriority.Goal,
                                    Operation = new Operation {
                                        Situation = "study",
                                        StartingRecipe = new RecipeSolution
                                        {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"study", new CardChoice{ElementId = "vitality"}},
                                                {"morevitality", new CardChoice {ElementId = "vitality"}}
                                            }
                                        }
                                    }
                                },
                                RefreshHealth
                            }
                        },
                        new Goal ("Get Swole 2") {
                            RequiredCards = new GameStateCondition (
                                ConditionMode.ALL_OF,
                                    new CardChoice { ElementId = "skillhealtha"}
                                ),
                            CompletedByCards = new GameStateCondition (
                                ConditionMode.ANY_OF,
                                    new CardChoice { ElementId = "skillhealthb"},
                                    new CardChoice { ElementId = "skillhealthc"},
                                    new CardChoice { ElementId = "skillhealthd_strength" },
                                    new CardChoice { ElementId = "skillhealthd_grace" }
                                ),
                            Imperatives = new List<Imperative> {
                                RefreshHealth,
                                new Imperative ("exercise") {
                                    Priority = TaskPriority.Maintenance,
                                    ForbidWhenCardsPresent = new GameStateCondition (
                                        ConditionMode.ALL_OF,
                                        // Do not monopolize our study Situation if we have two vitality waiting to be upgraded.
                                        new CardChoice { ElementId = "vitalityplus"},
                                        new CardChoice { ElementId = "vitalityplus"}
                                    ),
                                    Operation = new Operation {
                                        Situation = "study",
                                        StartingRecipe = new RecipeSolution
                                        {
                                            Slots = new Dictionary<string, CardChoice> {
                                                { "study", new CardChoice{ElementId = "health"}}
                                            }
                                        }
                                    }
                                },
                                PaintAwayRestlessness,
                                HealtAfflictionWithFunds,
                                new Imperative ("Learn a lesson from Vitality") {
                                    Priority = TaskPriority.Goal,
                                    ForbidWhenCardsPresent = new GameStateCondition (
                                            ConditionMode.ALL_OF,
                                            // Do not monopolize our study Situation if we have two vitality waiting to be upgraded.
                                            new CardChoice { ElementId = "vitalityplus"},
                                            new CardChoice { ElementId = "vitalityplus"}
                                        ),
                                    Operation = new Operation {
                                        Situation = "study",
                                        StartingRecipe = new RecipeSolution
                                        {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"study", new CardChoice{ElementId = "vitality"}},
                                                {"morevitality", new CardChoice {ElementId = "vitality"}}
                                            }
                                        }
                                    }
                                },
                                new Imperative ("Stronger Physique work") {
                                    Priority = TaskPriority.Maintenance,
                                    ForbidWhenCardsPresent = new GameStateCondition (
                                            ConditionMode.ALL_OF,
                                            // Do not monopolize our skill card if we have two vitality waiting to be upgraded.
                                            new CardChoice { ElementId = "vitalityplus"},
                                            new CardChoice { ElementId = "vitalityplus"}
                                        ),
                                    Operation = new Operation {
                                        Situation = "work",
                                        StartingRecipe = new RecipeSolution
                                        {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"work", new CardChoice { ElementId = "skillhealtha"}},
                                                {"Health", new CardChoice { ElementId = "health"}}
                                            }
                                        }
                                    }
                                },
                                new Imperative ("Get Swole 2") {
                                    Priority = TaskPriority.Goal,
                                    Operation = new Operation {
                                        Situation = "study",
                                        StartingRecipe = new RecipeSolution {
                                            Slots = new Dictionary<string, CardChoice> {
                                                {"study", new CardChoice { ElementId = "skillhealtha"}},
                                                {"V1", new CardChoice {ElementId="vitalityplus"}},
                                                {"V2", new CardChoice {ElementId="vitalityplus"}}
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new Goal ("Get Swole 3") {
                            RequiredCards = new GameStateCondition (
                                            ConditionMode.ALL_OF,
                                    new CardChoice { ElementId = "skillhealthb" }
                            ),
                            CompletedByCards = new GameStateCondition (
                                            ConditionMode.ANY_OF,
                                    new CardChoice { ElementId = "skillhealthc" },
                                    new CardChoice { ElementId = "skillhealthd_strength" },
                                    new CardChoice { ElementId = "skillhealthd_grace" }
                            ),
                            Imperatives = new List<Imperative> {
                                RefreshHealth,
                                PaintAwayRestlessness,
                                HealtAfflictionWithFunds,
                                ExerciseC,
                                UpgradeVitalityC,
                                WorkPhysiqueC,
                                GetSwoleC
                            }
                        },
                    }
                };
            }
        }*/
    }
}