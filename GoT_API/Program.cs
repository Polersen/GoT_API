namespace GoT_API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Jag ska bara ha böckerna 1, 2, 3, 5, 8



            ApiService apiService = new ApiService();

            BookService bookService = new BookService(apiService);

            List<string> bookUrls = new List<string>
            {
                "https://www.anapioficeandfire.com/api/books/1",
                "https://www.anapioficeandfire.com/api/books/2",
                "https://www.anapioficeandfire.com/api/books/3",
                "https://www.anapioficeandfire.com/api/books/5",
                "https://www.anapioficeandfire.com/api/books/8"
            };

            List<string> houseUrls = new List<string>
            {
                "https://www.anapioficeandfire.com/api/houses/7",
                "https://anapioficeandfire.com/api/houses/11"
            };


            var booksWithDetails = await bookService.FetchBooksWithDetails(bookUrls, houseUrls);

            Console.WriteLine();

            //var bocker = booksWithDetails.Sort(c => c.Characters);

            

            foreach (var book in booksWithDetails)
            {
                Console.WriteLine($"Book: {book.Name}:\n");
                foreach (var character in book.RelevantCharacters)
                {
                    Console.WriteLine($"\tCharacter: {character.Name}" +
                                      $"\n\tAllegiances: {string.Join(", ", character.Allegiances)}" +
                                      $"\n\tTitles: {string.Join(", ", character.Titles)}" +
                                      $"\n\tBooks: {string.Join(", ", character.Books)}" +
                                      $"\n\tPovBooks: {string.Join(", ", character.PovBooks)}\n");
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------");
                Console.WriteLine("\n");
            }















            //List<Book> books = new List<Book>();


            //Book book1 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/1");
            //Book book2 = await bookService.FetchBooksAsync("https://www.anapioficeandfire.com/api/books/2");
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

            //List<Character> charactersBook1 = new List<Character>();
            //List<Character> charactersBook2 = new List<Character>();
            //List<Character> charactersBook3 = new List<Character>();
            //List<Character> charactersBook5 = new List<Character>();
            //List<Character> charactersBook8 = new List<Character>();

            // lägg till en delay så inte för många requests sker på för kort tid

            // försök att hämta endast nödvändig data om karaktärerna, till att börja med: Name

            //string arrynOfTheEyrie = "https://www.anapioficeandfire.com/api/houses/7";

            //string baelishOfTheFingers = "https://anapioficeandfire.com/api/houses/11";



            ////so far only arryn of the eyrie for all five books
            //charactersBook1 = await bookService.FetchCharactersForBookFromHouse(book1, arrynOfTheEyrie);

            //int i = 1;
            //Console.WriteLine("Book 1");
            //foreach (Character character in charactersBook1)
            //{
            //    Console.WriteLine($"{i}: {character.Name} || ");
            //    i++;

            //    foreach (var house in character.Allegiances)
            //        Console.WriteLine($"{house}");
            //}

            //Console.WriteLine();

            //charactersBook2 = await bookService.FetchCharactersForBookFromHouse(book2, arrynOfTheEyrie);

            //i = 1;
            //Console.WriteLine("Book 2");
            //foreach (Character character in charactersBook2)
            //{
            //    Console.WriteLine($"{i}: {character.Name} ||");
            //    i++;
            //}

            //Console.WriteLine();

            //charactersBook3 = await bookService.FetchCharactersForBookFromHouse(book3, arrynOfTheEyrie);

            //i = 1;
            //Console.WriteLine("Book 3");
            //foreach (Character character in charactersBook3)
            //{
            //    Console.WriteLine($"{i}: {character.Name} ||");
            //    i++;
            //}

            //Console.WriteLine();

            //charactersBook5 = await bookService.FetchCharactersForBookFromHouse(book5, arrynOfTheEyrie);

            //i = 1;
            //Console.WriteLine("Book 5");
            //foreach (Character character in charactersBook5)
            //{
            //    Console.WriteLine($"{i}: {character.Name} ||");
            //    i++;
            //}

            //Console.WriteLine();

            //charactersBook8 = await bookService.FetchCharactersForBookFromHouse(book8, arrynOfTheEyrie);

            //i = 1;
            //Console.WriteLine("Book 8");
            //foreach (Character character in charactersBook8)
            //{
            //    Console.WriteLine($"{i}: {character.Name} ||");
            //    i++;
            //}


            //Console.WriteLine();
            //Console.WriteLine();


            //List<Character> combinedList = charactersBook1
            //    .Union(charactersBook2, new CharacterComparer())
            //    .Union(charactersBook3, new CharacterComparer())
            //    .Union(charactersBook5, new CharacterComparer())
            //    .Union(charactersBook8, new CharacterComparer()).ToList();

            //i = 1;

            //foreach (Character character in combinedList)
            //{
            //    Console.WriteLine($"{i}: {character.Name}");
            //    i++;
            //}




























            //charactersBook1 = await bookService.FetchCharactersNew(book1, arrynOfTheEyrie);
            //charactersBook2 = await bookService.FetchCharactersNew(book2, arrynOfTheEyrie);
            //charactersBook3 = await bookService.FetchCharactersNew(book3);
            //charactersBook5 = await bookService.FetchCharactersNew(book5);
            //charactersBook8 = await bookService.FetchCharactersNew(book8);

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
