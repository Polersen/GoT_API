using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT_API
{
    public class Character
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Culture { get; set; }
        public string Born {  get; set; }
        public string Died { get; set; }
        public List<string> Titles { get; set; } = new List<string>();
        public List<string> Aliases { get; set; } = new List<string>();
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Spouse { get; set; }
        public List<string> Allegiances { get; set; } = new List<string>();
        public List<string> Books { get; set; } = new List<string>();
        public List<string> PovBooks { get; set; } = new List<string>();
        public List<string> TvSeries { get; set; } = new List<string>();
        public List<string> PlayedBy { get; set; } = new List<string>();
    }
}
