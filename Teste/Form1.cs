using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtIdade.Text, "[^0-9]"))
            {
                MessageBox.Show("Por favor utilize apenas números.");
                txtIdade.Text = txtIdade.Text.Remove(txtIdade.Text.Length - 1);
            }
        }

        private void lbNome_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCad_Click(object sender, EventArgs e)
        {
            var StrConexao = "server=localhost;uid=root;pwd=password;database=teste";
            var conexao = new MySqlConnection(StrConexao);
            conexao.Open();

            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtIdade.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Todos os campos devem estar preenchidos corretamente.");
            }
            else 
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO pessoa VALUES (null, '" + txtNome.Text + "', " + txtIdade.Text + ", '" + txtEmail.Text + "')", conexao);
                    cmd.ExecuteReader();

                    MessageBox.Show("Pessoa " + txtNome.Text + " cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro no cadastro de pessoa: " + ex.Message);
                }
            } 
            conexao.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = load();
        }
        private BindingSource load()
        {
            var StrConexao = "server=localhost;uid=root;pwd=password;database=teste";
            var conexao = new MySqlConnection(StrConexao);
            conexao.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataSet ds = new DataSet();
            BindingSource bs = new BindingSource();

            string sql = "SELECT * FROM pessoa";
            cmd = new MySqlCommand(sql, conexao);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);

            bs.DataSource = ds.Tables[0];

            return bs;
        }
    }
}
