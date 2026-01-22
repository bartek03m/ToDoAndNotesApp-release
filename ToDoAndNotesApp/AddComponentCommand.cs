public class AddComponentCommand : ICommand
{
    private TaskManager _manager;
    private IComponent _component;
    private IComponent? _parent;

    // Initializes the command with the manager, component to add, and optional parent.
    public AddComponentCommand(TaskManager manager, IComponent component, IComponent? parent = null)
    {
        _manager = manager;
        _component = component;
        _parent = parent;
    }

    // Executes the command to add the component via the manager.
    public void Execute()
    {
        _manager.AddComponent(_component, _parent);
    }

    // Reverses the operation by removing the added component.
    public void Undo()
    {
        _manager.RemoveComponent(_component);
    }
}
