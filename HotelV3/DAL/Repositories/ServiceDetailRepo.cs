using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServiceDetailRepo
    {
        private Hotel3Context _context;

        public List<BookedService> GetAll()
        {
            _context = new();
            return _context.BookedServices.Include("NameServiceNavigation").Include("RoomNumberNavigation").ToList();
        }

        public List<Service> GetAllService()
        {
            _context = new();
            return _context.Services.ToList();
        }

        public void saveService(BookedService Ser)
        {
            _context = new();

            _context.BookedServices.Add(Ser);
            _context.SaveChanges();
        }
        public string GetGuestNameByRoomNumber(int roomNumber)
        {
            _context = new();
            BookedService booking = _context.BookedServices.FirstOrDefault(b => b.RoomNumber == roomNumber);
            string? cuong = booking.RoomNumber.ToString();
            return cuong;
        }

        public List<BookedService> getServiceByRoomNumber(int roomNumber)
        {
            return GetAll().FindAll(sc => sc.RoomNumber == roomNumber);

        }
        public void DeleteServiceCheckOut(int roomNumber)
        {
            _context = new();
            var services = _context.BookedServices.Where(sd => sd.RoomNumber == roomNumber).ToList();
            var room = _context.BookedServices.FirstOrDefault(b => b.RoomNumber == roomNumber);

            foreach (var service in services)
            {
                _context.BookedServices.Remove(service);

            }

            _context.SaveChanges();
        }

    }
}
