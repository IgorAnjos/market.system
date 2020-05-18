using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;
using mktSystem.Models;

namespace mktSystem.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ApplicationDbContext database;
        public FornecedorController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }
        
        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorDTO)
        {
            if(ModelState.IsValid)
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorDTO.Nome;
                fornecedor.Email = fornecedorDTO.Email;
                fornecedor.Telefone = fornecedorDTO.Telefone;
                fornecedor.Status = true;

                database.Fornecedores.Add(fornecedor);
                database.SaveChanges();

                return RedirectToAction("Fornecedores","Gestao");
            }
            else
            {
                return View("../Gestao/EditarFornecedor");
            }
        }
        
        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorDTO)
        {
            if(ModelState.IsValid)
            {
                var fornecedor = database.Fornecedores.First(_=>_.Id == fornecedorDTO.Id);
                fornecedor.Nome = fornecedorDTO.Nome;
                fornecedor.Email = fornecedorDTO.Email;
                fornecedor.Telefone = fornecedorDTO.Telefone;

                database.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else
            {
                return View("../Gestao/EditarFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if(id > 0)
            {
                var fornecedor = database.Fornecedores.First(_ => _.Id == id);
                fornecedor.Status = false;

                database.SaveChanges();
                return RedirectToAction("Fornecedores","Gestao");
            }
            else
            {
                return View("../Gestao/Fornecedores");
            }
        }
    }
}