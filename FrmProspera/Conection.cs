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
                var azureDbServer = "prosperamodel.database.windows.net";
                var azureDbName = "prosperamodel";
                var azureDbUser = "ProsperaModel";
                var azureDbPassword = "Prospera2023@";
                var connectionString = $"Server={azureDbServer};Database={azureDbName};User Id={azureDbUser};Password={azureDbPassword};Trusted_Connection=False;Encrypt=True;";
              
                Conectar = new SqlConnection(connectionString);
                
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
