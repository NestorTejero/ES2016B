using System.Collections.Generic;
using System.Linq;

namespace AssemblyCSharp
{
    public class KnapsackProblem
    {
        private readonly List<Unit> benefit;
        private readonly List<DamageCostUnit> listDmgCost;
        private int maxCoins;
		private int maxUnits;	//Max units that ai can buy

        public KnapsackProblem()
        {
            benefit = new List<Unit>();
            listDmgCost = new List<DamageCostUnit>();
			maxUnits = 6;
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
            int position, actualCost, actualUnits, abhorUnit;

            solution = new List<Unit>();
            position = 0;
            actualCost = 0;
			actualUnits = 0;	//int that controls the number of units we have bought
			abhorUnit = 0;	//int that controls the max of same unit bought

			while ((actualCost < maxCoins) && (position < benefit.Count) && (actualUnits <= maxUnits))
            {
                var unitAux = benefit[position];

				if ((actualCost + unitAux.purchaseCost <= maxCoins) && (abhorUnit < 2))
                {
                    solution.Add(unitAux);
                    actualCost += unitAux.purchaseCost;
					actualUnits++;
					abhorUnit++;
                }
                else
                {
					abhorUnit = 0;
                    position++;
                }
            }
            return solution;
        }
    }
}