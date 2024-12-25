using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BookListRepo
    {
        private Hotel3Context _context; 

        public  List<BookedList> GetAll()
        {
            _context = new(); 
            return _context.BookedLists.ToList();   
        }

        public void saveBookList(BookedList bookedList) {
            _context = new Hotel3Context(); 
            _context.BookedLists.Add(bookedList);
            _context.SaveChanges();
        }
        public Member GetMemberByEmail(string email)
        {
            _context = new();
            return _context.Members.FirstOrDefault(c => c.Email == email);

        }
        public List<BookedList> GetPhone(string phone) => GetAll().FindAll(m => m.PhoneNumber.Contains(phone));
        public List<BookedList> GetName(string name) => GetAll().FindAll(m => m.GuestName.ToLower().Contains(name.ToLower()));


    }
}
