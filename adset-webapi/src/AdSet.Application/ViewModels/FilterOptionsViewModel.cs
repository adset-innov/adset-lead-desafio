using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSet.Application.ViewModels
{
    public class FilterOptionsViewModel
    {
        public List<int> Years { get; set; } = new List<int>();
        public List<string> PriceRanges { get; set; } = new List<string>();
        public List<string> PhotoOptions { get; set; } = new List<string>();
        public List<string> Colors { get; set; } = new List<string>(); 
    }
}
