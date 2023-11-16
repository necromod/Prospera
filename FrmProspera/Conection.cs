using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmProspera
{
  public class Conection
    {
        protected SqlCommand cmd;
        protected SqlDataReader dr;
        protected SqlConnection Conectar;

        protected void Conexao()
        {


            try
            {
                Conectar = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Prospera;Integrated Security=True;");
                Conectar.Open();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        protected void Desconectar()
        {
            try
            {
                Conectar.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
