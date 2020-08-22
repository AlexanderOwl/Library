using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Library
{
    class Menu
    {

        public List<Book> books;
        public Menu(List<Book> books)
        { 
            this.books = books;
            bool flag = true;
            while (flag)
            {
                switch (Console.ReadLine())
                {
                    case "help":
                        Console.WriteLine(" help - список команд." +
                            "\n add - " +
                            "\n print [order] [field] - " +
                            "\n delete - " +
                            "\n save - " +
                            "\n load - " +
                            "\n take - " +
                            "\n return - ");                        
                        break;

                    case "add":
                        Add(books);
                        break;


                    case "exit":
                        flag = false;
                        break;


                    case "print":
                        Print(books);
                        break;
                    
                    case "print ascending name":
                        Print(books, "descending", "name");
                        break;

                    case "print descending name":
                        Print(books, "ascending", "name");
                        break;

                    case "search":
                        Search(books);
                        break;

                    case "delete":
                        Console.WriteLine("Введите название книги которую вы хотите удалить");
                        Delete(Console.ReadLine(), books);
                        break;

                    case "save":
                        SerializeToXML(books);
                        break;

                    case "load":
                        books = DeserializeFromXML();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нет такой команды");                        
                        Console.ResetColor();
                        break;
                }
            }
        }

        private static void Delete(string name, List<Book> books)
        {
            foreach (Book book in books.Where(b => b.Name == name))
            {
                Console.WriteLine("Удаление...");
                Console.WriteLine("Название: {0}, Автор: {1}, Издательство: {2}", book.Name, book.Author, book.Publisher);
                books.Remove(book); // возвращает true или false если елемент удален или нет. Нужно вывести сообщение книга удалена или такой книги не найдено. 
                break;
            }
        }
        private static void Print(List<Book> books)
        {
            foreach (Book book in books)
            {
                Console.WriteLine("Название: {0}, Автор: {1}, Издательство: {2}", book.Name, book.Author, book.Publisher);
            }
        }
        
        /// <summary>
         /// Нужно добавить все сортировки по спаданию и возрастанию 
         /// По Названию
         /// Автору
         /// Издалтельству
         /// Году
         /// </summary>
         /// <param name="books"> Книжки </param>
         /// <param name="orederBy"> Очередность по спадпнию или возрастанию Descending/Ascending  </param>
         /// <param name="field"> Поле по которому сортируем</param>
        private static void Print(List<Book> books, string orederBy, string field)
        {
            IOrderedEnumerable<Book> sortedBooks = null;
            if (orederBy == "ascending")
            {
                if (field == "name")
                {
                    sortedBooks = books.OrderBy(book => book.Name);
                }
                else if(field == "author")
                {
                    sortedBooks = books.OrderBy(book => book.Author);
                }
            }

            else if(orederBy == "descending")
            {
                if (field == "name")
                {
                    sortedBooks = books.OrderByDescending(book => book.Name);
                }
                else if (field == "author")
                {
                    sortedBooks = books.OrderByDescending(book => book.Author);
                }               
            }

            foreach (Book book in sortedBooks)
            {
                Console.WriteLine("Название: {0}, Автор: {1}, Издательство: {2}", book.Name, book.Author, book.Publisher);
            }

        } /// <summary>

        private static void Search(List<Book> books)
        {
            bool find = false;
            Console.Write("Найти: ");
            string readLine = Console.ReadLine();
            if (readLine != null)
            {
                foreach (Book book in books)
                {
                    if (book.Author.Contains(readLine) || book.Name.Contains(readLine) || book.Publisher.Contains(readLine))
                    {
                        Console.WriteLine("Название: {0}, Автор: {1}, Издательство: {2}", book.Name, book.Author, book.Publisher);
                        find = true;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Пустой поиск!");
                Console.ResetColor();
            }

            if (!find)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ничего нет!");
                Console.ResetColor();
            }

        } /// <summary>

        private static void Add(List<Book> books) /// нужно добавить год и проверку на меньше нуля или год в будущем
        {

            Console.WriteLine("Что бы добавить книгу введите через запятую\n" +
                                          "Имя автора, название книги, название издательства");
            string readLine = Console.ReadLine();
            if (readLine != null)
            {
                string[] temp = readLine.Split(',');
                books.Add(new Book(temp[0], temp[1], temp[2]));
            }
            
        }

        static public void SerializeToXML(List<Book> lib)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            TextWriter textWriter = new StreamWriter(@"lib.xml");
            serializer.Serialize(textWriter, lib);
            textWriter.Close();
        }//Сохраняет в файл ///Добавить в команду exit

        public static List<Book> DeserializeFromXML()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Book>));
            TextReader textReader = new StreamReader(@"lib.xml");
            List<Book> lib = (List<Book>)deserializer.Deserialize(textReader);
            textReader.Close();

            return lib;
        }//Загружает книги из файла

    }
}
