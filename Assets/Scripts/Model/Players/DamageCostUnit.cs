using System;

namespace AssemblyCSharp
{
	public class DamageCostUnit
	{
		public Unit unit;
		private float dmgCost;


		public DamageCostUnit (Unit unit)
		{
		
			this.unit = unit;
			dmgCost = (unit.weapon.baseDamage)/unit.purchaseCost;

		}

		public int Cmp (DamageCostUnit other)
		{
			return this.dmgCost.CompareTo(other.dmgCost);
		}
	}
}

