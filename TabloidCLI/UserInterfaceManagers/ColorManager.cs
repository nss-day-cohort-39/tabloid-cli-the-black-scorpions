using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private ColorRepository _colorRepository;
        private string _connectionString;

        public ColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _colorRepository = new ColorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Color Menu");
            Console.WriteLine(" 1) Red");
            Console.WriteLine(" 2) Dark Blue");
            Console.WriteLine(" 3) White");
            Console.WriteLine(" 4) Magenta");
            Console.WriteLine(" 5) Black (Default)");
            Console.WriteLine(" 0) Go Back");
            Color chosenColor = new Color();
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    chosenColor.ActiveColor = "Red";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    _colorRepository.Update(chosenColor);
                    return this;
                case "2":
                    chosenColor.ActiveColor = "Blue";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    _colorRepository.Update(chosenColor);
                    return this;
                case "3":
                    chosenColor.ActiveColor = "White";
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    _colorRepository.Update(chosenColor);
                    return this;
                case "4":
                    chosenColor.ActiveColor = "Magenta";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    _colorRepository.Update(chosenColor);
                    return this;
                case "5":
                    chosenColor.ActiveColor = "Black";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    _colorRepository.Update(chosenColor);
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
                    
            }

        }

        public void SetColor()
        {
            Color setColor = _colorRepository.Get();

            switch (setColor.ActiveColor)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case "White":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case "Black":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }

    }
}
