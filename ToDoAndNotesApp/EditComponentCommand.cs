public class EditComponentCommand : ICommand
{
    private IComponent _target;
    private IComponent _previousState;
    private IComponent _newState;

    // Initializes the command, capturing the target and new state, and cloning the current state for undo.
    public EditComponentCommand(IComponent target, IComponent newState)
    {
        _target = target;
        _newState = newState;
        _previousState = _target.Clone();
    }

    // Executes the edit by updating the target with new values.
    public void Execute()
    {
        UpdateState(_target, _newState);
        Console.WriteLine("[Edit] Element updated successfully.");
    }

    // Reverts changes by restoring the previous state.
    public void Undo()
    {
        UpdateState(_target, _previousState);
        Console.WriteLine("[Undo] Reverted element changes.");
    }

    // Helper method to copy properties from source to destination based on component type.
    private void UpdateState(IComponent destination, IComponent source)
    {
        if (destination is Task destTask && source is Task sourceTask)
        {
            destTask.Title = sourceTask.Title;
            destTask.Description = sourceTask.Description;
            destTask.PriorityLevel = sourceTask.PriorityLevel;
            destTask.DueDate = sourceTask.DueDate;
            destTask.IsDone = sourceTask.IsDone;
            destTask.Tags =
                sourceTask.Tags != null ? new List<Tag>(sourceTask.Tags) : new List<Tag>();
        }
        else if (destination is Note destNote && source is Note sourceNote)
        {
            destNote.Title = sourceNote.Title;
            destNote.Content = sourceNote.Content;
            destNote.Tags =
                sourceNote.Tags != null ? new List<Tag>(sourceNote.Tags) : new List<Tag>();
        }
        else if (destination is Category destCat && source is Category sourceCat)
        {
            destCat.Name = sourceCat.Name;
        }
    }
}
