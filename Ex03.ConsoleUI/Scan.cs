using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Scan
    {
        public static bool GetTypeOfWheelInflation()
        {
            Console.WriteLine("Enter '1' for costume air Pressure or any other key for maximum possible: ");
            
            bool fillToMax = Console.ReadLine() != "1";

            return fillToMax;
        }

        public static float GetAirPressureToFill()
        {
            float airAmount;

            Console.WriteLine("Please enter wanted air pressure: ");

            while(!(float.TryParse(Console.ReadLine(), out airAmount)))
            {
                Ex02.ConsoleUtils.Screen.Clear();

                Console.WriteLine("Invalid Input. Please try again.");
                Console.WriteLine("Please enter wanted air pressure: ");
            }

            return airAmount;
        }

        public static Ticket.eCurrentStatus GetStatusFromUserScan()
        {
            Ticket.eCurrentStatus wantedStatus = Ticket.eCurrentStatus.InFixings;
            bool isInputValid = false;
            while(!isInputValid)
            {
                string userInput = Console.ReadLine();
                if(userInput == "1")
                {
                    wantedStatus = Ticket.eCurrentStatus.InFixings;
                }

                else if(userInput == "2")
                {
                    wantedStatus = Ticket.eCurrentStatus.Fixed;
                }

                else if(userInput == "3")
                {
                    wantedStatus = Ticket.eCurrentStatus.Paid;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please try again. ");
                    continue;
                }

                isInputValid = true;
            }

            return wantedStatus;
        }

        public static bool GetPlateAndCheckIfInGarageTickets(out string o_UserInput, Dictionary<int, Ticket> m_GarageTickets)
        {
            bool plateInTickets = false;
            Console.WriteLine("Please enter car license plate: ");
            string userInput = Console.ReadLine();

            try
            {
                if(m_GarageTickets.ContainsKey(userInput.GetHashCode()))
                {
                    plateInTickets = true;
                    o_UserInput = userInput;
                }

                else
                {
                    Console.WriteLine("License plate not found. Returning to main menu. ");
                    o_UserInput = "";
                }
            }

            catch(NullReferenceException)
            {
                Console.WriteLine("You tried to enter empty input, which is not valid. ");
                o_UserInput = "";
            }
            
            return plateInTickets;
        }

        public static string GetLicenseNumberFromUser()
        {
            Console.WriteLine("Please enter vehicle's license number. ");
            string userLicenseNumber = Console.ReadLine();

            return userLicenseNumber;
        }

        public static bool CheckUserInput(string i_UserInput)
        {
            bool inputIsValid = true;
            inputIsValid = (i_UserInput == "1" || i_UserInput == "2" || i_UserInput == "3" || i_UserInput == "4"
                            || i_UserInput == "5" || i_UserInput == "6" || i_UserInput == "7");

            return inputIsValid;
        }

        public static void GetDetailsForRefill(out float o_refillAmount, out Fuel.eFuelType o_FuelType)
        {
            bool isValidInput = false;
            o_refillAmount = 0;
            o_FuelType = Fuel.eFuelType.Soler;
            Console.WriteLine("Please enter your refill amount. ");

            while(!isValidInput)
            {
                string userInput = Console.ReadLine();
                if(!float.TryParse(userInput, out float refillAmount))
                {
                    Console.WriteLine("You entered invalid input. Please enter a number above 0.");
                }

                else if(refillAmount < 0)
                {
                    Console.WriteLine("Can't add negative values to refill amount. Please write numbers above 0.");
                }

                else
                {
                    Print.PrintFuelOptionsToRefill();
                    Fuel.eFuelType fuelToRefill = getFuelTypeToRefillFromUser();
                    o_FuelType = fuelToRefill;
                    isValidInput = true;
                    o_refillAmount = refillAmount;
                }
            }
        }

        private static Fuel.eFuelType getFuelTypeToRefillFromUser()
        {
            Fuel.eFuelType fuelUserInput = Fuel.eFuelType.Soler;
            while(true)
            {
                string userInput = Console.ReadLine();
                if(userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4")
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                else
                {
                    switch(userInput)
                    {
                        case "1":
                            {
                                fuelUserInput = Fuel.eFuelType.Soler;
                                break;
                            }

                        case "2":
                            {
                                fuelUserInput = Fuel.eFuelType.Octan95;
                                break;
                            }

                        case "3":
                            {
                                fuelUserInput = Fuel.eFuelType.Octan96;
                                break;
                            }

                        case "4":
                            {
                                fuelUserInput = Fuel.eFuelType.Octan98;
                                break;
                            }
                    }

                    break;
                }
            }

            return fuelUserInput;
        }

        public static void GetDetailsForRecharge(out float o_HoursToFill)
        {
            bool isValidInput = false;
            o_HoursToFill = 0;
            Console.WriteLine("Please enter your refill amount. (Liters for fuel, hours for electric) ");

            while(!isValidInput)
            {
                string userInput = Console.ReadLine();
                if(!float.TryParse(userInput, out float refillAmount))
                {
                    Console.WriteLine("You entered invalid input. Please enter a number above 0.");
                }

                else if(refillAmount < 0)
                {
                    Console.WriteLine("Can't add negative values to refill amount. Please write numbers above 0.");
                }

                else
                {
                    isValidInput = true;
                    o_HoursToFill = refillAmount;
                }
            }
        }

        public static string GetVehicleType()
        {
            bool validInput = false;
            string userInput = Console.ReadLine();

            while(!validInput)
            {
                if(userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4" || userInput == "5"
                   || userInput == "6")
                {
                    validInput = true;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please choose 1-6.");
                    userInput = Console.ReadLine();
                }
            }

            return userInput;
        }

        public static Motorcycle GetDetailsForElectricBike(string i_UserLicensePlate, string o_ModelName)
        {
           // GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "ElectricBike");
            Motorcycle userMotorcycle = SystemVehiclesCreator.NewGenericElectricMotorcycle(o_ModelName, i_UserLicensePlate);
            return userMotorcycle;
        }

        private static void checkValidEnergyAmount(string i_TypeOfVehicle, float currentEnergyAmount, out  float o_NewValidEnergyAmount)
        {
            //string modelName = "";
            bool validEnergyAmount = false;
            float maximumEnergy = 0;
            switch(i_TypeOfVehicle)
            {
                case "FuelBike":
                    {
                        maximumEnergy = 6.2f;
                        break;
                    }

                case "ElectricBike":
                    {
                        maximumEnergy = 2.5f;
                        break;
                    }

                case "FuelCar":
                    {
                        maximumEnergy = 38f;
                        break;
                    }

                case "ElectricCar":
                    {
                        maximumEnergy = 3.3f;
                        break;
                    }

                case "Truck":
                    {
                        maximumEnergy = 120f;
                        break;
                    }
            }
            while (!validEnergyAmount)
            {
                if (currentEnergyAmount > maximumEnergy)
                {
                    Console.WriteLine("Invalid input. Maximum capacity is {0} while you tried to enter {1}. Please try again.", maximumEnergy, currentEnergyAmount);
                    getEnergyDetailsForVehicle(out currentEnergyAmount);
                }

                else
                {
                    validEnergyAmount = true;
                }
            }
            o_NewValidEnergyAmount = currentEnergyAmount;
        }

        public static Motorcycle GetDetailsForFuelBike(string i_UserLicensePlate, string o_ModelName)
        {
         //   GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "FuelBike");
            Motorcycle userMotorcycle = SystemVehiclesCreator.NewGenericFuelMotorcycle(o_ModelName, i_UserLicensePlate);
            return userMotorcycle;
        }

        public static Ticket GetDetailsForTicket(Vehicle i_Vehicle)
        {
            Console.WriteLine("Please enter owner's name. ");
            string ownerName = Console.ReadLine();
            Console.WriteLine("Please enter owner phone number.");
            string ownerNumber = Console.ReadLine();
            if(!double.TryParse(ownerNumber, out _))
            {
                throw new ArgumentOutOfRangeException();
            }

            Ticket ticketToReturn = new Ticket(i_Vehicle, ownerName, ownerNumber);

            return ticketToReturn;
        }

        private static void getEnergyDetailsForVehicle(out float o_CurrentEnergyAmount)
        {
            o_CurrentEnergyAmount = 0;
            bool isValid = false;
            while(!isValid)
            {
                Console.WriteLine("Enter current litters of fuel tank / hours of battery left. ");
                string userInput = Console.ReadLine();
                if(float.TryParse(userInput, out float energyResult))
                {
                    o_CurrentEnergyAmount = energyResult;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again. ");
                }
            }
        }

        //-----This is the old one!! here only for the rest of the UN_GENERIC code-----------------
        private static void getDetailsForWheel(out string o_ManufacturerName, out float o_CurrentAirPressure, string i_VehicleType)
        {
            int maxAirPressure = Int32.MaxValue;
            o_CurrentAirPressure = 0;
            bool isValid = false;

            Console.WriteLine("Enter wheel manufacturer name. ");
            o_ManufacturerName = Console.ReadLine();
            Console.WriteLine("Please enter current air pressure. ");

            switch (i_VehicleType)
            {
                case "ElectricCar":
                case "FuelCar":
                    {
                        maxAirPressure = 29;
                        break;
                    }

                case "FuelBike":
                case "ElectricBike":
                    {
                        maxAirPressure = 31;
                        break;
                    }

                case "Truck":
                    {
                        maxAirPressure = 24;
                        break;
                    }
            }
            while (!isValid)
            {
                string airPressure = Console.ReadLine();
                if (float.TryParse(airPressure, out float userAirPressure))
                {
                    if (userAirPressure > 0 && userAirPressure <= maxAirPressure)
                    {
                        o_CurrentAirPressure = userAirPressure;
                        isValid = true;
                    }

                    else if (userAirPressure > maxAirPressure)
                    {
                        Console.WriteLine("You are trying to fill more air pressure than maximum. Maximum is: {0}.", maxAirPressure);
                    }

                    else
                    {
                        Console.WriteLine("Air pressure can't have values under 0.");
                    }
                }
                else
                {
                    Console.WriteLine("Could not read your air pressure. Please make sure you use only numbers.");
                }
            }

        }

        //----------------New and Generic -----------------------
        private static void getDetailsForGenericWheel(out string o_ManufacturerName, out float o_CurrentAirPressure, float i_MaxAirPressure)
        {
            o_CurrentAirPressure = 0;
            bool isValid = false;

            Console.WriteLine("Enter wheel manufacturer name. ");
            o_ManufacturerName = Console.ReadLine();
            Console.WriteLine("Please enter current air pressure. ");

            while (!isValid)
            {
                string airPressure = Console.ReadLine();

                if (float.TryParse(airPressure, out float userAirPressure))
                {
                    if (userAirPressure > 0 && userAirPressure <= i_MaxAirPressure)
                    {
                        o_CurrentAirPressure = userAirPressure;
                        isValid = true;
                    }

                    else if (userAirPressure > i_MaxAirPressure)
                    {
                        Console.WriteLine("You are trying to fill more air pressure than maximum. Maximum is: {0}.", i_MaxAirPressure);
                    }

                    else
                    {
                        Console.WriteLine("Air pressure can't have values under 0.");
                    }
                }
                else
                {
                    Console.WriteLine("Could not read your air pressure. Please make sure you use only numbers.");
                }
            }

        }

        

        public static string[] getUniqueDataFromAnyVehicle(Vehicle i_Vehicle)
        {
            bool isValidInput = false;
            string[] uniqueData = new string[i_Vehicle.GetUniqueData.Length];
            
            while (!isValidInput)
            {
                for (int i = 0; i < i_Vehicle.GetUniqueData.Length; i++)
                {
                    Console.WriteLine("Enter {0}", i_Vehicle.GetUniqueData[i]);
                    string userInput = Console.ReadLine();
                    uniqueData[i] = userInput;
                }

                if (i_Vehicle.ValidateUniqueData(uniqueData) != -1)
                {
                    Console.WriteLine("Unique data was not entered properly. Please try again.");
                }

                else
                {
                    isValidInput = true;
                }
            }

            return uniqueData;
        }

        public static Car GetDetailsForFuelCar(string i_LicenseNumber, string o_ModelName)
        {
         //  GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "FuelCar");
            // unique data for car: color (red white green blue)
            // number of doors (2,3,4,5)
            // octan 95
            Car userCar = SystemVehiclesCreator.NewGenericFuelCar(o_ModelName, i_LicenseNumber);
            return userCar;
        }
        public static Car GetDetailsForElectricCar(string i_LicenseNumber, string o_ModelName)
        {
            //  GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "ElectricCar");
            Car userCar = SystemVehiclesCreator.NewGenericElectricCar(o_ModelName, i_LicenseNumber);
            //userCar.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount); // as a vehicle
            //string[] uniqueData = getUniqueDataFromAnyVehicle(userCar);
            //userCar.SetUniqueData(uniqueData);
            return userCar;
        }


     
        public static void GetDetailsForOtherVehicle(string i_LicenseNumber, Vehicle userVehicle)

        {
            float currentEnergyAmount, currentAirPressure;
            string manufacturerName;

            getEnergyDetailsForVehicle(out currentEnergyAmount);

          //  Vehicle userVehicle = SystemVehiclesCreator.NewGenericTypeOfVehicle(modelName, i_LicenseNumber);

            if(userVehicle.EnergySource.MaxEnergy < currentEnergyAmount)
            {
                throw new ValueOutOfRangeException();
            }

            getDetailsForGenericWheel(out manufacturerName, out currentAirPressure, userVehicle.Wheels[0].MaxAirPressure);
            userVehicle.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount);
            string[] uniqueData = getUniqueDataFromAnyVehicle(userVehicle);
            userVehicle.SetUniqueData(uniqueData);
        }

        public static Truck GetDetailsForTruck(string i_LicenseNumber, string o_ModelName)
        {
            Truck truckFromUser = SystemVehiclesCreator.NewGenericFuelTruck(o_ModelName, i_LicenseNumber);
            return truckFromUser;
        }

        public static string getModelName()
        {
            Console.WriteLine("Enter model name: ");
            string name = Console.ReadLine();

            return name;
        }

        public static void GetDetailsForGenericVehicle(
            out string o_ModelName,
            out string o_ManufacturerName,
            out float o_CurrentEnergyAmount,
            out float o_CurrentAirPressure, string i_vehicleType)
        {
            o_ModelName = getModelName();
            getEnergyDetailsForVehicle(out float currentEnergyAmount);
            checkValidEnergyAmount(i_vehicleType, currentEnergyAmount, out o_CurrentEnergyAmount);
            getDetailsForWheel(out o_ManufacturerName, out o_CurrentAirPressure, i_vehicleType);
        }

        //private static void getLicenseTypeForMotorcycle(out Motorcycle.eLicenseType o_LicenseType)
        //{
        //    bool validLicense = false;
        //    o_LicenseType = Motorcycle.eLicenseType.B1;
        //    Console.WriteLine("Please enter license type. ");
        //    while(!validLicense)
        //    {
        //        string userInput = Console.ReadLine();
        //        switch (userInput.ToLower())
        //        {
        //            case "a":
        //                {
        //                    o_LicenseType = Motorcycle.eLicenseType.A;
        //                    validLicense = true;
        //                    break;
        //                }
        //            case "a1":
        //                {
        //                    o_LicenseType = Motorcycle.eLicenseType.A1;
        //                    validLicense = true;
        //                    break;
        //                }
        //            case "b1":
        //                {
        //                    o_LicenseType = Motorcycle.eLicenseType.B1;
        //                    validLicense = true;
        //                    break;
        //                }
        //            case "bb":
        //                {
        //                    o_LicenseType = Motorcycle.eLicenseType.BB;
        //                    validLicense = true;
        //                    break;
        //                }
        //            default:
        //                {
        //                    Console.WriteLine("Please enter valid motorcycle license. A, A1, B1, BB. ");
        //                    break;
        //                }
        //        }
        //    }
        //}
    }
}


