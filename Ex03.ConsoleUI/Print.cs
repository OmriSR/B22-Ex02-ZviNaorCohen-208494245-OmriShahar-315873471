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
            PressAnyKeyToReturnToMainMenu();
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
            foreach (KeyValuePair<int, Ticket> ticket in i_GarageTickets)
            {
                string output = String.Format("Vehicle plate: {0}. Current status: {1}", ticket.Value.Vehicle.LicenseNumber, ticket.Value.CurrentStatus);
                Console.WriteLine(output);
            }
            Print.PressAnyKeyToReturnToMainMenu();
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
            PressAnyKeyToReturnToMainMenu();
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
        }
    }
}
