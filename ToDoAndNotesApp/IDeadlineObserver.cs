// Interface defining the contract for deadline observers

public interface IDeadlineObserver
{
    // Method called to notify the observer about a task status
    void Update(Task overdueTask);
}
