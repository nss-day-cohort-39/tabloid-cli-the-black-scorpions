using System;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class SearchResults<T>
    {
        private List<T> _results = new List<T>();

        public string Title { get; set; } = "Search Results";

        public bool NoResultsFound
        {
            get
            {
                return _results.Count == 0;
            }
        }

        public void Add(T result)
        {
            _results.Add(result);
        }

        public void Display()
        {
            Console.WriteLine(Title);
            
            foreach (T result in _results)
            {
                string type = result.GetType().ToString();
                if (type == "TabloidCLI.Models.Author")
                {
                    Console.WriteLine("Author: " + result);
                }
                else if (type == "TabloidCLI.Models.Post")
                {
                    Console.WriteLine("Post: " + result);
                }
                else if (type == "TabloidCLI.Models.Blog")
                {
                    Console.WriteLine("Blog: " + result);
                }
            }

            Console.WriteLine();
        }
    }
}
