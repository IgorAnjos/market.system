using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mktSystem.Data;
using mktSystem.Models;

namespace mktSystem.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext database;
        public GestaoController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        ///Categoria
        public IActionResult Categorias()
        {
            var listCategorias = database.Categorias.Where(cat => cat.Status == true).ToList();
            return View(listCategorias);
        }
        public IActionResult NovaCategoria()
        {
            return View();
        }
       
        public IActionResult EditarCategoria(int id)
        {
            var categoria = database.Categorias.First(_cat => _cat.Id == id);
            CategoriaDTO categoriaDTO = new CategoriaDTO();
            categoriaDTO.Id = categoria.Id;
            categoriaDTO.Nome = categoria.Nome;

            return View(categoriaDTO);
        }

        //Fornecedor
        public IActionResult Fornecedores()
        {
            var listFornecedores = database.Fornecedores.Where(forn => forn.Status == true).ToList();
            return View(listFornecedores);
        }
        public IActionResult NovoFornecedor(){
            return View();
        }
        
        
        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = database.Fornecedores.First(_forn => _forn.Id == id);
            FornecedorDTO fornecedorDTO = new FornecedorDTO();
            fornecedorDTO.Id = fornecedor.Id;
            fornecedorDTO.Nome = fornecedor.Nome;
            fornecedorDTO.Email = fornecedor.Email;
            fornecedorDTO.Telefone = fornecedor.Telefone;

            return View(fornecedorDTO);
        }

        //Produto
        public IActionResult Produtos()
        {
            var listProdutos = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(_ => _.Status == true).ToList();
            return View(listProdutos);
        }
        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View();
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = database.Produtos.Include(produto => produto.Categoria).Include(produto => produto.Fornecedor).First(_ => _.Id == id);
            ProdutoDTO produtoDTO = new ProdutoDTO();
            produtoDTO.Id = produto.Id;
            produtoDTO.Nome = produto.Nome;
            produtoDTO.CategoriaID = produto.Categoria.Id;
            produtoDTO.FornecedorID = produto.Fornecedor.Id;
            produtoDTO.PrecoCusto = produto.PrecoCusto;
            produtoDTO.PrecoVenda = produto.PrecoVenda;
            produto.Medicao = produto.Medicao;
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View(produtoDTO);
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            return View();
        }
    }
}