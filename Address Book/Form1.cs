using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Data.OleDb;

namespace Address_Book
 
{



    public partial class Form1 : Form
    {
        readonly String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database2.mdb";
        OleDbConnection connection;


        private SoundPlayer soundPlayer;
        private SoundPlayer soundPlayer2;
        private SoundPlayer soundPlayer3;
        private SoundPlayer soundPlayer4;




        public class Contact//Δημιουργια της class Contact
        {
            public String firstName;
            public String lastName;
            public int phoneNumber;
            public String email;
            public String Address;
            public DateTime birthday;
            public Image picture;
            public String musicFile;
            
}
        
        bool Check;
        int check;
        int Break=0;
        List<Contact> contacts = new List<Contact>();
        List<String> birthdays = new List<String>();
        Image image1=new Bitmap("white page.jpg");//ενα default image (λευκη εικονα)
        int num;
        int num2=-1;
        List<int> numbers = new List<int>();
        String musicfilen="";
        String musicfilen2 = "";
        String musicfilen3 = "";
        String musicfilen4 = "";
        int sameNumber=0;
        String filen="";
        String filen2="";
        List<String> images = new List<String>();
        public Form1()
        {
            InitializeComponent();
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);





            //using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", mdfFile)))
            //{
                OleDbCommand selectCommand = new OleDbCommand("SELECT TOP  2147483 * FROM Table1", connection); //Μεταφορα στοιχειων απο τη database στη λιστα με τις επαφες
                
                    connection.Open();

                    DataTable table = new DataTable();
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    adapter.SelectCommand = selectCommand;
                    adapter.Fill(table);
                    




                    foreach (DataRow row in table.Rows)
                    {
                        Contact contact = new Contact();
                        contact.firstName = row["FirstName"].ToString();
                        contact.lastName = row["LastName"].ToString();
                        contact.phoneNumber = Int32.Parse(row["PhoneNumber"].ToString());
                        contact.email = row["Email"].ToString();
                        contact.Address = row["Address"].ToString();
                        contact.birthday = Convert.ToDateTime(row["Birthday"].ToString());
                        String imageFile = row["Picture"].ToString();
                        if (imageFile != "")
                    {
                            contact.picture = new Bitmap(imageFile);
                    }
                        
                        contact.musicFile = row["Music"].ToString();
                        contacts.Add(contact);
                        numbers.Add(contact.phoneNumber);
                        images.Add(imageFile);
                        comboBox1.Items.Add(contact.firstName + " " + contact.lastName + " " + "(" + contact.phoneNumber.ToString() + ")");
                        comboBox2.Items.Add(contact.firstName + " " + contact.lastName + " " + "(" + contact.phoneNumber.ToString() + ")");
            }
            connection.Close();
                
            //}








            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog2.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog3.Filter= "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            openFileDialog4.Filter= "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            openFileDialog3.InitialDirectory = @"C:\Users\ALEXIS\source\repos\Address Book\Address Book";
            openFileDialog4.InitialDirectory = @"C:\Users\ALEXIS\source\repos\Address Book\Address Book";
            pictureBox1.Image = image1;
            pictureBox2.Image = image1;
            pictureBox3.Image = image1;
           
