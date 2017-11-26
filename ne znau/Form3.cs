using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
using System.IO;
namespace ne_znau
{
    public partial class Form3 : Form
    {


        OleDbConnection DBConnection = new OleDbConnection();
        OleDbDataAdapter DataAdapter;
        
        DataTable LocalDataTable = new DataTable();

        /// <summary>
        /// ФОРМИРОВАНИЕ ИНИЦИАЛИЗАЦИИ
        /// </summary>

        int rowPosition = 0;
        int rowNumber = 0;

      

        public Form3()
        {
            InitializeComponent();

            DBConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Polzovateli.mdb;
//Persist Security Info=False;";

            {

            }



            SuspendLayout();
            сохранитьToolStripMenuItem.Text = "Открыть";
            сохранитьToolStripMenuItem.Click += new EventHandler(сохранитьToolStripMenuItem_Click);




        }

        private void Form3_Load(object sender, EventArgs e)
        {
 
            ConnectToDatabase();
            /*
       DBConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Пользователи.mdb;
Persist Security Info=False;";

            try
            {

                DBConnection.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = DBConnection;
                string query = "select * from  Users";
                command.CommandText = query;

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                   // comboBox1.Items.Add(reader["Log"].ToString());
                   label3.Text = (reader["Log"].ToString());
                    label4.Text= (reader["Логин"].ToString());

                }
                DBConnection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
            */

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Polzovateli.mdb"))
            using (OleDbCommand Command = new OleDbCommand(" SELECT  * from Users", con))
            //using (OleDbCommand Command = new OleDbCommand(" SELECT top 1 * from Users", con))

            {
                con.Open();
                OleDbDataReader DB_Reader = Command.ExecuteReader();
                if (DB_Reader.HasRows)
                {
                    DB_Reader.Read();
                    // label3.Text = DB_Reader.ToString();
                    //textBox1.Text = DB_Reader.GetString("Логин");

                    label3.Text = (DB_Reader["Фамилия"].ToString());
                    label4.Text = (DB_Reader["Имя"].ToString());
                    label5.Text = (DB_Reader["Отчество"].ToString());
                    label6.Text = (DB_Reader["Должность"].ToString());
                }
                }

                }

        private void ConnectToDatabase()
        {

            DBConnection.ConnectionString = @"Provider=Microsoft.jet.OLEDB.4.0;Data Source=Polzovateli.mdb";
            DBConnection.Open();

            DataAdapter = new OleDbDataAdapter("Select * From LearnActiv ", DBConnection);
            DataAdapter.Fill(LocalDataTable);
            if (LocalDataTable.Rows.Count != 0)

            {

                rowPosition = LocalDataTable.Rows.Count;

            }

        }
        private void RefreshDBConnection()
        {
            if (DBConnection.State.Equals(ConnectionState.Open))
            {
                DBConnection.Close();
                LocalDataTable.Clear();
                ConnectToDatabase();

            }


        }








        private void tabPage2_OsnovnayaChast_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

           

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // выход, если была нажата кнопка Отмена и прочие (кроме ОК)
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            // всё. имя файла теперь хранится в openFileDialog1.FileName
            // MessageBox.Show("Выбран файл: "+openFileDialog1);

            
                    
                

        }
                

        
        
    

        private void ToolStripMenuItem_File_Save_Click(object sender, EventArgs e)
        {
            string ReadResFilePlace = ""; // путь к файлу с ответом
            saveFileDialog1.Filter = "out data (*.exe)|*.exe|All files (*.*)|*.*";
            saveFileDialog1.FileName = "result";
            saveFileDialog1.DefaultExt = "exe";
            DialogResult drs = saveFileDialog1.ShowDialog();
           if (drs == DialogResult.OK)
{
        ReadResFilePlace = saveFileDialog1.FileName; //считываем путь к файлу с результатом
        StreamWriter sw = new StreamWriter(ReadResFilePlace);
     //   sw.WriteLine(lbImages.Text);
        sw.Close();
        {
        }
        
              
        

        }
        }


        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {


        }






        private void btnPath_Click(object sender, EventArgs e)
        {

             
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }


