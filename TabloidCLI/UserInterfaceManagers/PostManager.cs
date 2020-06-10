using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
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
                    //Edit();
                    return this;
                case "4":
                    //Remove();
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
            List<Post> posts = _postRepository.GetAll();
            Console.WriteLine("-----------------------------------");
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
            Console.WriteLine("-----------------------------------");
        }

        private void Add()
        {
            Console.WriteLine("New Post Entry");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            post.PublishDateTime = DateTime.Now;

            post.Author = Choose("Select an Author");

            post.Blog = ChooseBlog("Select a Blog");

            _postRepository.Insert(post);
        }

        //private Post Choose(string prompt = null)
        //{
        //    if (prompt == null)
        //    {
        //        prompt = "Please choose a Post Entry:";
        //    }

        //    Console.WriteLine(prompt);

        //    List<Post> journalEntries = _postRepository.GetAll();

        //    for (int i = 0; i < journalEntries.Count; i++)
        //    {
        //        Post journal = journalEntries[i];
        //        Console.WriteLine($" {i + 1}) {journal.Title}");
        //    }
        //    Console.Write("> ");

        //    string input = Console.ReadLine();
        //    try
        //    {
        //        int choice = int.Parse(input);
        //        return journalEntries[choice - 1];
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Invalid Selection");
        //        return null;
        //    }
        //}

        //private void Edit()
        //{
        //    Post entryToEdit = Choose("Which journal entry would you like to edit?");
        //    if (entryToEdit == null)
        //    {
        //        return;
        //    }

        //    Console.WriteLine();
        //    Console.Write("New entry title (blank to leave unchanged: ");
        //    string title = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(title))
        //    {
        //        entryToEdit.Title = title;
        //    }
        //    Console.Write("New content (blank to leave unchanged: ");
        //    string content = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(content))
        //    {
        //        entryToEdit.Content = content;
        //    }

        //    _postRepository.Update(entryToEdit);
        //}

        //private void Remove()
        //{
        //    Post entryToDelete = Choose("Which journal entry would you like to remove?");
        //    if (entryToDelete != null)
        //    {
        //        _postRepository.Delete(entryToDelete.Id);
        //    }
        //}

        private Author Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
    }
}
