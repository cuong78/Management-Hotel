using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MemberRepository

    {
        private Hotel3Context _context;

        public List<Member> GetAllMembers()
        {
            _context = new();
            return _context.Members.ToList().FindAll(m => m.Status == 1);
        }
        public Member Login(string email, string password)
        {
            _context = new();
            return GetAllMembers().FirstOrDefault(m => m.Email.ToLower().Equals(email.ToLower()) && m.Password.Equals(password));
        }
        public void AddMember(Member member)
        {
            _context = new();
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void Update(Member member)
        {
            _context = new();
            _context.Members.Update(member);
            _context.SaveChanges();
        }

        public void Remove(Member mem)
        {
            _context = new();
            _context.Members.Remove(mem);
            _context.SaveChanges();
        }



        public List<Member> SearchNameAndRole(string name, string role)
        {
            _context = new();
            return _context.Members.ToList().FindAll(x => x.Name.ToLower().Contains(name.ToLower()) && x.Role.ToLower().Contains(role.ToLower()));
        }
        public List<Member> GetMembersByName(string name) => GetAllMembers().FindAll(m => m.Name.ToLower().Contains(name.ToLower()));


        public bool IsEmailExist(string email)
        {
            _context = new();
            return _context.Members.Any(m => m.Email.ToLower() == email.ToLower());
        }

        public Member GetMemberByEmail(string email) {
            _context = new();
            return _context.Members.FirstOrDefault(c => c.Email == email);
        
        }
        public List<Member> GetMembersByPhone(string phone) => GetAllMembers().FindAll(m => m.Phone.Contains(phone));

    }
}
