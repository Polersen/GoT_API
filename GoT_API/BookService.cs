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

        public async Task<Book> FetchBooksAsync(string url)
        {
            var book = await _apiService.FetchDataAsync<Book>(url);

            if (book == null)
            {
                Console.WriteLine("Fetching book failed!");

                return null;
            }

            return book;
        }

        public async Task<List<DetailedBook>> FetchBooksWithDetails(List<string> bookUrls, List<string> houseUrls)
        {
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

                var normalizedHouseCharacters = houseCharacters
                ?.Where(url => url != null)
                .Select(url => url.Trim().ToLowerInvariant().TrimEnd('/'))
                .ToList() ?? new List<string>();

                var normalizedBookCharacters = book.Characters
                ?.Where(url => url != null)
                .Select(url => url.Trim().ToLowerInvariant().TrimEnd('/'))
                .ToList() ?? new List<string>();

                var relevantCharacterUrls = normalizedBookCharacters.Intersect(normalizedHouseCharacters).ToList();

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
                        foreach(var allegianceUrl in character.Allegiances)
                        {
                            var house = await _apiService.FetchDataAsync<House>(allegianceUrl);
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
                                bookNames.Add(bookName.Name);
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
                            character.PovBooks = new List<string>
                            {
                                "No Pov Books!"
                            };
                        }
                        character.PovBooks = character.PovBooks;

                        if (!character.Titles.Any())
                        {
                            character.Titles = new List<string>
                            {
                                "No titles!"
                            };
                        }
                        character.Titles = character.Titles;

                        detailedCharacters.Add(character);
                    }
                }

                var sortedCharacters = SortedCharacters(detailedCharacters);

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

        public static List<Character> SortedCharacters(List<Character> characterList)
        {
            var sortedCharacterList = characterList
                                        .Where(c => c.Allegiances.Any())
                                        .OrderBy(c => c.Allegiances.FirstOrDefault())
                                        .ThenBy(c => c.Name)
                                        .ToList();

            return sortedCharacterList;
        }





































        //public async Task<List<Book>> FetchBookLists(List<string> houseUrls)
        //{
        //    var specificBookUrls = new List<string>
        //    {
        //        "https://www.anapioficeandfire.com/api/books/1",
        //        "https://www.anapioficeandfire.com/api/books/2",
        //        "https://www.anapioficeandfire.com/api/books/3",
        //        "https://www.anapioficeandfire.com/api/books/5",
        //        "https://www.anapioficeandfire.com/api/books/8"
        //    };

        //    var bookTasks = specificBookUrls.Select(async url =>
        //    {
        //        try
        //        {
        //            return await _apiService.FetchDataAsync<Book>(url);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine();
        //            return null;
        //        }
        //    });

        //    var books = (await Task.WhenAll(bookTasks)).Where(book => book != null).ToList();

        //    if (books == null || !books.Any())
        //    {
        //        Console.WriteLine("Failed when fetching books or no books available.");
        //        return new List<Book>();
        //    }

        //    var bookDetails = new List<Book>();

        //    foreach (var book in books)
        //    {
        //        Console.WriteLine($"Processing book: {book.Name}");

        //        var houseCharacters = new List<string>();

        //        foreach (var houseUrl in houseUrls)
        //        {
        //            var characters = await FetchHouseCharacters(houseUrl);
        //            houseCharacters.AddRange(characters);
        //        }

        //        var normalizedHouseCharacters = houseCharacters
        //        ?.Where(url => url != null)
        //        .Select(url => url.Trim().ToLowerInvariant().TrimEnd('/'))
        //        .ToList() ?? new List<string>();

        //        var normalizedBookCharacters = book.Characters
        //        ?.Where(url => url != null)
        //        .Select(url => url.Trim().ToLowerInvariant().TrimEnd('/'))
        //        .ToList() ?? new List<string>();

        //        var relevantCharacterUrls = normalizedBookCharacters.Intersect(normalizedHouseCharacters).ToList();

        //        if (!relevantCharacterUrls.Any())
        //        {
        //            Console.WriteLine($"Could not find any characters in the specified houses for book: {book.Name}");
        //            continue;
        //        }

        //        Console.WriteLine($"{relevantCharacterUrls.Count()} characters found for book: {book.Name}");

        //        var characterTasks = relevantCharacterUrls.Select(async characterUrl =>
        //        {
        //            try
        //            {
        //                await Task.Delay(1000);
        //                var character = await _apiService.FetchDataAsync<Character>(characterUrl);

        //                if (character != null)
        //                {
        //                    var houseTasks = character.Allegiances.Select(async houseUrl =>
        //                    {
        //                        try
        //                        {
        //                            var house = await _apiService.FetchDataAsync<House>(houseUrl);
        //                            return house?.Name;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Console.WriteLine($"Could not fetch name of house for character: {character.Name}");
        //                            return null;
        //                        }
        //                    });
        //                    var houseNames = (await Task.WhenAll(houseTasks)).Where(name => name != null).ToList();
        //                    return new Character
        //                    {
        //                        Name = character.Name,
        //                        Allegiances = houseNames,
        //                        Titles = character.Titles ?? new List<string>(),
        //                        Books = character.Books.Where(b => b != book.Url).ToList(),
        //                        PovBooks = character.Books.Where(b => b!= book.Url).ToList()
        //                        //PovBooks = book.PovCharacters.Contains(characterUrl)
        //                    };
        //                }
        //                return null;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Fetching data for characters failed: {ex.Message}");
        //                return null;
        //            }
        //        });

        //        var characters = (await Task.WhenAll(characterTasks)).Where(c => c != null).ToList();

        //        books.Add(new Book
        //        {
        //            Name = book.Name,
        //            Characters = book.Characters
        //        });

        //        // fortsätt härifrån
        //    }

        //    return books;
        //}







        public async Task<List<string>> FetchHouseCharacters(string houseUrl)
        {
            try
            {
                await Task.Delay(1000);

                var house = await _apiService.FetchDataAsync<House>(houseUrl);
                return house?.SwornMembers ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fetching house data failed: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<List<Character>> FetchCharactersForBookFromHouse(Book book, string houseUrl)
        {
            var houseCharacters = await FetchHouseCharacters(houseUrl) ?? new List<string>();

            var bookCharacters = book?.Characters ?? new List<string>();

            if (houseCharacters == null || houseCharacters.Contains(null))
                Console.WriteLine("Null found in houseCharacters!");

            if (bookCharacters == null || bookCharacters.Contains(null))
                Console.WriteLine("Null found in bookCharacters!");

            //var normalizedHouseCharacters = houseCharacters.Where(url => !string.IsNullOrEmpty(url))
            //.Select(url => url.Trim()
            //.ToLowerInvariant().TrimEnd('/')).ToList();

            var normalizedHouseCharacters = houseCharacters
            ?.Where(url => url != null)
            .Select(url => url.Trim().ToLowerInvariant().TrimEnd('/'))
            .ToList() ?? new List<string>();

            //var normalizedBookCharacters = book.Characters.Where(url => !string.IsNullOrEmpty(url))
            //.Select(url => url.Trim()
            //.ToLowerInvariant().TrimEnd('/')).ToList();

            var normalizedBookCharacters = bookCharacters
            ?.Where(url => url != null)
            .Select(url => url.Trim().ToLowerInvariant().TrimEnd('/'))
            .ToList() ?? new List<string>();

            var relevantCharacterUrls = normalizedBookCharacters.Intersect(normalizedHouseCharacters).ToList();

            var characterTasks = relevantCharacterUrls.Select(async characterUrl =>
            {
                try
                {
                    await Task.Delay(1000);

                    return await _apiService.FetchDataAsync<Character>(characterUrl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fetching data for relevant characters failed: {ex.Message}");
                    return null;
                }
            });

            var characters = (await Task.WhenAll(characterTasks)).Where(c => c != null).ToList();
            return characters;
        }

        //public async Task<List<Book>> FetchAllBooksAsync(string urls)
        //{
        //    List<Book> bookList = new List<Book>();

        //    foreach (var url in urls)
        //    {
        //        var book = (await _apiService.FetchDataAsync<Book>(url));

        //        if (book != null)
        //        {
        //            bookList.Add(book);
        //        }
        //    }

        //    return bookList; // Om detta funkar, bygg vidare för endast de specifika böckerna
        //}

        //public async Task<List<House>> FetchHousesAsync(string url)
        //{
        //    var houses = await _apiService.FetchDataAsync<List<House>>(url);

        //    if (houses == null)
        //    {
        //        Console.WriteLine("Fetching houses failed!");
        //        return null;
        //    }

        //    return houses;
        //}

        public async Task<List<Book>> FetchBooksForCharacter(Character character)
        {
            var bookTasks = character.Books.Select(async bookUrl =>
            {
                try
                {
                    return await _apiService.FetchDataAsync<Book>(bookUrl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed when fetching books for characters: {ex.Message}");
                    return null;
                }
            });

            var books = (await Task.WhenAll(bookTasks)).Where(book => book != null).ToList();
            return books;
        }

        public async Task<List<Character>> FetchCharactersNew(Book book, string houseUrl)
        {
            //-------------------------- Verkar fungera -------------------------------------- icke som jag vill

            var characterTasks = book.Characters.Select(async characterUrl =>
            {
                try
                {
                    var character = await _apiService.FetchDataAsync<Character>(characterUrl);

                    //if (character == null)
                    //{
                    //    Console.WriteLine($"Null data for URL: {characterUrl}");
                    //}

                    if (character != null && character.Allegiances.Contains(houseUrl))
                        return character;

                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed when fetching characters for book: {ex.Message}");
                    return null;
                }
            });

            var characters = (await Task.WhenAll(characterTasks)).Where(c => c != null).ToList();
            return characters;

            //-------------------------- Verkar fungera --------------------------------------
        }

        public async Task<List<Character>> FetchCharactersForBook(Book book, string houseUrl)
        {
            //-------------------------- Verkar fungera --------------------------------------

            var characterTasks = book.Characters.Select(async characterUrl =>
            {
                try
                {
                    var character = await _apiService.FetchDataAsync<Character>(characterUrl);

                    if (character != null && character.Allegiances.Contains(houseUrl))
                    {
                        return character;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed when fetching characters for book: {ex.Message}");
                    return null;
                }
            });

            var characters = (await Task.WhenAll(characterTasks)).Where(character => character != null).ToList();
            return characters;

            //-------------------------- Verkar fungera --------------------------------------
        }

        public async Task<List<House>> FetchHousesForCharacters(Character character)
        {
            var houseTasks = character.Allegiances.Select(async houseUrl =>
            {
                try
                {
                    return await _apiService.FetchDataAsync<House>(houseUrl);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Failed when fetching houses for characters: {ex.Message}");
                    return null;
                }
            });

            var houses = (await Task.WhenAll(houseTasks)).Where(house => house != null).ToList();
            return houses;
        }

        //private async Task<List<string>> FetchPovCharactersForBook(Book book)
        //{
        //    List<string> povCharacters = new List<string>();

        //    foreach (var povCharacterUrl in book.PovCharacters)
        //    {
        //        string povCharacter = await _apiService.FetchDataAsync<string>(povCharacterUrl);
        //        if (povCharacter != null)
        //        {
        //            povCharacters.Add(povCharacter);
        //        }
        //    }

        //    return povCharacters;
        //}
    }
}
