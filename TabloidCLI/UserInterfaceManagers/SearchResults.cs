using System;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    public class SearchResults<T>
    {
        public List<T> Results = new List<T>();

        public string Title { get; set; } = "Search Results";

        public bool NoResultsFound
        {
            get
            {
                return Results.Count == 0;
            }
        }

        public void Add(T result)
        {
            Results.Add(result);
        }

        public void AddList(List<T> result)
        {
            Results.AddRange(result);
        }

        public void Display()
        {
            
            foreach (T result in Results)
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
