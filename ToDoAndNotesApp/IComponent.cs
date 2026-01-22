// Defines the common interface for all components in the hierarchy.
public interface IComponent
{
    DateTime CreationDate { get; }
    void Display();
    void Accept(IVisitor visitor);
    IComponent Clone();
}
