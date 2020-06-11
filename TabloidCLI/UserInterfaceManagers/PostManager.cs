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
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Post post = ChoosePost();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
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
            List<Post> posts = _postRepository.GetAll();
            Console.WriteLine("-----------------------------------");
            foreach (Post post in posts)
            {
                Console.WriteLine($@"{post.Title}
{post.Url}
{post.PublishDateTime}
{post.Author.FullName}
{post.Blog.Title}
");
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

            DateTime pubDate;
            while(true)
            {

            Console.WriteLine("Enter Publication Date: (mm/dd/yyyy HH:MM:SS)");
            bool allowed = DateTime.TryParse(Console.ReadLine(), out pubDate);
            if(allowed)
                {
                    break;
                }
            else
                {
                    Console.WriteLine("Must enter a valid publication date");
                }
            }

            post.PublishDateTime = pubDate;

            post.Author = ChooseAuthor("Select the post's author");

            post.Blog = ChooseBlog("Select the blog");

            _postRepository.Insert(post);
        }

        private void Edit()
        {
            Post postToEdit = ChoosePost("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New post title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                postToEdit.Url = content;
            }

            DateTime pubDate = postToEdit.PublishDateTime;
            while (true)
            {

                Console.WriteLine("Enter New Publication Date: (mm/dd/yyyy HH:MM:SS)");
                string resp = Console.ReadLine();
                bool allowed = DateTime.TryParse(resp, out pubDate);
                if (allowed)
                {
                    break;
                }
                else if (resp == "")
                {
                    pubDate = postToEdit.PublishDateTime;
                    break;
                }
                else
                {
                    Console.WriteLine("Must enter a valid publication date");
                }
            }

            postToEdit.PublishDateTime = pubDate;

            Author newAuthor = ChooseAuthor("New Author (blank to leave unchanged: ");
            if (newAuthor != null)
            {
                postToEdit.Author = newAuthor;
            }

            Blog newBlog = ChooseBlog("New Blog (blank to leave unchanged: ");
            if (newBlog != null)
            {
                postToEdit.Blog = newBlog;
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = ChoosePost("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }

        private Post ChoosePost(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseAuthor(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> posts = _authorRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Author author = posts[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
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
                return null;
            }
        }
    }
}
