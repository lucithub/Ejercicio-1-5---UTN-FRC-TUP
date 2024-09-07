using Problema1_6.Domain;
using Problema1_6.Repositories.Contracts;
using Problema1_6.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_6.Repositories.Implementations
{
    internal class TipoCuentaRepository : ITipoCuentaRepository
    {
        private SqlConnection _connection;
        private DataHelper _instance;
        public TipoCuentaRepository()
        {
            _connection = new SqlConnection(Properties.Resources.strConnection);
        }
        public List<TiposCuentas> GetAll()
        {
            _instance = DataHelper.GetInstance();
            List<TiposCuentas> lst = new List<TiposCuentas>();
            DataTable dt = new DataTable();
            dt = _instance.ExecuteSPQuery("OBTENER_TIPOS_CUENTAS");
            foreach (DataRow item in dt.Rows)
            {
                TiposCuentas oTipoCuenta = new TiposCuentas();
                oTipoCuenta.ID = Convert.ToInt32(item[0]);
                oTipoCuenta.Nombre = Convert.ToString(item[1]);
                lst.Add(oTipoCuenta);
            }
            return lst;
        }
    }
}
