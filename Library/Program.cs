using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
 
namespace Library
{
    class Program
    {
        static List<Book> books;
        static List<Book> unavailablebooks;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Добро пожаловать в консольную библиотеку Library V.1 \nДля справки введите help \n");
            Console.ResetColor();
            
            books = !File.Exists("lib.xml") ? new List<Book>() : Menu.DeserializeFromXML();
            Menu menu = new Menu(books);
        }

        
    }
}
