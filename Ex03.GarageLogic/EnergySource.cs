using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected float m_MaxEnergy;
        protected float m_CurrentEnergy;

        protected EnergySource(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
        }

        public virtual void FillEnergy(float i_AmountToFill)
        {
            float newEnergyAmount = m_CurrentEnergy + i_AmountToFill;

            if (newEnergyAmount < m_MaxEnergy)
            {
                m_CurrentEnergy = newEnergyAmount;
            }
            else
            {
                throw new ValueOutOfRangeException();
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
