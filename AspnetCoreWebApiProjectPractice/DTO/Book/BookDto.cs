using System.ComponentModel.DataAnnotations;

namespace AspnetCoreWebApiProjectPractice.DTO.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public class CreateBookDto
    {
        [Required(ErrorMessage = "عنوان کتاب الزامی است")]
        [MaxLength(100, ErrorMessage = "عنوان کتاب نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
        public string Title { get; set; }



        [Required(ErrorMessage = "نام نویسنده الزامی است")]
        [MaxLength(50)]
        public string Author { get; set; }
    }
}
