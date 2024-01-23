using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KendoEmployeeApp.Models.SubModels
{
    public class _Employee
    {
        [Key]
        public int EmpId{ get; set; }
        public string EmpName{ get; set; }
        public string Address { get; set; }

    }
}