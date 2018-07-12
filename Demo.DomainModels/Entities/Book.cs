using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo.DomainModels.Entities
{
    public class Book : IEntityBase
    {
        public Book()
        {
            DateOfPublication = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(120)]
        [Column(TypeName = "VARCHAR(120)")]
        public string Name { get; set; }

        [StringLength(120)]
        [Column(TypeName = "VARCHAR(120)")]
        public string Authors { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}
