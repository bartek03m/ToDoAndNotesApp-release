// Interface for the Command design pattern.
public interface ICommand
{
    // Executes the command logic.
    void Execute();

    // Reverses the command logic.
    void Undo();
}
