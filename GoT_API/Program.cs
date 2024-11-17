namespace GoT_API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Jag ska bara ha böckerna 1, 2, 3, 5, 8



            ApiService apiService = new ApiService();

            BookService bookService = new BookService(apiService);

            //List<Book> books = new List<Book>();

            Book book1 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/1");
            Book book2 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/2");
            //Book book3 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/3");
            //Book book5 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/5");
            //Book book8 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/8");

            //books.Add(book1);
            //books.Add(book2);
            //books.Add(book3);
            //books.Add(book5);
            //books.Add(book8);


            //foreach (Book book in books)
            //{
            //    Console.WriteLine(book.Name);
            //}

            Console.WriteLine("\n");

            List<Character> charactersBook1 = new List<Character>();
            List<Character> charactersBook2 = new List<Character>();
            List<Character> charactersBook3 = new List<Character>();
            List<Character> charactersBook5 = new List<Character>();
            List<Character> charactersBook8 = new List<Character>();

            // lägg till en delay så inte för många requests sker på för kort tid

            // försök att hämta endast nödvändig data om karaktärerna, till att börja med: Name

            string arrynOfTheEyrie = "https://www.anapioficeandfire.com/api/houses/7";

            //charactersBook1 = await bookService.FetchCharactersNew(book1, arrynOfTheEyrie);


            charactersBook2 = await bookService.FetchCharactersNew(book2, arrynOfTheEyrie);
            //charactersBook3 = await bookService.FetchCharactersNew(book3);
            //charactersBook5 = await bookService.FetchCharactersNew(book5);
            //charactersBook8 = await bookService.FetchCharactersNew(book8);


            int i = 1;

            foreach (Character character in charactersBook2)
            {
                Console.WriteLine(i + " " + character.Name);
                i++;
            }

            //var ch2 = book2.Characters.ToList();

            //foreach (var character in ch2)
            //    Console.WriteLine(character); // hämta endast karaktärer för specifika hus

            //Console.WriteLine(ch2.Count());

            //Console.WriteLine(charactersBook1.Count());

            //Console.WriteLine(charactersBook2.Count());
            //Console.WriteLine(charactersBook3.Count());
            //Console.WriteLine(charactersBook5.Count());
            //Console.WriteLine(charactersBook8.Count());

            //List<List<Character>> characters = new List<List<Character>>(); // kan kanske en lista med listor funka?

            //foreach (var book in books)
            //{
            //    characters = await bookService.FetchCharactersNew(book); // Detta om det funkar blir en enda stor lista för alla böcker, inte vad jag vill ha
            //}

            // Få ut en count på karaktärer per bok


















            //string arryn = "https://www.anapioficeandfire.com/api/houses/7";
            //string 

            //List<Character> characters = await bookService.FetchCharactersForBook(book1, arryn);


            //List<House> houses = await bookService.FetchHousesAsync("https://www.anapioficeandfire.com/api/houses");

            //foreach (House house in houses)
            //    Console.WriteLine(house.Name);

            //--------------------------------------------------------------------------------------------

            //List<string> bookUrls = new List<string>();

            //string allBookUrls = "https://www.anapioficeandfire.com/api/books";

            //List<Book> allBooks = new List<Book>();

            //allBooks = await bookService.FetchAllBooksAsync()

            //--------------------------------------------------------------------------------------------


            //Console.WriteLine(book1.Name);
            //Console.WriteLine();

            //foreach (Character character in characters)
            //{
            //    List<Book> characterBooks = await bookService.FetchBooksForCharacter(character);

            //    Console.WriteLine($"Character: {character.Name}\n");

            //    List<House> characterHouses = await bookService.FetchHousesForCharacters(character);

            //    foreach (var house in characterHouses)
            //    {
            //        Console.WriteLine($"Houses: {house.Name}");
            //    }

            //    Console.WriteLine();

            //    foreach (var book in characterBooks)
            //    {
            //        Console.WriteLine($"Book: {book.Name}");
            //    }
            //    Console.WriteLine("-----------------------------------------------------------------");
            //    Console.WriteLine("-----------------------------------------------------------------\n");
            //}






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
