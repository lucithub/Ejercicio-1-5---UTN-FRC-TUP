using Problema1_6.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_6.Repositories.Contracts
{
    public interface ICuentaRepository
    {
        List<Cuentas> GetAll();
        int Add(Cuentas oCuenta);
    }
}
