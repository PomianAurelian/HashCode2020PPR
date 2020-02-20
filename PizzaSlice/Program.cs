using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSlice
{
    public class Program
    {
        public const string INPUT_FILE_NAME = @"e_also_big.in";
        public const string OUTPUT_FILE_NAME = @"e_also_big_output.in";
        public static string separator = "\n";

        static void Main(string[] args)
        {
            PizzaSliceInputEntity pizzaInputEntity = new PizzaSliceInputEntity();
            PizzaSliceOutputEntity pizzaSliceOutput = new PizzaSliceOutputEntity();
            PizzaHandler handler = new PizzaHandler();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME);

            string fullText = File.ReadAllText(filePath);

            var fullTextSplitted = PizzaHelper.SplitTextInput(fullText, separator);

            handler.ToPizzaEntity(fullTextSplitted, pizzaInputEntity);

            //calculate logic
            handler.CalculateNumberOfSlices(pizzaInputEntity, pizzaSliceOutput);

            var outputText = PizzaHelper.CreateOutputText(pizzaSliceOutput);

            if (File.Exists(OUTPUT_FILE_NAME))
            {
                File.Delete(OUTPUT_FILE_NAME);
            }

            File.WriteAllText(OUTPUT_FILE_NAME, outputText);
        }
    }
}
