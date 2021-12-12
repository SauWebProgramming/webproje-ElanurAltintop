using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace InventoryBeginners.Models
{
    public enum SortOrder { Ascending = 0, Descending = 1 }
    public class Unit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [StringLength(75)]
        public string Description { get; set; }
    }
}
