using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace Auto_sistema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// adds autos to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int kt2 = 1;
            Add_auto a = new Add_auto(textBoxNr.Text, textBoxMarke.Text, dateTimePicker1.Value, dateTimePicker2.Value, textBoxRida.Text, kt2);
            if (textBoxRida.Text == "" || textBoxNr.Text == "" || textBoxMarke.Text == "")
                MessageBox.Show("Jus neivedete duomenu!", "Pranesimas",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            else
            {
                int TextBoxNumber;
                TextBoxNumber = int.Parse(textBoxRida.Text);
                a.saveAuto();
            //    GetAutos();
                textBoxNr.Text = String.Empty;
                textBoxMarke.Text = String.Empty;
                textBoxRida.Text = String.Empty;
            }
            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            GetAutos();
        }
        /// <summary>
        /// Button cancels action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Adds employes to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {   
            Add_employee b = new Add_employee(textBoxName.Text, textBoxSurname.Text);
            if (textBoxName.Text == "" || textBoxSurname.Text == "")
                MessageBox.Show("Jus neivedete duomenu!", "Pranesimas",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            else
            {
                b.saveAuto();
                this.comboBox2.Items.Clear();
                GetNames();
                textBoxName.Text = String.Empty;
                textBoxSurname.Text = String.Empty;
            } 
        }
        /// <summary>
        /// Button cancels action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Rents a car, updates database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                int autoId = Convert.ToInt16(((ComboboxItem)comboBox1.SelectedItem).Value);
                int darbId = Convert.ToInt16(((ComboboxItem)comboBox2.SelectedItem).Value);
                int kt = 0;
                Rent_auto c = new Rent_auto(dateTimePicker3.Value, autoId, darbId, kt);
                c.updateAutoRow();
                comboBox3.Items.Add(comboBox1.SelectedItem);
                comboBox1.Items.Remove(comboBox1.SelectedItem);
                comboBox2.Items.Remove(comboBox2.SelectedItem);
            }
        }
        /// <summary>
        /// Button cancels action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Returns auto, updates database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                int kuras = Convert.ToInt32(textBoxRidapo.Text) - Convert.ToInt32(label14.Text);
                int kt1 = 1;
                int id = Convert.ToInt16(((ComboboxItem)comboBox3.SelectedItem).Value);

                Return_auto d = new Return_auto(dateTimePicker4.Value, textBoxRidapo.Text, kt1, label14.Text, kuras.ToString(), id);

                d.g_laikas();

                comboBox1.Items.Add(comboBox3.SelectedItem);
                comboBox3.Items.Remove(comboBox3.SelectedItem);

                textBoxRidapo.Text = String.Empty;
                label14.Text = String.Empty;
            }
        }
        /// <summary>
        ///  Button cancels action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();           
        }
        /// <summary>
        /// Launches methods on form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            GetAutos();
            GetNames();
            updateDataGridView();
            updateDataGridView2();
        }
        /// <summary>
        /// Method for getting information from database about autos
        /// </summary>
        private void GetAutos()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string qstring = "select Id,Valstybinis_Nr, K_t from [Lenta]";
                using (SqlCommand cmd = new SqlCommand(qstring, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            ComboboxItem item = new ComboboxItem();
                            item.Text = reader[1].ToString();
                            item.Value = Convert.ToInt16(reader[0]);
                            int a = Convert.ToInt16(reader[2]);
                                                      
                            comboBox4.Items.Add(item);
                            //Check a non empty 
                            if (a == 0)
                                comboBox3.Items.Add(item);
                            else
                                comboBox1.Items.Add(item);
                        }
                        comboBox4.Sorted = true;
                        comboBox3.Sorted = true;
                        comboBox2.Sorted = true;
                        comboBox1.Sorted = true;
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Method for getting information from database about employees
        /// </summary>
        private void GetNames()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string qstring = "select Id,Pavarde from [Darbuotojai]";
                using (SqlCommand cmd = new SqlCommand(qstring, conn))

                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            ComboboxItem item = new ComboboxItem();
                            item.Text = reader[1].ToString();
                            item.Value = Convert.ToInt16(reader[0]);
                            comboBox2.Items.Add(item);
                        }
                        comboBox2.Sorted = true;
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    MessageBox.Show("a");
        }
        /// <summary>
        /// Method for getting value in label when selected item in combobox3 changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string qstring = "select Automobilio_rida_pries, Automobilio_rida_po from [Lenta] where id = @comboid";

                using (SqlCommand cmd = new SqlCommand(qstring, conn))

                {
                    int id = Convert.ToInt16(((ComboboxItem)comboBox3.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@comboid",id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            int a = Convert.ToInt32(reader[1]);

                            if (a == 0) 
                            label14.Text = reader.GetValue(0).ToString();
                            else
                            label14.Text = reader.GetValue(1).ToString();
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Method sets info to gridview1 from database
        /// </summary>
        private void updateDataGridView()
        {         
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                //string qstring = "select Id, Valstybinis_Nr, Automobilio_marke, Tech_apziuros_galiojimo_laikas, Draudimo_galiojimo_laikas, Automobilio_rida_pries," +
                //  "AsmuoId_FK, Automobilio_paemimo_data, Automobilio_grazinimo_data, Remonto_islaidos, Kuro_islaidos, Kitos_islaidos, Automobilio_rida_po from [Lenta]";
                //string qstring2 = "select * from Info";
                // string qstring = "select Valstybinis_Nr, Automobilio_marke, Tech_apziuros_galiojimo_laikas, Draudimo_galiojimo_laikas, Automobilio_rida_pries, Vardas,Pavarde, Automobilio_paemimo_data, Automobilio_grazinimo_data, Remonto_islaidos, Kuro_islaidos, Kitos_islaidos, Automobilio_rida_po from [Lenta] inner join Darbuotojai on (Lenta.AsmuoId_FK=Darbuotojai.Id or Lenta.AsmuoId_FK is NULL) ";
                string qstring = "select Valstybinis_Nr, Automobilio_marke, Tech_apziuros_galiojimo_laikas, Draudimo_galiojimo_laikas, Automobilio_rida_pries, Vardas,Pavarde, Automobilio_paemimo_data, Automobilio_grazinimo_data, Remonto_islaidos, Kuro_islaidos, Kitos_islaidos, Automobilio_rida_po from [Lenta] left join Darbuotojai on Lenta.AsmuoId_FK=Darbuotojai.Id";
                using (SqlCommand cmd = new SqlCommand(qstring, conn))
                {
                    DataTable table = new DataTable();

                    table.Load(cmd.ExecuteReader());

                    dataGridView1.DataSource = table;
                }
                //Hide Id column
                //dataGridView1.Columns[0].Visible = false;
            }
        }
        /// <summary>
        /// Button updates info in gridview after changes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            updateDataGridView();    
        }
        /// <summary>
        /// Button deletes row from gridview1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
        //    int idtodelete = dataGridView1.Cells dataGridView1.SelectedRows[0] .Index;
            DataGridViewRow selectedRow = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index];
           int idtodelete = Convert.ToInt32(selectedRow.Cells["Automobilio_rida_pries"].Value);

            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string qstring = "delete from [Lenta] where Automobilio_rida_pries = @Automobilio_rida_pries ";

                using (SqlCommand cmd = new SqlCommand(qstring, conn))
                {
                    if (MessageBox.Show("Ar tikrai norite istrinti?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cmd.Parameters.AddWithValue("@Automobilio_rida_pries", idtodelete);
                        SqlDataReader reader = cmd.ExecuteReader();
                        updateDataGridView();
                    }
                    comboBox1.Items.Clear();
                    comboBox3.Items.Clear();
                    comboBox4.Items.Clear();
                    GetAutos();
                }
            }
        }
        /// <summary>
        /// Button adds data about expences to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddExpenses_Click(object sender, EventArgs e)
        {
            expenses b = new expenses(textBoxRemontas.Text, textBoxKitos.Text);
           /* if (textBoxRemontas.Text == "" || textBoxKitos.Text == "")
                MessageBox.Show("Jus neivedete duomenu!", "Pranesimas",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);*/
            if (comboBox4.SelectedItem != null)
            {
                int a = Convert.ToInt16(((ComboboxItem)comboBox4.SelectedItem).Value);
               b.add_expenses(a);
               
            }
            textBoxRemontas.Text = String.Empty;
            textBoxKitos.Text = String.Empty;
            comboBox4.Items.Remove(comboBox4.SelectedItem);
        }
        /// <summary>
        /// Button cancels action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Method sets info to gridview2 from database
        /// </summary>
        private void updateDataGridView2()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string qstring = "select Id, Vardas, Pavarde from [Darbuotojai]";
                //string qstring2 = "select * from Info";
                // string qstring = "select Valstybinis_Nr, Automobilio_marke, Tech_apziuros_galiojimo_laikas, Draudimo_galiojimo_laikas, Automobilio_rida_pries, Vardas,Pavarde, Automobilio_paemimo_data, Automobilio_grazinimo_data, Remonto_islaidos, Kuro_islaidos, Kitos_islaidos, Automobilio_rida_po from [Lenta] inner join Darbuotojai on (Lenta.AsmuoId_FK=Darbuotojai.Id or Lenta.AsmuoId_FK is NULL) ";
               // string qstring = "select Valstybinis_Nr, Automobilio_marke, Tech_apziuros_galiojimo_laikas, Draudimo_galiojimo_laikas, Automobilio_rida_pries, Vardas,Pavarde, Automobilio_paemimo_data, Automobilio_grazinimo_data, Remonto_islaidos, Kuro_islaidos, Kitos_islaidos, Automobilio_rida_po from [Lenta] left join Darbuotojai on Lenta.AsmuoId_FK=Darbuotojai.Id";
                using (SqlCommand cmd = new SqlCommand(qstring, conn))
                {
                    DataTable table = new DataTable();

                    table.Load(cmd.ExecuteReader());

                    dataGridView2.DataSource = table;
                }
                //Hide Id column
                dataGridView2.Columns[0].Visible = false;
            }
        }
        /// <summary>
        /// Button deletes row from gridview2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.Rows[dataGridView2.SelectedRows[0].Index];
            int idtodelete = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);

            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string qstring = "delete from [Darbuotojai] where Id = @Id ";

                using (SqlCommand cmd = new SqlCommand(qstring, conn))
                {
                    if (MessageBox.Show("Ar tikrai norite istrinti?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        cmd.Parameters.AddWithValue("@Id", idtodelete);
                        SqlDataReader reader = cmd.ExecuteReader();
                        updateDataGridView();
                    }
                    comboBox2.Items.Clear();      
                    GetNames();
                }
            }
        }
        /// <summary>
        /// Button cancels action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
