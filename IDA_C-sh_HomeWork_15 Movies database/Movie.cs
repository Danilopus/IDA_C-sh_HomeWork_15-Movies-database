using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IDA_C_sh_HomeWork_15_Movies_database
{
    public class Movie
    {
        public string Title_ { set; get; } = "default";
        public string Genre_ { set; get; } = "default";
        public string Year_ { set; get; } = "default";
        public string Rating_ { set; get; } = "0.0"; 

        public static List<Movie> LoadDataFromXML(string path)
        {
            List<Movie> movies_list = new List<Movie>();

            XmlDocument xmlDocument_1 = new XmlDocument();
            xmlDocument_1.Load(path);

            foreach (XmlElement movie in xmlDocument_1.DocumentElement)
            {
                Movie temp_obj = new();
                foreach (XmlElement movie_data_item in movie)
                    switch (movie_data_item.Name)
                    {
                        case "title": temp_obj.Title_ = movie_data_item.InnerText; break;
                        case "genre": temp_obj.Genre_ = movie_data_item.InnerText; break;
                        case "year": temp_obj.Year_ = movie_data_item.InnerText; break;
                        case "rating": temp_obj.Rating_ = movie_data_item.InnerText; break;
                        default: throw new Exception("data read failure");
                    }
                movies_list.Add(temp_obj);
            }
            return movies_list;

        }

        public override string ToString()
        {
            return Title_ + " " + Year_;
        }
    }
}
