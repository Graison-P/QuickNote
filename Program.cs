// this file is the main program file coded with Visual Studio by Microsoft. If you are-
// modifing this software, I recommend using VS as it knows these libs already.
// Also ensure you have the proper NuGet packages aswell!
using System;
using System.IO;
using Spectre.Console;

class Program
{
    // logic - puts the notes right on your desktop for easy access
    private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "quick_notes.txt");

    static void Main(string[] args)
    {
        // if user types: qn "remember to fix the thing"
        if (args.Length > 0)
        {
            SaveNote(string.Join(" ", args));
        }
        else
        {
            // if not start interactive
            RunInteractiveNote();
        }
    }

    // handels saving the text to the file
    static void SaveNote(string note)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("MM/dd HH:mm");
            string formattedNote = $"[{timestamp}] {note}";

            // appends the note. if the file doesnt exist it makes it automatically
            File.AppendAllLines(_filePath, new[] { formattedNote });

            AnsiConsole.MarkupLine("[bold green]SAVED![/] Note added to the file [underline]quick_notes.txt[/] on the desktop.");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
        }
    }

    // simple loop if you have a lot to dump at once
    static void RunInteractiveNote()
    {
        // logic - fixed the 'Color' ambiguity by being specific because c# wants me to AAAAAAAAAAAAAAAAAA
        AnsiConsole.Write(new FigletText("QN").Color(Spectre.Console.Color.Green));
        Console.WriteLine("Type a note and hit Enter (type 'exit' to stop):");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine(); // ? stops the null warning

            if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit")
                break;

            SaveNote(input);
        }
    }

}
