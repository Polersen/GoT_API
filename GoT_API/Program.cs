namespace GoT_API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ApiService aS = new ApiService();

            string url = "https://www.anapioficeandfire.com/api/books";

            string data = await aS.FetchDataAsync(url);

            List<string> list = new List<string>();

            if (data != null)
            {
                //Console.WriteLine(data);

                foreach (var item in list)
                {
                    Console.WriteLine(item);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("........");
            }
        }
    }
}
