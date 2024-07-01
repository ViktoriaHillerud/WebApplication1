namespace WebApplication1.Models
{
    public class Kitten
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Race { get; set; }
        public int Age { get; set; }
        public string? FavouriteTreat { get; set; }
        public bool HasOwner { get; set; }

        public Cat Parent { get; set; }

    }
}