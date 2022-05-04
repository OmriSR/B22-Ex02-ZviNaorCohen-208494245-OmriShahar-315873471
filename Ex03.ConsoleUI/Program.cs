﻿using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Ex02.ConsoleUtils;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        private Dictionary<string, Ticket> m_GarageTickets = new Dictionary<string, Ticket>();  // license plate, ticket.

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
                pressAnyKeyToReturnToMainMenu();
                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        private static void pressAnyKeyToReturnToMainMenu()
        {
            Console.WriteLine("Press any key to return to main menu. ");
            Console.ReadLine();
        }

        private static short getMainMenuOption()
        {
            bool validUserInput = true;
            string userInput = "";
            short menuOption = 0;

            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                userInput = printMainMenu();
                validUserInput = checkUserInput(userInput);
                if (!validUserInput)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Please enter VALID input!");
                    pressAnyKeyToReturnToMainMenu();
                }
            } while (!validUserInput);
            menuOption = Convert.ToInt16((userInput));
            

            return menuOption;
        }

        private static string printMainMenu()
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
            string userInput = Console.ReadLine();
            return userInput;
        }

        private static bool checkUserInput(string i_UserInput)
        {
            bool inputIsValid = true;
            inputIsValid = (i_UserInput == "1" || i_UserInput == "2" || i_UserInput == "3" || i_UserInput == "4"
                            || i_UserInput == "5" || i_UserInput == "6" || i_UserInput == "7");
            return inputIsValid;
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
                        pressAnyKeyToReturnToMainMenu();
                        break;
                    }
            }
        }

        private void enterNewVehicle()
        {
            // Enter vehicle's license plate.
            // Search plate in current tickets list.
            // If exists, change ticket's enum to "In Fixings", and print that vehicle exists.
            // Else, make a new ticket and insert it to garage list.
            string licenseNumber = getLicenseNumberFromUser();
            if(m_GarageTickets.ContainsKey(licenseNumber))
            {
                Console.WriteLine("Vehicle exists. Changing status to IN FIXINGS.");
                m_GarageTickets[licenseNumber].CurrentStatus = Ticket.eCurrentStatus.InFixings;
            }
            else
            {
                // make a new ticket and insert it to garage list.
                // But we need to check with Guy what info a vehicle has (Ravid Yael posted on facebook).
            }
        }

        private static string getLicenseNumberFromUser()
        {
            string userLicenseNumber = "";
            Console.WriteLine("Please enter vehicle's license number. ");
            userLicenseNumber = Console.ReadLine();
            return userLicenseNumber;
        }

        private void showLicensePlates()
        {
            bool validInput = false;
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Show -ALL- license plates in the garage.");
            Console.WriteLine("2. Show all license plates depends on its current status.");
            while(!validInput)
            {
                string userInput = "";
                userInput = Console.ReadLine();
                if(userInput == "1")
                {
                    showAllLicensePlates();
                    validInput = true;
                }
                else if(userInput == "2")
                {
                    showSpecificLicensePlates();
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again.");
                }
            }
        }

        private void showAllLicensePlates()   // ticket does not contains license plate --- maybe it should
        {
            foreach (KeyValuePair<string, Ticket> ticket in m_GarageTickets)
            {
                string output = String.Format("Vehicle plate: {0}. Current status: {1}", ticket.Key, ticket.Value.CurrentStatus);
                Console.WriteLine(output);
            }
            pressAnyKeyToReturnToMainMenu();
        }

        private void showSpecificLicensePlates()
        {
            bool validInput = false;
            Console.WriteLine("Please choose which status to see: ");
            Console.WriteLine("1. In fixings. ");
            Console.WriteLine("2. Fixed ");
            Console.WriteLine("3. Paid ");
            while(!validInput)
            {
                string userInput = Console.ReadLine();
                if(userInput == "1" || userInput == "2" || userInput == "3")
                {
                    printSpecificLicensePlateStatus(userInput);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again. ");
                    continue;
                }

                validInput = true;
            }
            pressAnyKeyToReturnToMainMenu();
        }

        private void printSpecificLicensePlateStatus(string i_UserInput)
        {
            Ticket.eCurrentStatus statusFromUser = Ticket.eCurrentStatus.Fixed;
            int counter = 0;
            switch(i_UserInput)
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

            foreach (KeyValuePair<string, Ticket> ticket in m_GarageTickets)
            {
                if(ticket.Value.CurrentStatus == statusFromUser)
                {
                    string output = String.Format("Vehicle plate: {0}. Current status: {1}", ticket.Key, ticket.Value.CurrentStatus);
                    Console.WriteLine(output);
                    counter++;
                }
            }

            if(counter == 0)
            {
                Console.WriteLine("No data for vehicles in the status you chose. ");
            }
            pressAnyKeyToReturnToMainMenu();
        }

        private void changeStatus()
        {
            string licenseNumber = "";
            Ticket.eCurrentStatus wantedStatus = Ticket.eCurrentStatus.InFixings;
            if (getPlateAndCheckIfInGarageTickets(out licenseNumber))
            {
                wantedStatus = getStatusFromUser();
                m_GarageTickets[licenseNumber].CurrentStatus = wantedStatus;
            }
        }

        private Ticket.eCurrentStatus getStatusFromUser()
        {
            bool isInputValid = false;
            Ticket.eCurrentStatus wantedStatus = Ticket.eCurrentStatus.InFixings;
            Console.WriteLine("Please choose to which status you want to change: ");
            Console.WriteLine("1. In Fixings. ");
            Console.WriteLine("2. Fixed. ");
            Console.WriteLine("3. Paid. ");
            while(!isInputValid)
            {
                string userInput = Console.ReadLine();
                if (userInput == "1")
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

        void inflateWheels()
        {
            string licensePlate;
            if(getPlateAndCheckIfInGarageTickets(out licensePlate))
            {
                 m_GarageTickets[licensePlate].Vehicle.InflateAllWheels();
            }
        }

        void showCarFullDetails()
        {
            string licensePlate;
            if (getPlateAndCheckIfInGarageTickets(out licensePlate))
            {
                // print full details
            }
        }

        void chargeElectricVehicle()
        {
            string licensePlate;
            if(getPlateAndCheckIfInGarageTickets(out licensePlate))
            {
                // check if given plate is electric. if yes, recharge. else, print error.
            }
        }

        void refillVehicle()
        {
            string licensePlate;
            if(getPlateAndCheckIfInGarageTickets(out licensePlate))
            {
                // check if given plate is running on fuel. If yes, recharge. Else, print error.
            }
        }

        bool getPlateAndCheckIfInGarageTickets(out string o_UserInput)
        {
            string userInput;
            bool plateInTickets = false;
            Console.WriteLine("Please enter car license plate: ");
            userInput = Console.ReadLine();
            if (m_GarageTickets.ContainsKey(userInput))
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
    }
}
