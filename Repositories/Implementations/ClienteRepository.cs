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

namespace Problema1_6.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private SqlConnection _connection;
        private DataHelper _instance;
        public ClienteRepository()
        {
            _connection = new SqlConnection(Properties.Resources.strConnection);
        }
        public List<Clientes> GetAll()
        {
            _instance = DataHelper.GetInstance();
            List<Clientes> lst = new List<Clientes>();
            DataTable dt = new DataTable();
            dt = _instance.ExecuteSPQuery("OBTENER_CLIENTES");
            foreach (DataRow item in dt.Rows)
            {
                Clientes oClientes = new Clientes();
                oClientes.ID = Convert.ToInt32(item[0]);
                oClientes.Nombre = Convert.ToString(item[1]);
                oClientes.Apellido = Convert.ToString(item[2]);
                oClientes.DNI = Convert.ToString(item[3]);
                lst.Add(oClientes);
            }
            return lst;
        }
        public int AddCliente(Clientes oClientes)
        {
            Clientes cliente = oClientes;
            _instance = DataHelper.GetInstance();
            int result = 0;
            SqlTransaction t = null;
            try
            {
                _connection.Open();
                t = _connection.BeginTransaction();
                SqlCommand cmd = new SqlCommand("CREAR_CLIENTE", _connection, t);
                cmd.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
                cmd.Parameters.AddWithValue("@APELLIDO", cliente.Apellido);
                cmd.Parameters.AddWithValue("@DNI", cliente.DNI);
                SqlParameter param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                param.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(param);
                result = _instance.ExecuteSPInsert("CREAR_CLIENTE");
                cliente.ID = Convert.ToInt32(param.Value);

                foreach (var cuenta in cliente.ListaCuentas)
                {
                    var cmdCuenta = new SqlCommand("CREAR_CUENTA", _connection, t);
                    cmdCuenta.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCuenta.Parameters.AddWithValue("@CBU", cuenta.CBU);
                    cmdCuenta.Parameters.AddWithValue("@SALDO", cuenta.Saldo);
                    cmdCuenta.Parameters.AddWithValue("@TIPO_CUENTA_ID", cuenta.TipoCuenta.ID);
                    cmdCuenta.Parameters.AddWithValue("@ULTIMO_MOVIMIENTO", cuenta.UltimoMovimiento);
                    cmdCuenta.Parameters.AddWithValue("@CLIENTE_ID", cliente.ID);
                    cmdCuenta.ExecuteNonQuery();

                }
                t.Commit();
                result = 1;
            }
            catch (SqlException)
            {
                result = 0;
                t.Rollback();
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }
        
    }
}