            if (contacts.Count > 0)
            {
                for (int i = 0; i < contacts.Count; i++)//ελεγχος γενεθλιων
                {
                    String s = Reverse(contacts[i].birthday.ToShortDateString());
                    String s2 = Reverse(DateTime.Today.ToShortDateString());
                    
                    
                    
                    if (s.Substring(s.Length-5) == s2.Substring(s2.Length-5))
                    {
                        birthdays.Add(contacts[i].firstName + " " + contacts[i].lastName);
                    }
                }

                if (birthdays.Count > 0)
                {
                    String birthday = "";
                    for (int i = 0; i < birthdays.Count; i++)
                    {
                        if (i == birthdays.Count - 1)
                        {
                            birthday = birthday + birthdays[i];
                        }
                        else
                        {
                            birthday = birthday + birthdays[i] + ",";
                        }


                    }
                    MessageBox.Show("Today is the birthday of " + birthday + "!Say happy birthday to them.");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (musicfilen != "")


            {
                soundPlayer.Stop();
            }
            if (musicfilen2 != "")


            {
                soundPlayer2.Stop();
            }
            if (musicfilen3 != "")


            {
                soundPlayer3.Stop();
            }
            button1.Visible = true;
            button3.Visible = false;
            button4.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            dateTimePicker1.Visible = true;
            comboBox1.Visible = false;
            button2.Visible = false;
            richTextBox1.Visible = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            comboBox2.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            dateTimePicker2.Visible = false;
            pictureBox3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            button8.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            pictureBox4.Visible = false;
            richTextBox2.Visible = false;
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox14.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox16.ReadOnly = false;
            button16.Visible = true;
            button14.Visible = true;
            button15.Visible = false;
            label22.Visible = true;
            button13.Visible = true;
            button17.Visible = false;
            label23.Visible = false;
            button18.Visible = false;
            button19.Visible = false;
            button20.Visible = false;
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (musicfilen != "")


            {
                soundPlayer.Stop();
            }
            if (musicfilen2 != "")


            {
                soundPlayer2.Stop();
            }
            if (musicfilen3 != "")


            {
                soundPlayer3.Stop();
            }
            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            dateTimePicker1.Visible = false;
            comboBox1.Visible = false;
            button2.Visible = false;
            richTextBox1.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            comboBox2.Visible = true;
            textBox9.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;
            textBox12.Visible = true;
            textBox13.Visible = true;
            dateTimePicker2.Visible = true;
            pictureBox3.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            label16.Visible = true;
            label17.Visible = true;
            button8.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            pictureBox4.Visible = false;
            richTextBox2.Visible = false;
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox14.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox16.ReadOnly = false;
            button16.Visible = false;
            button15.Visible = false;
            button14.Visible = false;
            label22.Visible = false;
            button13.Visible = false;
            button17.Visible = false;
            label23.Visible = true;
            button18.Visible = true;
            button19.Visible = true;
            button20.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (musicfilen != "")


            {
                soundPlayer.Stop();
            }
            if (musicfilen2 != "")


            {
                soundPlayer2.Stop();
            }
            if (musicfilen3 != "")


            {
                soundPlayer3.Stop();
            }
            button1.Visible = false;
            button3.Visible = true;
            button4.Visible = false;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            dateTimePicker1.Visible = false;
            comboBox1.Visible = false;
            button2.Visible = false;
            richTextBox1.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            comboBox2.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            dateTimePicker2.Visible = false;
            pictureBox3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            button8.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            pictureBox4.Visible = false;
            richTextBox2.Visible = false;
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox14.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox16.ReadOnly = false;
            button15.Visible = false;
            button16.Visible = false;
            button14.Visible = false;
            label22.Visible = false;
            button13.Visible = false;
            button17.Visible = false;
            label23.Visible = false;
            button18.Visible = false;
            button19.Visible = false;
            button20.Visible = false;
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (musicfilen != "")


            {
                soundPlayer.Stop();
            }
            if (musicfilen2 != "")


            {
                soundPlayer2.Stop();
            }
            if (musicfilen3 != "")


            {
                soundPlayer3.Stop();
            }
            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            dateTimePicker1.Visible = false;
            comboBox1.Visible = false;
            button2.Visible = false;
            richTextBox1.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            comboBox2.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            dateTimePicker2.Visible = false;
            pictureBox3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            button8.Visible = true;
            label18.Visible = true;
            label19.Visible = true;
            label20.Visible = true;
            button15.Visible = false;
            button16.Visible = false;
            button14.Visible = false;
            label22.Visible = false;
            button13.Visible = false;
            button17.Visible = true;
            label23.Visible = false;
            button18.Visible = false;
            button19.Visible = false;
            button20.Visible = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (musicfilen != "")


            {
                soundPlayer.Stop();
            }
            if (musicfilen2 != "")


            {
                soundPlayer2.Stop();
            }
            if (musicfilen3 != "")


            {
                soundPlayer3.Stop();
            }
            button1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            dateTimePicker1.Visible = false;
            comboBox1.Visible = true;
            button2.Visible = true;
            richTextBox1.Visible = true;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            comboBox2.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            dateTimePicker2.Visible = false;
            pictureBox3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            button8.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            pictureBox4.Visible = false;
            richTextBox2.Visible = false;
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox14.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox16.ReadOnly = false;
            button14.Visible = false;
            button15.Visible = true;
            button16.Visible = false;
            button17.Visible = false;
            button18.Visible = false;
            button19.Visible = false;
            button20.Visible = false;
            label23.Visible = false;
            label22.Visible = false;
            button13.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)// το κουμπι που προσθετει μια επαφη στο Αddress Book
        {
            for (int i = 0; i <= 0; i++)
            {
                
                Check = int.TryParse(textBox3.Text, out check);
                if (Check)
                {

                    for (int j = 0; j < numbers.Count; j++)
                    {
                        if (Int32.Parse(textBox3.Text) == numbers[j])
                        {
                            sameNumber = 1;
                            break;
                        }
                    }

                }
                if (sameNumber == 1)
                {
                    sameNumber = 0;
                    MessageBox.Show("Some contact with the same number exists.Put another number.");
                    break;
                }
                 
                    if ((Check) & (((textBox4.Text.ToString() == "")) || (((IsValid(textBox4.Text) == true)))))



                {
                    
                    int x = Int32.Parse(textBox3.Text);
                    Contact contact = new Contact();
                    contact.firstName = textBox1.Text;
                    contact.lastName = textBox2.Text;
                    contact.phoneNumber = x;
                    contact.email = textBox4.Text;
                    contact.Address = textBox5.Text;
                    contact.birthday = dateTimePicker1.Value;
                    contact.picture = pictureBox1.Image;
                    contact.musicFile = musicfilen;

                    contacts.Add(contact);


                    connection.Open();
                    String query = "Insert into Table1(FirstName,LastName,PhoneNumber,Email,Address,Birthday,Picture,Music,N) " +
                        "values ('" + textBox1.Text + "','" + textBox2.Text + "','"+textBox3.Text.ToString()+"','"+textBox4.Text+ "','"+textBox5.Text+"','"+dateTimePicker1.Value.ToString()+"','"+filen+"','"+musicfilen+"','"+(contacts.Count)+"')";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    int count = command.ExecuteNonQuery();
                    connection.Close();
                    
                    

                    
                    
                        numbers.Add(contact.phoneNumber);
                    images.Add(filen);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    MessageBox.Show("Contact created.");
                    musicfilen = "";
                    pictureBox1.Image = image1;
                    comboBox1.Items.Add(contact.firstName + " " + contact.lastName+" "+"("+contact.phoneNumber.ToString()+")");
                    comboBox2.Items.Add(contact.firstName + " " + contact.lastName +" "+ "(" + contact.phoneNumber.ToString() + ")");
                    
                }
                else
                {
                    MessageBox.Show("Try Again.Some value isn't valid.(Check your email and number)");
                    
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)// το κουμπι που εμφανιζει τις λεπτομεριες των επαφων (show details)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a contact");
            }
            else
            {

                

                for (int i = 0; i < contacts.Count; i++)
                {
                    
                    if (comboBox1.Text==contacts[i].firstName+" " + contacts[i].lastName +" "+ "(" + contacts[i].phoneNumber.ToString() + ")")
                    {

                        richTextBox1.Text = "";
                        richTextBox1.AppendText("Full Name:"+ contacts[i].firstName + " " + contacts[i].lastName+ Environment.NewLine+ Environment.NewLine + "First Name:"+contacts[i].firstName + Environment.NewLine+ "Last Name:" + contacts[i].lastName + Environment.NewLine + "Number:" + contacts[i].phoneNumber+Environment.NewLine+"Email:"+ contacts[i].email+Environment.NewLine+"Address:"+ contacts[i].Address+ Environment.NewLine + "Birthday" + contacts[i].birthday.ToShortDateString());
                        pictureBox2.Image = contacts[i].picture;
                        musicfilen2 = contacts[i].musicFile;
                        if (musicfilen2 != "")
                        {
                            soundPlayer2 = new SoundPlayer(musicfilen2);
                            soundPlayer2.Play();
                        }
                        
                    }   

                }
              
            
            }
        }

       

       

        private void button3_Click(object sender, EventArgs e)// το κουμπι που διαγραφει τις επαφες(delete contact)
        {


            Check = Int32.TryParse(textBox8.Text, out check);
            if (Check) { 
            for (int i = 0; i < contacts.Count; i++)
            {
                if ((textBox6.Text==contacts[i].firstName) & (textBox7.Text == contacts[i].lastName)& (Int32.Parse(textBox8.Text)==contacts[i].phoneNumber))
                {
                  for (int j = 0; j < comboBox1.Items.Count; j++)
                      {
                          if (comboBox1.Items[j].ToString() == contacts[i].firstName + " " + contacts[i].lastName + " " + "(" + contacts[i].phoneNumber.ToString() + ")")
                            {
                                
                                comboBox1.Items.RemoveAt(j);
                                comboBox2.Items.RemoveAt(j);
                                
                            }
                      }


                        
                        connection.Open();
                        String query = "Delete from Table1 " +
                        "Where PhoneNumber=?";
                        OleDbCommand command = new OleDbCommand(query, connection);
                        command.Parameters.AddWithValue("PhoneNumber", contacts[i].phoneNumber.ToString());
                        int count = command.ExecuteNonQuery();
                        connection.Close();
                        if (contacts.Count > 1)
                        {
                            for (int k = i + 1; k < contacts.Count; k++)
                            {
                                connection.Open();

                                OleDbCommand Command = new OleDbCommand("UPDATE Table1 SET N = ? WHERE N = ?", connection);
                                Command.Parameters.AddWithValue("N", k);
                                Command.Parameters.AddWithValue("N", k + 1);
                                Command.ExecuteNonQuery();

                                connection.Close();
                            }
                        }
                        //MessageBox.Show(count.ToString() + " row affected!");
                        
                        contacts.RemoveAt(i);
                        numbers.RemoveAt(i);
                        images.RemoveAt(i);

                        Break = 1;
                   break;
                }
            }
            if (Break == 1)
                {
                    MessageBox.Show("Contact deleted.");
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    richTextBox1.Text = "";
                    pictureBox2.Image = image1;
                    pictureBox3.Image = image1;
                    Break = 0;
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";
                    musicfilen2 = "";
                }
                else
                {
                    MessageBox.Show("This contact doesn't exist.");
                }
            }
            else
            {
                MessageBox.Show("Try Again.Check your number value.");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        

        

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filen = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)//το κουμπι που επιλεγεις ποια επαφη θα επεξεργαστει(edit)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Please select a contact");
            }
            else
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    if(comboBox2.Text == contacts[i].firstName + " " + contacts[i].lastName +" "+ "(" + contacts[i].phoneNumber.ToString() + ")")
                    {
                        textBox9.Text = contacts[i].firstName;
                        textBox10.Text = contacts[i].lastName;
                        textBox11.Text = contacts[i].phoneNumber.ToString();
                        textBox12.Text = contacts[i].email;
                       textBox13.Text = contacts[i].Address;
                        dateTimePicker2.Value = contacts[i].birthday;
                        pictureBox3.Image = contacts[i].picture;
                        musicfilen4 = contacts[i].musicFile;
                        filen2 = images[i];
                        num = i;
                    }
                }

