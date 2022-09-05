using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GeometryController : Controller
    {
        public IActionResult Square()
        {
            Rectangle rect = new Rectangle(5, 10);
            return Json(rect);
        }
        public string Square(Rectangle rectangle)
        {
            //Geometry/Square?arr=5&arr=20  => автоматически парсит в массив

            //Geometry/Square?Width=5&Height=20    => автоматически парсит в объект

            //Geometry/Square?rectangles[0].Width=5&rectangles[0].Height=20&rectangles[1].Width=15&rectangles[1].Height=20
            //=> автоматически парсит в массив объектов
            return $"Площадь прямоугольника равна {rectangle.CalculateSquare()}";
        }
        //[HttpPost]
        //public double Square(int width, int height)
        //{
        //    return width*height;
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
    public class Rectangle
    {
        public Rectangle(int a, int b)
        {
            Width = a;
            Height = b;
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public double CalculateSquare()
        {
            return Width * Height;
        }
    }
}
