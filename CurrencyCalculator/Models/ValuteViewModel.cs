using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DailyInfoService;

namespace CurrencyCalculator.Models
{
    // Модель представления, которая передается в View
    public class ValuteViewModel
    {
        DailyInfoSoapClient dailyInfo = new DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap12); // Создаем экземляр класса сервиса ЦБ РФ
        public List<Valute> ValuteList { get; set; } // Коллекция для хранения всех валют
        public Valute Valute { get; set; } // Свойство, для хранения выбранной пользователем валюты из списка
        public string InputValue { get; set; } // Введенное пользователем значение
        public string[] ConvertedValues { get; set; } // Массив для хранения результата
        // Считается по формуле [КУРС_ВЫБРАННОЙ_ИЗ_СПИСКА_ВАЛЮТЫ] / [КУРС_i-ой_ВАЛЮТЫ_ИЗ_КОЛЛЕКЦИИ] * [ВВЕДЕННОЕ_ПОЛЬЗОВАТЕЛЕМ_ЗНАЧЕНИЕ]

        public ValuteViewModel()
        {
            ValuteList = new List<Valute>(); // Создаем экземпляр коллекции валют
            ValuteList.Add(new Valute() { Curs = 1, Name = "Российский рубль" }); // Добавляем в коллекцию рубль
            foreach (XmlNode tableName in dailyInfo.GetCursOnDateXMLAsync(DateTime.Now).Result) // Получаем от ЦБ РФ курс в XML-формате
            {
                // Создаем экземляр валюты, инициализируем его и добавляем в коллекцию
                Valute valute = new Valute();
                foreach (XmlNode valName in tableName)
                {
                    if (valName.Name == "Vname")
                    {
                        valute.Name = valName.InnerText;
                    }
                    if (valName.Name == "Vcurs")
                    {
                        valute.Curs = Double.Parse(valName.InnerText, CultureInfo.InvariantCulture);
                    }
                }
                ValuteList.Add(valute);
            }
            // Создаем экземпляр массива результатов
            ConvertedValues = new string[ValuteList.Count];
            // Пока значение не выбрано, по умолчанию будет Российский Рубль
            Valute = ValuteList[0];
            InputValue = "1";
            for (int i = 0; i < ConvertedValues.Length; i++)
                ConvertedValues[i] = ValuteList[i].Curs.ToString();
        }
    }
}
