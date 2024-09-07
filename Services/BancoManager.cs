using Problema1_6.Domain;
using Problema1_6.Repositories;
using Problema1_6.Repositories.Contracts;
using Problema1_6.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_6.Services
{
    public class BancoManager
    {
        IClienteRepository clienteRepository;
        ITipoCuentaRepository tipoCuentaRepository;
        ICuentaRepository cuentaRepository;
        public BancoManager()
        {
            clienteRepository = new ClienteRepository();
            tipoCuentaRepository = new TipoCuentaRepository();
            cuentaRepository = new CuentaRepository();
        }

        public List<Clientes> GetAllClientes()
        {
            return clienteRepository.GetAll();
        }
        public List<TiposCuentas> GetAllTiposCuentas()
        {
            return tipoCuentaRepository.GetAll();
        }
        public int AddCuenta(Cuentas cuenta)
        {
            return cuentaRepository.Add(cuenta);
        }
        public List<Cuentas> GetAllCuentas()
        {
            return cuentaRepository.GetAll();
        }
        public int AddCliente(Clientes cliente)
        {
            return clienteRepository.AddCliente(cliente);
        }
    }
}
