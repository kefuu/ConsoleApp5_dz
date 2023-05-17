// VSE OSTALNOE U MENYA NEBILO IZ-ZA PEREUSTANOVKI WINDOWSA


namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookList myBooks = new BookList();
            int choice = 0;

            do
            {
                Console.WriteLine("1. Add a Book\n2. Remove a Book\n3. Check if Book Exists\n4. Get Book by Index\n5. Exit");
                try
                {
                    choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter the title of the book to add:");
                            string titleToAdd = Console.ReadLine();
                            myBooks.AddBook(titleToAdd);
                            Console.WriteLine($"Book {titleToAdd} added!\n");
                            break;
                        case 2:
                            Console.WriteLine("Enter the title of the book to remove:");
                            string titleToRemove = Console.ReadLine();
                            if (myBooks.Contains(titleToRemove))
                            {
                                myBooks.RemoveBook(titleToRemove);
                                Console.WriteLine($"Book {titleToRemove} removed!\n");
                            }
                            else
                            {
                                Console.WriteLine("Book not found in the list!\n");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter the title of the book to check if it exists:");
                            string titleToCheck = Console.ReadLine();
                            Console.WriteLine(myBooks.Contains(titleToCheck) ? "Book exists in the list.\n" : "Book not found in the list!\n");
                            break;
                        case 4:
                            Console.WriteLine("Enter the index of the book to get:");
                            int index = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                            Console.WriteLine($"Book at index {index}: {myBooks[index]}\n");
                            break;
                        case 5:
                            Console.WriteLine("Exiting...\n");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please choose a valid option.\n");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}\n");
                }
            } while (choice != 5);
        }
    }

    class Book
    {
        public string Title { get; private set; }

        public Book(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Book title cannot be empty or null.");
            Title = title;
        }

        public static bool operator ==(Book a, Book b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Title == b.Title;
        }

        public static bool operator !=(Book a, Book b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Title == ((Book)obj).Title;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }

    class BookList
    {
        private List<Book> books;

        public BookList()
        {
            books = new List<Book>();
        }

        public void AddBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Book title cannot be empty or null.");
            books.Add(new Book(title));
        }

        public void RemoveBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Book title cannot be empty or null.");
            books.RemoveAll(book => book.Title == title);
        }

        public bool Contains(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Book title cannot be empty or null.");
            return books.Exists(book => book.Title == title);
        }

        public string this[int i]
        {
            get
            {
                if (i >= books.Count || i < 0)
                {
                    throw new ArgumentOutOfRangeException("Index is out of range of the book list.");
                }
                return books[i].Title;
            }
        }
    }
}
