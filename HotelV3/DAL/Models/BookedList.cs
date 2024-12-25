using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class BookedList
{
    public int BookingId { get; set; }

    public int? RoomNumber { get; set; }

    public string? GuestName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public List<string> Services { get; set; } 

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public decimal? TotalBill { get; set; }
   
}
