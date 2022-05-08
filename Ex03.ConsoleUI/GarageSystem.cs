using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Ex02.ConsoleUtils;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageSystem
    {
        private static Dictionary<int, Ticket> m_GarageTickets = new Dictionary<int, Ticket>();  // license plate, ticket.

        public void RunGarage()
        {
            bool runGarage = true;
            while(runGarage)
            {
                short menuOption = getMainMenuOption();
                // if menuOption == 7 then exit?
                runMenuOption(menuOption);
                Print.PressAnyKeyToReturnToMainMenu();
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }
        
        private static short getMainMenuOption()   // scan class
        {
            bool validUserInput = true;
            string userInput = "";
            short menuOption = 0;

            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                userInput = runMainMenu();
                validUserInput = Scan.CheckUserInput(userInput);

                if (!validUserInput)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Please enter VALID input!");
                    Print.PressAnyKeyToReturnToMainMenu();
                }

            } while (!validUserInput);

            menuOption = Convert.ToInt16((userInput));

            return menuOption;
        }

        private static string runMainMenu()
        {
            Print.PrintMainMenu();
            string userInput = Console.ReadLine();

            return userInput;
        }

        private void runMenuOption(short i_MenuOption)
        {
            switch(i_MenuOption)
            {
                case 1:
                    {
                        enterNewVehicle();
                        break;
                    }

                case 2:
                    {
                        showLicensePlates();
                        break;
                    }

                case 3:
                    {
                        changeStatus();
                        break;
                    }

                case 4:
                    {
                        inflateWheels();
                        break;
                    }

                case 5:
                    {
                        // refill vehicle that is using fuel
                        refillVehicle();
                        break;
                    }

                case 6:
                    {
                        // charge vehicle that use electricity.
                        chargeElectricVehicle();
                        break;
                    }

                case 7:
                    {
                        // show car full details
                        showCarFullDetails();
                        break;
                    }

                default:
                    {
                        Print.PressAnyKeyToReturnToMainMenu();
                        break;
                    }
            }
        }

        private static void enterNewVehicle()
        {
            string licenseNumber = Scan.GetLicenseNumberFromUser();

            if(m_GarageTickets.ContainsKey(licenseNumber.GetHashCode()))
            {
                Console.WriteLine("Vehicle exists. Changing status to IN FIXINGS.");
                m_GarageTickets[licenseNumber.GetHashCode()].CurrentStatus = Ticket.eCurrentStatus.InFixings;
            }

            else
            {
                Print.PrintVehicleDoesntExist();
                //  string userInput = Scan.GetVehicleType();

                // SystemVehiclesCreator.eVehicleType userInputVehicleType = GetUserInputVehicleType(); // Here I check if the type is supported by our garage.
                // If input is not valid, return to main menu and don't enter switches.
                // else, input is valid:
                //Scan.GetDetailsForGenericVehicle(out string modelName, out string manufacturerName, out float currentEnergyAmount, out float currentAirPressure, userInput.ToLower());



                Console.WriteLine("Enter type of supported vehicle (for now: Motorcycle / Car / ...");
                string userInput = "fuelcar";

                // relevant to new implementation:
                // 1. Check that the vehicle is supported.
                // 2. If it is, convert it ToLower
                // 3. Make a generic vehicle with generic values.
                // 4. Make the specific type of vehicle the user requested.
                // 5. Get the rest of the data
                // 6. Make ticket and send to garage tickets.


                string modelName = Scan.getModelName();
                Vehicle chassis = SystemVehiclesCreator. NewGenericTypeOfVehicle(modelName, licenseNumber);
                
                try
                {
                    switch (userInput)
                    {
                        case "fuelbike":
                            {
                                chassis = Scan.GetDetailsForFuelBike(licenseNumber, modelName);
                                break;
                            }

                        case "electricbike":
                            {
                                chassis = Scan.GetDetailsForElectricBike(licenseNumber, modelName);
                                break;
                            }

                        case "fuelcar":
                            {
                                chassis = Scan.GetDetailsForFuelCar(licenseNumber, modelName);
                                break;
                            }

                        case "electriccar":
                            {
                                chassis = Scan.GetDetailsForElectricCar(licenseNumber, modelName);
                                break;
                            }

                        case "truck":
                            {
                                chassis = Scan.GetDetailsForTruck(licenseNumber, modelName);
                                break;
                            }

                        //case "other":
                        //    {
                        //        Vehicle inputNewType = Scan.GetDetailsForOtherVehicle(licenseNumber);
                        //        Ticket newTypeTicket = Scan.GetDetailsForTicket(inputNewType);
                        //        m_GarageTickets.Add(newTypeTicket.Vehicle.LicenseNumber.GetHashCode(), newTypeTicket);
                        //        break;
                        //    }

                        //default:
                        //    {
                        //        Console.WriteLine("Invalid input. Returning to main menu.");
                        //        break;
                        //    }
                    }

                    Scan.GetDetailsForOtherVehicle(licenseNumber, chassis);
                    Ticket shildaTicket = Scan.GetDetailsForTicket(chassis);
                    m_GarageTickets.Add(shildaTicket.Vehicle.LicenseNumber.GetHashCode(), shildaTicket);
                }

                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("You filled owner number with letters, which is impossible. Please try again. ");
                }

                catch(ValueOutOfRangeException)
                {
                    Console.WriteLine("You tried to initialize higher value than maximum.");
                }

            }
        }

        private void showLicensePlates()
        {
            bool validInput = false;

            Print.ShowLicensePlatesPrint();

            while(!validInput)
            {
                string userInput = "";
                userInput = Console.ReadLine();

                if(userInput == "1")
                {
                    Print.ShowAllLicensePlates(m_GarageTickets);
                    validInput = true;
                }

                else if(userInput == "2")
                {
                    Print.ShowSpecificLicensePlates(m_GarageTickets);
                    validInput = true;
                }

                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }
        }

        private void changeStatus()
        {
            Ticket.eCurrentStatus wantedStatus = Ticket.eCurrentStatus.InFixings;

            if (Scan.GetPlateAndCheckIfInGarageTickets(out string licenseNumber, m_GarageTickets))
            {
                wantedStatus = getStatusFromUser();
                m_GarageTickets[licenseNumber.GetHashCode()].CurrentStatus = wantedStatus;
            }
        }

        private Ticket.eCurrentStatus getStatusFromUser()
        {
            Print.GetStatusFromUserPrint();

            return Scan.GetStatusFromUserScan();
        }

        private void inflateWheels()
        {
            if(Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                bool fillToMax = Scan.GetTypeOfWheelInflation();

                inflateAllWheels(m_GarageTickets[licensePlate.GetHashCode()].Vehicle.Wheels, fillToMax);
            }
        }

        private void inflateAllWheels(Wheel[] i_WheelsArray, bool i_FillToMax)
        {
            if (i_FillToMax)
            {
                foreach (Wheel wheel in i_WheelsArray)
                {
                    wheel.InflateWheelToMaximum();
                }
            }

            else
            {
                float amountToFill = Scan.GetAirPressureToFill();

                if (i_WheelsArray[0].MaxAirPressure < amountToFill)
                {
                    Console.WriteLine(string.Format("The air pressure given is over the maximum according to {0}'s instructions", i_WheelsArray[0].Manufacturer));
                }

                else
                {
                    try
                    {
                        foreach(Wheel wheel in i_WheelsArray)
                        {
                            wheel.InflateWheel(amountToFill); // need to try and catch exeption inside the method
                        }
                    }

                    catch(ValueOutOfRangeException)
                    {
                        Console.WriteLine("You tried to fill more air than maximum. Please try again. ");
                    }
                }
            }
        }

        private void showCarFullDetails()
        {
            if (Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                Print.PrintVehicleStaticData(m_GarageTickets[licensePlate.GetHashCode()], licensePlate);
            }
        }

        private void chargeElectricVehicle()
        {
            if(Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                if (m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource.GetType() == typeof(Electric))// check if given plate is electric. if yes, recharge. else, print error.
                {
                    Scan.GetDetailsForRecharge(out float refillAmount);
                    try
                    {
                        m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource.FillEnergy(refillAmount);
                    }

                    catch(ValueOutOfRangeException)
                    {
                        Console.WriteLine("You tried to charge above maximum, which is impossible. Please try again. ");
                    }
                }
                else
                {
                    Print.CarIsNotRunningOnElectricity();
                }
            }
        }

        private void refillVehicle()
        {
            if(Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                if(m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource.GetType() == typeof(Fuel)) // Check if car is running on fuel
                {
                    try
                    {
                        Scan.GetDetailsForRefill(out float refillAmount, out Fuel.eFuelType fuelTypeFromUser);
                        Fuel newObj =
                            m_GarageTickets[licensePlate.GetHashCode()].Vehicle
                                .EnergySource as Fuel; // IM NOT SURE THAT THIS IS THE PERFECT IMPLEMENTATION !!!!
                        newObj.FillFuel(refillAmount, fuelTypeFromUser);
                    }

                    catch(ArgumentException)
                    {
                        Console.WriteLine("You tried to fill different fuel than vehicle's fuel. Please try again. ");
                    }

                    catch(ValueOutOfRangeException)
                    {
                        Console.WriteLine("You tried to fill more fuel than maximum. Please try again.");
                    }
                }
                else
                {
                    Print.CarIsNotRunningOnFuel();
                }
            }
        }
    }
}
