using System.Collections.Generic;
using ClientesCRUD.App_Start;
using ClientesCRUD.Models;
using ClientesCRUD.Repositories;

namespace ClientesCRUD.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService()
        {
            _repository = new ClienteRepository();
        }

        public Cliente GetById(int id)
        {
            var cliente = RedisHelper.GetClienteCache(id);
            if (cliente != null) return cliente;

            cliente = _repository.GetById(id);
            if (cliente != null) RedisHelper.SetClienteCache(cliente);
            return cliente;
        }

        public IList<Cliente> GetAll()
        {
            return _repository.GetAll();
        }

        public Cliente GetByNome(string nome)
        {
            return _repository.GetByNome(nome);
        }

        public void Save(Cliente cliente)
        {
            _repository.Save(cliente);
            RedisHelper.SetClienteCache(cliente);
        }

        public void Update(Cliente cliente)
        {
            _repository.Update(cliente);
            RedisHelper.SetClienteCache(cliente);
        }

        public void Delete(Cliente cliente)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                // Garante que os telefones estejam carregados
                cliente = session.Get<Cliente>(cliente.Id);
                foreach (var tel in cliente.Telefones)
                {
                    tel.Cliente = cliente; // garante vínculo explícito
                }

                session.Delete(cliente);
                RedisHelper.RemoveClienteCache(cliente.Id);
                transaction.Commit();
            }
        }
    }
}
