using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10Csharp
{
    /// <summary>
    /// UniqueElements — класс для управления уникальными элементами.
    /// </summary>
    public class UniqueElements
    {
        private HashSet<string> _uniqueElementsHashSet = new HashSet<string>();

        public HashSet<string> UniqueElementsHashSet
        {
            get => _uniqueElementsHashSet;
            set => _uniqueElementsHashSet = value;
        }

        /// <summary>
        /// Добавление нового уникального элемента в HashSet.
        /// </summary>
        /// <param name="element">Новый элемент который необходимо добавить, если в строке несколько слов, то происходит разделение и очистки пунктуации, проверка на NULL.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddInput(string element)
        {
            if (element == null)
            {
                throw new ArgumentOutOfRangeException("This part does not exist!");
            }

            string[] inputSplit = SplitInput(element);
            foreach (string word in inputSplit)
            {
                string inputWithoutPunctuation = ClearPunctuation(word);
                UniqueElementsHashSet.Add(inputWithoutPunctuation);
            }
        }

        /// <summary>
        /// Удаление нового уникального элемента в HashSet.
        /// </summary>
        /// <param name="element">Элемент который нужно удалить, проверка на NULL.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool RemoveElement(string element)
        {
            if (element == null)
            {
                throw new ArgumentOutOfRangeException("This part does not exist!");
            }
            return UniqueElementsHashSet.Remove(element);
        }

        /// <summary>
        /// Разделение
        /// </summary>
        /// <param name="input"> Выполнение и разделение строки input на подстроки, используя пробел как разделитель. Результатом будет массив подстрок. StringSplitOptions.RemoveEmptyEntries указывает, что нужно удалить все пустые элементы массива (например, если был введен двойной пробел), проверка на NULL.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string[] SplitInput(string input)
        {
            if (input == null)
            {
                throw new ArgumentOutOfRangeException("This part does not exist!");
            }
            return input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Разделение
        /// </summary>
        /// <param name="source"> Метод который принимает строку source в качестве аргумента и возвращает новую строку, в которой удалены все знаки пунктуации из исходной строки, проверка на NULL.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string ClearPunctuation(string source)
        {
            if (source == null)
            {
                throw new ArgumentOutOfRangeException("This part does not exist!");
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                if (!char.IsPunctuation(source[i]))
                {
                    builder.Append(source[i]);
                }
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in UniqueElementsHashSet)
            {
                sb.AppendLine(element);
            }
            return sb.ToString();
        }

    }
}
