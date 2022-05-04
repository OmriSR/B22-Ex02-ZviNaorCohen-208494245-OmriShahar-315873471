using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class SystemVehiclesCreator
    {
        public enum eVehicleType { ElectricCar, FuelCar, ElectricMotoecycle, FuelMotorcycle, FuelTruck }
 
        public Car NewGenericElectricCar(string i_VehicleModel, string i_LicenseNumber, short i_NumOfWheels)
        {
            Electric ElectricEngin = new Electric();
            Wheel[] Wheels = new Wheel[4];

            ElectricEngin.MaxEnergy = Convert.ToSingle(3.3);

            foreach (Wheel wheel in Wheels)
            {
                wheel.MaxAirPressure = 29;
            }

            return new Car(i_VehicleModel, i_LicenseNumber, ElectricEngin, Wheels);
        }

        public Car newFuelCar()
        {
            Car newCar;
            return newCar;
        }
    }
}
