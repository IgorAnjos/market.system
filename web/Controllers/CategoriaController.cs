using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;
using mktSystem.Models;

namespace mktSystem.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext database;
        public CategoriaController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }
        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaDTO)
        {
            if(ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaDTO.Nome;
                categoria.Status = true;

                database.Categorias.Add(categoria);
                database.SaveChanges();
            return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
            return View("../Gestao/NovaCategoria");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(CategoriaDTO categoriaDTO)
        {
            if(ModelState.IsValid)
            {
                var categoria = database.Categorias.First(cat_ => cat_.Id == categoriaDTO.Id);
                categoria.Nome = categoriaDTO.Nome;
                
                database.SaveChanges();
                return RedirectToAction("Categorias","Gestao");
            }
            else
            {
                return View("../Gestao/EditarCategoria");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            
            if(id > 0)
            {
                var categoria = database.Categorias.First(_cat => _cat.Id == id);
                categoria.Status = false;
                database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
                return View("../Gestao/Categorias");
        }
    }
}