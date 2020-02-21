using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSlice
{
    public class LibraryHandler
    {
        public void ToLibraryEntity(string[] text, LibraryInputEntity pizzaItem)
        {
            if (!text.Any())
            {
                return;
            }

            int nbOfBooks;
            int nbOfLibraries;
            int daysOfScannng;
            var switchLine = true;
            int libraryId = 0;
            int bookIndex = 0;

            var firstLine = text.FirstOrDefault().Split(new char[0]);

            if (Int32.TryParse(firstLine.FirstOrDefault(), out nbOfBooks)) { pizzaItem.NbOfBooks = nbOfBooks; }
            if (Int32.TryParse(firstLine[1], out nbOfLibraries)) { pizzaItem.NbOfLibraries = nbOfLibraries; }
            if (Int32.TryParse(firstLine[2], out daysOfScannng)) { pizzaItem.NbOfDaysToScan = daysOfScannng; }

            pizzaItem.BooksScore = SplitLine(text[1].Split(new char[0]).ToArray());

            foreach (var item in text.Skip(2).ToArray())
            {
                if (String.IsNullOrEmpty(item))
                    continue;

                if (switchLine)
                {
                    var res = item.Split(new char[0]).ToArray();

                    var library = new LibraryEntity
                    {
                        LibrariId = libraryId,
                        NbOfBooks = Int32.Parse(res[0]),
                        SignUpProcessDuration = Int32.Parse(res[1]),
                        NbOfBooksCanShipPerDay = Int32.Parse(res[2])
                    };

                    pizzaItem.Library.Add(library);

                    switchLine = false;
                    
                }
                else
                {
                    var res = item.Split(new char[0]).ToArray();

                    foreach (var bookItem in res)
                    {
                        var book = new BooksEntity
                        {
                            Index = bookIndex,
                            BoockId = Int32.Parse(bookItem)
                        };

                        pizzaItem.Library.Where(lb => lb.LibrariId == libraryId).FirstOrDefault().Books.Add(book);                 

                        bookIndex++;                        
                    }

                    libraryId++;
                    switchLine = true;
                    bookIndex = 0;
                }
            }           
        }

        public List<int> SplitLine(string[] text)
        {
            List<int> result = new List<int>();

            foreach (var item in text)
            {
                int output;

                if (Int32.TryParse(item, out output)) { result.Add(output); }      
            }

            return result;
        }

        public void CalculateLibraries(LibraryInputEntity libraries, LibraryOutputEntity librariesOutput)
        {
            var listToCalculate = new List<PizzaSliceInputWithIndexEntity>();

            var numberOfSlicesToOrder = new List<long>();

            var librariesOrderd = libraries.Library.OrderBy(l => l.SignUpProcessDuration).ToList();

            var daysToScan = libraries.NbOfDaysToScan;
            var nbOfLibraries = libraries.Library.Count;

            while (nbOfLibraries != 0)
            {
                foreach (var library in librariesOrderd)
                {
                    var libraryEnt = new LibraryOutEntity
                    {
                        LibrariId = library.LibrariId
                    };                   

                    daysToScan = daysToScan - library.SignUpProcessDuration;

                    var signUpComplete = daysToScan;
                    
                    while(signUpComplete > 0
                          && library.Books.Any())
                    {
                        List<BooksOutputEntity> booksToUpload = new List<BooksOutputEntity>();
                        
                        var daysToShipBooks = signUpComplete;

                        while(daysToShipBooks > 0
                              && library.Books.Any())
                        {
                            var booksPerDay = library.NbOfBooksCanShipPerDay;

                            for ( int i = library.NbOfBooksCanShipPerDay; i >= 0; i--)
                            {                                        
                                //shipp books per day
                                foreach (var item in library.Books)
                                {
                                    if (booksPerDay == 0)
                                    {
                                        booksPerDay = library.NbOfBooksCanShipPerDay;
                                        break;
                                    }                                      

                                    var BookToSend = new BooksOutputEntity
                                    {
                                        Index = item.Index,
                                        BoockId = item.BoockId
                                    };

                                    booksToUpload.Add(BookToSend);                                
                                    booksPerDay--;
                                }

                                library.Books.RemoveAll(a => booksToUpload.Any(bo => bo.Index == a.Index));

                                daysToShipBooks--;

                                if (daysToShipBooks == 0)
                                {
                                    signUpComplete = 0;
                                    
                                    break;
                                }

                                signUpComplete--;
                            }                     
                        }

                        libraryEnt.NbOfBooksForScanning = booksToUpload.Count;

                        libraryEnt.BooksOutput.AddRange(booksToUpload);                       
                    }

                    if (libraryEnt.BooksOutput.Any())
                        librariesOutput.Library.Add(libraryEnt);

                    nbOfLibraries--;
                }
            }

            librariesOutput.NbOfLinrariesForScanning = librariesOutput.Library.Count;
        }
    }
}
