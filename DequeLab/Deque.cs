using System;
using System.Collections.Generic;

namespace DequeLab
{
    internal class Deque<T>
    {
        /// <summary>
        /// Список элементов
        /// </summary>
        private List<int> elements;

        /// <summary>
        /// Конструктор класса очереди
        /// </summary>
        public Deque(int maxSize, int frontMinValue, int frontMaxValue)
        {
            Elements = elements;
            MaxSize = maxSize;
            FrontMinValue = frontMinValue;
            FrontMaxValue = frontMaxValue;
        }

        /// <summary>
        /// Свойство, позволяющее менять список элементов
        /// </summary>
        public List<int> Elements
        {
            get
            {
                return elements;
            }
            private set
            {
                elements = new List<int>();
            }
        }

        /// <summary>
        /// Максимальный размер очереди
        /// </summary>
        public int MaxSize { get; private set; }

        /// <summary>
        /// Минимально допустимое значение элемента, при котором
        /// он будет добавлен в начало очереди
        /// </summary>
        public int FrontMinValue { get; private set; }

        /// <summary>
        /// Максимально допустимое значение элемента, при котором
        /// он будет добавлен в начало очереди
        /// </summary>
        public int FrontMaxValue { get; private set; }

        /// <summary>
        /// Добавление элемента в конец очереди
        /// </summary>
        public void Push_Back(int element)
        {
            Elements.Add(element);
        }

        /// <summary>
        /// Взятие элемента из конца очереди
        /// с последующим удалением его из очереди
        /// </summary>
        public int Pop_Back()
        {
            int result = Elements[Elements.Count - 1];
            Elements.RemoveAt(Elements.Count - 1);
            return result;
        }

        /// <summary>
        /// Добавление элемента в начало очереди
        /// </summary>
        public void Push_Front(int element)
        {
            Elements.Insert(0, element);
        }

        /// <summary>
        /// Взятие элемента из начала очереди
        /// с последующим удалением его из очереди
        /// </summary>
        public int Pop_Front()
        {
            int result = Elements[0];
            Elements.RemoveAt(0);
            return result;
        }

        /// <summary>
        /// Текущий размер очереди
        /// </summary>
        public int currSize()
        {
            return Elements.Count;
        }

        /// <summary>
        /// Метод проверки, является ли очередь
        /// полностью заполненной
        /// </summary>
        public bool IsFull()
        {
            if (currSize() < MaxSize)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод проверки, является ли очередь пустой
        /// </summary>
        public bool IsEmpty()
        {
            if (currSize() > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Заполнение очереди элементами.
        /// В зависимости от значения элемент будет добавлен
        /// в начало или конец очереди.
        /// </summary>
        public void Fill(string s)
        {
            int element = Convert.ToInt32(s);
            if (InFrontRange(element))
            {
                Push_Front(element);
            }
            else
            {
                Push_Back(element);
            }
        }

        /// <summary>
        /// Диапазон значений вхождения элемента в начало очереди
        /// </summary>
        public bool InFrontRange(int element)
        {
            if (element >= FrontMinValue && element <= FrontMaxValue)
            {
                return true;
            }
            return false;
        }
    }
}
