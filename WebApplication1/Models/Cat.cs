using WebApplication1.Models.DTOs;

namespace WebApplication1.Models
{
    public class Cat
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Race { get; set; }
        public int Age { get; set; }
        public string? FavouriteTreat { get; set; }
        public bool HasOwner { get; set; }

        public virtual List<Kitten>? Kittens { get; set; }

       
    }
}
