using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class BookedService
{
    public int ServiceDetailId { get; set; }

    public string NameService { get; set; } = null!;

    public int RoomNumber { get; set; }

    public int Quantity { get; set; }

    public DateTime DateCreated { get; set; }

    public int TaskId { get; set; }

    public virtual Service NameServiceNavigation { get; set; } = null!;

    public virtual Booked RoomNumberNavigation { get; set; } = null!;
}
