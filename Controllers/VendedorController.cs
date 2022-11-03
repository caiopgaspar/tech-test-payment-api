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
    public class VendedorController : ControllerBase
    {
        private readonly VendaContext _context;
        public VendedorController(VendaContext context){
            _context = context;
        }

        // [HttpPost]
        // public IActionResult CadastroVendedor (Vendedor vendedor){
        //     _context.Add(vendedor);
        //     _context.SaveChanges();
        //     return CreatedAtAction(nameof(BuscarTodosOsVendedores), new { id = vendedor.VendedorId }, vendedor);
        // }

        // [HttpGet("BuscarTodosOsVendedores")]
        // public IActionResult BuscarTodosOsVendedores()
        // {          
        //     var vendedoresBanco = _context.Vendedores.ToList();

        //     if ( vendedoresBanco == null)
        //         return NotFound();
            
        //     return Ok(vendedoresBanco);
        // }

        


    }
}