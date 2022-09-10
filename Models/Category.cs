using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите назвние категории")]
        [Remote(action: "CheckVategoryName", controller: "Validation", ErrorMessage = "Такая категория уже есть")]
        public string Name { get; set; }
    }
}
