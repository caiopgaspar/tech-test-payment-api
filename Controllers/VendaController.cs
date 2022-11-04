using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Models;
using tech_test_payment_api.Context;
using System.Data;

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

        [HttpPost("RegistrarVenda")]
        public IActionResult RegistrarVenda (Venda venda)
        {
            _context.Add(venda);

            if (venda.Quantidade == 0 || venda.Quantidade == null)
                return NotFound();

            _context.SaveChanges();
            
            return CreatedAtAction(nameof(BuscarVendaPorId), new { id = venda.VendaId }, venda);
        }

        [HttpGet("BuscarVendaPor{id}")]
        public IActionResult BuscarVendaPorId (int id){
            Venda venda = _context.Vendas.Find(id);
                       
            _context.Entry(venda).Reference(x => x.Vendedor).Load();

            if ( venda == null)
                return NotFound();

            return Ok(venda);
        }

        [HttpPut("AtualizarStatusVenda{id}")]
        public IActionResult AtualizarStatusVenda (int id, EnumStatusVenda novoStatus){
            Venda vendaBanco = _context.Vendas.Find(id);

            if (vendaBanco == null)
                return NotFound();                        
           
            vendaBanco.Status = novoStatus;            

            if (novoStatus == EnumStatusVenda.AguardandoPagamento) {
                _context.Vendas.Update(vendaBanco);
                if (vendaBanco.Status == EnumStatusVenda.PagamentoAprovado || vendaBanco.Status == EnumStatusVenda.Cancelada){
                    return Ok(vendaBanco);
                }
            } else if (novoStatus == EnumStatusVenda.PagamentoAprovado) {
                _context.Vendas.Update(vendaBanco);
                if (vendaBanco.Status == EnumStatusVenda.Cancelada || vendaBanco.Status == EnumStatusVenda.EnviadoParaTransportadora){
                    return Ok(vendaBanco);              
                }
            
            } else if (novoStatus == EnumStatusVenda.EnviadoParaTransportadora) {
                _context.Vendas.Update(vendaBanco);
                if (vendaBanco.Status == EnumStatusVenda.Entregue){
                    return Ok(vendaBanco);
                }
            } else{
                return NotFound();
            }

            _context.SaveChanges();

            return Ok(vendaBanco);

        }

    }
}