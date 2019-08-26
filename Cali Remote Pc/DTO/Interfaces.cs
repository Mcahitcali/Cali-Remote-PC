using Cali_Remote_Pc.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action = Cali_Remote_Pc.Entity.Action;

namespace Cali_Remote_Pc.DTO
{
    public interface IUserRepository
    {
    }

    public interface IClientRepository:IGenericRepository<Client>
    {

    }

    public interface IActionRepository:IGenericRepository<Action>
    { 
    }

    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(Guid Id);
        IEnumerable<T> GetAll(string userId);
        T GetActive();
        void SetActive(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid Id);
        void Save();
    }
}
