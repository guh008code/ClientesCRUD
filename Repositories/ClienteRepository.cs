using System.Collections.Generic;
using System.Linq;
using ClientesCRUD.App_Start;
using ClientesCRUD.Models;
using NHibernate;

namespace ClientesCRUD.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public Cliente GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<Cliente>(id);
            }
        }

        public IList<Cliente> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<Cliente>().ToList();
            }
        }

        public Cliente GetByNome(string nome)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<Cliente>().FirstOrDefault(c => c.Nome == nome);
            }
        }

        public bool TelefoneExiste(string numero)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<Telefone>().Any(t => t.Numero == numero);
            }
        }

        public void Save(Cliente cliente)
        {
            //Garante que todos os telefones tenham a referência do cliente
            if (cliente.Telefones != null)
            {
                // Filtra apenas telefones com número válido
                cliente.Telefones = cliente.Telefones
                    .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                    .ToList();

                // Agora garante a referência cliente
                foreach (var telefone in cliente.Telefones)
                {
                    telefone.Cliente = cliente;
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(cliente);
                    transaction.Commit();
                }
            }
        }

        public void Update(Cliente cliente)
        {

            // Garante a integridade dos telefones
            //Garante que todos os telefones tenham a referência do cliente
            if (cliente.Telefones != null)
            {
                // Filtra apenas telefones com número válido
                cliente.Telefones = cliente.Telefones
                    .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                    .ToList();

                // Agora garante a referência cliente
                foreach (var telefone in cliente.Telefones)
                {
                    telefone.Cliente = cliente;
                }
            }

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(cliente);
                    transaction.Commit();
                }
            }
        }

        public void Delete(Cliente cliente)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(cliente);
                    transaction.Commit();
                }
            }
        }
    }
}
