using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class BookService
    {
        private BookRepository _repo = new BookRepository();

        public List<Booked> GetAll()
        {
            return _repo.GetAll();
        }
        public void saveBooking(Booked book) { 
            _repo.saveBooking(book);
         }

        public void UpdateRoomStatus(int roomNumber, string status)
        {
            _repo.UpdateRoomStatus(roomNumber, status);
        }
        public Booked? GetBookedRoomNumbers(int roomNumber)
        {
            return _repo.GetAll().FirstOrDefault(b => b.RoomNumber == roomNumber);
        }
        public string GetGuestNameByRoomNumber(int roomNumber)
        {
            return _repo.GetGuestNameByRoomNumber(roomNumber);
        }
        public void RemoveBookingByRoomNumber(int roomNumber)
        {
            _repo.RemoveBookingByRoomNumber(roomNumber);
        }
    
      

    }
}
