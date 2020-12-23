using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LaSumka.Models
{
    public class Categories
    {
        public int Id { get; set; }
        [Display(Name = "Назва категорії")]
        [Required(ErrorMessage = "Назва категорії є обов'язковою")]
        public string Name { get; set; }
        public List<Bag> Bags { get; set; }
    }
}
