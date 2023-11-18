using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.Client.Models
{
    public class CreateProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Загрузите изображение")]
        [Display(Name = "Изображение")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
