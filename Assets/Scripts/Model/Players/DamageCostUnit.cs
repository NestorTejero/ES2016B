﻿using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class DamageCostUnit
	{
		public Unit unit;
		public float dmgCost;


		public DamageCostUnit (Unit unit)
		{
			this.unit = unit;
			dmgCost = (unit.damage)/unit.purchaseCost;

		}
			
	}
}

