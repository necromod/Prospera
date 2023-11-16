using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FrmProspera
{
    internal class ValidaForm : Conection
    {
        public Usuario VerificarCredenciais(Usuario Obj)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("SELECT * FROM Usuario WHERE NomeUsuario = @NomeUsuario AND SenhaUsuario = @SenhaUsuario;", Conectar);
                cmd.Parameters.AddWithValue("@NomeUsuario", Obj.NomeUsuario);
                cmd.Parameters.AddWithValue("@SenhaUsuario", Obj.SenhaUsuario);
                dr = cmd.ExecuteReader();
                Usuario objUser = null;
                if (dr.Read())
                {
                    objUser = new Usuario();
                    objUser.NomeUsuario = dr["NomeUsuario"].ToString();
                    objUser.SenhaUsuario = dr["SenhaUsuario"].ToString();
                    objUser.TpUsuario = dr["TpUsuario"].ToString();
                }
                return objUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Usuário não cadastrado ! {ex.Message}");
            }
            finally
            {
                Desconectar();
            }


        }
        public List<Usuario> ListarUsuario()
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("SELECT * FROM Usuario", Conectar);
                dr = cmd.ExecuteReader();
                List<Usuario> lista = new List<Usuario>();
                while (dr.Read())
                {
                    Usuario objUser = new Usuario();
                    objUser.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    objUser.NomeUsuario = dr["NomeUsuario"].ToString();
                    objUser.SenhaUsuario = dr["SenhaUsuario"].ToString();
                    objUser.EmailUsuario = dr["EmailUsuario"].ToString();
                    objUser.CPFUsuario = dr["CPFUsuario"].ToString();
                    objUser.CargoUsuario = dr["CargoUsuario"].ToString();
                    objUser.DatCadastroUsuario = dr["DatCadastroUsuario"].ToString();
                    objUser.StatusUsuario = dr["StatusUsuario"].ToString();
                    objUser.TpUsuario = dr["TpUsuario"].ToString();
                    lista.Add(objUser);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Buscar Usuarios ! {ex.Message}");
            }
            finally
            {
                if (Conectar.State == ConnectionState.Open)
                {
                    Desconectar();
                }
            }
        }




        public Usuario ExcluirUsuario(string nome, string senha)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("SELECT * FROM Usuario WHERE NomeUsuario = @NomeUsuario AND SenhaUsuario = @SenhaUsuario;", Conectar);
                cmd.Parameters.AddWithValue("@NomeUsuario", nome);
                cmd.Parameters.AddWithValue("@SenhaUsuario", senha);
                dr = cmd.ExecuteReader();
                Usuario objUser = null;
                if (dr.Read())
                {
                    objUser = new Usuario();
                    objUser.NomeUsuario = dr["NomeUsuario"].ToString();
                    objUser.SenhaUsuario = dr["SenhaUsuario"].ToString();
                    objUser.TpUsuario = dr["TpUsuario"].ToString();
                }
                return objUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Usuário não cadastrado ! {ex.Message}");
            }
            finally
            {
                Desconectar();
            }


        }

        public void CadastrarUsuario(Usuario UserCad)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("INSERT INTO Usuario (NomeUsuario,EmailUsuario,SenhaUsuario,CPFUsuario,CargoUsuario,StatusUsuario,TpUsuario,DatCadastroUsuario) VALUES(@NomeUsuario,@EmailUsuario,@SenhaUsuario,@CPFUsuario,@CargoUsuario,@StatusUsuario,@TpUsuario,@DatCadastroUsuario);", Conectar);
                cmd.Parameters.AddWithValue("@NomeUsuario", UserCad.NomeUsuario).ToString();
                cmd.Parameters.AddWithValue("@EmailUsuario", UserCad.EmailUsuario).ToString();
                cmd.Parameters.AddWithValue("@SenhaUsuario", UserCad.SenhaUsuario).ToString();
                cmd.Parameters.AddWithValue("@CPFUsuario", UserCad.CPFUsuario).ToString();
                cmd.Parameters.AddWithValue("@CargoUsuario", UserCad.CargoUsuario).ToString();
                cmd.Parameters.AddWithValue("@StatusUsuario", UserCad.StatusUsuario).ToString();
                cmd.Parameters.AddWithValue("@DatCadastroUsuario", Convert.ToDateTime(UserCad.DatCadastroUsuario));
                cmd.Parameters.AddWithValue("@TpUsuario", Convert.ToInt32(UserCad.TpUsuario));            
                cmd.ExecuteNonQuery();

            

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }

        }
        public void CadastrardATatual(DateTime Usercadastro)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("INSERT INTO Usuario(DatUltimoAcesUsuario) VALUES (@DatUltimoAcesUsuario);", Conectar);
                cmd.Parameters.AddWithValue("@DatUltimoAcesUsuario", DateTime.Now);
            
          
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }

        }
        public List<TipoUsuarioModel> PopularCBO()
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("SELECT * FROM TipoUsuario", Conectar);
                dr = cmd.ExecuteReader();
                List<TipoUsuarioModel> lista = new List<TipoUsuarioModel>();

                while (dr.Read())
                {
                    TipoUsuarioModel objTpUser = new TipoUsuarioModel();
                    objTpUser.IdTpUsuario = Convert.ToInt32(dr["IdTpUsuario"]);
                    objTpUser.DescricaoTpUsuario = dr["DescricaoTpUsuario"].ToString();

                    lista.Add(objTpUser);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao popular TipoUsuarioModel: {ex.Message}");
            }
            finally
            {
                Desconectar();
            }
        }

        public void ExcluirUsuario(int objDel)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("DELETE FROM Usuario WHERE IdUsuario = @Id;", Conectar);
                cmd.Parameters.AddWithValue("@Id", objDel);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }


        public void EditarUsuario(Usuario objEdita)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("UPDATE Usuario SET [NomeUsuario] = @NomeUsuario,[SenhaUsuario]=@SenhaUsuario,[EmailUsuario]=@EmailUsuario,[CPFUsuario] = @CPFUsuario,[CargoUsuario] = @CargoUsuario,[StatusUsuario] = @StatusUsuario,[TpUsuario] = @TpUsuario WHERE IdUsuario = @id", Conectar);
                cmd.Parameters.AddWithValue("@NomeUsuario", objEdita.NomeUsuario);
                cmd.Parameters.AddWithValue("@SenhaUsuario", objEdita.SenhaUsuario);
                cmd.Parameters.AddWithValue("@EmailUsuario", objEdita.EmailUsuario);
                cmd.Parameters.AddWithValue("@CPFUsuario", objEdita.CPFUsuario);
                cmd.Parameters.AddWithValue("@CargoUsuario", objEdita.CargoUsuario);
                cmd.Parameters.AddWithValue("@StatusUsuario", objEdita.StatusUsuario);
                cmd.Parameters.AddWithValue("@TpUsuario", Convert.ToInt32(objEdita.TpUsuario));

                cmd.Parameters.AddWithValue("@id", objEdita.IdUsuario);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }

        }

        public Usuario BuscaPorId(int objId)
        {
            try
            {
                Conexao();
                cmd = new SqlCommand("SELECT Usuario.*, TipoUsuario.DescricaoTpUsuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.TpUsuario = TipoUsuario.IdTpUsuario WHERE Usuario.IdUsuario = @IdUsuario;", Conectar);
                cmd.Parameters.AddWithValue("@IdUsuario", objId);
                dr = cmd.ExecuteReader();
                Usuario obj = null;
                if (dr.Read())
                {
                    obj = new Usuario();
                    obj.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    obj.NomeUsuario = dr["NomeUsuario"].ToString();
                    obj.SenhaUsuario = dr["SenhaUsuario"].ToString();
                    obj.EmailUsuario = dr["EmailUsuario"].ToString();
                    obj.NomeUsuario = dr["NomeUsuario"].ToString();
                    obj.CPFUsuario = dr["CPFUsuario"].ToString();
                    obj.CargoUsuario = dr["CargoUsuario"].ToString();
                    obj.DatCadastroUsuario = dr["DatCadastroUsuario"].ToString();
                    obj.StatusUsuario = dr["StatusUsuario"].ToString();
                    obj.TpUsuario = dr["DescricaoTpUsuario"].ToString();

                    /*   obj.DataNascUsuario = Convert.ToDateTime(dr["DataNascUsuario"]);
                       obj.UsuarioTp = dr["UsuarioTp"].ToString();*/
                }
                return obj;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
