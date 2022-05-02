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
        private eCurrentStatus m_CurrentStatus;

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public eCurrentStatus currentStatus
        {
            get
            {
                return m_CurrentStatus;
            }
            set
            {
                m_CurrentStatus = value;
            }
        }
}
}
