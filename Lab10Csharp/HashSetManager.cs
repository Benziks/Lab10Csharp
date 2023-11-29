using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10Csharp
{
    public class HashSetManager
    {
        public delegate bool CompareDelegate<T>(T left, T right);
        public delegate bool SearchDelegate<T>(T obj, string searchValue);

        /// <summary>
        /// Cортировка элементов в коллекции по заданному критерию, используя алгоритм пузырьковой сортировки.
        /// </summary>
        /// <typeparam name="T">Тип элементов в множестве</typeparam>
        /// <param name="colectionElements">Множество элементов</param>
        /// <param name="compareDelegate">>Делегат, представляющий метод сравнения двух элементов</param>
        public static void Sort<T>(HashSet<T> colectionElements, CompareDelegate<T> compareDelegate)
        {
            T[] arrayElements = new T[colectionElements.Count];
            colectionElements.CopyTo(arrayElements, 0);
            for (int i = 0; i < arrayElements.Length - 1; i++)
            {
                for (int j = 0; j < arrayElements.Length - 1; j++)
                {
                    if (compareDelegate(arrayElements[j], arrayElements[j + 1]))
                    {
                        (arrayElements[j], arrayElements[j + 1]) = (arrayElements[j + 1], arrayElements[j]);
                    }
                }
            }
            colectionElements.Clear();
            foreach (var item in arrayElements)
            {
                colectionElements.Add(item);
            }
        }

        /// <summary>
        /// Фильтрует элементы множества на основе заданного условия с использованием делегата поиска.
        /// </summary>
        /// <typeparam name="T">Тип элементов в множестве</typeparam>
        /// <param name="colectionElements">Множество элементов</param>
        /// <param name="searchValue">Значение, по которому производится поиск</param>
        /// <param name="searchDelegate">Делегат, представляющий метод поиска </param>
        /// <returns></returns>
        public static HashSet<T> Filter<T>(HashSet<T> colectionElements, string searchValue, SearchDelegate<T> searchDelegate)
        {
            HashSet<T> filteredCollection = new HashSet<T>();
            foreach (var item in colectionElements)
            {
                if (searchDelegate(item, searchValue))
                {
                    filteredCollection.Add(item);
                }
            }
            return filteredCollection;
        }
    }
}
