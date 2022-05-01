using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Ex02.ConsoleUtils;

namespace Ex03.ConsoleUI
{
    class Program
    {
        private List<GarageLogic.Ticket> m_GarageTickets; // MAYBE USE DICTIONARY FOR THETA(1) FINDINGS???

        static void Main()
        {
            RunGarage();
        }

        public static void RunGarage()
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

        private static void runMenuOption(short i_MenuOption)
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
                        break;
                    }
                case 4:
                    {
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
            }
        }

        private static void enterNewVehicle()
        {
            // Enter vehicle's license plate.
            // Search plate in current tickets list.
            // If exists, change ticket's enum to "In Fixings", and print that vehicle exists.
            // Else, make a new ticket and insert it to garage list.
            string licenseNumber = getLicenseNumberFromUser();
            if(doesLicenseNumberExists(licenseNumber))
            {
                // change ticket's enum to in fixings, and print that vehicle exists.
            }
            else
            {
                // make a new ticket and insert it to garage list.
            }
        }

        private static string getLicenseNumberFromUser()
        {
            string userLicenseNumber = "";
            Console.WriteLine("Please enter vehicle's license number. ");
            userLicenseNumber = Console.ReadLine();
            return userLicenseNumber;
        }

        private static bool doesLicenseNumberExists(string i_LicenseNumber)
        {
            bool exists = false;
            // CHECK IF LICENSENUMBER IS IN LIST. BUT INSTEAD OF LIST, MAYBE USE DICTIONARY????
            return exists;
        }
    }
}
