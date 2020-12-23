using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace LaSumka.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        [Range(1, 20, ErrorMessage = "You should enter value from 1 to 20")]
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BagId { get; set; }
        public Bag Bag { get; set; }
    }
}
