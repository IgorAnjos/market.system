using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;

namespace web.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;
        public EstoqueController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }

        public IActionResult Estoque()
        {
            return View();
        }
    }
}