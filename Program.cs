using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionClassesDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Implementing IEnumerable and creating an enumerator by implementing IEnumerator example:");
            var books = new[]
            {
                "The Corrections",
                "Cloud Atlas",
                "The Kite Runner"
            };

            var library = new Library(books);

            foreach (var bookTitle in library)
            {
                Console.WriteLine(bookTitle);
            }

            Console.WriteLine();
            Console.WriteLine("Implementing IEnumerable<T> and creating a compile-time enumerator with iterators and the \"yield\" keyword example:");
            var days = new Days();
            foreach (var day in days)
            {
                Console.WriteLine(day);
            }

            Console.WriteLine();
            Console.WriteLine("Returning the arrays enumerator example:");
            var cars = new[]
            {
                new Car("BMW", "M5"),
                new Car("Toyota", "Avensis"),
                new Car("Audi", "A2")
            };
            var carCollection = new CarCollection(cars);

            foreach (var car in carCollection)
            {
                Console.WriteLine(car);
            }
        }
    }

    // Easy
    internal class Library : IEnumerable<string>
    {
        public string[] BookTitles { get; }

        public Library(string[] bookTitles) => BookTitles = bookTitles ?? throw new ArgumentNullException($"{nameof(BookTitles)} has to be set");

        public int Length => BookTitles.Length;

        public IEnumerator<string> GetEnumerator() => new LibraryEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Implementing a enumerator for Library
        private class LibraryEnumerator : IEnumerator<string>
        {
            private int _posisition = -1;
            private string[] _books;

            public LibraryEnumerator(Library library) => this._books = library.BookTitles ?? throw new ArgumentNullException($"{nameof(_books)} has to be set to an instance");

            public string Current => _books[_posisition];

            object IEnumerator.Current => _books[_posisition];

            public bool MoveNext()
            {
                var moveNext = false;

                if (_books.Length - 1 > _posisition)
                {
                    _posisition++;
                    moveNext = true;
                }

                return moveNext;
            }

            public void Reset() => _posisition = -1;

            public void Dispose() => _books = null;

        }
    }

    // Easier 
    internal class Days : IEnumerable<string>
    {
        private enum Day
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<string> GetEnumerator()
        {
            yield return Day.Monday.ToString();
            yield return Day.Tuesday.ToString();
            yield return Day.Wednesday.ToString();
            yield return Day.Thursday.ToString();
            yield return Day.Friday.ToString();
            yield return Day.Saturday.ToString();
            yield return Day.Sunday.ToString();
        }
    }

    // Easiest
    internal class CarCollection
    {
        private readonly Car[] _cars;

        public CarCollection(Car[] cars) => _cars = cars ?? throw new ArgumentNullException();

        public int Count => _cars.Length;

        public IEnumerator GetEnumerator() => _cars.GetEnumerator();
    }

    internal class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        public Car(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public override string ToString() => $"{Brand}: {Model}";
    }
}