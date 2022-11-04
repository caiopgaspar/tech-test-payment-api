using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Models
{      
    public class Venda
    {
        public int VendaId { get; set; }        
        public Vendedor Vendedor { get; set; }
        public DateTime DataVenda { get; set; }
        public string ItensVenda { get; set; }
        public  EnumStatusVenda Status { get; set; }

       

    }
}