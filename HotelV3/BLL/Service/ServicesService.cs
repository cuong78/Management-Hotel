using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ServicesService
    {
        private ServiceRepo _repo = new();

        public List<DAL.Models.Service> GetAll() => _repo.GetAll();
    }
}
