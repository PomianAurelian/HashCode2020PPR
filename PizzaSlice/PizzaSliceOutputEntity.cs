using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSlice
{
    public class PizzaSliceOutputEntity
    {
        public PizzaSliceOutputEntity()
        {
            this.PizzaSummedNumbers = new List<long>();
        }

        public int NumberOfPizzaTypes { get; set; }

        public List<long> PizzaSummedNumbers { get; set; }
    }
}
