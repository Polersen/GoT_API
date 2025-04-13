namespace GoT_API
{
    internal class Program
    {
        //Method that prints data fetched from the api
        static void PrintInfo(List<DetailedBook> detailedBooks)
        {
            foreach (var book in detailedBooks)
            {
                Console.WriteLine($"Book: {book.Name}:\n");
                foreach (var character in book.RelevantCharacters)
                {
                    Console.WriteLine($"\tCharacter: {character.Name}" +
                                      $"\n\tAllegiances: {string.Join(", ", character.Allegiances)}" +
                                      $"\n\tTitles: {string.Join(", ", character.Titles)}" +
                                      $"\n\tOther books: {string.Join(", ", character.Books)}" +
                                      $"\n\tPov books: {string.Join(", ", character.PovBooks)}\n");
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\n");
            }
        }

        static async Task Main(string[] args)
        {
            ApiService apiService = new ApiService();

            BookService bookService = new BookService(apiService);

            //Relevant Book Apis
            List<string> bookUrls = new List<string>
            {
                "https://www.anapioficeandfire.com/api/books/1",
                "https://www.anapioficeandfire.com/api/books/2",
                "https://www.anapioficeandfire.com/api/books/3",
                "https://www.anapioficeandfire.com/api/books/5",
                "https://www.anapioficeandfire.com/api/books/8"
            };

            //Relevant House Apis
            List<string> houseUrls = new List<string>
            {
                "https://www.anapioficeandfire.com/api/houses/7",
                "https://www.anapioficeandfire.com/api/houses/11"
            };

            var booksAndCharacters = await bookService.FetchBooksWithDetails(bookUrls, houseUrls);

            Console.WriteLine();

            PrintInfo(booksAndCharacters);
        }
    }
}