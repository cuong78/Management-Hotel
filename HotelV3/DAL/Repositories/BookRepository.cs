
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookRepository
    {
        private Hotel3Context _context = new();

        //     string status = "Booked";
        public List<Booked> GetAll()
        {
            _context = new();
            return _context.Bookeds.Include("RoomNumberNavigation").Include("BookedServices").ToList();
        }

        public void saveBooking(Booked booked)
        {
            _context = new();

            _context.Bookeds.Add(booked);
            _context.SaveChanges();
        }
        public void UpdateRoomStatus(int roomNumber, string status)
        {
            _context = new();
            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                room.RoomStatus = status;
                _context.SaveChanges();
            }
        }

        public string GetGuestNameByRoomNumber(int roomNumber)
        {
            _context = new();
            Booked booking = _context.Bookeds.FirstOrDefault(b => b.RoomNumber == roomNumber);
            string? cuong = booking.GuestName;
            return cuong;
        }


        public void RemoveBookingByRoomNumber(int roomNumber)
        {
            _context = new();
            Booked booking = _context.Bookeds.ToList().FirstOrDefault(b => b.RoomNumber == roomNumber);
            Room room = _context.Rooms.ToList().FirstOrDefault(r => r.RoomNumber == roomNumber);

            room.RoomStatus = "Available";
            _context.Bookeds.Remove(booking);

            _context.SaveChanges();
        }

        public List<string> getListServicesByRoomNumber (int roomNumber)
        {
            _context = new();

            // Get the booking for the specified room number
            var booking = _context.Bookeds
                .Include(b => b.BookedServices) // Include the related services
                .FirstOrDefault(b => b.RoomNumber == roomNumber);

            // If booking is found, select the NameService from the BookedServices
            if (booking != null)
            {
                return booking.BookedServices
                    .Select(bs => bs.NameService)
                    .ToList();
            }

            // Return an empty list if no booking is found
            return new List<string>();
        } 

    }

}
