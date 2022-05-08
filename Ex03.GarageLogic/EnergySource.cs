using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        private float m_MaxEnergy;
        private float m_CurrentEnergy;

        protected EnergySource(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
        }

        public void FillEnergy(float i_AmountToFill)
        {
            float newEnergyAmount = m_CurrentEnergy + i_AmountToFill;

            if (newEnergyAmount < m_MaxEnergy)
            {
                m_CurrentEnergy = newEnergyAmount;
            }
            else
            {
                throw new ValueOutOfRangeException(string.Format("You tried to fill {0}. But now we have {1} and the maximum is {2}.", i_AmountToFill, m_CurrentEnergy, m_MaxEnergy));
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return ((m_CurrentEnergy / m_MaxEnergy) * 100);
            }
        }

        public float MaxEnergy
        {
            get
            {
                return m_MaxEnergy;
            }

            set
            {
                m_MaxEnergy = value;
            }
        }
    }
}
