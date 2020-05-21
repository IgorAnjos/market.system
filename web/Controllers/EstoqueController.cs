using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;
using mktSystem.Models;

namespace web.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;
        public EstoqueController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque _estoque)
        {
            if(ModelState.IsValid)
            {
                database.Estoques.Add(_estoque);
                database.SaveChanges();
                return RedirectToAction("Estoque","Gestao");
            }
            else
            {
                ViewBag.Estoque = database.Produtos.Where(_ => _.Status == true).ToList();
                return RedirectToAction("NovoEstoque","Gestao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(Estoque _estoque)
        {
            if(ModelState.IsValid)
            {
                Estoque estoque = new Estoque();

                estoque.Produto = _estoque.Produto;
                estoque.Quantidade = _estoque.Quantidade;

                database.SaveChanges();
                return RedirectToAction("Estoque","Gestao");
            }
            else
            {
                ViewBag.Categorias = database.Categorias.ToList();
                ViewBag.Fornecedores = database.Fornecedores.ToList();
                return RedirectToAction("EditarEstoque","Gestao");
                // outra forma return View("../Gestao/EditarEstoque");
            }
        }
    }
}