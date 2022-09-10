using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес электронной почты")]
        [Remote(action: "CheckMail", controller: "Validation", ErrorMessage = "Этот Email занят")]
        public string Email { get; set;}

        public string PhoneNumber { get; set; }
    }
}
