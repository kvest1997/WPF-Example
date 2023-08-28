using System.Globalization;
using System.Net;

namespace WPFExampleConsole
{
    internal class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        /// <summary>
        /// Метод возращающий поток данных, скачивает не все данные а только сначала заголовки, чтобы не было нагрузки
        /// </summary>
        /// <returns></returns>
        private static async Task<Stream> GetDataStream()
        {
            //Запрос к серверу
            var client = new HttpClient(); //клиент
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);//получение запроса с заголовками

            return await response.Content.ReadAsStreamAsync(); // возращает поток
        }

        /// <summary>
        /// Считывание данных из потока
        /// </summary>
        /// <returns></returns>
        private static  IEnumerable<string> GetDataLines()
        {
            using var data_stream =  GetDataStream().Result; //получаем поток
            using var data_reader = new StreamReader(data_stream); //считываем данные из потока

            while(!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine(); // получаем данные пока строка не пустая

                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,","Korea -").Replace("Bonaire,", "Bonaire -");
            }
        }

        /// <summary>
        /// Получение только дат с помощью LINQ
        /// Возращает только данные ДАТА остальное не скачивается
        /// </summary>
        /// <returns></returns>
        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

                                    //кортеж, чтобы вернуть несколько переменных 
        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)                                //сколько столбцов надо пропустить
                .Select(line => line.Split(','));       //по какому критерию надо выбирать данные

            foreach(var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country_name, province, counts);
            }
        }

        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var response = client.GetAsync(data_url).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            //var dates = GetDates();

            //Console.WriteLine(string.Join("\r\n", dates));

            var russia_data = GetData()
                .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.Counts, (date, count)=>$"{date:dd:MM} - {count}")));

            Console.ReadKey();
        }
    }
}