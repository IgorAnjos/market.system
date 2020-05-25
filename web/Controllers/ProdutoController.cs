using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                ViewBag.Categorias = database.Categorias.Where(_ => _.Status == true).ToList();
                ViewBag.Fornecedores = database.Fornecedores.Where(_ => _.Status == true).ToList();
                return View("../Gestao/NovoProduto");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoDTO)
        {
            if(ModelState.IsValid)
            {
                var produto = database.Produtos.First(_ => _.Id == produtoDTO.Id);
                produto.Nome = produtoDTO.Nome;
                produto.Categoria = database.Categorias.First(_ => _.Id == produtoDTO.CategoriaID);
                produto.Fornecedor = database.Fornecedores.First(_ => _.Id == produtoDTO.FornecedorID);
                produto.PrecoCusto = produtoDTO.PrecoCusto;
                produto.PrecoVenda = produtoDTO.PrecoVenda;
                produto.Medicao = produtoDTO.Medicao;

                database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            }
            else
            {
                ViewBag.Categorias = database.Categorias.Where(_ => _.Status == true).ToList();
                ViewBag.Fornecedores = database.Fornecedores.Where(_ => _.Status == true).ToList();
                return View("../Gestao/NovoProduto");
            }
        }

        public IActionResult Deletar(int id)
        {
            if(id > 0)
            {
                var produto = database.Produtos.First(_ => _.Id == id);
                produto.Status = false;

                database.SaveChanges();
                return RedirectToAction("Produtos","Gestao");
            }else
            {
                return RedirectToAction("Produtos","Gestao");
            }
        }

        public IActionResult Produto(int id)
        {
            if(id > 0)
            {
                var produto = database.Produtos.Where(_ => _.Status == true).Include(p => p.Categoria).Include(c => c.Fornecedor).First(_ => _.Id == id);
                
                if(produto != null)
                {
                    var estoque = database.Estoques.First(e => e.Produto.Id == produto.Id);
                    if(estoque == null)
                    {
                        produto = null;
                        // return Json(estoque);
                    }
                }
                
                if(produto != null)
                {
                    Response.StatusCode = 200;
                    return Json(produto);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(null);
                }
                var promocao = database.Promocoes.First(p => p.Produto.Id == produto.Id && p.Status == true);
                if(promocao != null){
                    produto.PrecoVenda -= (produto.PrecoVenda * promocao.Porcentagem / 100);
                }
            }
            else
            {
                Response.StatusCode = 404;
                return Json(null);
            }
        }
    }
}