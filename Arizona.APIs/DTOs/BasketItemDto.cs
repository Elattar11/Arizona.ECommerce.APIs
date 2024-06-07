using System.ComponentModel.DataAnnotations;

namespace Arizona.APIs.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(0.1 , double.MaxValue, ErrorMessage = "Invalid Price !!!")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Quantity !!!")]
        public int Quantity { get; set; }
    }
}