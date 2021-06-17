using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_s_book_library
{
    class Book
    {        
        public string Name { get; protected set; }
        public string Author { get; protected set; }
        public string Category { get; protected set; }
        public string Language { get; protected set; }
        public DateTime Publication_date { get; protected set; }
        public string ISBN { get; protected set; }
        public bool Taken { get; protected set; }

        public Book(string name = "", string author = "",
                    string category = "", string language = "", 
                    DateTime publication_date = new DateTime(), string isbn = "", bool taken = false)
        {
            this.Name = name; this.Author = author; 
            this.Category = category; this.Language = language; 
            this.Publication_date = publication_date; 
            this.ISBN = isbn; this.Taken = taken;

        }        
        public void setName(string name) { this.Name = name; }
        public void setAuthor(string author) { this.Author = author; }
        public void setCategory(string category) { this.Category = category; }
        public void setLanguage(string language) { this.Language = language; }
        public void setPublication_date(DateTime date) { this.Publication_date = date; }
        public void setISBN(string isbn) { this.ISBN = isbn; }
        public void setTaken(bool taken) { this.Taken = taken; }

        public override string ToString()
        {
            return string.Format("{0, -35} {1, -30} {2, -25} {3, -20} {4, -15} {5, -20} {6, -5}",
                Name, Author, Category, Language, Publication_date, ISBN, Taken);
        }

    }
}