﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        bool m_HasCoolingUnit;
        float m_TrunkCapacity;

        public Truck(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource) :
            base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[16];
            for(int i = 0; i < 16; i++)
            {
                m_Wheels[i] = new Wheel();
            }
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.MaxAirPressure = 24;
            }
        }

        //-----------unique data check
        bool isValidAnswer(string i_Color)
        {
            bool validColor = false;

            switch (i_Color.ToLower())
            {
                case "yes":
                case "no":
                    validColor = true;
                    break;
            }

            return validColor;
        }

        bool isValidCapacity(string i_TrunkCapacity)
        {
            return float.TryParse(i_TrunkCapacity, out float TrunkCapacity);
        }

        public override short ValidateUniqueData(string[] i_UniqueData)
        {
            short errorIndex = -1;

            if (!isValidAnswer(i_UniqueData[0]))
            {
                errorIndex = 0;
            }

            else if (!isValidCapacity(i_UniqueData[1]))
            {
                errorIndex = 1;
            }

            return errorIndex;
        }

        //----------------unique data------------------
        public override void SetUniqueData(string[] i_UniqueData)
        {
           // m_HasCoolingUnit = i_UniqueData[0].ToLower() == "True";
            m_HasCoolingUnit = i_UniqueData[0].ToLower() == "yes" ? m_HasCoolingUnit = true : m_HasCoolingUnit = false;
            m_TrunkCapacity = Convert.ToSingle(i_UniqueData[1]);
        }

        public override string[] PrintUniqueData()
        {
            //   string[] UniqeDataMembers = { "'Yes' if the truck has a cooling unit, 'No' otherwise ", "the trunck capacity" };  
            string[] uniqueDataMembers = { string.Format("Cooling unit: {0}.\n Trunk capacity: {1}", m_HasCoolingUnit, m_TrunkCapacity) };
            return uniqueDataMembers;
        }
        public override string[] GetUniqueData
        {
            get
            {
             //   string[] UniqeDataMembers = { "'Yes' if the truck has a cooling unit, 'No' otherwise ", "the trunck capacity" };  
             string[] uniqueDataMembers = { "Cooling Unit (YES/NO): ", "Trunk Capacity: "};
             return uniqueDataMembers;
            }
        }
    }
}
