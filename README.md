# ToDoAndNotesApp

ToDoAndNotesApp is a console-based application designed to organize tasks, notes, and manage information efficiently. It allows users to maintain a To-Do list and store longer text notes in one place, offering features like sorting, grouping, hierarchical categorization, and deadline tracking.

## Features

### Core Functionality
- **Add Tasks & Notes**: Create tasks with priorities, deadlines, tags, and descriptions. Create text notes with titles and content.
- **Hierarchical Categories**: Organize items into a tree structure of categories and subcategories.
- **Edit & Delete**: Modify existing tasks/notes or remove them entirely.
- **Search**: Find tasks and notes by keywords.
- **Tree View**: View all items in a structured hierarchical tree format.

### Organization & Management
- **Priorities**: Assign priorities (Low, Medium, High) to tasks.
- **Deadlines**: Set due dates for tasks and receive alerts for overdue items.
- **Tags**: Label items with colored tags for easy identification.
- **Sorting**: Sort items by Date or Priority.
- **Grouping**: Group items by Tag, Category, or Priority.

### Advanced Features
- **Undo/Redo**: Mistake-proof your workflow with full Undo and Redo capabilities for recent actions.
- **Reporting**: Generate summaries of your current tasks and notes.
- **Statistics**: View activity stats, such as total tasks and completed tasks.

## Design Patterns

The application is built using robust software design patterns to ensure maintainability and scalability:

- **Composite**: Treats individual items (Tasks, Notes) and groups (Categories) uniformly, allowing operations on the entire hierarchy.
- **Builder**: Simplifies the creation of complex Task objects with various optional parameters.
- **Multiton**: Manages unique instances of Tags to ensure consistency and memory efficiency.
- **Strategy**: Enables dynamic switching between different sorting and grouping algorithms.
- **Visitor**: Separates operations like reporting and console display from the object structure.
- **Command**: Encapsulates user actions to support Undo/Redo functionality.
- **Observer**: Monitors task deadlines and automatically triggers alerts.

## Technology Stack

- **Language**: C#
- **Framework**: .NET Runtime 10.0
- **Platform**: Windows 10/11
- **Key C# Features Used**:
  - Pattern Matching
  - Switch Expressions
  - LINQ
  - Nullable Reference Types
  - Records & Init-only Properties

## Installation

### Prerequisites
- Operating System: Windows 10 or 11
- Environment: .NET Runtime 10.0

## Usage Guide

1. **Add Item**: Choose '1' for Task or '2' for Note. Follow the prompts to set details.
2. **Add Category**: Choose '3'. You can create a root category or a subcategory.
3. **Edit/Delete**: Use '4' to Edit and '5' to Delete. Select the item from the list.
4. **View**: Use '6' for Tree View to see the full hierarchy.
5. **Search**: Use '7' and enter keywords.
6. **Sort/Group**: Use '8' to Sort (Date/Priority) and '9' to Group (Tag/Category/Priority).
7. **Reports/Stats**: Use '10' for Reports and '11' for Statistics.
8. **Undo/Redo**: Use '13' to Undo and '14' to Redo.

## Contributors
- **Bartosz Majewski**
    - Implementation of the **Command** design pattern.
    - Implementation of the `TaskManager` class.
    - Handling Pull Requests, code verification, and project repository management.
    - Technical support for team members and assistance in resolving implementation issues.
    - Implementation of the main program file (`Program.cs`).
    - Merging and editing all parts of the documentation provided by team members.
    - Development of the initial application architecture skeleton.
    - Preparation of the first project presentation.
    - Work on the initial UML class diagram.

- **Contributor 1**
    - Implementation of the **Visitor** design pattern.
    - Implementation of the **Builder** design pattern.
    - Development of detailed descriptions of design patterns for the technical documentation.
    - Co-creation of the initial UML class diagram.
    - Creation of the corrected, second UML class diagram.
    - Preparation of materials, notes, and content for the initial presentation.
    - Assistance in ongoing team coordination and information flow.
    - Assistance in merging and editing the documentation.

- **Contributor 2**
    - Implementation of the **Composite** design pattern.
    - Implementation of the **Multiton** design pattern.
    - Co-creation of the initial UML class diagram.
    - Description of specific C# language solutions used in the project.
    - Preparation of software installation instructions.

- **Contributor 3**
    - Implementation of the **Strategy** design pattern.
    - Implementation of the **Observer** design pattern.
    - Preparation of user installation instructions. 