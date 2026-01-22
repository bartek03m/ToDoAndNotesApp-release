using System;
using System.Collections.Generic;
using System.Linq;

// Main entry point for the console application.
// Handles user interaction, the main event loop, and delegates commands to the Invoker/TaskManager.
public class Program
{
    private static TaskManager _taskManager = null!;
    private static Invoker _invoker = null!;

    // The main loop that displays the menu and processes user input.
    static void Main(string[] args)
    {
        var root = new Category("My Stuff");
        _taskManager = new TaskManager(root);
        _invoker = new Invoker();

        _taskManager.Attach(new DeadlineAlert());

        while (true)
        {
            PrintHeader("ToDo & Notes App Manager");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Add Note");
            Console.WriteLine("3. Add Category");
            Console.WriteLine("4. Edit Component");
            Console.WriteLine("5. Delete Component");
            Console.WriteLine("6. View All (Tree)");
            Console.WriteLine("7. Search");
            Console.WriteLine("8. Change Sort Strategy");
            Console.WriteLine("9. Change Grouping Strategy");
            Console.WriteLine("10. Generate Report");
            Console.WriteLine("11. Show Statistics");
            Console.WriteLine("12. Check Deadlines");
            Console.WriteLine("13. Undo Last Action");
            Console.WriteLine("14. Redo Last Action");
            Console.WriteLine("0. Exit");
            Console.ResetColor();

            Console.Write("\nSelect an option: ");

            var input = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (input)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        AddNote();
                        break;
                    case "3":
                        AddCategory();
                        break;
                    case "4":
                        EditComponent();
                        break;
                    case "5":
                        DeleteComponent();
                        break;
                    case "6":
                        ViewAll();
                        break;
                    case "7":
                        Search();
                        break;
                    case "8":
                        ChangeSort();
                        break;
                    case "9":
                        ChangeGrouping();
                        break;
                    case "10":
                        GenerateReport();
                        break;
                    case "11":
                        ShowStatistics();
                        break;
                    case "12":
                        CheckDeadlines();
                        break;
                    case "13":
                        _invoker.UndoLast();
                        PrintSuccess("Undo executed.");
                        break;
                    case "14":
                        _invoker.RedoLast();
                        PrintSuccess("Redo executed.");
                        break;
                    case "0":
                        return;
                    default:
                        PrintError("Invalid option. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                PrintError($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            try
            {
                if (!Console.IsInputRedirected)
                {
                    Console.ReadKey();
                }
                else
                {
                    Console.ReadLine();
                }
            }
            catch
            {
                // Ignore errors
            }
            SafeClear();
        }
    }

    private static void SafeClear()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Ignore console clear errors
        }
    }

    private static void PrintHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("=================================");
        Console.WriteLine($"   {title}");
        Console.WriteLine("=================================");
        Console.ResetColor();
    }

    private static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[SUCCESS] {message}");
        Console.ResetColor();
    }

    private static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ResetColor();
    }

    // Handles the UI for creating and adding a new task.
    private static void AddTask()
    {
        SafeClear();
        PrintHeader("Add New Task");
        Console.Write("Enter Title: ");
        string? title = Console.ReadLine();
        Console.Write("Enter Description: ");
        string? desc = Console.ReadLine();

        Console.WriteLine("Priority (0=Low, 1=Medium, 2=High): ");
        Priority prio = Priority.Low;
        if (int.TryParse(Console.ReadLine(), out int p) && Enum.IsDefined(typeof(Priority), p))
        {
            prio = (Priority)p;
        }

        Console.Write("Due Date (yyyy-mm-dd): ");
        DateTime dueDate = DateTime.Now.AddDays(1);
        if (DateTime.TryParse(Console.ReadLine(), out DateTime d))
        {
            dueDate = d;
        }

        var tags = InputTags();
        var parent = SelectCategory();

        var builder = new TaskBuilder();
        builder
            .SetTitle(title ?? "Untitled")
            .SetDescription(desc ?? "")
            .SetPriority(prio)
            .SetDueDate(dueDate);

        foreach (var t in tags)
        {
            builder.AddTag(t.Name);
        }

        Task task = builder.Build();
        task.Tags = tags;

        var cmd = new AddComponentCommand(_taskManager, task, parent);
        _invoker.ExecuteCommand(cmd);
        PrintSuccess("Task added successfully.");
    }

    // Handles the UI for creating and adding a new note.
    private static void AddNote()
    {
        SafeClear();
        PrintHeader("Add New Note");
        Console.Write("Enter Title: ");
        string? title = Console.ReadLine();
        Console.Write("Enter Content: ");
        string? content = Console.ReadLine();
        var tags = InputTags();
        var parent = SelectCategory();

        var note = new Note(title, content, tags);
        var cmd = new AddComponentCommand(_taskManager, note, parent);
        _invoker.ExecuteCommand(cmd);
        PrintSuccess("Note added successfully.");
    }

    // Handles the UI for creating and adding a new category.
    private static void AddCategory()
    {
        SafeClear();
        PrintHeader("Add New Category");
        Console.Write("Enter Category Name: ");
        string? name = Console.ReadLine();
        var parent = SelectCategory();

        var category = new Category(name);
        var cmd = new AddComponentCommand(_taskManager, category, parent);
        _invoker.ExecuteCommand(cmd);
        PrintSuccess("Category added successfully.");
    }

    // Helper method to input one or more tags, including color selection.
    private static List<Tag> InputTags()
    {
        var list = new List<Tag>();
        Console.Write("Add tags? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() != "y")
            return list;

        while (true)
        {
            Console.Write("Tag Name (or empty to finish): ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                break;

            string colorName = SelectColor();
            list.Add(Tag.GetInstance(name, colorName));
        }
        return list;
    }

    private static string SelectColor()
    {
        Console.WriteLine("Select Tag Color:");
        var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        for (int i = 0; i < colors.Count; i++)
        {
            Console.ForegroundColor = colors[i];
            Console.WriteLine($"{i + 1}. {colors[i]}");
        }
        Console.ResetColor();

        Console.Write("Choice: ");
        if (int.TryParse(Console.ReadLine(), out int c) && c > 0 && c <= colors.Count)
        {
            return colors[c - 1].ToString();
        }
        return "Gray";
    }

    // Orchestrates the editing of a component: Search -> Select -> Clone -> Modify -> Command.
    private static void EditComponent()
    {
        SafeClear();
        PrintHeader("Edit Component");
        Console.Write("Enter Title/Name to search for edit: ");
        string? keyword = Console.ReadLine();
        var results = _taskManager.Search(keyword ?? "");

        if (results.Count == 0)
        {
            PrintError("No components found.");
            return;
        }

        var target = SelectComponent(results);
        if (target == null)
            return;

        // Clone current state as base for new state
        var newState = target.Clone();

        Console.WriteLine("Editing... (Use Arrows/Backspace to edit existing text)");

        if (newState is Task t)
        {
            t.Title = ConsoleEditor.EditLine("Title:", t.Title);
            t.Description = ConsoleEditor.EditLine("Description:", t.Description ?? "");

            string dateStr = ConsoleEditor.EditLine(
                "Due Date (yyyy-mm-dd):",
                t.DueDate.ToString("yyyy-MM-dd")
            );
            if (DateTime.TryParse(dateStr, out DateTime d))
                t.DueDate = d;

            string prioStr = ConsoleEditor.EditLine(
                $"Priority ({(int)t.PriorityLevel}):",
                ((int)t.PriorityLevel).ToString()
            );
            if (int.TryParse(prioStr, out int p) && Enum.IsDefined(typeof(Priority), p))
                t.PriorityLevel = (Priority)p;

            string doneStr = ConsoleEditor.EditLine("Is Done? (y/n):", t.IsDone ? "y" : "n");
            t.IsDone = (doneStr.Trim().ToLower() == "y");
        }
        else if (newState is Note n)
        {
            n.Title = ConsoleEditor.EditLine("Title:", n.Title);
            n.Content = ConsoleEditor.EditLine("Content:", n.Content);
        }
        else if (newState is Category c)
        {
            c.Name = ConsoleEditor.EditLine("Name:", c.Name);
        }

        var cmd = new EditComponentCommand(target, newState);
        _invoker.ExecuteCommand(cmd);
        PrintSuccess("Component updated.");
    }

    // Orchestrates the deletion of a component: Search -> Select -> Command.
    private static void DeleteComponent()
    {
        SafeClear();
        PrintHeader("Delete Component");
        Console.Write("Enter Title/Name to search for delete: ");
        string? keyword = Console.ReadLine();
        var results = _taskManager.Search(keyword ?? "");

        if (results.Count == 0)
        {
            PrintError("No components found.");
            return;
        }

        var target = SelectComponent(results);
        if (target == null)
            return;

        var parent = _taskManager.FindParent(target);

        var cmd = new DeleteComponentCommand(_taskManager, target, parent!);
        _invoker.ExecuteCommand(cmd);
        PrintSuccess("Component deleted.");
    }

    // Displays a list of found components and lets the user pick one by index.
    private static IComponent? SelectComponent(List<IComponent> list)
    {
        Console.WriteLine("Found items:");
        for (int i = 0; i < list.Count; i++)
        {
            string info = "?";
            if (list[i] is Task t)
                info = $"[Task] {t.Title}";
            else if (list[i] is Note n)
                info = $"[Note] {n.Title}";
            else if (list[i] is Category c)
                info = $"[Category] {c.Name}";
            Console.WriteLine($"{i + 1}. {info}");
        }
        Console.Write("Select item number (or 0 to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= list.Count)
        {
            return list[idx - 1];
        }
        return null;
    }

    // Displays the entire project structure using the ConsoleTreeVisitor.
    private static void ViewAll()
    {
        SafeClear();
        PrintHeader("Current Structure");
        var visitor = new ConsoleTreeVisitor();
        _taskManager.RunVisitor(visitor);
    }

    // Searches the project for components containing a keyword.
    private static void Search()
    {
        SafeClear();
        PrintHeader("Search");
        Console.Write("Enter keyword: ");
        string? kw = Console.ReadLine();
        var results = _taskManager.Search(kw ?? "");
        Console.WriteLine($"Found {results.Count} matches:");
        foreach (var r in results)
            r.Display();
    }

    // Updates the sorting strategy for the manager.
    private static void ChangeSort()
    {
        SafeClear();
        PrintHeader("Change Sort Strategy");
        Console.WriteLine("1. Date");
        Console.WriteLine("2. Priority");
        var ch = Console.ReadLine();
        if (ch == "1")
            _taskManager.SetSortStrategy(new DateSortStrategy());
        else if (ch == "2")
            _taskManager.SetSortStrategy(new PrioritySortStrategy());

        PrintSuccess("Sort strategy updated. (Applies when calling GetSortedComponents)");

        var sorted = _taskManager.GetSortedComponents();
        foreach (var c in sorted)
            c.Display();
    }

    // Updates the grouping strategy for the manager.
    private static void ChangeGrouping()
    {
        SafeClear();
        PrintHeader("Change Grouping Strategy");
        Console.WriteLine("1. Tag");
        Console.WriteLine("2. Category");
        Console.WriteLine("3. Priority");
        var ch = Console.ReadLine();
        if (ch == "1")
            _taskManager.SetGroupingStrategy(new TagGroupingStrategy());
        else if (ch == "2")
            _taskManager.SetGroupingStrategy(new CategoryGroupingStrategy());
        else if (ch == "3")
            _taskManager.SetGroupingStrategy(new PriorityGroupingStrategy());

        Console.WriteLine("Grouping preview:");
        var groups = _taskManager.GetGroupedComponents();
        foreach (var g in groups)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Group: {g.Key}");
            Console.ResetColor();
            foreach (var item in g.Value)
            {
                Console.Write(" - ");
                if (item is Task t)
                    Console.WriteLine(t.Title);
                else if (item is Note n)
                    Console.WriteLine(n.Title);
                else if (item is Category c)
                    Console.WriteLine(c.Name);
            }
        }
    }

    // Generates and displays a text report of all tasks/notes.
    private static void GenerateReport()
    {
        SafeClear();
        PrintHeader("Full Report");
        var visitor = new ReportVisitor();
        _taskManager.RunVisitor(visitor);
        Console.WriteLine(visitor.GetFullReport());
    }

    // Calculates and displays basic statistics (counts, completion %).
    private static void ShowStatistics()
    {
        SafeClear();
        PrintHeader("Statistics");
        var visitor = new StatisticsVisitor();
        _taskManager.RunVisitor(visitor);
        Console.WriteLine(visitor.GetStatistics());
    }

    // Manually triggers a deadline check (usually runs automatically if threaded, here manual).
    private static void CheckDeadlines()
    {
        SafeClear();
        PrintHeader("Checking Deadlines...");
        _taskManager.CheckDeadlines();
        PrintSuccess("Deadline check complete. Alerts shown above if any.");
    }

    // Helper to select a category from the recursive list of all categories.
    private static IComponent? SelectCategory()
    {
        Console.WriteLine("Select destination Category (0 for Root/General):");
        var categories = _taskManager.GetAllCategories();
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i].Name}");
        }
        Console.Write("Choice: ");
        if (
            int.TryParse(Console.ReadLine(), out int choice)
            && choice > 0
            && choice <= categories.Count
        )
        {
            return categories[choice - 1];
        }
        return null;
    }
}
