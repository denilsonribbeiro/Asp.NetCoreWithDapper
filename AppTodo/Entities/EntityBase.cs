using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class EntityBase<T> where T : struct
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
    }
}
