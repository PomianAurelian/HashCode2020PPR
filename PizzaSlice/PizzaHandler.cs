using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSlice
{
    public class PizzaHandler
    {
        public void ToPizzaEntity(string[] text, PizzaSliceInputEntity pizzaItem)
        {
            if (!text.Any())
            {
                return;
            }

            int pizzaOrder;
            int pizzaTypes;            

            var firstLine = text.FirstOrDefault().Split(new char[0]);
            var slicesLine = SplitSlicesLinesInFile(text.Skip(1).ToArray());

            if (Int32.TryParse(firstLine.FirstOrDefault(), out pizzaOrder)) { pizzaItem.PizzaToOrder = pizzaOrder; }
            if (Int32.TryParse(firstLine.LastOrDefault(), out pizzaTypes)) { pizzaItem.PizzaTypesToOrder = pizzaTypes; }

            foreach (var item in slicesLine)
            {
                int pizzaSlices;

                if (Int32.TryParse(item, out pizzaSlices)) { pizzaItem.NumberOfSlicesToOrder.Add(pizzaSlices); }
            }
        }

        public List<string> SplitSlicesLinesInFile(string[] text)
        {
            List<string> result = new List<string>();

            foreach (var item in text)
            {
                //split and add each slice from line, in entity
                result.AddRange(item.Split(new char[0]));
            }

            return result;
        }

        public void CalculateNumberOfSlices(PizzaSliceInputEntity pizzaInput, PizzaSliceOutputEntity pizzaOutput)
        {
            var listToCalculate = new List<PizzaSliceInputWithIndexEntity>();

            var numberOfSlicesToOrder = new List<long>();

            numberOfSlicesToOrder.AddRange(pizzaInput.NumberOfSlicesToOrder);

            var withIndex = pizzaInput.NumberOfSlicesToOrder.Select((item, indexf) => new
            {
                Value = item,
                Index = indexf
            })
            .ToList();           

            listToCalculate = withIndex.Select(ass => new PizzaSliceInputWithIndexEntity
            {
                Index = ass.Index,
                Value = ass.Value
            })
            .ToList();

            var listDescToCalculate = listToCalculate.OrderByDescending(n => n.Value).ToList();

            pizzaOutput.PizzaSummedNumbers = CalculateSumRecursion(listDescToCalculate, pizzaInput.PizzaToOrder).OrderBy(o => o).ToList();

            pizzaOutput.NumberOfPizzaTypes = pizzaOutput.PizzaSummedNumbers.Count;

        }

        //recursion method. Has very good precision, but bad performance with big data sets
        public List<long> CalculateSumRecursion(List<PizzaSliceInputWithIndexEntity> pizzaInput, long pizzaSlicesToOrder)
        {
            var bestResult = new List<long>();
            var bestResultMethod = new List<long>();

            var add = new List<long>();

            var result = CalculateSumFunction(pizzaInput, pizzaSlicesToOrder, pizzaInput.Count() - 1);

            List<long> CalculateSumFunction(List<PizzaSliceInputWithIndexEntity> pizzaIn, long pizzaSlices, long index = 0)
            {
                for (var i = index; i >= 0; i--)
                {
                    var pizzaSliceValue = pizzaIn.Where(p => p.Index == i).FirstOrDefault()?.Value;
                    var pizzaSliceIndex = pizzaIn.Where(p => p.Index == i).FirstOrDefault()?.Index;

                    long stopIndex = pizzaSlices - pizzaSliceValue.GetValueOrDefault();

                    // if the current number is too big for the target, skip
                    if (stopIndex < 0)
                    {
                        continue;
                    }
                    // if the current number is a solution, return a list with it
                    else if (stopIndex == 0)
                    {
                        add.Add(pizzaSliceValue.GetValueOrDefault());
                        return new List<long>() { pizzaSliceIndex.GetValueOrDefault() };
                    }
                    else
                    {
                        //save current number for best result
                        bestResultMethod.Add(pizzaSliceIndex.GetValueOrDefault());

                        // otherwise try to find a sum for the remainder later in the list
                        var res = CalculateSumFunction(pizzaIn, stopIndex, i - 1);

                        if (i - 1 >= pizzaIn.Count
                            || stopIndex - (pizzaSliceValue.GetValueOrDefault() - 1) < 0) //check if next iteraion was below 0
                        {
  
                            //check if list is empty at first run
                            if(!bestResult.Any())
                            {
                                bestResult.AddRange(bestResultMethod);
                            }
                            else
                            {
                                //compare the sum of current full iteration to best founded result and override if necessary
                                var bestResultSum = bestResult.Sum(b => b);
                                var bestResultMethodSum = bestResultMethod.Sum(b => b);


                                if (bestResultMethodSum < pizzaSlicesToOrder
                                    && bestResultMethodSum > bestResultSum)
                                {
                                    bestResult = new List<long>();
                                    bestResult.AddRange(bestResultMethod);
                                }
                            }

                            bestResultMethod.Remove(pizzaSliceIndex.GetValueOrDefault());
                        }

                        // if no number was returned, we could’t find a solution, so skip
                        if (res.Count == 0)
                            continue;

                        // otherwise we found a solution, so add our current number to it
                        // and return the result
                        res.Add(pizzaSliceIndex.GetValueOrDefault());
                        add.Add(pizzaSliceValue.GetValueOrDefault());
                        return res;
                    }
                }

                return new List<long>();
            }

            var dasda = add.Sum(ad => ad);

            return result.Any() ? result : bestResult;
        }


        public List<int> CalculateSum(List<int> pizzaInput, int pizzaSlicesToOrder)
        {
            var res = new List<int>();

            foreach (var item in pizzaInput)
            {



            }


            return res;
        }



    }
}
