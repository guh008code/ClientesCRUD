using System.Collections.Generic;
using ClientesCRUD.Models;

namespace ClientesCRUD.Repositories
{
    public interface IClienteRepository
    {
        Cliente GetById(int id);
        IList<Cliente> GetAll();
        Cliente GetByNome(string nome);
        void Save(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Cliente cliente);
    }
}
