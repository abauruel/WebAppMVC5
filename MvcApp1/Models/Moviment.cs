using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApp1.Models
{
    public class Moviment
    {
        [Key]
        public int Id { get; set; }
       [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Price { get; set; }
    }
}