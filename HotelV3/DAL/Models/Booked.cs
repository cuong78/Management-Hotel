using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Booked
{
    public int RoomNumber { get; set; }

    public string GuestName { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public string BookStatus { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<BookedService> BookedServices { get; set; } = new List<BookedService>();

    public virtual Room RoomNumberNavigation { get; set; } = null!;
}
