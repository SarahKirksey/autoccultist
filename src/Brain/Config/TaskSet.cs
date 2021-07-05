using Autoccultist.Brain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autoccultist.Brain.Config
{
    /**
     * A TaskSet represents a singular, overarching goal that is usually comprised of smaller Goals, Imperatives, or TaskSets.
     * Like a Goal, the "aspiration" of a TaskSet can change depending on the board state.
     * Unlike a Goal to Imperative, a TaskSet can encompass any number of Verbs/Situations.
     * At least in theory. In practice, it might not work so well.
     * 
     * A TaskSet has the following members:
     * PrimaryTasks: Tasks that must be satisfied before the TaskSet will suggest any other action.
     * SecondaryTasks: Tasks that must be blocked/satisfied before the TaskSet will suggest Tertiary tasks.
     * TertiaryTasks: Tasks that will be done only if all other Tasks are satisfied or blocked.
     * Subtasks: Additional TaskSets 
     */
    class TaskSet : ITask
    {
        public string Name { get; }

        public List<ITask> PrimaryTasks { get; }
        public List<ITask> SecondaryTasks { get; }
        public List<ITask> TertiaryTasks { get; }

        public TaskSet(String name)
        {
            Name = name;
            PrimaryTasks = new List<ITask>();
            SecondaryTasks = new List<ITask>();
            TertiaryTasks = new List<ITask>();
        }
        
        public bool ShouldExecute(IGameState state)
        {
            foreach (ITask task in PrimaryTasks)
            {
                if (task.ShouldExecute(state)) return true;
            }

            foreach (ITask task in SecondaryTasks)
            {
                if (task.ShouldExecute(state)) return true;
            }

            foreach (ITask task in TertiaryTasks)
            {
                if (task.ShouldExecute(state)) return true;
            }
            return false;
        }

        public bool IsSatisfied(IGameState state)
        {
            foreach (ITask t in PrimaryTasks)
            {
                if (!t.IsSatisfied(state)) return false;
            }
            foreach (ITask t in SecondaryTasks)
            {
                if (!t.IsSatisfied(state)) return false;
            }
            foreach (ITask t in TertiaryTasks)
            {
                if (!t.IsSatisfied(state)) return false;
            }
            return true;
        }
    }
}
