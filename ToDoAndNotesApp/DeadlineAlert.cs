// Concrete implementation that displays alerts in the console
public class DeadlineAlert : IDeadlineObserver
{
    public void Update(Task overdueTask)
    {
        // Check if the task object is valid; if null, exit the method
        if (overdueTask == null)
        {
            return;
        }
        // Check if the deadline has already passed
        if (overdueTask.DueDate < DateTime.Now)
        {
            // Change console text color to Red for urgent alerts
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                $"[ALERT] The task '{overdueTask.Title}' is overdue! Deadline was: {overdueTask.DueDate}"
            );
            Console.ResetColor();
        }
        else
        {
            // If the deadline hasn't passed yet, treat it as a warning
            // Change console text color to Yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                $"[WARNING] The task '{overdueTask.Title}' deadline is approaching! Due: {overdueTask.DueDate}"
            );
            // Reset console color to default
            Console.ResetColor();
        }
    }
}
