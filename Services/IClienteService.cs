using System.Collections.Generic;
using ClientesCRUD.Models;

namespace ClientesCRUD.Services
{
    public interface IClienteService
    {
        Cliente GetById(int id);
        IList<Cliente> GetAll();
        void Save(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Cliente cliente);
    }
}
