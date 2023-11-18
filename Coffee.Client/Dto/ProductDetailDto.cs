using System.ComponentModel.DataAnnotations;

namespace Coffee.Client.Dto
{
    public class ProductDetailDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
      
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
    }
}
