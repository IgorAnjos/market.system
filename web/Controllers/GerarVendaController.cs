using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mktSystem.Data;
using mktSystem.Models;

namespace mktSystem.Controllers
{
    public class GerarVendaController : Controller
    {
        private readonly ApplicationDbContext database;
        public GerarVendaController(ApplicationDbContext _dbContext)
        {
            this.database = _dbContext;
        }

        [HttpPost]
        public IActionResult GerarVenda ([FromBody] VendaDTO dados)
        {
            Venda venda = new Venda();
            venda.Total = dados.Total;
            venda.Troco = dados.Troco;
            venda.ValorPago = dados.Troco <= 0.01f ? dados.Total : dados.Total + dados.Troco;
            venda.Data = DateTime.Now;
            database.Vendas.Add(venda);
            database.SaveChanges();

            //registrar saida
            List<Saida> saidas = new List<Saida>();
            foreach(var saida in dados.Produtos)
            {
                Saida s = new Saida();
                s.Quantidade = saida.Quantidade;
                s.ValorVenda = saida.Subtotal;
                s.Venda = venda;
                s.Produto = database.Produtos.First(_ => _.Id == saida.Produto);
                s.Data = DateTime.Now;
                saidas.Add(s);
            }
            // salvar no banco
            database.AddRange(saidas);
            database.SaveChanges();
            return Ok(new {msg="Venda concluída com sucesso"});
        }
    }
}