public class Invoker
{
    private Stack<ICommand> _history = new();
    private Stack<ICommand> _redoStack = new();

    // Executes a command, pushes it to history, and clears the redo stack.
    public void ExecuteCommand(ICommand cmd)
    {
        cmd.Execute();
        _history.Push(cmd);
        _redoStack.Clear();
    }

    // Undoes the last command from history and moves it to the redo stack.
    public void UndoLast()
    {
        if (_history.Count > 0)
        {
            ICommand cmd = _history.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);
        }
        else
        {
            Console.WriteLine("[Undo] Nothing to undo");
        }
    }

    // Re-executes the last undone command and moves it back to history.
    public void RedoLast()
    {
        if (_redoStack.Count > 0)
        {
            ICommand cmd = _redoStack.Pop();
            cmd.Execute();
            _history.Push(cmd);
        }
        else
        {
            Console.WriteLine("[Redo] Nothing to redo");
        }
    }
}
