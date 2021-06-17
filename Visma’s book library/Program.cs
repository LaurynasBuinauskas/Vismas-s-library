using System;
using System.Collections.Generic;
using System.Globalization;


namespace Visma_s_book_library
{
    class Program
    {        
        static void Main(string[] args)
        {
            JSON json = new JSON();
            IList<Book> books = json.readJson<Book>();
            IList<User> users = json.readJson<User>();                                         
            
            

            Console.WriteLine("Welcome to Visma’s library!");                    
            
            while (true)
            {
                Console.WriteLine("If you'd like to see the list of books, type \"list\"");
                Console.WriteLine("If you'd like to add a new book, type \"add\"");
                Console.WriteLine("If you'd like to take a book, type \"take\"");
                Console.WriteLine("If you'd like to return a book, type \"return\"");
                Console.WriteLine("If you'd like to filter the book list, type \"filter\"");
                Console.WriteLine("If you'd like to delete a book, type \"delete\"");
                Console.Write("Command: ");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "list":
                        Console.WriteLine("Book list:");
                        books = json.readJson<Book>();
                        showBookList(books);
                        break;
                    case "add":
                        Console.WriteLine("You have the chosen to add a new book. To do so you need to follow the format that is shown below.");
                        Console.WriteLine("Format: Name of the book|Author's full name|Book's category|Language the book is written in|Book's publication date|Book's ISBN");
                        Console.WriteLine("Example: All Quiet on the Western Front|Erich Maria Remarque|War novel|English|1996-01-01|0099532816");
                        Console.Write("The book you want to add: ");
                        string new_book_input = Console.ReadLine();
                        int check_for_mistakes = addNewBook(ref books, new_book_input);
                        if (check_for_mistakes == 1) { json.write(books); books = json.readJson<Book>(); Console.WriteLine("The book has been added"); }
                        else Console.WriteLine("The book was not added, please check the formating");                        
                        
                        break;
                    case "take":
                        Console.WriteLine("You have the chosen to take a book. To do so you need to follow the format that is shown below.");
                        Console.WriteLine("Format: Your name|Your surname|The period you'd like to take the book (in days)|The book's ISBN");
                        Console.WriteLine("Example: Thomas|Smithmires|54|0099532816");
                        Console.Write("Command: ");
                        string take_book_input = Console.ReadLine();
                        string response = takeBook(take_book_input, ref books, ref users);
                        Console.WriteLine(response);
                        if(response == "The book was successfully taken") 
                            json.write(books); books = json.readJson<Book>(); json.write(users); users = json.readJson<User>();

                        break;

                    case "return":
                        Console.WriteLine("You have the chosen to return a book. To do so you need to follow the format that is shown below.");
                        Console.WriteLine("Format: Your name|Your surname|The book's ISBN");
                        Console.WriteLine("Example: Thomas|Smithmires|0099532816");
                        Console.Write("Command: ");
                        string return_book_input = Console.ReadLine();
                        string response_on_return = returnBook(return_book_input, ref books, ref users);
                        Console.WriteLine(response_on_return);
                        string[] parts;
                        parts = response_on_return.Split(' ');
                        if (parts.Length > 2 && parts[2] == "successfully")
                            json.write(books); books = json.readJson<Book>(); json.write(users); users = json.readJson<User>();

                        break;
                    case "filter":
                        Console.WriteLine("You have the chosen to filter the book list. To do so you need to follow the format that is shown below.");
                        Console.WriteLine("Format: Parameter|value or Parameter|taken/not taken (if filtering by book availability)");
                        Console.WriteLine("Parameters: name, auhtor, category, language, ISBN, availability");
                        Console.WriteLine("Example: name|Great book or availability|taken");
                        Console.Write("Command: ");
                        string filter_input = Console.ReadLine();
                        filterTheList(books, filter_input);
                        break;
                    case "delete":
                        Console.WriteLine("You have the chosen to delete a book. To do so you need to follow the format that is shown below.");
                        Console.WriteLine("Format: ISBN code");
                        Console.WriteLine("Example: 101");
                        Console.Write("Command: ");
                        string delete_input = Console.ReadLine();
                        delete(delete_input, ref books);
                        Console.WriteLine("Deleted");
                        json.write(books);
                        books = json.readJson<Book>();

                        break;
                    case "Users":
                        showUserList(users);
                        break;

                }
            }                       

        }
        static void delete(string delete_input, ref IList<Book> books)
        {
            List<Book> filtered = books as List<Book>;
            filtered.RemoveAll(x => x.ISBN == delete_input);
        }
        static void showUserList(IList<User> users)
        {
            if (users.Count == 0)
            {
                Console.WriteLine("The list is currently empty");
            }
            foreach (User book in users)
            {
                Console.WriteLine(book.ToString());
            }
            Console.WriteLine();
        }

