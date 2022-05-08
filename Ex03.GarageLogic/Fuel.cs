using System;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        public enum eFuelType { Soler ,Octan95 ,Octan96 ,Octan98 }

        readonly eFuelType m_FuelType;

        public Fuel(eFuelType i_FuelType, float i_MaxLiter) : base(i_MaxLiter)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuelType()
        {
            return m_FuelType;
        }

        public void FillFuel(float i_LitersOfFuel, eFuelType i_FuelType)
        {
            if(m_FuelType == i_FuelType)
            {
                FillEnergy(i_LitersOfFuel);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
