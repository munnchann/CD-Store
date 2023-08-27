using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHN.Areas.Admin.Helper
{
    public class RandomGeneric
    {
        private static readonly Random random = new Random();

        private const int randomSymbolsDefaultCount = 8;
        private const string availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private int randomSymbolsIndex = 0;

        public string GetRandomSymbols()
        {
            return GetRandomSymbols(randomSymbolsDefaultCount);
        }

        public string GetRandomSymbols(int count)
        {
            var index = randomSymbolsIndex;
            var result = new string(
                Enumerable.Repeat(availableChars, count)
                          .Select(s => {
                              index += random.Next(s.Length);
                              if (index >= s.Length)
                                  index -= s.Length;
                              return s[index];
                          })
                          .ToArray());
            randomSymbolsIndex = index;
            return result;
        }

    }

}