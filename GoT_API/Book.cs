using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT_API
{
    internal class Book
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public List<string> Authors { get; set; } = new List<string>();
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public string MediaType { get; set; }
        public DateTime Released { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Character> PovCharacters { get; set; } = new List<Character>();
    }
}
