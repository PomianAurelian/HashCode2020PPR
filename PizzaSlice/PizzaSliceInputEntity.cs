using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSlice
{
    public class PizzaSliceInputEntity
    {
        public PizzaSliceInputEntity()
        {
            this.NumberOfSlicesToOrder = new List<long>();
        }

        public long PizzaToOrder { get; set; }

        public long PizzaTypesToOrder { get; set; }

        public List<long> NumberOfSlicesToOrder { get; set; }
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