        private void button3_Click(object sender, EventArgs e)
        {


            try

            {

                LocalDataTable.Rows[rowNumber].Delete();

                OleDbCommandBuilder CommandBuilder = new OleDbCommandBuilder(DataAdapter);
                MessageBox.Show("запись удалена успешно");
                DataAdapter.Update(LocalDataTable);
                RefreshDBConnection();

                rowNumber--;
                pictureBox1.Image = ReadImageFromDB();
            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.ToString());


            }


        }

        private void richTextBox_Razdel_1_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            
            //Далее обработка в событии FileOk  событие срабатывает если пользователь нажал Ok в диалоговом окне
            
          //Ну и сдесь обработка... путь куда сохраняет пользователь находится в SaveDialog1.FileName
          //И понадобится класс StreamWriter для записи в файл
        


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (OpenImagDlg.ShowDialog() == DialogResult.OK)

            {


                pictureBox1.Image = Image.FromFile(OpenImagDlg.FileName);
                button5.Enabled = true;
            }




        }
        private void button5_Click(object sender, EventArgs e)
        {
            //convertAndStoreToDB(pictureBox1.Image);
            StoreData(ConvertImageToBytes(pictureBox1.Image));

        }

        private byte[] ConvertImageToBytes(Image InputImage)
        {
            // return null;

            Bitmap BmpImage = new Bitmap(InputImage);
            MemoryStream Mystrem = new MemoryStream();
            BmpImage.Save(Mystrem, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] ImageAsBytes = Mystrem.ToArray();

            return ImageAsBytes;
        }

        private void StoreData(byte[] ImageAsBytes)
        {
            if (DBConnection.State.Equals(ConnectionState.Closed))
                DBConnection.Open();


            try
            {

                MessageBox.Show("Сохранение изображения по индексу: " + rowPosition.ToString());

                OleDbCommand OledbInsert = new OleDbCommand("Insert INTO LearnActiv (ImgID,ImgName,Img) VALUES ('" + rowPosition.ToString() + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "',@MyImg)", DBConnection);


                //"Insert INTO ImagesTable (ImgID,ImgName,Img) VALUES ('" + rowPosition.ToString + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "',@MyImg)", DBConnection);

                OleDbParameter imageParameter = OledbInsert.Parameters.AddWithValue("@Img", SqlDbType.Binary);
                imageParameter.Value = ImageAsBytes;
                imageParameter.Size = ImageAsBytes.Length;
                int rowsAffected = OledbInsert.ExecuteNonQuery();
                MessageBox.Show("Данные успешно сохранены в " + rowsAffected.ToString() + "Ряд");

                rowPosition++;
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show(ex.StackTrace.ToString());

            }
            finally
            {
                RefreshDBConnection();
            }
        


    }

    private void button6_Click(object sender, EventArgs e)
        {
            RefreshDBConnection();
            pictureBox1.Image = ReadImageFromDB();
            button7.Enabled = true;
            button8.Enabled = true;
            button3.Enabled = true;
            button5.Text = "Сохранить еще";
        }


        private Image ReadImageFromDB()
        {
            // return null;
            Image FetcheadImg;
            if (rowNumber >= 0)
            {
                byte[] FetcheadImgBytes = (byte[])LocalDataTable.Rows[rowNumber]["Img"];
                MemoryStream stream = new MemoryStream(FetcheadImgBytes);

                FetcheadImg = Image.FromStream(stream);

                return FetcheadImg;
            }

            else
            {

                MessageBox.Show("в базе данных нет изображений. повторно подключите или добавьте несколько изображений.");
                return null;


            }
        }






            private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        




        private void button7_Click(object sender, EventArgs e)
        {

            if (rowNumber == LocalDataTable.Rows.Count - 1)
                MessageBox.Show("Вы достигли последнего изображения!");

            else
                rowNumber++;

            pictureBox1.Image = ReadImageFromDB();



        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (rowNumber == 0)
                MessageBox.Show("Это первое изображение!");
            else

            {

                rowNumber--;
                pictureBox1.Image = ReadImageFromDB();
            }

           


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*

            DBConnection.Open();
            OleDbDataReader reader = null;
           OleDbCommand cmd = new OleDbCommand("select* from Users", DBConnection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                label4.Text = (reader["Логин"].ToString());
            }
            DBConnection.Close();
            */

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Polzovateli.mdb"))
            using (OleDbCommand Command = new OleDbCommand(" SELECT  * from Users", con))
            //using (OleDbCommand Command = new OleDbCommand(" SELECT top 1 * from Users", con))

            {
                con.Open();
                OleDbDataReader DB_Reader = Command.ExecuteReader();
                if (DB_Reader.HasRows)
                {
                    DB_Reader.Read();
                    // label3.Text = DB_Reader.ToString();
                    //textBox1.Text = DB_Reader.GetString("Логин");

                    label4.Text = (DB_Reader["Логин"].ToString());
                }
            }


        }
    }


}