using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo.DomainModels
{
   public interface IEntityBase
    {
        /// <summary>
        /// Primary Key of the Entity
        /// </summary>   
        int Id { get; set; }
    }
}
