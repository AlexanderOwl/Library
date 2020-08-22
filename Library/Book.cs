using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book ///добавить год
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }

        public Book(string author,string name,string publisher)
        {
            this.Author = author;
            this.Name = name;
            this.Publisher = publisher;
        }

        public Book()
        {

        }
    }
}
