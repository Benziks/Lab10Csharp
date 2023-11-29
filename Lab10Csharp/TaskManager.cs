using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab10Csharp
{
    public class TaskManager
    {
        private HashSet<string> _uniqueElementsHashSet = new HashSet<string>();
        private readonly object _lockObject = new object();
        private static readonly Random _random = new Random();

        /// <summary>
        /// Генерация случайной строки заданной длины.
        /// </summary>
        /// <param name="length">Длина строки</param>
        /// <returns>Случайно сгенерированная строка</returns>
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }

            return result.ToString();
        }
        /// <summary>
        /// Заполнение хэш-множества уникальными элементами.
        /// </summary>
        /// <param name="count">Количество элементов для генерации</param>
        private void FillHashSet(int count)
        {
            for (int i = 0; i < count; i++)
            {
                string generatedString;
                do
                {
                    generatedString = GenerateRandomString(8);
                    lock (_lockObject)
                    {
                        if (!_uniqueElementsHashSet.Contains(generatedString))
                        {
                            _uniqueElementsHashSet.Add(generatedString);
                            break;
                        }
                    }
                } while (true);
            }
        }
        /// <summary>
        /// Генерация уникальных элементов в параллельных задачах.
        /// </summary>
        /// <param name="elementsPerThread">Количество элементов для каждого потока</param>
        public void GenerateUnique(params int[] elementsPerThread)
        {
            if (elementsPerThread.Length != 4)
            {
                throw new ArgumentException("Exactly 4 values for elementsPerThread must be provided.");
            }

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 4; i++)
            {
                int elementsCount = elementsPerThread[i];
                Task task = Task.Run(() =>
                {
                    FillHashSet(elementsCount);
                    Console.WriteLine($"Thread {Task.CurrentId} generated {elementsCount} elements.");
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

        }
        /// <summary>
        /// Параллельная сортировка строк в хэш-множестве с использованием заданного количества потоков.
        /// </summary>
        /// <param name="threadCount">Количество потоков для сортировки</param>
        /// <returns>Отсортированное хэш-множество</returns>
        public HashSet<string> SortStringsParallel(int threadCount)
        {
            if (_uniqueElementsHashSet.Count == 0 || threadCount <= 0)
            {
                throw new Exception("HashSet is empty or thread count is less than 1");
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string> stringsList = _uniqueElementsHashSet.ToList();
            int sublistSize = (int)Math.Ceiling((double)stringsList.Count / threadCount);
            List<List<string>> parts = new List<List<string>>();
            for (int i = 0; i < stringsList.Count; i += sublistSize)
            {
                parts.Add(stringsList.GetRange(i, Math.Min(sublistSize, stringsList.Count - i)));
            }
            var sortedParts = new List<List<string>>();
            Parallel.ForEach(parts, partition =>
            {
                partition.Sort(StringComparer.OrdinalIgnoreCase);
                lock (sortedParts)
                {
                    sortedParts.Add(partition);
                }
            });

            var sortedStrings = sortedParts.SelectMany(x => x).ToList();
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            _uniqueElementsHashSet = new HashSet<string>(sortedStrings);

            Console.WriteLine($"Thread Count: {threadCount}, Sorting Time: {elapsedTime.TotalMilliseconds} ms");
            return _uniqueElementsHashSet;
        }

        /// <summary>
        /// Сортировка элементов в хэш-множестве.
        /// </summary>
        /// <returns>Отсортированное хэш-множество</returns>
        public HashSet<string> SortHashSet()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] sortedArray = _uniqueElementsHashSet.ToArray();
            Array.Sort(sortedArray);
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"Sorting Time: {elapsedTime.TotalMilliseconds} ms");
            return new HashSet<string>(sortedArray);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in _uniqueElementsHashSet)
            {
                sb.AppendLine(element);
            }
            return sb.ToString();
        }

    }
}
