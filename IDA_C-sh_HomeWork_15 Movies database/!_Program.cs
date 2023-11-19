// HomeWork template 1.4 // date: 17.10.2023

using Service;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Serialization;
using IDA_C_sh_HomeWork_15_Movies_database;
using System.Xml;
using System.IO;
using System.Text.Json;
using System.Collections;

/// QUESTIONS ///
/// 1. 

// HomeWork_15 Movies database --------------------------------

namespace IDA_C_sh_HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu.MainMenu mainMenu = new MainMenu.MainMenu();

            do
            {
                Console.Clear();
                mainMenu.Show_menu();
                if (mainMenu.User_Choice_Handle() == 0) break;
                Console.ReadKey();
            } while (true);
            // Console.ReadKey();
        }

        public static void Task_1(string work_name)
        /* Вам предоставляется файл movies.xml, содержащий информацию о различных фильмах в формате XML. 
        Ваша задача состоит в том, чтобы написать программу на C#, 
        которая будет анализировать этот файл и выводить определенную информацию о фильмах.

            Шаги для выполнения задания:

            Прочитайте файл movies.xml и загрузите его содержимое в программу.

            Используя LINQ, выполните следующие действия:

            Выведите список всех фильмов, отсортированных по названию в алфавитном порядке.
            Выведите список всех уникальных жанров фильмов.
            Посчитайте общее количество фильмов в файле.
            Найдите фильм с самым высоким рейтингом и выведите его название и рейтинг.
            Выведите список фильмов, отсортированных по году выпуска, начиная с самых новых.
            Найдите фильмы, выпущенные в последние 5 лет, и выведите их названия и год выпуска.
            Посчитайте средний рейтинг всех фильмов.
            Найдите фильмы, которые были выпущены после 2010 года и имеют рейтинг выше 8.

            Сохраните полученные результаты в файл output.json в формате JSON.

            Ваше решение должно быть оформлено в виде одного или нескольких классов, 
            обеспечивающих структуру программы и логику работы с XML и JSON файлами. 

            Комментарии к коду, поясняющие логику решения, также будут весьма приветствоваться. */
        { Console.WriteLine("\n***\t{0}\n\n", work_name);

            string origin_data_filename = "movies.xml";
            string output_data_filename = "output.json";


            // Надежда на интеллектуальную десериализацию не оправдалась
            using (FileStream fileStream_1 = new FileStream(origin_data_filename, FileMode.Open))
            {
                XmlSerializer xmlSerializer_1 = new XmlSerializer(typeof(Movie));
                XmlSerializer xmlSerializer_2 = new XmlSerializer(typeof(List<Movie>));
                XmlSerializer xmlSerializer_3 = new XmlSerializer(typeof(Movie[]));
                //var data_from_xml = xmlSerializer_1.Deserialize(fileStream_1); // System.InvalidOperationException: 'There is an error in XML document (2, 2).'
                //var data_from_xml_2 = xmlSerializer_2.Deserialize(fileStream_1); // System.InvalidOperationException: 'There is an error in XML document (2, 2).'
                //var data_from_xml_3 = xmlSerializer_2.Deserialize(fileStream_1); // System.InvalidOperationException: 'There is an error in XML document (2, 2).'
            }

            // Будем выгружать в ручном режиме
            // Считываем данные из movie.xml в movie_list
            List<Movie> movies_list = Movie.LoadDataFromXML(origin_data_filename);

            //
            List<object> modified_views = new();
 

            // Выведите список всех фильмов, отсортированных по названию в алфавитном порядке.
            Console.WriteLine("\n\n *** Выведите список всех фильмов, отсортированных по названию в алфавитном порядке:");
            var temp = movies_list.OrderBy(s => s.Title_);
                foreach (var movie in temp)
                    Console.WriteLine($"{movie.Title_}".PadLeft(30));
                modified_views.Add(temp);

            // Выведите список всех уникальных жанров фильмов.
            Console.WriteLine("\n\n *** Выведите список всех уникальных жанров фильмов:");
            var temp2 = movies_list.OrderBy(s => s.Genre_).GroupBy(m => m.Genre_);
            foreach (var movie in temp2)
                Console.WriteLine($"{movie.Key}".PadLeft(20));
            modified_views.Add(temp2);                   

            Console.WriteLine("\n\n *** Посчитайте общее количество фильмов в файле: {0}", movies_list.Count);

            Console.WriteLine("\n\n *** Найдите фильм с самым высоким рейтингом и выведите его название и рейтинг:");
            var temp3 = movies_list.OrderBy(s => s.Title_).Where(s => s.Rating_ == movies_list.Max(s => s.Rating_));
            foreach (var movie in temp3)
                Console.WriteLine($"{movie.Title_}".PadLeft(30) + " " + movie.Rating_);
            modified_views.Add(temp3);

            Console.WriteLine("\n\n *** Выведите список фильмов, отсортированных по году выпуска, начиная с самых новых:");
            var temp4 = movies_list.OrderByDescending(s => s.Year_);
            foreach (var movie in temp4)
                Console.WriteLine($"{movie.Title_}".PadLeft(30) + " " + movie.Year_);
            modified_views.Add(temp4);

            Console.WriteLine("\n\n *** Найдите фильмы, выпущенные в последние 10 лет, и выведите их названия и год выпуска:");
            var temp5 = movies_list.Where(s => (int.Parse(s.Year_) >= DateTime.Now.Year-10)).OrderByDescending(s => s.Year_);
            foreach (var movie in temp5)
                Console.WriteLine($"{movie.Title_}".PadLeft(30) + " " + movie.Year_);
            modified_views.Add(temp5);


            Console.WriteLine("\n\n *** Посчитайте средний рейтинг всех фильмов: {0}", movies_list.Average(s => double.Parse(s.Rating_.Replace('.',','))));

            Console.WriteLine("\n\n *** Найдите фильмы, которые были выпущены после 2010 года и имеют рейтинг выше 8:");
            var temp6 = movies_list.Where(s => (int.Parse(s.Year_) >= 2010)).Where(s => double.Parse(s.Rating_.Replace('.', ',')) >= 8);
            foreach (var movie in temp6)
                Console.WriteLine($"{movie.Title_}".PadLeft(30) + " " + movie.Year_ + " rating: " + movie.Rating_);
            modified_views.Add(temp6);


            // Сохраним все полученные представления в JSON файл
            using (FileStream fileStream_2 = new FileStream(output_data_filename, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter_1 = new StreamWriter(fileStream_2))
                {
                    streamWriter_1.WriteLine(JsonSerializer.Serialize(modified_views));
                }
            }
            Console.WriteLine("\n\n--- --- --- " + output_data_filename + " writed OK\n\n");

            // Прорверка что содержится в списке сохраненных отображений данных
            //PrintIEnumerable(modified_views);
        
            /*   foreach (IEnumerable view in modified_views)
                foreach (var item in view)
                    Console.WriteLine($"{item}");*/



            // А теперь для верности считаем JSON файл и проверим работает ли все как надо с этим хитрым списком List<OBject>
            Console.WriteLine("\n\nLet's check is there a right data at output.json\n" +
                "Trying to restore & view saved datasets\n ... press any key");
            Console.WriteLine("\n\n *** Using System.IO.StreamReader\n");
                        Console.ReadKey();
            Console.WriteLine("\n\n *** Using System.IO.StreamReader");
            using (StreamReader streamReader = new StreamReader(output_data_filename))
               {
                List<Object> a = JsonSerializer.Deserialize<List<Object>>(streamReader.ReadToEnd());
                foreach (var item in a)
                    Console.WriteLine(item);
               }

            Console.WriteLine("\n\n *** Using System.IO.FileStream\n");
            Console.ReadKey();
            using (FileStream fileStream = new FileStream(output_data_filename, FileMode.Open))
                {
                    var b = JsonSerializer.Deserialize<List<Object>>(fileStream);
                  foreach (var item in b)
                    Console.WriteLine(item);
                }
            Console.WriteLine("\n\n--- --- --- If you see some data: JSON Deserialization - OK\n");

        }

        static void PrintIEnumerable(IEnumerable collection)
        {
            foreach (IEnumerable view in collection)
                foreach (var item in view)
                    //Console.WriteLine($"{movie.Title_}".PadLeft(30) + " " + movie.Year_ + " rating: " + movie.Rating_);
                    Console.WriteLine($"{item}");

        }

    }// class Program
}// namespace