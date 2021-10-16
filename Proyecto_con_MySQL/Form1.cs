using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Proyecto_con_MySQL
{
    public partial class Form1 : Form
    {
        public string cadena_conexion = "Database=agenda;Data Source=localhost;User Id=grupo2;Password=grupo2";
        public string usuario_modificar;
         
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            try
            {
                string consulta = "select * from usuarios";
                MySqlConnection conexion = new MySqlConnection(cadena_conexion);
                MySqlDataAdapter comando = new MySqlDataAdapter(consulta, conexion);
                System.Data.DataSet ds = new System.Data.DataSet();
                comando.Fill(ds, "agenda");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "agenda";
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "Seleccione nivel";
            textBox1.Focus();
            button5.Visible = false;
            button10.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                string myInsertQuery = "INSERT INTO usuarios(nombre,clave,nivel) Values(?nombre,?clave,?nivel)";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);

                myCommand.Parameters.Add("?nombre", MySqlDbType.VarChar, 40).Value = textBox1.Text;
                myCommand.Parameters.Add("?clave", MySqlDbType.VarChar, 45).Value = textBox2.Text;
                myCommand.Parameters.Add("?nivel", MySqlDbType.Int32, 4).Value = comboBox1.Text;

                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                MessageBox.Show("Usuario agregado con éxito", "Ok", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

                string consulta = "select * from usuarios";

                MySqlConnection conexion = new MySqlConnection(cadena_conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds, "agenda");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "agenda";
            }
            catch 
            {
                MessageBox.Show("Ya existe el usuario", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            button5.Visible = true;
            button10.Visible = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            button5.Focus();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Focus();
            button7.Visible = false;
            button11.Visible = true;

            usuario_modificar = textBox1.Text.ToString();
          
        }

        private void button9_Click(object sender, EventArgs e)
        {
           try
            {
                string consult = "SELECT * From usuarios WHERE idusuario = " + textBox3.Text + "";
                MySqlConnection coneccion = new MySqlConnection(cadena_conexion);
                MySqlDataAdapter comand = new MySqlDataAdapter(consult, coneccion);

                System.Data.DataSet ds = new System.Data.DataSet();

                comand.Fill(ds, "agenda");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "agenda";

                MySqlConnection myConnetion = new MySqlConnection(cadena_conexion);
                string myInsertQuery = "select * from usuarios where idusuario = " + textBox3.Text + "";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery, myConnetion);

                myCommand.Connection = myConnetion;
                myConnetion.Open();

                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    textBox1.Text = (myReader.GetString(1));
                    textBox2.Text = (myReader.GetString(2));
                    comboBox1.Text = (myReader.GetString(3));

                }
                else
                {
                    MessageBox.Show("El usuario no existe", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                myReader.Close();
                myConnetion.Close();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Campo de busqueda esta vacio", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
           

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(cadena_conexion);

                string nombre = textBox1.Text.ToString();
                string clave = textBox2.Text.ToString();
                string nivel = comboBox1.Text.ToString();

                string myInsertQuery = "UPDATE usuarios SET nombre = '" + nombre + "',clave = '" + clave + "',nivel = '" + nivel + "'WHERE nombre = '" + usuario_modificar + "'";
                
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);

                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();


               
              MessageBox.Show("Usuario modificado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string consulta = "select * from usuarios";

                MySqlConnection conexion = new MySqlConnection(cadena_conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds, "agenda");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "agenda";
            }
            catch(MySqlException)
            {
                MessageBox.Show("Ya existe el usuario", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            button7.Visible = true;
            button11.Visible = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
            button7.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                string myInsertQuery = "DELETE From usuarios WHERE idusuario = " + textBox3.Text + "";
                MySqlCommand myCommand = new MySqlCommand(myInsertQuery);

                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

                MessageBox.Show("Usuario eliminado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string consulta = "SELECT * From usuarios";

                MySqlConnection conexion = new MySqlConnection(cadena_conexion);
                MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion);
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds, "agenda");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "agenda";

            }
            catch (MySqlException)
            {
                MessageBox.Show("Error al eliminar el usuario, primero realice la búsqueda");
            }
            button5.Visible = true;
            button10.Visible = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            comboBox1.Enabled = false;
        }
    }
}
