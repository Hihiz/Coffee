using System.ComponentModel.DataAnnotations;

namespace Coffee.Client.Models
{
    public class CreateEvent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите заголовок новости")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите текст новости")]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        //[Display(Name = "Дата публикации")]
        //public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Активность новости")]
        public bool IsActive { get; set; }
    }
}
