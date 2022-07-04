using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//adicionar os "using" a bibliotecas selecionadas

namespace PetLife
{
  
    public partial class Form1 : Form
    {

        //definir as variaveis usadas a serem utilizadas na aplicação
        //não esquecer de adicionar a referencia ao banco de dados /projeto/adicionar/referencia/ SQLData

        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        int ID = 0;
       // MySqlDataReader dr;
        string strqSQL;

        public Form1()
        {
            InitializeComponent();
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {   
                //endereço da conexão com o banco de dados e instancia...
                conexao = new MySqlConnection("Server=localhost;Database=estoque_controle;Uid=root;Pwd=123456;");
                //fazer referencia ao banco de dados com o comando usado no banco e instanciar
                strqSQL = "INSERT INTO CAD_PRODUTO(NOME_PRODUTO, PRECO, CODIGO, QUANTIDADE) VALUES (@NOME_PRODUTO, @PRECO, @CODIGO, @QUANTIDADE)";
                comando = new MySqlCommand(strqSQL, conexao);

                //adicionar os parametros de entrada com os campos utilizados.
                comando.Parameters.AddWithValue("@NOME_PRODUTO", textNome.Text);
                comando.Parameters.AddWithValue("@PRECO", textPrecoVenda.Text);
                comando.Parameters.AddWithValue("@CODIGO", textCodigoProd.Text);
                comando.Parameters.AddWithValue("@QUANTIDADE", textQuantidade.Text);

                conexao.Open();
                DisplayData();
                comando.ExecuteNonQuery();

                MessageBox.Show("Cadastrado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                //fechar conexão com o banco de dados
                conexao.Close();
                conexao = null;
                conexao = null;
            }
        }
        private void DisplayData()
        {
            conexao = new MySqlConnection("Server=localhost;Database=estoque_controle;Uid=root;Pwd=123456;");
            conexao.Open();
            DataTable dt = new DataTable();
            da = new MySqlDataAdapter("select * from CAD_PRODUTO", conexao);
            da.Fill(dt);
            dataGridView.DataSource = dt;
            conexao.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //conexao com o banco de dados e endereço
                conexao = new MySqlConnection("Server=localhost;Database=estoque_controle;Uid=root;Pwd=123456;");
                
                strqSQL = "SELECT * FROM CAD_PRODUTO";

                da = new MySqlDataAdapter(strqSQL, conexao);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView.DataSource = dt;
                //mostra as informações do banco de dados no GridView

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conexao.Close();
                conexao = null;
                conexao = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            conexao = new MySqlConnection("Server=localhost;Database=estoque_controle;Uid=root;Pwd=123456;");
            strqSQL = "DELETE FROM CAD_PRODUTO WHERE ID_PRODUTO = @ID";
            comando = new MySqlCommand(strqSQL, conexao);
            comando.Parameters.AddWithValue("@ID", textID.Text);
            //Exclusão será realizada somente pelo ID
            conexao.Open();
            comando.ExecuteNonQuery();

            strqSQL = "SELECT * FROM CAD_PRODUTO";

            da = new MySqlDataAdapter(strqSQL, conexao);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView.DataSource = dt;
            //mostra as informações do banco de dados no GridView

            if (conexao.State ==ConnectionState.Open)
            {
                try
                {
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Removido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha na Remoção", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    //Sempre fechar a conexão.
                    conexao.Close();
                    conexao = null;
                    conexao = null;
                }
            }
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            if (textID.Text != "" && textBoxName.Text != "" && textBoxPreco.Text != "" && textBoxCod.Text != "" && textBoxQuantidade.Text != "") {
                try
                {
                    //caminho com o banco de dados.
                    conexao = new MySqlConnection("Server=localhost;Database=estoque_controle;Uid=root;Pwd=123456;");
                    //informar os comandos SQL a variavel e instanciar as variaveis
                    strqSQL = "UPDATE CAD_PRODUTO SET NOME_PRODUTO = @NOME_PRODUTO, PRECO = @PRECO, CODIGO = @CODIGO, QUANTIDADE = @QUANTIDADE  WHERE ID_PRODUTO = @ID_PRODUTO";

                    comando = new MySqlCommand(strqSQL, conexao);

                    //relacionar os parametros das caixas de entrada com os dados do banco
                    comando.Parameters.AddWithValue("@ID_PRODUTO", textID.Text);
                    comando.Parameters.AddWithValue("@NOME_PRODUTO", textBoxName.Text);
                    comando.Parameters.AddWithValue("@PRECO", textBoxPreco.Text);
                    comando.Parameters.AddWithValue("@CODIGO", textBoxCod.Text);
                    comando.Parameters.AddWithValue("@QUANTIDADE", textBoxQuantidade.Text);

                    MessageBox.Show("Atualizado com sucesso");

                    conexao.Open();
                    DisplayData();
                    comando.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    //caixa de excessão caso aconteça um erro.
                    MessageBox.Show(ex.Message);


                }
                finally
                {

                    //fechar a conexão com o banco de dados e finalizar 
                    conexao.Close();
                    conexao = null;
                    conexao = null;
                }
            } }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //limpar todos campos
            textNome.Text = "";
            textPrecoVenda.Text = "";
            textCodigoProd.Text = "";
            textQuantidade.Text = "";
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //limpar todos os campos
            textID.Text = "";
            textBoxName.Text = "";
            textBoxPreco.Text = "";
            textBoxCod.Text = "";
            textBoxQuantidade.Text = "";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textQuantidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxQuantidade_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textID_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
                textID.Text = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxName.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBoxPreco.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxCod.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(); ;
                textBoxQuantidade.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {

            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=estoque_controle;Uid=root;Pwd=123456;");

                strqSQL = "SELECT * FROM CAD_PRODUTO";

                comando = new MySqlCommand(strqSQL, conexao);
                conexao.Open();
                da = new MySqlDataAdapter("SELECT * FROM CAD_PRODUTO WHERE CODIGO LIKE '" + textPesquisa.Text + "%'", conexao);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView.DataSource = dt;
                conexao.Close();
                //mostra as informações do banco de dados no GridView
            }
            catch (Exception ex)
            {
                //caixa de excessão caso aconteça um erro.
                MessageBox.Show(ex.Message);


            }
            finally
            {

                //fechar a conexão com o banco de dados e finalizar 
                conexao.Close();
                conexao = null;
                conexao = null;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    
}
