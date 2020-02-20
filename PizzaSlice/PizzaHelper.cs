using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSlice
{
    public static class PizzaHelper
    {
        public static string[] SplitTextInput(string text, string separator)
            => text.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

        public static string CreateOutputText(PizzaSliceOutputEntity pizzaSliceOutput)
        {
            return $@"{pizzaSliceOutput.NumberOfPizzaTypes}{Environment.NewLine}"+
                   $@"{String.Join(" ", pizzaSliceOutput.PizzaSummedNumbers.Select(p => p.ToString()))}";
        }
    }
}
