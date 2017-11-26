using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
namespace ne_znau
{
    public partial class Form1 : Form
    {

       // private OleDbConnection connection = new OleDbConnection();


        public Form1()
        {
            InitializeComponent();

           //connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Пользователи.mdb;
//Persist Security Info=False;";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //получаем ссылку на кнопку, на которую мы нажали
            Button b = (Button)sender;
            //Создаем новую кнопку
            Button temp = new Button();
            temp.Text = "Пример";
            temp.Width = b.Width;
            //Размещаем ее правее (на 10px) кнопки, на которую мы нажали
            temp.Location = new Point(b.Location.X + b.Width + 10, b.Location.Y);
            //Добавляем событие нажатия на новую кнопку 
            //(то же что и при нажатии на исходную)
            temp.Click += new EventHandler(button1_Click);
            //Добавляем элемент на форму
            this.Controls.Add(temp);
        
        
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
               // connection.Open();
                
               // connection.Close();
            }

            catch (Exception ex)

            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            #region net

            /*

            if (textBox1.Text == "1" && textBox2.Text == "1")
            {


                Form3 f3 = new Form3();
                f3.ShowDialog();
                this.Hide();
                Application.Exit();
            }

            else
            {
                textBox1.Text = " ";
                textBox2.Text = " ";
                MessageBox.Show("Неправильный логин или пароль!");
            }
*/

            #endregion


            


            


            OleDbConnection connection = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Пользователи.mdb ");
            int i=0;
            OleDbCommand cmd = new OleDbCommand();
            try

            {
                

                if ((textBox1.Text == string.Empty) || textBox2.Text == string.Empty)

                {

                    MessageBox.Show("напиши что нибудь");
                }

                



                cmd = new OleDbCommand("select count(*)from Users where Логин ='" + textBox1.Text + "'AND Пароль ='" + textBox2.Text + "'", connection);
                if(connection.State==ConnectionState.Closed)
                {
                    connection.Open();
                    i = (int)cmd.ExecuteScalar();



                }
                connection.Close();
                     if(i>0)

                {
                    //connection.Close();
                    //connection.Dispose();
                    Form3 f3 = new Form3();
                    this.Hide();



                    f3.ShowDialog();



                }

else

                {

                    MessageBox.Show(" не правильный логин или пароль");


                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());


            }
            
            }

            private void button3_Click(object sender, EventArgs e)
        {

            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Hide();
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true) textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }

        



            
        }
    }
