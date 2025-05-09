﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoT_API
{
    public class House
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public List<string> Titles { get; set; } = new List<string>();
        public List<string> Seats { get; set; } = new List<string>();
        public string CurrentLord { get; set; }
        public string Heir { get; set; }
        public string OverLord { get; set; }
        public string Founded { get; set; }
        public string Founder { get; set; }
        public string DiedOut { get; set; }
        public List<string> AncestralWeapons { get; set; } = new List<string>();
        public List<string> CadetBranches { get; set; } = new List<string>();
        public List<string> SwornMembers { get; set; } = new List<string>();
    }
}