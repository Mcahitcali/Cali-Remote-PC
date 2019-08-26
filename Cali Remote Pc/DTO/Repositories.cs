using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cali_Remote_Pc.Entity;
using Cali_Remote_Pc.Entity.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = Cali_Remote_Pc.Entity.Action;

namespace Cali_Remote_Pc.DTO
{


    public class UserRepository : IUserRepository
    {
        private CrpDBContext _crpDB;
        public UserRepository()
        {
        }

        public UserRepository(CrpDBContext crpDB, UserManager<User> userManager)
        {
            _crpDB = crpDB;
        }
        
    }
    
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public CrpDBContext _context;
        public GenericRepository(CrpDBContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            _context.Set<T>().Remove(entity);
        }

        public T GetActive()
        {
            return _context.Set<T>().Where(a => a.State == true).FirstOrDefault();
        }

        public void SetActive(Guid id)
        {

            var currnetEntity = GetById(id);
            currnetEntity.State = true;
        }

        public IEnumerable<T> GetAll(string userId)
        {
            return _context.Set<T>().Where(i => i.UserId == userId).AsEnumerable<T>();
        }

        public T GetById(Guid Id)
        {
            return _context.Set<T>().SingleOrDefault(s => s.Id == Id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }

    public class ActionRepository : GenericRepository<Action>, IActionRepository
    {
        public ActionRepository(CrpDBContext context) : base(context)
        {

        }
    }

    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(CrpDBContext context) : base(context)
        {

        }
        
    }
}
