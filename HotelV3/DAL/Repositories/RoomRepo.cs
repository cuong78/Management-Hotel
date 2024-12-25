using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoomRepo
    {
        private Hotel3Context _context;

        public List<Room> GetAll ()
        {
            _context = new Hotel3Context();
            return _context.Rooms.ToList ();
        }

        public bool IsRoomBooked(int roomNumber)
        {
            // Check if there is a booking for the specific room number
            return _context.Bookeds.Any(b => b.RoomNumber == roomNumber && b.BookStatus == "Booked");
        }

        public Booked GetBookingDetails(int roomNumber)
        {
            // Retrieve booking details for the specific room number
            return _context.Bookeds.FirstOrDefault(b => b.RoomNumber == roomNumber && b.BookStatus == "Booked");
        }
        public void CheckOut(int roomNumber)
        {
            _context = new();

            var booking = _context.Bookeds.FirstOrDefault(b => b.RoomNumber == roomNumber && b.BookStatus == "Booked");
            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (booking != null && room != null)
            {
                booking.BookStatus = "CheckedOut"; // Update booking status
                room.RoomStatus = "Available"; // Make room available
                _context.SaveChanges(); // Save changes to the database
            }
        }

    }
}
