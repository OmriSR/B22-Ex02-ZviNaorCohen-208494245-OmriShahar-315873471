using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class SystemVehiclesCreator
    {
        public enum eVehicleType { ElectricCar, FuelCar, ElectricMotorcycle, FuelMotorcycle, FuelTruck }
 
        public Car NewGenericElectricCar(string i_VehicleModel, string i_LicenseNumber)
        {
            Electric electricEngine = new Electric(Convert.ToSingle(3.3));
            return new Car(i_VehicleModel, i_LicenseNumber, electricEngine);
        }

        public Car NewGenericFuelCar(string i_VehicleModel, string i_LicenseNumber)
        {
            Fuel fuelEngine = new Fuel(Fuel.eFuelType.Octan95, Convert.ToSingle(38));

            return new Car(i_VehicleModel, i_LicenseNumber, fuelEngine);
        }

        public Motorcycle NewGenericElectricMotorcycle(string i_VehicleModel, string i_LicenseNumber)
        {
            Electric electricEngine = new Electric(Convert.ToSingle(2.5));

            return new Motorcycle(i_VehicleModel, i_LicenseNumber, electricEngine);
        }

        public static Motorcycle NewGenericFuelMotorcycle(string i_VehicleModel, string i_LicenseNumber)
        {
            Fuel fuelEngine = new Fuel(Fuel.eFuelType.Octan98, Convert.ToSingle(6.2));

            return new Motorcycle(i_VehicleModel, i_LicenseNumber, fuelEngine);
        }

        public Truck NewGenericFuelTruck(string i_VehicleModel, string i_LicenseNumber)
        {
            Fuel fuelEngine = new Fuel(Fuel.eFuelType.Soler, Convert.ToSingle(120));

            return new Truck(i_VehicleModel, i_LicenseNumber, fuelEngine);
        }
    }
}
