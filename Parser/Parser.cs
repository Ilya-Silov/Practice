using Microsoft.Win32;

using practice.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Parser
{
    public class Parser
    {
        public static void ParseCity()
        {
            var path = @"F:\ПБ-41\Практика\Задание к УП03\Ресурсы\Город_import.csv";
            if (!File.Exists(path))
            {
                return;
            }

            var lines = File.ReadAllLines(path);
            List<City> list = new List<City>();
            foreach (var line in lines)
            {
                string[] temp = line.Split(';');
                list.Add(
                    new City() {
                        CityName = temp[1],
                    });
            }
        }
    }
}
