using System.Collections.Generic;
using System.Linq;

namespace AssemblyCSharp
{
    public class KnapsackProblem
    {
        private readonly List<Unit> benefit;
        private readonly List<DamageCostUnit> listDmgCost;
        private int maxCoins;

        public KnapsackProblem()
        {
            benefit = new List<Unit>();
            listDmgCost = new List<DamageCostUnit>();
        }

        /**
         * This function obtains a sorted list of units with damage/cost
        */

        public void infoCalc(List<Unit> availableUnits, int actualCoins)
        {
            maxCoins = actualCoins;

            for (var i = 0; i < availableUnits.Count; i++)
            {
                var unitC = new DamageCostUnit(availableUnits[i]);
                listDmgCost.Add(unitC);
            }

            listDmgCost.OrderBy(i => i.dmgCost).Reverse();

            for (var i = 0; i < listDmgCost.Count; i++)
                benefit.Add(listDmgCost[i].unit);
        }

        /**
         * This function realize a greedy algorithm of Knopack problem
        */

        public List<Unit> Resolve()
        {
            List<Unit> solution;
            int position, actualCost;

            solution = new List<Unit>();
            position = 0;
            actualCost = 0;

            while ((actualCost < maxCoins) && (position < benefit.Count))
            {
                var unitAux = benefit[position];

                if (actualCost + unitAux.purchaseCost <= maxCoins)
                {
                    solution.Add(unitAux);
                    actualCost += unitAux.purchaseCost;
                }
                else
                {
                    position++;
                }
            }
            return solution;
        }
    }
}