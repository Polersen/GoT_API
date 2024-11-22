using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT_API
{
    public class BookService
    {
        private readonly ApiService _apiService;

        public BookService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<DetailedBook>> FetchBooksWithDetails(List<string> bookUrls, List<string> houseUrls)
        {
            var bookNamesList = new List<string>//Lazy solution but it works, helps add only relevant books to character.books
                        {
                            "A Game of Thrones",
                            "A Clash of Kings",
                            "A Storm of Swords",
                            "A Feast for Crows",
                            "A Dance with Dragons"
                        };

            var detailedBooks = new List<DetailedBook>();

            foreach (var bookUrl in bookUrls)
            {
                var book = await _apiService.FetchDataAsync<Book>(bookUrl);

                if (book == null)
                {
                    Console.WriteLine($"Fetching data failed for book: {bookUrl}");
                    continue;
                }

                Console.WriteLine($"Processing book: {book.Name}");

                var houseCharacters = new List<string>();
                foreach (var houseUrl in houseUrls)
                {
                    var characters = await FetchHouseCharacters(houseUrl);
                    houseCharacters.AddRange(characters);
                }

                var normalizedHouseCharacters = NormalizeUrls(houseCharacters);
                var normalizedBookCharacters = NormalizeUrls(book.Characters);

                var relevantCharacterUrls = normalizedBookCharacters
                    .Intersect(normalizedHouseCharacters)
                    .ToList();

                if (!relevantCharacterUrls.Any())
                {
                    Console.WriteLine($"Could not find any characters for book: {book.Name}");
                    continue;
                }

                var detailedCharacters = new List<Character>();

                foreach (var characterUrl in relevantCharacterUrls)
                {
                    var character = await _apiService.FetchDataAsync<Character>(characterUrl);

                    if (character != null)
                    {
                        var houseNames = new List<string>();
                        foreach(var url in character.Allegiances)
                        {
                            var house = await _apiService.FetchDataAsync<House>(url);
                            if (house != null)
                            {
                                houseNames.Add(house.Name);
                            }
                        }

                        var bookNames = new List<string>();
                        foreach (var url in character.Books)
                        {
                            var bookName = await _apiService.FetchDataAsync<Book>(url);
                            if (bookName != null)
                            {
                                foreach (var name in bookNamesList)
                                {
                                    if (bookName.Name == name)
                                        bookNames.Add(bookName.Name);
                                }
                            }
                        }

                        var povBooks = new List<string>();
                        foreach (var url in character.PovBooks)
                        {
                            var povBook = await _apiService.FetchDataAsync<Book>(url);
                            if (povBook != null)
                            {
                                povBooks.Add(povBook.Name);
                            }
                        }

                        character.Allegiances = houseNames;
                        character.Books = bookNames;

                        if (!character.PovBooks.Any())
                        {
                            character.PovBooks = new List<string>//Add new list if pov books is empty
                            {
                                "No pov books!"
                            };
                        }
                        character.PovBooks = character.PovBooks;

                        if (!character.Titles.Any())
                        {
                            character.Titles = new List<string>//Add new list if titles is empty
                            {
                                "No titles!"
                            };
                        }
                        character.Titles = character.Titles;

                        detailedCharacters.Add(character);

                        foreach (var detailedCharacter in detailedCharacters)
                        {
                            detailedCharacter.Books.Remove(book.Name);//Removing book that character is listed under

                            if (!detailedCharacter.Books.Any())
                            {
                                detailedCharacter.Books = new List<string>//Add new list if there now are no books left
                                {
                                    "No other books!"
                                };
                            }
                        }
                    }
                }

                var sortedCharacters = SortCharacters(detailedCharacters);

                //Adds DetailedBook inheriting from Book + sortedCharacters
                detailedBooks.Add(new DetailedBook
                {
                    Url = book.Url,
                    Name = book.Name,
                    ISBN = book.ISBN,
                    Authors = book.Authors,
                    NumberOfPages = book.NumberOfPages,
                    Publisher = book.Publisher,
                    Country = book.Country,
                    MediaType = book.MediaType,
                    Released = book.Released,
                    Characters = book.Characters,
                    PovCharacters = book.PovCharacters,
                    RelevantCharacters = sortedCharacters
                });
            }

            return detailedBooks;
        }

        //Method that fetches characters belonging to a specific house
        public async Task<List<string>> FetchHouseCharacters(string houseUrl)
        {
            try
            {
                await Task.Delay(1000);//Delay the task 1 second

                var house = await _apiService.FetchDataAsync<House>(houseUrl);
                return house?.SwornMembers ?? new List<string>();//Returns a list of character urls or a new empty list
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fetching house data failed: {ex.Message}");
                return new List<string>();//Exception returns a new empty list
            }
        }

        //Method that converts url strings to be an exact match
        public static List<string> NormalizeUrls(List<string> urlList)
        {
            var normalizedList = urlList?
                .Where(url => url != null)
                .Select(url => url.Trim()
                    .ToLowerInvariant()
                    .TrimEnd('/'))
                .ToList() ?? new List<string>();

            return normalizedList;
        }

        //Method that sorts characters by house and then by their name
        public static List<Character> SortCharacters(List<Character> characterList)
        {
            var sortedCharacterList = characterList
                                        .Where(c => c.Allegiances.Any())
                                        .OrderBy(c => c.Allegiances.FirstOrDefault())
                                        .ThenBy(c => c.Name)
                                        .ToList();

            return sortedCharacterList;
        }
    }
}
