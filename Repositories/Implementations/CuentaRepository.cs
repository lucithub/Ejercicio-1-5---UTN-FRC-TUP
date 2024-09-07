using Problema1_6.Domain;
using Problema1_6.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problema1_6.Repositories.Contracts;

namespace Problema1_6.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private SqlConnection _connection;
        private DataHelper _instance;
        public CuentaRepository()
        {
            _connection = new SqlConnection(Properties.Resources.strConnection);
        }
        public List<Cuentas> GetAll()
        {
            _instance = DataHelper.GetInstance();
            List<Cuentas> lst = new List<Cuentas>();
            DataTable dt = new DataTable();
            dt = _instance.ExecuteSPQuery("OBTENER_CUENTAS");
            foreach (DataRow item in dt.Rows)
            {
                Cuentas oCuenta = new Cuentas();
                oCuenta.ID = Convert.ToInt32(item[0]);
                oCuenta.CBU = Convert.ToString(item[1]);
                oCuenta.Saldo = Convert.ToDecimal(item[2]);
                oCuenta.TipoCuenta = new TiposCuentas();
                oCuenta.TipoCuenta.ID = Convert.ToInt32(item[3]);
                oCuenta.UltimoMovimiento = Convert.ToDateTime(item[4]);
                oCuenta.Cliente = new Clientes();
                oCuenta.Cliente.ID = Convert.ToInt32(item[5]);
                lst.Add(oCuenta);
            }
            return lst;
        }
        public int Add(Cuentas oCuentas)
        {
            Cuentas cuentas = oCuentas;
            _instance = DataHelper.GetInstance();
            int result = 0;
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand("CREAR_CUENTA", _connection);
                
                cmd.Parameters.AddWithValue("@CBU", cuentas.CBU);
                cmd.Parameters.AddWithValue("@SALDO", cuentas.Saldo);
                cmd.Parameters.AddWithValue("@TIPO_CUENTA_ID", cuentas.TipoCuenta.ID);
                cmd.Parameters.AddWithValue("@ULTIMO_MOVIMIENTO", cuentas.UltimoMovimiento);
                cmd.Parameters.AddWithValue("@CLIENTE_ID", cuentas.Cliente.ID);
                result = _instance.ExecuteSPInsert("CREAR_CUENTA");
                _connection.Close();
                
            }
            catch (SqlException)
            {
                result = 0;
            }
            return result;
        }
    }
}
