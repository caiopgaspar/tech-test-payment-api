using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Models;
using tech_test_payment_api.Context;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase    
    {
        private readonly VendaContext _context;
        public VendaController(VendaContext context){
            _context = context;
        }

        [HttpPost("{RegistrarVenda}")]
        public IActionResult RegistrarVenda (Venda venda) //EnumStatusVenda status)
        {
            // _context.Vendedores.Find();
            _context.Add(venda);
            
            //receber status "AguardandoPagamento"

            return CreatedAtAction(nameof(BuscarVendaPorId), new { id = venda.VendaId }, venda);
        }

        [HttpGet("{BuscarVendaPorId}")]
        public IActionResult BuscarVendaPorId (int id){
            var venda = _context.Vendas.Find(id);

            if ( venda == null)
                return NotFound();

            return Ok(venda);
        }

        [HttpPut("{AtualizarStatusVenda}")]
        public IActionResult AtualizarStatusVenda (int id){
            var venda = _context.Vendas.Update();
        }



        
    }
}