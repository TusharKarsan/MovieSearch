using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebsite.Models
{
    public class SearchViewModel
    {
        public int[] Years { get; set; }

        public string[] Genres { get; set; }
    }
}
