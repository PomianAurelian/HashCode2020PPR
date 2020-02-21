using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSlice
{
    public class LibraryOutputEntity
    {
        public LibraryOutputEntity()
        {
            this.Library = new List<LibraryOutEntity>();
        }

        public int NbOfLinrariesForScanning { get; set; }

        public List<LibraryOutEntity> Library { get; set; }
    }

    public class LibraryOutEntity
    {
        public LibraryOutEntity()
        {
            this.BooksOutput = new List<BooksOutputEntity>();
        }

        public int LibrariId { get; set; }

        public int NbOfBooksForScanning { get; set; }

        public List<BooksOutputEntity> BooksOutput { get; set; }
    }

    public class BooksOutputEntity
    {
        public int Index { get; set; }

        public int BoockId { get; set; }
    }
}
