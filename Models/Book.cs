using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите название книги")]
        [Remote(action: "CheckBookName", controller: "Validation", ErrorMessage = "Книга с таким названием уже есть")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите Автора")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "Добавьте ссылку на фото обложки")]
        public string PhotoLink { get; set; }
        [Required(ErrorMessage = "Укажите дату выпуска")]

        public string ReleaseDate { get; set; }
        public bool IsGiven { get; set; } = false;
        [Required(ErrorMessage = "Добавьте описание")]

        public string About { get; set; }
        public string AddingDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
