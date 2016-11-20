using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace AssemblyCSharp
{
	public class KnapsackProblem : MonoBehaviour
	{
		List<Unit> benefit;
		private int maxCoins;
		List<DamageCostUnit> listDmgCost;


		public KnapsackProblem ()
		{
			benefit = new List<Unit> ();
			listDmgCost = new List<DamageCostUnit> ();
		}

		/**
		 * This function obtains a sorted list of units with damage/cost
		*/
		public void infoCalc(List<Unit> availableUnits, int actualCoins){
			maxCoins = actualCoins;

			for (int i = 0; i < availableUnits.Count - 1; i++)
			{
				DamageCostUnit unitC = new DamageCostUnit (availableUnits [i]);
				listDmgCost.Add (unitC);
			}

			listDmgCost.Sort();

			for (int i = 0; i < listDmgCost.Count - 1; i++) 
			{
				benefit.Add (listDmgCost [i].unit);
			}
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

			while(actualCost < maxCoins && position < benefit.Count)
			{
				Unit unitAux = benefit [position];

				if (actualCost + unitAux.purchaseCost <= maxCoins) {
					solution.Add (unitAux);
					actualCost += unitAux.purchaseCost;
				} else {
					position++;
				}
			}

			return solution;

		}

	}
}

