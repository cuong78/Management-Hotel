using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class MemberService
    {
        private MemberRepository _repo = new();

        public List<Member> GetAllMembers() => _repo.GetAllMembers();
        public Member Login(string email, string password) => _repo.Login(email, password);
        public void AddMember(Member member) => _repo.AddMember(member);

        public void Update(Member member) => _repo.Update(member);

        public void Remove(Member mem) => _repo.Remove(mem);
        public List<Member> Search(string name, string role) => _repo.SearchNameAndRole(name, role);

        public List<Member> GetMembersByName(string name) => _repo.GetMembersByName(name);
        public bool IsEmailExist(string email)
        {
            return _repo.IsEmailExist(email);   
        }
        public Member GetMemberByEmail(string email)
        {
            return _repo.GetMemberByEmail(email);
        }
        public List<Member> GetMembersByPhone(string phone)
        {
            return _repo.GetMembersByPhone(phone);
        }
    }
}
