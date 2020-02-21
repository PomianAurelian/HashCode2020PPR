using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSlice
{
    public class LibraryInputEntity
    {
        public LibraryInputEntity()
        {
            this.Library = new List<LibraryEntity>();
        }

        public int NbOfBooks { get; set; }

        public int NbOfLibraries { get; set; }

        public int NbOfDaysToScan { get; set; }

        public List<int> BooksScore { get; set; }

        public List<LibraryEntity> Library { get; set; }
    }

    public class LibraryEntity
    {
        public LibraryEntity()
        {
            this.Books = new List<BooksEntity>();
        }

        public int LibrariId { get; set; }

        public int NbOfBooks { get; set; }

        public int SignUpProcessDuration { get; set; }

        public int NbOfBooksCanShipPerDay { get; set; }

        public List<BooksEntity> Books { get; set; }
    }

    public class BooksEntity
    {
        public int Index { get; set; }

        public int BoockId { get; set; }
    }

    public class PizzaSliceInputWithIndexEntity
    {
         long _index;
         long _value;

        public long Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }

        public long Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }
}

