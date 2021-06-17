using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visma_s_book_library
{
    class User
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public List<TimeSpan> Periods { get; protected set; }
        public List<DateTime> Taken_dates { get; protected set; }

        public IList<Book> Taken_books { get; protected set; }

        public User(string name = "", string surname = "",
            List<TimeSpan> periods = null, List<DateTime> taken_dates = null,
            IList<Book> taken_books = null)

        {
            this.Name = name;
            this.Surname = surname;
            this.Periods = periods;
            this.Taken_books = taken_books;
            this.Taken_dates = taken_dates;

        }
        public void setName(string name) { this.Name = name; }
        public void setSurname(string surname) { this.Surname = surname; }
        public void setPeriod(List<TimeSpan> periods) { this.Periods = periods; }
        public void setTaken_books(IList<Book> taken_books) { this.Taken_books = taken_books; }
        public void setTaken_dates(List<DateTime> taken_dates) { this.Taken_dates = taken_dates; }

        public override string ToString()
        {
            return string.Format("{0, -10} {1, -10} {2, -15} {3, -5}",
                Name, Surname, Periods.Count, Taken_books.Count);
        }
    }
}
