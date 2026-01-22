using System;
using System.Collections.Generic;
using System.Text;

public static class ConsoleEditor
{
    // Enables interactive line editing with arrow key navigation.
    public static string EditLine(string prompt, string defaultValue)
    {
        if (Console.IsInputRedirected)
        {
            Console.Write($"{prompt} (Current: {defaultValue}) ");
            return Console.ReadLine() ?? defaultValue;
        }

        Console.Write($"{prompt} ");

        List<char> buffer = new List<char>(defaultValue.ToCharArray());
        int cursorPosition = buffer.Count;

        Console.Write(defaultValue);

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                // Confirms the input and returns the string.
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return new string(buffer.ToArray());

                // Deletes the character before the cursor.
                case ConsoleKey.Backspace:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                        buffer.RemoveAt(cursorPosition);

                        int left = Console.CursorLeft;
                        int top = Console.CursorTop;

                        Console.SetCursorPosition(left - 1, top);

                        for (int i = cursorPosition; i < buffer.Count; i++)
                        {
                            Console.Write(buffer[i]);
                        }

                        Console.Write(" ");

                        Console.SetCursorPosition(left - 1, top);
                    }
                    break;

                // Deletes the character at the current cursor position.
                case ConsoleKey.Delete:
                    if (cursorPosition < buffer.Count)
                    {
                        buffer.RemoveAt(cursorPosition);

                        int left = Console.CursorLeft;
                        int top = Console.CursorTop;

                        for (int i = cursorPosition; i < buffer.Count; i++)
                        {
                            Console.Write(buffer[i]);
                        }
                        Console.Write(" ");
                        Console.SetCursorPosition(left, top);
                    }
                    break;

                // Moves the cursor one position to the left.
                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                    break;

                // Moves the cursor one position to the right.
                case ConsoleKey.RightArrow:
                    if (cursorPosition < buffer.Count)
                    {
                        cursorPosition++;
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                    break;

                // Moves the cursor to the beginning of the line.
                case ConsoleKey.Home:
                    while (cursorPosition > 0)
                    {
                        cursorPosition--;
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                    break;

                // Moves the cursor to the end of the line.
                case ConsoleKey.End:
                    while (cursorPosition < buffer.Count)
                    {
                        cursorPosition++;
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                    break;

                // Inserts a character if it is not a control key.
                default:
                    if (!char.IsControl(keyInfo.KeyChar))
                    {
                        buffer.Insert(cursorPosition, keyInfo.KeyChar);
                        cursorPosition++;

                        int left = Console.CursorLeft;
                        int top = Console.CursorTop;

                        Console.Write(keyInfo.KeyChar);

                        if (cursorPosition < buffer.Count)
                        {
                            for (int i = cursorPosition; i < buffer.Count; i++)
                            {
                                Console.Write(buffer[i]);
                            }

                            Console.SetCursorPosition(left + 1, top);
                        }
                    }
                    break;
            }
        }
    }
}
