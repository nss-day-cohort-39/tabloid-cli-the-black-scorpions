using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using System.Threading.Tasks;

namespace TabloidCLI.UserInterfaceManagers
{
    class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Entry");
            Console.WriteLine(" 3) Edit Entry");
            Console.WriteLine(" 4) Remove Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }

        private void List()
        {
            List<Journal> journalEntries = _journalRepository.GetAll();
            int i = 0;
            void write(int i)
            {
                Console.Clear();
                Console.WriteLine(journalEntries[i]);
            }
            int next(int n) 
            { 
                n = (n + 1) % journalEntries.Count;
                i = n;
                return n; 
            }
            int prev(int n) { 
                n = (n == 0)
                    ? journalEntries.Count - 1 
                    : (n - 1) % journalEntries.Count;
                i = n;
                return i; }
            write(i);
            while (true)
            {
                var pressedKey = Console.ReadKey(true).Key;
                switch (pressedKey)
                {
                    case ConsoleKey.DownArrow:
                        write( next(i) );
                        break;
                    case ConsoleKey.UpArrow:
                        write( prev(i) );
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            while (true)
            {
                Console.Write("Title: ");
                string resp = Console.ReadLine();
                if (resp.Length > 55)
                {
                    Console.WriteLine("Journal titles must be less than 55 characters.");
                }
                else
                {
                    journal.Title = resp;
                    break;
                }
            }

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journal);
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Journal Entry:";
            }

            Console.WriteLine(prompt);

            List<Journal> journalEntries = _journalRepository.GetAll();

            for (int i = 0; i < journalEntries.Count; i++)
            {
                Journal journal = journalEntries[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journalEntries[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Journal entryToEdit = Choose("Which journal entry would you like to edit?");
            if (entryToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            while (true)
            {
                Console.Write("New entry Title(blank to leave unchanged: ");
                string title = Console.ReadLine();
                if (title.Length > 55)
                {
                    Console.WriteLine("Journal entry titles must be less than 55 characters.");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        entryToEdit.Title = title;
                    }
                    break;
                }
            }
            Console.Write("New content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                entryToEdit.Content = content;
            }

            _journalRepository.Update(entryToEdit);
        }

        private void Remove()
        {
            Journal entryToDelete = Choose("Which journal entry would you like to remove?");
            if (entryToDelete != null)
            {
                _journalRepository.Delete(entryToDelete.Id);
            }
        }
    }
}
