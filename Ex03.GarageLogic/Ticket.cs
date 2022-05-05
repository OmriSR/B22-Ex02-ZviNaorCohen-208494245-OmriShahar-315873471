using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Ticket
    {
        public enum eCurrentStatus { InFixings, Fixed, Paid }

        Vehicle m_Vehicle;
        private string m_OwnersName;
        private string m_OwnersPhoneNumber;
        private eCurrentStatus m_CurrentStatus;

        public Ticket(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnersName = i_OwnerName;
            m_OwnersPhoneNumber = i_OwnerNumber;
            m_CurrentStatus = eCurrentStatus.InFixings;
        }

        public override int GetHashCode()
        {
            return m_OwnersPhoneNumber.GetHashCode();
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnersName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnersPhoneNumber;
            }
            set
            {
                m_OwnersPhoneNumber = value;
            }
        }   // not sure if needed --- check in the end

        public eCurrentStatus CurrentStatus
        {
            get;
            set;
        }
}
}
