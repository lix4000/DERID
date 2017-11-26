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
using System.IO;



namespace ne_znau


{
    public partial class Form2 : Form
    {

       private OleDbConnection Connection = new OleDbConnection();
        //OleDbDataAdapter DataAdapter;

        DataTable LocalDataTable = new DataTable();


        


        /// <summary>
        /// FORM INITIALIZER
        /// </summary>





        public Form2()
        {
            InitializeComponent();

            Connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Polzovateli.mdb;
Persist Security Info=False;";
            {

     

            }
        }
        

        

       

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void tabPage0_ObSveden_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

         

            try
            {
                //Connection.Open();
              
               // Connection.Close();
            }

            catch (Exception ex)

            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

           
        }





      
     









        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (OpenImagDlg.ShowDialog() == DialogResult.OK)

            {


                pictureBox1.Image = Image.FromFile(OpenImagDlg.FileName);
                
            }

        }

        


    






    private void button4_Click(object sender, EventArgs e)
        {
            try { 
            Connection.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = Connection;
           
            command.CommandText = "insert into Users(Логин,Пароль,Электронная_почта,Фамилия,Имя,Отчество,Должность)values('" + textBox1.Text + "','" + textBox13.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox12.Text + "','" + textBox14.Text + "','" + textBox2.Text + "')";



                if ((textBox1.Text == string.Empty) || textBox13.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty || textBox12.Text == string.Empty || textBox14.Text == string.Empty || textBox2.Text == string.Empty || pictureBox1.Image == null)

                {
                    Connection.Close();
                    MessageBox.Show("Заполните все поля");
                }



                command.ExecuteNonQuery();





                MessageBox.Show("добавлен");
               
                Form3 f3 = new Form3();
                this.Hide();
                f3.ShowDialog();

                Connection.Close();
            }

              catch

            {

                MessageBox.Show("Ошибка!");

            }



          
        }



    }


}
