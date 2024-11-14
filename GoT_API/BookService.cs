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

        public async Task<List<Character>> FetchCharactersForBook(Book book)
        {
            //-------------------------- Verkar fungera --------------------------------------

            var characterTasks = book.Characters.Select(async characterUrl =>
            {
                try
                {
                    return await _apiService.FetchDataAsync<Character>(characterUrl);
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
