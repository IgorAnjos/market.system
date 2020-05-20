using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;
using mktSystem.Models;

namespace web.Controllers
{
    public class PromocaoController : Controller
    {
        private readonly ApplicationDbContext database;
        public PromocaoController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }
        [HttpPost]
        public IActionResult Salvar(PromocaoDTO _promocaoDTO)
        {
            if(ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                
                promocao.Nome = _promocaoDTO.Nome;
                promocao.Produto = database.Produtos.First(_p => _p.Id == _promocaoDTO.ProdutoID);
                promocao.Porcentagem = _promocaoDTO.Porcentagem;
                promocao.Status = true;

                database.Promocoes.Add(promocao);
                database.SaveChanges();

                return RedirectToAction("Promocoes","Gestao");
            }
            else
            {
                ViewBag.Produtos = database.Produtos.Where(_p => _p.Status == true).ToList();
                return View("../Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO _promocaoDTO)
        {
            if(ModelState.IsValid)
            {
                var promocao = database.Promocoes.First(_ => _.Id == _promocaoDTO.Id);

                promocao.Nome = _promocaoDTO.Nome;
                promocao.Produto = database.Produtos.First(_ => _.Id == _promocaoDTO.ProdutoID);
                promocao.Porcentagem = _promocaoDTO.Porcentagem;

                database.SaveChanges();
                return RedirectToAction("Promocoes","Gestao");
            }
            else
            {
                ViewBag.Produtos = database.Produtos.Where(_ => _.Status == true).ToList();
                return View("../Gestao/NovaPromocao");
            }
        }
        
        public IActionResult Deletar(int id)
        {
            if(id > 0)
            {
                var promocao = database.Promocoes.First(_ => _.Id == id);
                promocao.Status = false;

                database.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                return RedirectToAction("Promocoes", "Gestao");
            }
        }
    }
}