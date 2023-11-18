using System.ComponentModel.DataAnnotations;

namespace Coffee.Client.Models
{
    public class CreateCategory
    {
        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Заголовок")]
        public string Name { get; set; }
    }
}
