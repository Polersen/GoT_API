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
            var houseCharacters = await FetchHouseCharacters(houseUrl);

            var normalizedHouseCharacters = houseCharacters.Where(url => !string.IsNullOrEmpty(url))
            .Select(url => url.Trim()
            .ToLowerInvariant().TrimEnd('/')).ToList();

            var normalizedBookCharacters = book.Characters.Where(url => !string.IsNullOrEmpty(url))
            .Select(url => url.Trim()
            .ToLowerInvariant().TrimEnd('/')).ToList();

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
