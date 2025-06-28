using System.ComponentModel.DataAnnotations;

namespace AspnetCoreWebApiProjectPractice.Models
{
    public class Book
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "عنوان کتاب الزامی است")]
        [MaxLength(100, ErrorMessage = "عنوان کتاب نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
        public string Title { get; set; }


        [Required(ErrorMessage = "نام نویسنده الزامی است")]
        [MaxLength(50)]
        public string Author { get; set; }

        public string? ImageUrl { get; set; }
    }
}
