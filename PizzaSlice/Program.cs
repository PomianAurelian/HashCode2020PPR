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
        public const string INPUT_FILE_NAME = @"f_libraries_of_the_world.txt";
        public const string OUTPUT_FILE_NAME = @"f_libraries_of_the_world_output.txt";
        public static string separator = "\n";

        static void Main(string[] args)
        {
            var filePathOutput = Path.Combine(Directory.GetCurrentDirectory(), OUTPUT_FILE_NAME);
            var filePathInput = Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME);

            LibraryInputEntity libraryInput = new LibraryInputEntity();
            LibraryOutputEntity liberyOutput = new LibraryOutputEntity();
            LibraryHandler handler = new LibraryHandler();
            TextWriter sw = new StreamWriter(filePathOutput);

            string fullText = File.ReadAllText(filePathInput);

            var fullTextSplitted = LibraryHelper.SplitTextInput(fullText, separator);

            handler.ToLibraryEntity(fullTextSplitted, libraryInput);

            //calculate logic
            handler.CalculateLibraries(libraryInput, liberyOutput);


            //write output header
            var outputText = LibraryHelper.CreateOutputTextFirstLine(liberyOutput);
            sw.Write(outputText);

            //write output libraries
            foreach (var item in liberyOutput.Library)
            {
                var output = LibraryHelper.CreateOutputTextLibrariesHeader(item);
                sw.Write(output);               
            }

            sw.Close();
        }
    }
}
