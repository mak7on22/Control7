using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KR.Models.State;

namespace KR.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Изображение")]
        public string ImgUrl { get; set; }

        [Range(1800, 2023)]
        [Display(Name = "Год выпуска")]
        public int YearOfRelease { get; set; }
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        [Display(Name = "Дата добавления")]
        public DateTime? DateAdd { get; set; }
        [Display(Name = "Статус")]
        public string? State { get; set; }
        [NotMapped]
        public IbookStats? BookState { get; set; }
        public void TakeBook()
        {
            BookState?.TakeTheBook(this);
        }
        public void ReturnBook()
        {
            BookState?.ReturnTheBook(this);
        }
    }
}
