using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab10Csharp
{
    public class FileManager
    {
        /// <summary>
        /// Запись обьекта в файл .json , возвращает ArgumentException ,если исходный обьект равен нулю или строка именя файла не заканчивается на ".json" 
        /// </summary>
        /// <param name="objectToWrite">обьект для записи</param>
        /// <param name="fileName">имя файла, куда записывается обьект</param>
        public static void WriteJSON(Object objectToWrite, string fileName)
        {
            if (objectToWrite is not null && fileName.EndsWith(".json"))
            {
                string output = JsonSerializer.Serialize(objectToWrite);
                using var outFile = new FileStream(fileName, FileMode.Create);
                using StreamWriter writer = new(outFile);
                writer.Write(output);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Чтение из файла .json,  возвращает ArgumentException, если обьект для дисериализации равен нулю или строка именя файла не заканчивается на ".json" 
        /// </summary>
        /// <param name="fileName">имя файла, откуда считываем обьект</param>
        /// <param name="obj">обьект для дисериализации, считанный обьект из файла приводится к типу этого обьекта</param>
        public static Object ReadJSON(string fileName, Object obj)
        {
            if (obj is not null && fileName.EndsWith(".json"))
            {
                using var file = new FileStream(fileName, FileMode.Open);
                using StreamReader reader = new StreamReader(file);
                string json = reader.ReadToEnd();
                return JsonSerializer.Deserialize(json, obj.GetType());
            }
            else
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Запись обьекта в файл .bin , возвращает ArgumentException ,если исходный обьект равен нулю или строка именя файла не заканчивается на ".bin" 
        /// </summary>
        /// <param name="objectToWrite">обьект для записи</param>
        /// <param name="fileName">имя файла, куда записывается обьект</param>
        public static void WriteBIN(Object objectToWrite, string fileName)
        {
            if (objectToWrite is not null && fileName.EndsWith(".bin"))
            {
                using var file = new FileStream(fileName, FileMode.Create);
                new BinaryFormatter().Serialize(file, objectToWrite);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Чтение из файла .bin, необходимо приведение типа возврвщаеммого обьекта,  возвращает ArgumentException, если строка имени файла не заканчивается на ".json" 
        /// </summary>
        /// <param name="fileName">имя файла, откуда считываем обьект</param>
        public static Object ReadBIN(string fileName)
        {
            if (fileName.EndsWith(".bin"))
            {
                using var file = new FileStream(fileName, FileMode.Open);
                return new BinaryFormatter().Deserialize(file);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
