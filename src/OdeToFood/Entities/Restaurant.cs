using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Entities
{
    public enum CuisineType
    {
        None,
        Italian,
        French,
        Indian
    }
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
