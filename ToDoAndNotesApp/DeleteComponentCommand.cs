public class DeleteComponentCommand : ICommand
{
    private TaskManager _manager;
    private IComponent _component;
    private IComponent _parent;

    // Initializes the command with the manager, component to delete, and its parent.
    public DeleteComponentCommand(TaskManager manager, IComponent component, IComponent parent)
    {
        _manager = manager;
        _component = component;
        _parent = parent;
    }

    // Executes the delete operation, either from a parent category or the main manager.
    public void Execute()
    {
        if (_parent is Category list)
        {
            list.Remove(_component);
        }
        else if (_parent == null)
        {
            _manager.RemoveComponent(_component);
        }
    }

    // Reverses the delete operation by adding the component back.
    public void Undo()
    {
        if (_parent is Category list)
        {
            list.Add(_component);
        }
        else if (_parent == null)
        {
            _manager.AddComponent(_component);
        }
    }
}
