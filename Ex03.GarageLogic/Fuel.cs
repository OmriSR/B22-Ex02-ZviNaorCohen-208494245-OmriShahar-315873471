using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Fuel : EnergySource
    {
        public enum eFuelType { Soler ,Octan95 ,Octan96 ,Octan98 }

        eFuelType m_FuelType;

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

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }
    }
}