        static void showBookList(IList<Book> books)
        {
            if(books.Count == 0)
            {
                Console.WriteLine("The book list is currently empty");
            }
            foreach(Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
            Console.WriteLine();
        }

        static void filterTheList(IList<Book> books, string parameter)
        {
            
            List<Book> filtered = new List<Book>(books);
            
            string[] parts = parameter.Split('|');
            if(parts.Length == 2)
            {
                switch (parts[0])
                {
                    case "name":                        
                        filtered.RemoveAll(x => x.Name != parts[1]);
                        Console.WriteLine("Filter by name: {0}", parts[1]);
                        filtered.ForEach(i => Console.WriteLine(i));
                        Console.WriteLine();
                        break;
                    case "author":
                        filtered.RemoveAll(x => x.Author != parts[1]);
                        Console.WriteLine("Filter by author: {0}", parts[1]);
                        filtered.ForEach(i => Console.WriteLine(i));
                        Console.WriteLine();
                        break;
                    case "category":
                        filtered.RemoveAll(x => x.Category != parts[1]);
                        Console.WriteLine("Filter by category: {0}", parts[1]);
                        filtered.ForEach(i => Console.WriteLine(i));
                        Console.WriteLine();
                        break;
                    case "language":
                        filtered.RemoveAll(x => x.Language != parts[1]);
                        Console.WriteLine("Filter by language: {0}", parts[1]);
                        filtered.ForEach(i => Console.WriteLine(i));
                        Console.WriteLine();
                        break;
                    case "ISBN":
                        filtered.RemoveAll(x => x.ISBN != parts[1]);
                        Console.WriteLine("Filter by ISBN: {0}", parts[1]);
                        filtered.ForEach(i => Console.WriteLine(i));
                        break;
                    case "availability":
                        if(parts[1] == "taken")
                        {
                            filtered.RemoveAll(x => x.Taken != true);
                            Console.WriteLine("Filter by availability: {0}", "taken");
                            filtered.ForEach(i => Console.WriteLine(i));
                        }
                        if (parts[1] == "not taken")
                        {
                            filtered.RemoveAll(x => x.Taken != false);
                            Console.WriteLine("Filter by availability: {0}", "not taken");
                            filtered.ForEach(i => Console.WriteLine(i));
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        break;

                }
            }
            else
            {
                Console.WriteLine("Incorrect input");
            }
            
                
        }

        static int addNewBook(ref IList<Book> books, string new_book)
        {
            string[] parts;
            parts = new_book.Split('|');
            if(parts.Length != 6) { return -1; }
            for(int i = 0; i < parts.Length; i++)
            {
                if (parts[i] is string && parts[i] == "") return -1;
                
            }
            string name = parts[0];
            string author = parts[1];
            string category = parts[2];
            string language = parts[3];
            DateTime publication_date = new DateTime();
            if (!checkDate(parts[4], ref publication_date)) { return -1; }            
            string isbn = parts[5];
            books.Add(new Book(name, author, category, language, publication_date, isbn));
            return 1;
        }

        static bool checkDate(string date_input, ref DateTime date)
        {            
            bool check = DateTime.TryParseExact(
                date_input,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
            return check;
        }

        static string returnBook(string return_book, ref IList<Book> books, ref IList<User> users)
        {
            string[] parts;
            parts = return_book.Split('|');
            if (parts.Length != 3) { return "Bad formating"; }
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] is string && parts[i] == "") return "Bad formating";

            }
            string name = parts[0];
            string surname = parts[1];
            string isbn = parts[2];
            
            foreach(User user in users)
            {
                if(user.Name == name && user.Surname == surname)
                {
                    int i = 0;
                    foreach (Book book in user.Taken_books)
                    {
                        if(book.ISBN == isbn)
                        {
                            string time_check = "";
                            DateTime now = DateTime.Now;
                            DateTime start_date = user.Taken_dates[i];
                            TimeSpan period = user.Periods[i];
                            if (start_date.AddDays(period.TotalDays) < now) 
                                time_check = ". We get get it, you like books:D, but being punctual can be fun tooxd";
                            user.Taken_books.RemoveAt(i);
                            user.Taken_dates.RemoveAt(i);
                            user.Periods.RemoveAt(i);
                            book.setTaken(false);
                            return "Book was successfully returned" + time_check;
                        }
                        i++;
                    }
                }
               
            }

            return "Book was not returned successfully. Check if you've entered the right information";
        }

        static string takeBook(string take_book, ref IList<Book> books, ref IList<User> users)
        {
            string[] parts;
            parts = take_book.Split('|');
            if (parts.Length != 4) { return "Bad formating"; }
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] is string && parts[i] == "") return "Bad formating";

            }
            string name = parts[0];
            string surname = parts[1];
            int days = int.Parse(parts[2]);
            TimeSpan period = new TimeSpan();
            if (days <= 60) period = TimeSpan.FromDays(days);
            else return "You can't ake a book for more tahn 2 months (60 days)";            
            
            string isbn = parts[3];

            foreach (Book book in books) {
                if (book.ISBN == isbn && book.Taken == false)
                {

                    foreach(User user in users)
                    {
                        if(user.Name == name && user.Surname == surname)
                        {
                            if(user.Taken_books.Count <= 2)
                            {                                
                                user.Taken_books.Add(book);
                                if (user.Periods == null)
                                {
                                    user.setPeriod(new List<TimeSpan>());
                                }
                                user.Periods.Add(period);
                                user.Taken_dates.Add(DateTime.Now);
                                book.setTaken(true);
                                return "The book was successfully taken";

                            }
                            else return "You can't take more than 3 books";                            
                        }
                    }                    
                    if(book.Taken == false)
                    {                        
                        User new_user = new User(name, surname, new List<TimeSpan>(), new List<DateTime>(), new List<Book>());
                        new_user.Taken_books.Add(book);
                        new_user.Periods.Add(period);
                        new_user.Taken_dates.Add(DateTime.Now);
                        users.Add(new_user);
                        book.setTaken(true);
                        return "The book was successfully taken";
                    }
                }
                

            }
            return "The book is already taken";

        }
        

    }
}
