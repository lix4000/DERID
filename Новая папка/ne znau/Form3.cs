using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ne_znau
{
    public partial class Form3 : Form
    {
        OleDbConnection DBConnection = new OleDbConnection();
        OleDbDataAdapter DataAdapter;

        DataTable LocalDataTable = new DataTable();

        /// <summary>
        /// FORM INITIALIZER
        /// </summary>

        int rowPosition = 0;
        int rowNumber = 0;


        public Form3()
        {
            InitializeComponent();



            SuspendLayout();
            сохранитьToolStripMenuItem.Text = "Открыть";
            сохранитьToolStripMenuItem.Click += new EventHandler(сохранитьToolStripMenuItem_Click);




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

            // Отображем диалог для выбора папки
            // если пользователь выбрал папку, идём дальше
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Отображем путь к выбранной папке в текстовом поле
              
                // Очищаем текущий список изображений
               
                // Директория по выбранному пути
               
                // Заполняем ListBox файлами из папки
             
        }
}
        /// <summary>
        /// Событие выбора элемента в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }


        private void button3_Click(object sender, EventArgs e)
        {

            try

            {

                LocalDataTable.Rows[rowNumber].Delete();

                OleDbCommandBuilder CommandBuilder = new OleDbCommandBuilder(DataAdapter);
                MessageBox.Show("record deleted sucessfuly");
                DataAdapter.Update(LocalDataTable);
                RefreshDBConnection();

                rowNumber--;
                pictureBox4.Image = ReadImageFromDB();
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


                pictureBox4.Image = Image.FromFile(OpenImagDlg.FileName);
                button6.Enabled = true;
            }

        

        
            

       



   
       
  
        
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //convertAndStoreToDB(pictureBox1.Image);
            StoreData(ConvertImageToBytes(pictureBox4.Image));

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

                MessageBox.Show("Saving image at index: " + rowPosition.ToString());

                OleDbCommand OledbInsert = new OleDbCommand("Insert INTO Научная_деятельность (ImgID,ImgName,Img) VALUES ('" + rowPosition.ToString() + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "',@MyImg)", DBConnection);


                //"Insert INTO ImagesTable (ImgID,ImgName,Img) VALUES ('" + rowPosition.ToString + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "',@MyImg)", DBConnection);

                OleDbParameter imageParameter = OledbInsert.Parameters.AddWithValue("@Img", SqlDbType.Binary);
                imageParameter.Value = ImageAsBytes;
                imageParameter.Size = ImageAsBytes.Length;
                int rowsAffected = OledbInsert.ExecuteNonQuery();
                MessageBox.Show("Data Stored successfully in " + rowsAffected.ToString() + "Row");

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
            pictureBox4.Image = ReadImageFromDB();
           // btnNext.Enabled = true;
           // btnPrev.Enabled = true;
            button6.Enabled = true;
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

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "пользователиDataSet.Научная_деятельность". При необходимости она может быть перемещена или удалена.
          
            ConnectToDatabase();
        }


        private void ConnectToDatabase()
        {

            DBConnection.ConnectionString = @"Provider=Microsoft.jet.OLEDB.4.0;Data Source=Пользователи.mdb";
            DBConnection.Open();

            DataAdapter = new OleDbDataAdapter("Select * From Научная_деятельность ", DBConnection);
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

        private void button7_Click(object sender, EventArgs e)
        {
            if (rowNumber == LocalDataTable.Rows.Count - 1)
                MessageBox.Show("Вы достигли последнего изображения!");

            else
                rowNumber++;

            pictureBox4.Image = ReadImageFromDB();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (rowNumber == 0)
                MessageBox.Show("Это первое изображение!");
            else

            {

                rowNumber--;
                pictureBox4.Image = ReadImageFromDB();
            }
        }
    }


}