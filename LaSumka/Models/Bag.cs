using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LaSumka.Models
{
    public class Bag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва є обов'язковою")]

        public string Name { get; set; }
       
        [Required(ErrorMessage = "Фото є обов'язковим")]

        public string Photo { get; set; }

        [Required(ErrorMessage = "Вкажіть наявність")]
        public bool Available { get; set; }

        [Required(ErrorMessage = "Вкажіть чи відображати товар на головній сторінці")]
        public bool isFavourite { get; set; }

        [Required(ErrorMessage = "Ціна є обов'язковою")]
        public float Price { get; set; }
        public string Description { get; set; }

        public List<Order> Orders { get; set; }
        public List<ShopCart> ShopCarts { get; set; }
        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }
    }
}
