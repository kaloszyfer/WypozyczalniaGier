using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Operators
{
    /// <summary>
    /// Klasa wspomagająca operacje na tabelach
    /// </summary>
    class OperationHelper
    {
        /// <summary>
        /// Metoda zwraca wartość typu integer na podstawie podanego ciągu znaków. W przypadku niepowodzenia zwracana jest wartość parametru valueWhenFail (domyślnie 0). 
        /// </summary>
        /// <param name="cellStringValue"></param>
        /// <param name="valueWhenFail"></param>
        /// <returns></returns>
        public static int PrepareInt32Value(string cellStringValue, int valueWhenFail = 0)
        {
            if (cellStringValue.Length > 0)
            {
                int result;
                if (Int32.TryParse(cellStringValue, out result))
                {
                    return result;
                }
                else
                {
                    return valueWhenFail;
                }
            }
            else
            {
                return valueWhenFail;
            }
        }
        /// <summary>
        /// Metoda zwraca wartość typu DateTime na podstawie podanego ciągu znaków. W przypadku niepowodzenia zwracany jest null.
        /// </summary>
        /// <param name="cellStringValue"></param>
        /// <returns></returns>
        public static DateTime? PrepareDateTimeValue(string cellStringValue, string format = "dd.MM.yyyy HH:mm:ss") // jakby co u mnie format otrzymany z db miał właśnie taką postać
        {
            if (cellStringValue.Length > 0)
            {
                DateTime result;
                if (DateTime.TryParseExact(cellStringValue, format, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
                //return DateTime.ParseExact(cellStringValue, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                return null;
            }
        }
    }
}