using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyCalculator.Models
{
    // Анемичная модель для Валюты
    public class Valute
    {
        public string Name { get; set; } // Название валюты
        public double Curs { get; set; } // Курс валюты в рублях
    }
}
