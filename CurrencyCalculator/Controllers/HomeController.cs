using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CurrencyCalculator.Models;
using System.Xml;
using DailyInfoService;
using System.Globalization;

namespace CurrencyCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Создаем экземпляр класса модели представления
        ValuteViewModel vvm = new ValuteViewModel();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Передаем модель в View
        // valueSelect - значение, которое пользователь выбрал из списка
        // inputValue - значние, которое ввел пользователь
        public IActionResult Index(string valuteSelect, string inputValue)
        {
            if (valuteSelect != null)
            {
                vvm.Valute = vvm.ValuteList.Single(p => p.Name == valuteSelect); // Присваиваем Valute новое значение, выбранное пользователем, для передачи в View
                vvm.InputValue = inputValue; // Присваиваем InputValue новое значение, введенное пользователем, для передачи в View
                for (int i = 0; i < vvm.ValuteList.Count; i++) // Пересчитываем результат для новых значений пользователя
                {
                    vvm.ConvertedValues[i] = ((vvm.Valute.Curs / vvm.ValuteList[i].Curs) * Double.Parse(inputValue, CultureInfo.InvariantCulture)).ToString();
                }
            }
            return View(vvm); //Передаем модель представления в View
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
