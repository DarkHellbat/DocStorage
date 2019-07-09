using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStorage.Models.Filters
{
    public abstract class BaseFilter
    {
        public string SearchString { get; set; }
    }
}
