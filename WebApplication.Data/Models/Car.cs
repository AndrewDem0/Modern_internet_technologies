using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Марка")]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Модель")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Рік випуску")]
        public int Year { get; set; }

        [Display(Name = "Ціна")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
