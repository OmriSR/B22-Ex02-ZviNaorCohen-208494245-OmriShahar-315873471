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

        public static bool getPlateAndCheckIfInGarageTickets(
            out string o_UserInput,
            Dictionary<int, Ticket> GarageTickets)
        {
            string userInput;
            bool plateInTickets = false;
            Console.WriteLine("Please enter car license plate: ");
            userInput = Console.ReadLine();
            if(GarageTickets.ContainsKey(userInput.GetHashCode()))
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

        public static void GetDetailsForRecharge(out float o_minutesToFill)
        {
            bool isValidInput = false;
            o_minutesToFill = 0;
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
                    isValidInput = true;
                    o_minutesToFill = refillAmount;
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

        public static Motorcycle GetDetailsForFuelBike(string i_UserLicensePlate)
        {
            // Need to make this function more "readabily".
            bool validEnergyAmount = false, validAirPressure = false;
            string modelName, manufacturerName;
            float currentEnergyAmount, currentAirPressure;
            getDetailsForVehicle(out modelName, out currentEnergyAmount);
            while(!validEnergyAmount)
            {
                if (currentEnergyAmount > 6.2)
                {
                    Console.WriteLine("Fuel bike don't have more than 6.2 liters. Please try again.");
                    getDetailsForVehicle(out modelName, out currentEnergyAmount);
                }
                else
                {
                    validEnergyAmount = true;
                }
            }
            getDetailsForWheel(out manufacturerName, out currentAirPressure);
            while (!validAirPressure)
            {
                if (currentAirPressure > 31)
                {
                    Console.WriteLine("Fuel bike don't have more than 31 air pressure. Please try again.");
                    getDetailsForWheel(out manufacturerName, out currentAirPressure);
                }
                else
                {
                    validAirPressure = true;
                }
            }
            Motorcycle userMotorcycle = SystemVehiclesCreator.NewGenericFuelMotorcycle(modelName, i_UserLicensePlate);
            userMotorcycle.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount); // as a vehicle
            Console.WriteLine("Enter license type. A, A1, B1, BB. ");
            string licenseType = Console.ReadLine();
            string[] uniqueData = new string[2];
            uniqueData[0] = licenseType;
            uniqueData[1] = "6.2";
            userMotorcycle.SetUniqueData(uniqueData);
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

        private static void getDetailsForVehicle(
            out string o_ModelName,
            out float o_CurrentEnergyAmount)
        {
            o_CurrentEnergyAmount = 0;
            bool isValid = false;
            Console.WriteLine("Enter vehicle model name.");
            o_ModelName = Console.ReadLine();
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

        private static void getDetailsForWheel(out string o_ManufacturerName, out float o_CurrentAirPressure)
        {
            o_CurrentAirPressure = 0;
            bool isValid = false;
            Console.WriteLine("Enter wheel manufacturer name. ");
            o_ManufacturerName = Console.ReadLine();
            Console.WriteLine("Please enter current air pressure. ");
            while(!isValid)
            {
                string airPressure = Console.ReadLine();
                if(float.TryParse(airPressure, out float userAirPressure))
                {
                    if(userAirPressure > 0)
                    {
                        o_CurrentAirPressure = userAirPressure;
                        isValid = true;
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


