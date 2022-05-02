using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ex02.ConsoleUtils;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        private Dictionary<string, Ticket> m_GarageTickets = new Dictionary<string, Ticket>(); // MAYBE USE DICTIONARY FOR THETA(1) FINDINGS???

        public static void Main()
        {
            Program runProg = new Program();
            runProg.RunGarage();
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
                        break;
                    }
                case 6:
                    {
                        break;
                    }
                case 7:
                    {
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
            if(m_GarageTickets.ContainsKey(licenseNumber) && m_GarageTickets[licenseNumber].currentStatus != Ticket.eCurrentStatus.Paid)
            {
                // change ticket's enum to in fixings, and print that vehicle exists.
                Console.WriteLine("Vehicle exists. Changing status to IN FIXINGS.");
                m_GarageTickets[licenseNumber].currentStatus = Ticket.eCurrentStatus.InFixings;
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

        private void changeStatus()
        {
            string licenseNumber = "", userInput = "";
            Ticket.eCurrentStatus wantedStatus = Ticket.eCurrentStatus.InFixings;
            Console.WriteLine("Enter License Number: ");
            licenseNumber = Console.ReadLine();
            if(m_GarageTickets.ContainsKey(licenseNumber))
            {
                wantedStatus = getStatusFromUser();
            }
            else
            {
                Console.WriteLine("Vehicle not found. ");
            }
        }

        private Ticket.eCurrentStatus getStatusFromUser()
        {
            string userInput = "";
            bool isInputValid = false;
            Ticket.eCurrentStatus wantedStatus = Ticket.eCurrentStatus.InFixings;
            Console.WriteLine("Please choose to which status you want to change: ");
            Console.WriteLine("1. In Fixings. ");
            Console.WriteLine("2. Fixed. ");
            Console.WriteLine("3. Paid. ");
            userInput = Console.ReadLine();
            while(!isInputValid)
            {
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
                }
            }

            return wantedStatus;
        }

        void inflateWheels()
        {
            string userInput = "";
            Console.WriteLine("Please enter license plate: ");
            userInput = Console.ReadLine();
            if(m_GarageTickets.ContainsKey(userInput))
            {
              //  m_GarageTickets[userInput].Vehicle.inflateWheels();
            }
            else
            {
                Console.WriteLine("License plate not found. ");
            }
        }
    }
}
