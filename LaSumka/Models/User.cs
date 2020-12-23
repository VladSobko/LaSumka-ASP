using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LaSumka.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Прізвище обов'язкове")]
        public string Surname { get; set; }
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        public string Name { get; set; }
        [Display(Name = "Номер телефону")]
        [Required(ErrorMessage = "Номер телефону обов'язковий")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Некоректний номер телефону")]
        public string Phone { get; set; }
        [Display(Name = "Електронна адреса")]
        [Required(ErrorMessage = "Електронна адреса обов'язкова")]
        [EmailAddress(ErrorMessage = "Некоректна електронна адреса")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обов'язковий")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Пароль має містити 3-20 символів")]
        public string Password { get; set; }


        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<ShopCart> ShopCarts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
