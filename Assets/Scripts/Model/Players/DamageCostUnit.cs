namespace AssemblyCSharp
{
    public class DamageCostUnit
    {
        public float dmgCost;
        public Unit unit;

        public DamageCostUnit(Unit unit)
        {
            this.unit = unit;
            dmgCost = unit.GetDamage()/unit.purchaseCost;
        }
    }
}