                for(int j=0;j< comboBox1.Items.Count; j++)
                {
                    if (comboBox1.Items[j].ToString()== contacts[num].firstName + " " + contacts[num].lastName +" "+ "(" + contacts[num].phoneNumber.ToString() + ")")
                    {
                        num2 = j;
                        
                    }
                }
                
                
            }
        }

        

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)// το κουμπι για να πραγματοιποιηθει η επεξεργασια μιας επαφης
        {
            for (int i = 0; i <= 0; i++) { 
            Check = int.TryParse(textBox11.Text, out check);

            if (Check)
            {

                for (int j = 0; j < numbers.Count; j++)
                {
                    if (Int32.Parse(textBox11.Text) == numbers[j])
                    {
                            if (j != num)
                            {
                                sameNumber = 1;
                                break;
                            }
                        
                    }
                }

            }
            if (sameNumber == 1)
            {
                sameNumber = 0;
                MessageBox.Show("Some contact with the same number exists.Put another number.");
                break;
            }




            if ((Check) & (((textBox12.Text.ToString() == "")) || (((IsValid(textBox12.Text) == true)))))



            {
                int x = Int32.Parse(textBox11.Text);
                contacts[num].firstName = textBox9.Text;
                contacts[num].lastName = textBox10.Text;
                contacts[num].phoneNumber = x;
                contacts[num].email = textBox12.Text;
                contacts[num].Address = textBox13.Text;
                contacts[num].birthday = dateTimePicker2.Value;
                contacts[num].picture = pictureBox3.Image;
                contacts[num].musicFile = musicfilen4;
                numbers[num] = x;
                images[num] = filen2;
                if (num2 != -1){

                    comboBox1.Items[num2] = textBox9.Text + " " + textBox10.Text+" "+"("+textBox11.Text+")";
                    comboBox2.Items[num2] = textBox9.Text + " " + textBox10.Text + " "+"(" + textBox11.Text + ")";
                }


                    //string mdfFile = @"csharpexamples.mdb";

                    //using (OleDbConnection connection = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", mdfFile)))
                    
                        
                            connection.Open();

                            OleDbCommand Command = new OleDbCommand("UPDATE Table1 SET FirstName = ?, LastName = ?, PhoneNumber = ?, Email = ?, Address = ?, Birthday = ?, Picture = ?, Music = ? WHERE N = ?", connection);
                            Command.Parameters.AddWithValue("FirstName", contacts[num].firstName);
                            Command.Parameters.AddWithValue("LastName", contacts[num].lastName);
                            Command.Parameters.AddWithValue("PhoneNumber", contacts[num].phoneNumber.ToString());
                            Command.Parameters.AddWithValue("Email", contacts[num].email);
                            Command.Parameters.AddWithValue("Address", contacts[num].Address);
                            Command.Parameters.AddWithValue("Birthday", contacts[num].birthday);
                            //contacts[num].picture = openFileDialog5.File;
                            //pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                            //if (filen2 != "")
                            //{
                                Command.Parameters.AddWithValue("Picture", filen2);
                           // }
                            
                        

                        
                           
                            Command.Parameters.AddWithValue("Music", contacts[num].musicFile);
                            Command.Parameters.AddWithValue("N", (num+1).ToString());
                            Command.ExecuteNonQuery();

                    connection.Close();


                    textBox9.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                MessageBox.Show("Contact edited.");
                pictureBox3.Image = image1;
            }
            else
            {
                MessageBox.Show("Try Again.Some value isn't valid.(Check your email and number)");

            }
            }
        }

        public bool IsValid(string emailAddress)
        {

            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

       

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filen2 = openFileDialog2.FileName;
                pictureBox3.Image = new Bitmap(openFileDialog2.FileName);
            }

        }

        

        private void button9_Click(object sender, EventArgs e)// το κουμπι search που ολοκληρωνει την αναζητηση
        {
            if (textBox14.ReadOnly == false || textBox15.ReadOnly == false || textBox16.ReadOnly == false )
            {
                MessageBox.Show("Please fill in all the required values.");
            }
            else
            {


                for (int i = 0; i < contacts.Count; i++)
                {

                    if (textBox14.Text == contacts[i].firstName & textBox15.Text==contacts[i].lastName & textBox16.Text==contacts[i].phoneNumber.ToString())
                    {
                        richTextBox2.Text = "";
                        richTextBox2.AppendText("Full Name:" + contacts[i].firstName + " " + contacts[i].lastName + Environment.NewLine + Environment.NewLine + "First Name:" + contacts[i].firstName + Environment.NewLine + "Last Name:" + contacts[i].lastName + Environment.NewLine + "Number:" + contacts[i].phoneNumber + Environment.NewLine + "Email:" + contacts[i].email + Environment.NewLine + "Address:" + contacts[i].Address + Environment.NewLine + "Birthday" + contacts[i].birthday.ToShortDateString());
                        pictureBox4.Image = contacts[i].picture;
                        musicfilen3 = contacts[i].musicFile;
                        if (musicfilen3 != "")
                        {
                            soundPlayer3 = new SoundPlayer(musicfilen3);
                            soundPlayer3.Play();
                        }
                        
                        Break = 1;
                        break;
                    }

                }
                if (Break == 1)
                {
                    Break = 0;

                }
                else
                {
                    MessageBox.Show("This contact doesn't exist");
                }
            }
        }


        // διδιακασια αναζητησης (μεχρι το button 12)
        private void button8_Click(object sender, EventArgs e)
        {
            label21.Visible = true;
            textBox14.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            textBox15.Visible = false;
            textBox16.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox14.ReadOnly = false;
            textBox15.ReadOnly = false;
            textBox16.ReadOnly = false;
            richTextBox2.Text = "";
            pictureBox4.Image = image1;
            richTextBox2.Visible = true;
            pictureBox4.Visible = true;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            label21.Text = "Type Last Name (press Ok to continue)";
            textBox14.ReadOnly = true;
            textBox15.Visible = true;
            button10.Visible = false;
            button11.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label21.Text = "Type Number (press Ok to continue)";
            textBox15.ReadOnly = true;
            textBox16.Visible = true;
            button11.Visible = false;
            button12.Visible = true;
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label21.Text = "Type First Name (press Ok to continue)";
            label21.Visible = false;
            if (int.TryParse(textBox16.Text, out check) == true){
                textBox16.ReadOnly = true;
                button12.Visible = false;
            }
            else
            {
                MessageBox.Show("Invalid nuber value.Try again.");
            }
            
        }

        

        private void button13_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                musicfilen = openFileDialog3.FileName;
                
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {   

            if (musicfilen != "")


            {
                soundPlayer = new SoundPlayer(musicfilen); 
                soundPlayer.Play();
            }
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (musicfilen2 != "")


            {
                soundPlayer2.Stop();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (musicfilen != "")


            {
                
                soundPlayer.Stop();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (musicfilen3 != "")


            {
                soundPlayer3.Stop();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (openFileDialog4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                musicfilen4 = openFileDialog4.FileName;

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (musicfilen4 != "")


            {
                soundPlayer4 = new SoundPlayer(musicfilen4);
                soundPlayer4.Play();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (musicfilen4 != "")


            {

                soundPlayer4.Stop();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
           connection.Open();
            String query = "Select * from Table2";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();
            StringBuilder builder = new StringBuilder();
            while (reader.Read())
            {
                builder.AppendLine(reader.GetString(1) + "," + reader.GetString(2));
           }
            connection.Close();
        }

        private void button22_Click(object sender, EventArgs e)
        {

            //3. Άνοιγμα του connection
connection.Open();
           MessageBox.Show(" row affected!");
           // 4. Δημιουργία ενός query
            String query = "Select * from Table1";
           // 5. Δημιουργία αντικειμένου για την εκτέλεση του query
            OleDbCommand command = new OleDbCommand(query, connection);
            //6. Εκτέλεση του query και χρήση ενός αντικειμένου για να αποθηκεύσουμε το αποτέλεσμα
            OleDbDataReader reader = command.ExecuteReader();
            //7. Ανάγνωση των επιστρεφόμενων δεδομένων
            StringBuilder builder = new StringBuilder();
            while (reader.Read())
            {
                builder.AppendLine(reader.GetString(1) + "," + reader.GetString(2));
            }
            //8. Κλείσιμο της σύνδεσης
            connection.Close();
            //MessageBox.Show(builder.ToString());
            



            
            
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                MessageBox.Show(contacts[i].firstName + "," + contacts[i].lastName + ","+contacts[i].phoneNumber.ToString());
            }
        }
    }


    }

