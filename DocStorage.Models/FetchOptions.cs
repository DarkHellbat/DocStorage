using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace DocStorage.Models
{
    public class FetchOptions
    {
        public int Start { get; set; }

        public int Count { get; set; }

        public string SortExpression { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
