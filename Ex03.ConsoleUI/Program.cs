using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Ex02.ConsoleUtils;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        private static Dictionary<int, Ticket> m_GarageTickets = new Dictionary<int, Ticket>();  // license plate, ticket.

        public static void Main()
        {
            Program runProgram = new Program();
            runProgram.RunGarage();
        }

        public void RunGarage()
        {
            while(true)
            {
                short menuOption = getMainMenuOption();
                runMenuOption(menuOption);
                Print.PressAnyKeyToReturnToMainMenu();
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }
        
        private static short getMainMenuOption()
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
            // Enter vehicle's license plate.
            // Search plate in current tickets list.
            // If exists, change ticket's enum to "In Fixings", and print that vehicle exists.
            // Else, make a new ticket and insert it to garage list.
            string licenseNumber = Scan.GetLicenseNumberFromUser();
            if(m_GarageTickets.ContainsKey(licenseNumber.GetHashCode()))
            {
                Console.WriteLine("Vehicle exists. Changing status to IN FIXINGS.");
                m_GarageTickets[licenseNumber.GetHashCode()].CurrentStatus = Ticket.eCurrentStatus.InFixings;
            }
            else
            {
                Print.PrintVehicleDoesntExist();
                string userInput = Scan.GetVehicleType();
                switch(userInput)
                {
                    case "1":
                        {
                            Motorcycle inputFuelBike = Scan.GetDetailsForFuelBike(licenseNumber);
                            Ticket fuelBikeTicket = Scan.GetDetailsForTicket(inputFuelBike);
                            m_GarageTickets.Add(fuelBikeTicket.Vehicle.LicenseNumber.GetHashCode(), fuelBikeTicket);
                            break;
                        }
                    case "2":
                        {
                           // getDetailsForElectricBike();
                            break;
                        }
                    case "3":
                        {
                           // getDetailsForFuelCar();
                            break;
                        }
                    case "4":
                        {
                            //getDetailsForElectricCar();
                            break;
                        }
                    case "5":
                        {
                            //getDetailsForTruck();
                            break;
                        }
                    case "6":
                        {
                            //getDetailsForOtherVehicle();
                            break;
                        }
                }
                // make a new ticket and insert it to garage list.
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
            if (Scan.getPlateAndCheckIfInGarageTickets(out string licenseNumber, m_GarageTickets))
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
            if(Scan.getPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                 inflateAllWheels(m_GarageTickets[licensePlate.GetHashCode()].Vehicle.Wheels);
            }
        }
        private void inflateAllWheels(Wheel[] i_WheelsArray)
        {
            foreach(Wheel wheel in i_WheelsArray)
            {
                wheel.InflateWheelToMaximum();
            }
        }

        private void showCarFullDetails()
        {
            if (Scan.getPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                Print.PrintVehicleStaticData(m_GarageTickets[licensePlate.GetHashCode()], licensePlate);
            }
        }

        private void chargeElectricVehicle()
        {
            if(Scan.getPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                if (m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource.GetType() == typeof(Electric))// check if given plate is electric. if yes, recharge. else, print error.
                {
                    Scan.GetDetailsForRecharge(out float refillAmount);
                    m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource.FillEnergy(refillAmount);
                }
                else
                {
                    Print.CarIsNotRunningOnElectricity();
                }
                
            }
        }

        private void refillVehicle()
        {
            if(Scan.getPlateAndCheckIfInGarageTickets(out string licensePlate, m_GarageTickets))
            {
                if(m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource.GetType()  == typeof(Fuel)) // Check if car is running on fuel
                {
                    Scan.GetDetailsForRefill(out float refillAmount, out Fuel.eFuelType fuelTypeFromUser);
                    Fuel newObj = m_GarageTickets[licensePlate.GetHashCode()].Vehicle.EnergySource as Fuel; // IM NOT SURE THAT THIS IS THE PERFECT IMPLEMENTATION !!!!
                    newObj.FillFuel(refillAmount, fuelTypeFromUser);
                }
                else
                {
                    Print.CarIsNotRunningOnFuel();
                }
            }
        }
    }
}
