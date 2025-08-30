using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public enum ShipmentStatus
    {
        New = 1,
        InRoute = 2,
        ReturnToOffice = 3,
        Completed = 4,
        Cancelled = 5
    }
    public enum HistoryStatus
    {
        AddShipment = 1,
        UpdateShipmentStatus = 2,
    }

    public enum RoomType
    {
        LABORATOR = 1,
        LEKSION = 2,
        SEMINAR = 3
    }

    public enum ScheduleType
    {
        Leksion = 1,
        SEMINAR = 2,
        LAborator = 3
    }

    public enum EntityStatus
    {
        Active = 1,
        Inactive = 2,
        Disabled = 3
    }
}
