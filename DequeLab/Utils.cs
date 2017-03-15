using System;

namespace DequeLab
{
    public static class Utils
    {
        private static Random rnd = new Random();

        /// <summary>
        /// Метод генерации псевдослучайных целых чисел в диапазоне от -999 до 999.
        /// </summary>
        public static int genInt()
        {
            return rnd.Next(-999, 999);
        }

        public static string Calc(int x, int y, char c)
        {
            dynamic z = null;
            switch (c)
            {
                case (char)43:
                    z = x + y;
                    break;
                case (char)45:
                    z = x - y;
                    break;
                case (char)215:
                    z = x * y;
                    break;
                case (char)247:
                    if (y == 0)
                    {
                        return "Ошибка: делениe на ноль!\n";
                    }
                    z = (double)x / y;
                    break;
                default:
                    break;
            }
            return string.Format("{0} {1} {2} = {3}\n", x, c, y, z);
        }
    }
}
