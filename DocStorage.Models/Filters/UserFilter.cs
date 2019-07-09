using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStorage.Models.Filters
{
    public class UserFilter : BaseFilter
    {
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "ФИО")]
        public string FIO { get; set; }

        // public DateRange Date { get; set; }

    }
}
