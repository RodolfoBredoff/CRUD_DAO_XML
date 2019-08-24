using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUD_DAO.classes;
using System.Xml;

namespace CRUD_DAO
{
    public partial class ClientesDAO : Form
    {

        private Cliente dao = new Cliente("localhost", "root", "", "cruddao");

        public ClientesDAO()
        {
            InitializeComponent();
            AtualizaData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                var nome = inputName.Text;
                var email = inputEmail.Text;
                this.dao.Inserir_Registro("tbl_user", " 0,  '" + nome + "' , '" + email + "' ");
                MessageBox.Show("Usuário cadastrado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar usuário! \n" + "Detalhes:\n" + ex.Message);
            }

            AtualizaData();
        }

        private void InputName_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AtualizaData()
        {
            dataGridView1.DataSource = this.dao.ExecutaSelecaoSQL(" select * from tbl_user ");
        }

        private void TxtNome_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

            string caminho = "C:\\Users\\Rodolfo Soares\\source\\repos\\CRUD+DAO\\dados\\dadosUser.xml";
            XmlTextWriter writer = new XmlTextWriter(caminho, Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("usuarios");

            DataTable Dados = this.dao.ExecutaSelecaoSQL(" select * from tbl_user ");

            foreach (DataRow row in Dados.Rows)
            {

                writer.WriteStartElement("usuario");
                writer.WriteStartElement("id");
                writer.WriteString(row["id"].ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("nome");
                writer.WriteString(row["nome"].ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("email");
                writer.WriteString(row["email"].ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();
                

            }

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            DataColumn column;
            DataRow row;

            //monta o caminho do arquivo na raiz do projeto
            string caminho = "C:\\Users\\Rodolfo Soares\\source\\repos\\CRUD+DAO\\dados\\dadosUser.xml";
            XmlTextReader xmlReader = new XmlTextReader(caminho);
            DataTable dt = new DataTable();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "id";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "nome";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "email";
            dt.Columns.Add(column);

            string auxId = "";
            string auxNome = "";
            string auxEmail = "";
            string prox = "";
            bool first = true;

            while (xmlReader.Read())
            {
                if (prox == "id")
                {
                    auxId = xmlReader.Value;
                    prox = "";

                }
                else if (prox == "nome")
                {
                    auxNome = xmlReader.Value;
                    prox = "";
                }
                else if (prox == "email")
                {
                    auxEmail = xmlReader.Value;
                    prox = ""; 
                }
                else if (xmlReader.Name.ToString() == "usuario")
                {
                    if (!first)
                    {
                        row = dt.NewRow();
                        row["id"] = auxId;
                        row["nome"] = auxNome;
                        row["email"] = auxEmail;
                        dt.Rows.Add(row);

                    }
                    else
                    {
                        first = false;
                    }
                }
                else if (xmlReader.Name.ToString() == "id")
                {
                    prox = "id";

                }
                else if (xmlReader.Name.ToString() == "nome")
                {
                    prox = "nome";

                }
                else if (xmlReader.Name.ToString() == "email")
                {
                    prox = "email";

                }

            }

            dataGridView1.DataSource = dt;
        }

    }
}
