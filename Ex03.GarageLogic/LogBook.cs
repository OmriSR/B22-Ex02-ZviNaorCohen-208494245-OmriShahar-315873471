using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class LogBook
    {
        Dictionary<int, Ticket> m_CurrentVehiclesInGarageTickets;
        Dictionary<int, Vehicle> m_CurrentVehiclesInGarage;

        public void AddNewVehileToLogBook(Vehicle i_NewVehicle)   // need to check if not already in garage
        {
            try
            {
                Ticket newVehicleTicket = new Ticket(i_NewVehicle);
                m_CurrentVehiclesInGarage.Add(i_NewVehicle.GetHashCode(), i_NewVehicle);
                m_CurrentVehiclesInGarageTickets.Add(newVehicleTicket.GetHashCode(), newVehicleTicket);
            }
            catch(ArgumentException ae)
            {
                // key already exists
            }
            catch(ArgumentNullException ane)
            {
                //key is null
            }
        }

        public void ReleaseVehicleFromGarage(int i_VehicleHash, int i_TicketHash)  // need to check if vehicle in garage
        {
            m_CurrentVehiclesInGarage.Remove(i_VehicleHash);
            m_CurrentVehiclesInGarageTickets.Remove(i_TicketHash);
        }
    }
}
