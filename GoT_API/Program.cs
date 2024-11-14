namespace GoT_API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Jag ska bara ha böckerna 1, 2, 3, 5, 8



            ApiService apiService = new ApiService();

            BookService bookService = new BookService(apiService);

            List<Book> books = new List<Book>();

            Book book1 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/1");

            books.Add(book1);

            List<Character> characters = await bookService.FetchCharactersForBook(book1);

            Console.WriteLine(book1.Name);

            foreach (Character character in characters)
            {
                List<Book> characterBooks = await bookService.FetchBooksForCharacter(character);
                
                Console.WriteLine($"Character: {character.Name}\n");

                List<House> characterHouses = await bookService.FetchHousesForCharacters(character);

                foreach (var house in characterHouses)
                {
                    Console.WriteLine($"Houses: {house.Name}");
                }

                Console.WriteLine();

                foreach (var book in characterBooks)
                {
                    Console.WriteLine($"Book: {book.Name}");
                }
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("-----------------------------------------------------------------\n");
            }







            //Book book2 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/2");

            //books.Add(book2);

            //Book book3 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/3");

            //books.Add(book3);

            //Book book5 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/5");

            //books.Add(book5);

            //Book book8 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/8");

            //books.Add(book8);












            //if (book1 != null)
            //{

            //foreach (Book book in books)
            //{
            //    Console.WriteLine(book.Name);
            //    foreach (var character in book.Characters)
            //    {
            //        Console.WriteLine(character);
            //    }
            //    Console.WriteLine("----------------------------------------------------");
            //}

                //Console.WriteLine($"{book1.Name}");
                //foreach (var character in book1.Characters)
                //{
                //    Console.WriteLine($"Character: {character}");
                //}
            //}

















            //string url = "https://www.anapioficeandfire.com/api/books";


            //string data = await aS.FetchDataAsync(url);

            //List<string> list = new List<string>();

            //if (data != null)
            //{
            //    //Console.WriteLine(data);

            //    foreach (var item in list)
            //    {
            //        Console.WriteLine(item);
            //        Console.WriteLine();
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("........");
            //}
        }
    }
}
