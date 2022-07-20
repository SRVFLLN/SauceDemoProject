using System;
using System.Linq;

namespace SauceDemoProject.Utils
{
    public static class ExtensionMethods
    {
        public static double ExtractNumber(this string str, int decimals = 2) 
        {
            string numbers = string.Empty;
            for (int i = str.IndexOf((from pricestr in str where char.IsDigit(pricestr) select pricestr).First());
                i < str.Length; i++)
            {
                numbers += str[i];
            }
            if(decimals != 0) return Math.Round(float.Parse(numbers),decimals);
            return float.Parse(numbers);
        }
    }
}
