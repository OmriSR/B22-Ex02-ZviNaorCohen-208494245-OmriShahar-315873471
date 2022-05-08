using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Print
    {
        public static void GetStatusFromUserPrint()
        {
            Console.WriteLine("Please choose to which status you want to change: ");
            Console.WriteLine("1. In Fixings. ");
            Console.WriteLine("2. Fixed. ");
            Console.WriteLine("3. Paid. ");
        }

        public static void PrintVehicleDoesntExist()
        {
            Console.WriteLine("Vehicle doesn't exist. We need to open a new ticket. Please choose the type of vehicle: ");
            Console.WriteLine("1. Fuel bike.");
            Console.WriteLine("2. Electric bike.");
            Console.WriteLine("3. Fuel car.");
            Console.WriteLine("4. Electric car. ");
            Console.WriteLine("5. Truck. ");
            Console.WriteLine("6. Other. ");
        }

        public static void PrintFuelOptionsToRefill()
        {
            Console.WriteLine("Please enter type of fuel.");
            Console.WriteLine("1. Soler.");
            Console.WriteLine("2. Octan 95.");
            Console.WriteLine("3. Octan 96.");
            Console.WriteLine("4. Octan 98.");
        }

        public static void ShowLicensePlatesPrint()
        {
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Show -ALL- license plates in the garage.");
            Console.WriteLine("2. Show all license plates depends on its current status.");
        }

        public static void PrintSpecificLicensePlateStatus(string i_UserInput, Dictionary<int, Ticket> i_GarageTickets)
        {
            Ticket.eCurrentStatus statusFromUser = Ticket.eCurrentStatus.Fixed;
            int counter = 0;

            switch (i_UserInput)
            {
                case "1":
                    {
                        statusFromUser = Ticket.eCurrentStatus.InFixings;
                        break;
                    }
                case "2":
                    {
                        statusFromUser = Ticket.eCurrentStatus.Fixed;
                        break;
                    }
                case "3":
                    {
                        statusFromUser = Ticket.eCurrentStatus.Paid;
                        break;
                    }
            }

            foreach (KeyValuePair<int, Ticket> ticket in i_GarageTickets)
            {
                if (ticket.Value.CurrentStatus == statusFromUser)
                {
                    string output = String.Format("Vehicle plate: {0}. Current status: {1}", ticket.Value.Vehicle.LicenseNumber, ticket.Value.CurrentStatus);
                    Console.WriteLine(output);
                    counter++;
                }
            }

            if (counter == 0)
            {
                Console.WriteLine("No data for vehicles in the status you chose. ");
            }
        }

        public static void PressAnyKeyToReturnToMainMenu()
        {
            Console.WriteLine("Press any key to return to main menu. ");
            Console.ReadLine();
        }

        public static void PrintSpecificPlatesMenu()
        {
            Console.WriteLine("Please choose which status to see: ");
            Console.WriteLine("1. In fixings. ");
            Console.WriteLine("2. Fixed ");
            Console.WriteLine("3. Paid ");
        }

        public static void ShowAllLicensePlates(Dictionary<int, Ticket> i_GarageTickets)   // ticket does not contains license plate --- maybe it should
        {
            if (i_GarageTickets.Count == 0)
            {
                Console.WriteLine("The garage is empty! ");
            }

            else
            {
                foreach (KeyValuePair<int, Ticket> ticket in i_GarageTickets)
                {
                    string output = String.Format("Vehicle plate: {0}. Current status: {1}", ticket.Value.Vehicle.LicenseNumber, ticket.Value.CurrentStatus);
                    Console.WriteLine(output);
                }
            }
        }

        public static void ShowSpecificLicensePlates(Dictionary<int, Ticket> i_GarageTickets)
        {
            bool validInput = false;

            PrintSpecificPlatesMenu();

            while (!validInput)
            {
                string userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "2" || userInput == "3")
                {
                    PrintSpecificLicensePlateStatus(userInput, i_GarageTickets);
                }

                else
                {
                    Console.WriteLine("Invalid input. Please try again. ");
                    continue;
                }

                validInput = true;
            }
        }

        public static void PrintMainMenu()
        {
            Console.WriteLine("Hello and welcome to Omri and Zvika's Garage.");
            Console.WriteLine("Please enter an option: ");
            Console.WriteLine("1. Enter a new vehicle to the garage. ");
            Console.WriteLine("2. Show current vehicles license plates in the garage. ");
            Console.WriteLine("3. Change vehicle's status. ");
            Console.WriteLine("4. Inflate vehicle wheels. ");
            Console.WriteLine("5. Refill a vehicle running on fuel. ");
            Console.WriteLine("6. Refill a vehicle running on electric. ");
            Console.WriteLine("7. Show full information for specific license plate. ");
            Console.WriteLine("8. Exit. ");
        }

        public static void CarIsNotRunningOnFuel()
        {
            Console.WriteLine("Car is not running on fuel. Can't refill vehicle given. ");
        }

        public static void CarIsNotRunningOnElectricity()
        {
            Console.WriteLine("Car is not running on electricity. Can't recharge vehicle given. ");
        }

        public static void PrintVehicleStaticData(Ticket i_VehicleTicket, string i_LicenseNumber)
        {
            Console.WriteLine("Vehicle License Plate: {0}. ", i_LicenseNumber);
            Console.WriteLine("Vehicle type: {0}. ", i_VehicleTicket.Vehicle.GetType().Name);
            Console.WriteLine("Model Name: {0}. ", i_VehicleTicket.Vehicle.ModelName);
            Console.WriteLine("Owner Name: {0}. Owner Number: {1}. ", i_VehicleTicket.OwnerName, i_VehicleTicket.OwnerPhoneNumber);
            Console.WriteLine("Current Status: {0}. ", i_VehicleTicket.CurrentStatus);
            // Wheels --> air pressure and manufacturer
            Console.WriteLine("Number of wheels: {0}.", i_VehicleTicket.Vehicle.Wheels.Length);
            Console.WriteLine("Manufacturer for each: {0}.", i_VehicleTicket.Vehicle.Wheels[0].Manufacturer);
            Console.WriteLine("Current air pressure in each: {0}. ", i_VehicleTicket.Vehicle.Wheels[0].CurrentAirPressure);
            // Energy percent + kind of fuel / electric.
            Console.WriteLine("Energy percent: {0}. ", i_VehicleTicket.Vehicle.EnergySource.EnergyPercentage);
            Console.WriteLine("Type of energy: {0}", i_VehicleTicket.Vehicle.EnergySource.GetType().Name);
            if(i_VehicleTicket.Vehicle.EnergySource.GetType().Name == "Fuel")
            {
                Fuel fuelToPrint = i_VehicleTicket.Vehicle.EnergySource as Fuel;
                Console.WriteLine("Type of fuel: {0}. ", fuelToPrint.FuelType().ToString());
            }
            // Other vehicle extra information.
            string[] uniqueData = i_VehicleTicket.Vehicle.PrintUniqueData();
            Console.WriteLine(uniqueData[0]);
        }
    }
}
