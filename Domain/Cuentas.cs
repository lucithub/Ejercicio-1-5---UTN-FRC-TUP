using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_6.Domain
{
    public class Cuentas
    {
        public int ID { get; set; }
        public string CBU { get; set; }
        public decimal Saldo { get; set; }
        public TiposCuentas TipoCuenta { get; set; }
        public DateTime UltimoMovimiento { get; set; }
        public Clientes Cliente { get; set; }
    }
}
