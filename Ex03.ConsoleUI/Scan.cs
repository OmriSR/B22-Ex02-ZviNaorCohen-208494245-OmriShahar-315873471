using System;
using System.Collections.Generic;
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
            while (!isInputValid)
            {
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    wantedStatus = Ticket.eCurrentStatus.InFixings;
                }
                else if (userInput == "2")
                {
                    wantedStatus = Ticket.eCurrentStatus.Fixed;
                }
                else if (userInput == "3")
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

        public static bool getPlateAndCheckIfInGarageTickets(out string o_UserInput, Dictionary<int, Ticket> GarageTickets)
        {
            string userInput;
            bool plateInTickets = false;
            Console.WriteLine("Please enter car license plate: ");
            userInput = Console.ReadLine();
            if (GarageTickets.ContainsKey(userInput.GetHashCode()))
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

        
    }
}
