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
            
            // vendaBanco.Status = novoStatus;
                       
            if (vendaBanco.Status == EnumStatusVenda.AguardandoPagamento) {
                
                if (novoStatus == EnumStatusVenda.PagamentoAprovado || novoStatus == EnumStatusVenda.Cancelada){
                    vendaBanco.Status = novoStatus;
                    _context.Vendas.Update(vendaBanco);
                    _context.SaveChanges();
                    return Ok(vendaBanco);
                } else{
                    return BadRequest();
                }

            } else if (vendaBanco.Status == EnumStatusVenda.PagamentoAprovado) { 

                if (novoStatus == EnumStatusVenda.Cancelada || novoStatus == EnumStatusVenda.EnviadoParaTransportadora){
                    vendaBanco.Status = novoStatus;
                    _context.Vendas.Update(vendaBanco);
                    _context.SaveChanges();
                    return Ok(vendaBanco);              
                } else{
                    return BadRequest();
                }
            
            } else if (vendaBanco.Status == EnumStatusVenda.EnviadoParaTransportadora) {

                if (novoStatus == EnumStatusVenda.Entregue){
                    vendaBanco.Status = novoStatus;
                    _context.Vendas.Update(vendaBanco);
                    _context.SaveChanges();
                    return Ok(vendaBanco);
                }
            } else{
                return BadRequest();
            } 
            return BadRequest();
        }       
    }
}