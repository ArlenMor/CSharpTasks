using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Librarian librarian = new Librarian();

            librarian.Work();
        }
    }

    public class Librarian
    {
        private Library _library;

        public Librarian()
        {
            _library = new Library();
        }

        public void Work()
        {
            const char AddBookCommand = '1';
            const char RemoveBookByTitleCommand = '2';
            const char ShowAllBooksCommand = '3';
            const char ShowAllBooksByTitleCommand = '4';
            const char ShowAllBooksByAuthorCommand = '5';
            const char ShowAllBooksByPublicationYearCommand = '6';
            const char ExitCommand = '7';

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("Добро пожаловать в библиотеку!");
                Console.WriteLine($"{AddBookCommand} - добавить книгу в библиотеку.");
                Console.WriteLine($"{RemoveBookByTitleCommand} - удалить книгу из библиотеки по имени.");
                Console.WriteLine($"{ShowAllBooksCommand} - показать все книги.");
                Console.WriteLine($"{ShowAllBooksByTitleCommand} - показать все книги с определённым названием.");
                Console.WriteLine($"{ShowAllBooksByAuthorCommand} - показать все книги c определённым автором.");
                Console.WriteLine($"{ShowAllBooksByPublicationYearCommand} - показать все книги, которые были выпущены в определённый год.");
                Console.WriteLine($"{ExitCommand} - закрыть программу.");

                char userDecision = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (userDecision)
                {
                    case AddBookCommand:
                        AddBookToLibrary();
                        break;

                    case RemoveBookByTitleCommand:
                        RemoveBookByTitleFromLibrary();
                        break;

                    case ShowAllBooksCommand:
                        ShowAllBooks();
                        break;

                    case ShowAllBooksByTitleCommand:
                        ShowAllBooksByTitle();
                        break;

                    case ShowAllBooksByAuthorCommand:
                        ShowAllBooksByAuthor();
                        break;

                    case ShowAllBooksByPublicationYearCommand:
                        ShowAllBooksByPublicationYear();
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        ShowErrorMessage();
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолженя...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddBookToLibrary()
        {
            Console.Write("Введите название книги: ");
            string title;

            while (TryReadLine(out title) == false)
                Console.Write("Вы не ввели название книги, попробуйте ещё раз: ");

            Console.Write("Введите имя автора: ");
            string authorName;

            while (TryReadLine(out authorName) == false)
                Console.Write("Вы не ввели имя автора, попробуйте ещё раз: ");

            Console.Write("Введите фамилию автора: ");
            string authorSurname;

            while (TryReadLine(out authorSurname) == false)
                Console.Write("Вы не ввели фамилию автора, попробуйте ещё раз: ");

            Console.Write("Введите год выпуска книги (число со знаком \"-\" будет означать год до нашей эры): ");
            int publicationYear;

            while (int.TryParse(Console.ReadLine(), out publicationYear) == false)
                Console.Write("Нужно ввести число, а не строку. Попробуйте ещё раз: ");

            Author author = new Author(authorName, authorSurname);
            Book book = new Book(title, author, publicationYear);

            _library.AddBook(book);
            Console.WriteLine("Книга успешно добавлена в библиотеку!");
        }

        private void RemoveBookByTitleFromLibrary()
        {
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();

            List<Book> booksWithSameTitle = _library.GiveBooksByTitle(title);

            if (booksWithSameTitle == null)
            {
                Console.WriteLine("В библиотеке нет книги с таким названием.");
                return;
            }
            else
            {
                Console.WriteLine("Найдены следующие книги с таким названием: ");

                ShowBooks(booksWithSameTitle);

                int remoteIndex = 0;

                while (remoteIndex == 0)
                {
                    Console.Write("Какую книгу из списка вы хотите удалить? Укажите номер: ");
                    remoteIndex = ReadPositivInt(Console.ReadLine());

                    if (remoteIndex > booksWithSameTitle.Count)
                    {
                        Console.WriteLine("Введите число в пределах допустимых значений.");
                        remoteIndex = 0;
                    }
                }

                _library.RemoveBook(booksWithSameTitle[remoteIndex - 1]);

                Console.WriteLine("Книга успешно удалена из библиотеки!");
            }
        }


        private void ShowAllBooks()
        {
            List<Book> books = _library.GiveAllBooks();

            ShowBooks(books);
        }

        private void ShowAllBooksByTitle()
        {
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            List<Book> books = _library.GiveBooksByTitle(title);

            ShowBooks(books);
        }

        private void ShowAllBooksByAuthor()
        {
            Console.Write("Введите имя автора: ");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию автора: ");
            string surname = Console.ReadLine();

            Author author = new Author(name, surname);

            List<Book> books = _library.GiveBooksByAuthor(author);

            ShowBooks(books);
        }

        private void ShowAllBooksByPublicationYear()
        {
            int publicationYear;

            Console.WriteLine("Введите год выпуска книги: ");

            while (int.TryParse(Console.ReadLine(), out publicationYear) == false)
                Console.Write("Нужно ввести число, а не строку. Попробуйте ещё раз: ");

            List<Book> books = _library.GiveBooksByPublicationYear(publicationYear);

            ShowBooks(books);
        }

        private void ShowErrorMessage()
        {
            Console.WriteLine("Неизвестная команда. Попробуйте ещё раз...");
        }

        private int ReadPositivInt(string input)
        {
            int number;

            if (int.TryParse(input, out number) == false)
            {
                Console.WriteLine("Нужно ввести целое неторицательное число а, а не строку.");
                return 0;
            }

            if (number <= 0)
            {
                Console.WriteLine("Нужно ввести целое неотрицательное число.");
                return 0;
            }

            return number;
        }

        private bool TryReadLine(out string line)
        {
            line = Console.ReadLine();

            if (line == string.Empty)
                return false;

            return true;
        }

        private void ShowBook(Book book)
        {
            if (book != null)
                Console.Write($"Название: {book.Title} - Автор: {book.Author.Name} {book.Author.Surname} - Год публикации: {book.PublicationYear}");
        }

        private void ShowBooks(List<Book> books)
        {
            for (int i = 0; i < books.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                ShowBook(books[i]);
                Console.WriteLine();
            }
        }
    }

    public class Library
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            if (book != null)
                _books.Remove(book);
        }

        public List<Book> GiveBooksByTitle(string title)
        {
            List<Book> books = new List<Book>();

            if (title == string.Empty)
                return null;

            for (int i = 0; i < _books.Count; i++)
                if (_books[i].Title.ToLower() == title.ToLower())
                    books.Add(_books[i]);

            return books;
        }

        public List<Book> GiveBooksByAuthor(Author author)
        {
            List<Book> books = new List<Book>();

            if (author == null)
                return null;

            for (int i = 0; i < _books.Count; i++)
                if (_books[i].Author.Name.ToLower() == author.Name.ToLower() && _books[i].Author.Surname.ToLower() == author.Surname.ToLower())
                    books.Add(_books[i]);

            return books;
        }

        public List<Book> GiveBooksByPublicationYear(int year)
        {
            List<Book> books = new List<Book>();

            for (int i = 0; i < _books.Count; i++)
                if (_books[i].PublicationYear == year)
                    books.Add(_books[i]);

            return books;
        }

        public List<Book> GiveAllBooks()
        {
            List<Book> books = new List<Book>(_books);
            return books;
        }
    }

    public class Book
    {
        public Book(string title, Author author, int publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
        }

        public string Title { get; }
        public Author Author { get; }
        public int PublicationYear { get; }
    }

    public class Author
    {
        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }
        public string Surname { get; }
    }
}