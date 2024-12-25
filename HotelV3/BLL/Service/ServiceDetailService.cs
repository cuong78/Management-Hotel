using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public  class ServiceDetailService
    {
        private ServiceDetailRepo _repo = new();
        public List<BookedService> GetAll() { 
            
            return _repo.GetAll();  
        
        } 

        public List<DAL.Models.Service> GetAllService()
        {
            return _repo.GetAllService();

        }

        public void saveService(BookedService ser)
        {
            _repo.saveService(ser);
        }
        public List<BookedService> getServiceByRoomNumber(int roomNumber) {
            return _repo.getServiceByRoomNumber(roomNumber);
            }

        public void DeleteServiceCheckOut(int roomNumber)
        {
            _repo.DeleteServiceCheckOut(roomNumber);
        }

    }
}
