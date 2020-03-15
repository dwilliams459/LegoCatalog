using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LegoCatalog.Data
{
    public class Event
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(255)]
        public string Owner { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }
    }
}
