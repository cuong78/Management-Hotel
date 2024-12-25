using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Room
{
    public int RoomNumber { get; set; }

    public string RoomStatus { get; set; } = null!;

    public virtual Booked? Booked { get; set; }
}
