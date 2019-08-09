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

    //public class ClientRepository : IClientRepository
    //{
    //    private CrpDBContext _crpDB;

    //    public ClientRepository(CrpDBContext crpDB)
    //    {
    //        _crpDB = crpDB;
    //    }

    //    public void Add(Client entity)
    //    {
    //        _crpDB.ClientDB.Add(entity);
    //    }

    //    public void Delete(int id)
    //    {
    //        Client client = _crpDB.ClientDB.Where(c => c.ClientId == id).FirstOrDefault();
    //        _crpDB.ClientDB.Remove(client);
    //    }

    //    public Client GetById(int id)
    //    {
    //        Client client = _crpDB.ClientDB.Where(c => c.ClientId == id).FirstOrDefault();
    //        return client;
    //    }

    //    public void SaveChanges()
    //    {
    //        _crpDB.SaveChanges();
    //    }

    //    public void Update(Client entity)
    //    {
    //        _crpDB.ClientDB.Update(entity);
    //    }
    //}

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

        public IEnumerable<T> GetAll(string userId)
        {
            return _context.Set<T>().Where(i => i.UserId == userId).AsEnumerable<T>();
        }

        public T GetById(Guid Id)
        {
            return _context.Set<T>().SingleOrDefault(s => s.Id == Id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
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
