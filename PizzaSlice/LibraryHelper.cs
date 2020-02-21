using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSlice
{
    public static class LibraryHelper
    {
        public static string[] SplitTextInput(string fullText, string separator)
        {
            return fullText.Split(new[] { separator }, StringSplitOptions.None);
        }

        public static string CreateOutputTextFirstLine(LibraryOutputEntity libraryOutput)
        {
            return $@"{ libraryOutput.NbOfLinrariesForScanning }{ Environment.NewLine }";
        }

        public static string CreateOutputTextLibrariesHeader(LibraryOutEntity libraryOutput)
        {
            return $@"{ libraryOutput.LibrariId }{" "}{libraryOutput.NbOfBooksForScanning}{ Environment.NewLine }" +
                   $@"{ String.Join(" ", libraryOutput.BooksOutput.Select(b => b.BoockId).ToArray()) }{ Environment.NewLine }" ;
        }      
    }
}
