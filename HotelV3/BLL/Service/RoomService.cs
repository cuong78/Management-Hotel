using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class RoomService
    {
        private RoomRepo _repo = new RoomRepo();

        public List<Room> GetAll()
        {
            return _repo.GetAll();
        }
        public bool IsRoomBooked(int roomNumber)
        {
            return _repo.IsRoomBooked(roomNumber);
        }

        public Booked GetBookingDetails(int roomNumber)
        {
            return _repo.GetBookingDetails(roomNumber);
        }
        public void CheckOut(int roomNumber) { 
            _repo.CheckOut(roomNumber);       
        }
    }
}
