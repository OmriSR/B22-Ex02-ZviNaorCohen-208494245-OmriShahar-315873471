using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Scan
    {
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

        public static bool GetPlateAndCheckIfInGarageTickets(
            out string o_UserInput,
            Dictionary<int, Ticket> m_GarageTickets)
        {
            bool plateInTickets = false;
            Console.WriteLine("Please enter car license plate: ");
            string userInput = Console.ReadLine();
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


        public static Motorcycle GetDetailsForElectricBike(string i_UserLicensePlate)
        {
            GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "ElectricBike");
            Motorcycle userMotorcycle = SystemVehiclesCreator.NewGenericElectricMotorcycle(modelName, i_UserLicensePlate);
            userMotorcycle.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount); // as a vehicle
            Console.WriteLine("Enter license type. A, A1, B1, BB. ");
            string licenseType = Console.ReadLine();
            string[] uniqueData = new string[2];
            uniqueData[0] = licenseType;
            uniqueData[1] = "2.5";
            userMotorcycle.SetUniqueData(uniqueData);
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
                    getDetailsForSpecificVehicle(out currentEnergyAmount);
                }
                else
                {
                    validEnergyAmount = true;
                }
            }
            o_NewValidEnergyAmount = currentEnergyAmount;
        }
        public static Motorcycle GetDetailsForFuelBike(string i_UserLicensePlate)
        {
            // Need to make this function more "readabily".
            GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "FuelBike");
            Motorcycle userMotorcycle = SystemVehiclesCreator.NewGenericFuelMotorcycle(modelName, i_UserLicensePlate);
            userMotorcycle.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount); // as a vehicle
            Console.WriteLine("Enter license type. A, A1, B1, BB. ");
            string licenseType = Console.ReadLine();
            string[] uniqueData = new string[2];
            uniqueData[0] = licenseType;
            uniqueData[1] = "6.2";
            if(userMotorcycle.ValidateUniqueData(uniqueData) != -1)
            {
                Console.WriteLine("License Type was not entered properly.");
                // licensetype or 6.2 were not entered properly.
            }
            else
            {
                userMotorcycle.SetUniqueData(uniqueData);
            }
            return userMotorcycle;
        }


        public static Ticket GetDetailsForTicket(Vehicle i_Vehicle)
        {
            Console.WriteLine("Please enter owner's name. ");
            string ownerName = Console.ReadLine();
            Console.WriteLine("Please enter owner phone number.");
            string ownerNumber = Console.ReadLine();
            Ticket ticketToReturn = new Ticket(i_Vehicle, ownerName, ownerNumber);
            return ticketToReturn;
        }

        private static void getDetailsForSpecificVehicle(
            out float o_CurrentEnergyAmount)
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
            while(!isValid)
            {
                string airPressure = Console.ReadLine();
                if(float.TryParse(airPressure, out float userAirPressure))
                {
                    if(userAirPressure > 0 && userAirPressure <= maxAirPressure)
                    {
                        o_CurrentAirPressure = userAirPressure;
                        isValid = true;
                    }
                    else if(userAirPressure > maxAirPressure)
                    {
                        Console.WriteLine("You are trying to fill more air pressure than maximum. ");
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

        public static Car GetDetailsForFuelCar(string i_LicenseNumber)
        {
            GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "FuelCar");
            // unique data for car: color (red white green blue)
            // number of doors (2,3,4,5)
            // octan 95
            
            Car userCar = SystemVehiclesCreator.NewGenericFuelCar(modelName, i_LicenseNumber);
            userCar.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount); // as a vehicle
            Console.WriteLine("Enter number of doors. "); // move to another function and let user choose from 2,3,4,5.
            string numberOfDoorsInput = Console.ReadLine();
            short.TryParse(numberOfDoorsInput, out short numberOfDoors);
            string[] uniqueData = new string[2];
            Console.WriteLine("Enter color. ");
            string color = Console.ReadLine();
            uniqueData[0] = color;
            uniqueData[1] = numberOfDoorsInput;
            userCar.SetUniqueData(uniqueData);
            return userCar;
        }

        public static Car GetDetailsForElectricCar(string i_LicenseNumber)
        {
            GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "ElectricCar");
            Car userCar = SystemVehiclesCreator.NewGenericElectricCar(modelName, i_LicenseNumber);
            userCar.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount); // as a vehicle
            Console.WriteLine("Enter number of doors. "); // move to another function and let user choose from 2,3,4,5.
            string numberOfDoorsInput = Console.ReadLine();
            short.TryParse(numberOfDoorsInput, out short numberOfDoors);
            string[] uniqueData = new string[2];
            Console.WriteLine("Enter color. ");
            string color = Console.ReadLine();
            uniqueData[0] = color;
            uniqueData[1] = numberOfDoorsInput;
            userCar.SetUniqueData(uniqueData);
            return userCar;
        }

        public static Truck GetDetailsForTruck(string i_LicenseNumber)
        {
            GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, "Truck");
            Console.WriteLine("Do you contain refrigerated content? 1. Yes. 2. No.");
            string userInput = Console.ReadLine();
            bool refrigeratedContent = userInput == "1" ? refrigeratedContent = true : refrigeratedContent = false;
            Console.WriteLine("What is your cargo capacity?");
            string capacityInput = Console.ReadLine();
            float.TryParse(capacityInput, out float capacityFromUser);
            Truck truckFromUser = SystemVehiclesCreator.NewGenericFuelTruck(modelName, i_LicenseNumber);
            truckFromUser.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount);
            string[] uniqueData = new string[2];
            uniqueData[0] = refrigeratedContent.ToString();
            uniqueData[1] = capacityFromUser.ToString();
            truckFromUser.SetUniqueData(uniqueData);
            return truckFromUser;
        }

        private static string getModelName()
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
            getDetailsForSpecificVehicle(out float currentEnergyAmount);
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


