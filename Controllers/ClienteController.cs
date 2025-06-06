using System.Web.Mvc;
using ClientesCRUD.Models;
using ClientesCRUD.Services;

namespace ClientesCRUD.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController()
        {
            _service = new ClienteService();
        }

        public ActionResult Index()
        {
            var clientes = _service.GetAll();
            return View(clientes);
        }

        public ActionResult Details(int id)
        {
            var cliente = _service.GetById(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            if(string.IsNullOrWhiteSpace(cliente.Nome))
            {
                ModelState.AddModelError("Nome", "O nome é obrigatório.");
                return View(cliente);
            }

            // Verifica se já existe cliente com o mesmo nome
            var clienteExistente = _service.GetByNome(cliente.Nome);

            if (clienteExistente != null)
            {
                ModelState.AddModelError("Nome", "Já existe um cliente com este nome.");
                return View(cliente);
            }

            _service.Save(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var cliente = _service.GetById(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                ModelState.AddModelError("Nome", "O nome é obrigatório.");
                return View(cliente);
            }

            if (!ModelState.IsValid) return View(cliente);
            _service.Update(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var cliente = _service.GetById(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var cliente = _service.GetById(id);
            if (cliente != null)
            {
                _service.Delete(cliente);
            }
            return RedirectToAction("Index");
        }
    }
}
