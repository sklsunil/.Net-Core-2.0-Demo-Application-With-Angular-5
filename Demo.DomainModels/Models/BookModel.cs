using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.DomainModels.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        [StringLength(120)]
        public string Authors { get; set; }

        public int NumberOfPages { get; set; }

        public string TestName { get; set; }
    }
}
