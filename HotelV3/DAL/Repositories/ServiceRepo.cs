using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServiceRepo
    {
        private Hotel3Context _context;

        public List<Service> GetAll()
        {
            _context = new();
            return _context.Services.ToList();
        }
    }
}
