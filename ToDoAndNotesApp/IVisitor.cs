// Defines the Visitor interface for traversing the component structure (Composite pattern).
public interface IVisitor
{
    void VisitTask(Task task);
    void VisitNote(Note note);
    void VisitCategory(Category category);
}
