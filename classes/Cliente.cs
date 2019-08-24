using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_DAO.classes
{
    class Cliente
    {

        /// <summary>
        /// Atributos/Campos de conexão com banco 
        /// </summary>
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        public string vquery { get; set; }

        public Cliente(string Servidor, string Usuario, string Senha, string Banco) //construtor instanciando a conexao
        {
            con = new MySqlConnection("server=" + Servidor + ";uid=" + Usuario + ";pwd=" + Senha + ";database=" + Banco);
        }
        private void Abrir_conexao()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Visualizar_Registro(string tabela)
        {
            try
            {
                cmd = new MySqlCommand("select * from " + tabela, con);
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                Abrir_conexao();
                da.Fill(dt);
                con.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Inserir_Registro(string tabela, string valores)
        {
            try
            {
                cmd = new MySqlCommand("insert into " + tabela + " values( " + valores + " )", con);
                Abrir_conexao();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ExecutaSelecaoSQL(string SQL)
        {
            try
            {
                cmd = new MySqlCommand(SQL, con);
                da = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                Abrir_conexao();
                da.Fill(dt);
                con.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecutaQuerySQL(string SQL)
        {
            try
            {
                cmd = new MySqlCommand(SQL, con);
                Abrir_conexao();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
