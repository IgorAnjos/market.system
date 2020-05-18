using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;
using mktSystem.Models;

namespace mktSystemeb.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext database;
        public ProdutoController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }
        
        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoDTO)
        {
            if(ModelState.IsValid)
            {
                Produto produto = new Produto();

                produto.Nome = produtoDTO.Nome;
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoDTO.CategoriaID);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoDTO.FornecedorID);
                produto.PrecoCusto = produtoDTO.PrecoCusto; 
                produto.PrecoVenda = produtoDTO.PrecoVenda;
                produto.Medicao = produtoDTO.Medicao;
                produto.Status = true;

                database.Produtos.Add(produto);
                database.SaveChanges();

                return RedirectToAction("Produtos","Gestao");
            }
            else
            {
                ViewBag.Categorias = database.Categorias.ToList();
                ViewBag.Fornecedores = database.Fornecedores.ToList();
                return View("../Gestao/NovoProduto");
            }
        }
    }
}