using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT_API
{
    internal class House
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public List<string> Titles { get; set; } = new List<string>();
        public List<string> Seats { get; set; } = new List<string>();
        public Character CurrentLord { get; set; }
        public Character Heir { get; set; }
        public Character OverLord { get; set; }
        public string Founded { get; set; }
        public Character Founder { get; set; }
        public string DiedOut { get; set; }
        public List<string> AncestralWeapons { get; set; } = new List<string>();
        public List<House> CadetBranches { get; set; } = new List<House>();
        public List<Character> SwornMembers { get; set; } = new List<Character>();
    }
}
