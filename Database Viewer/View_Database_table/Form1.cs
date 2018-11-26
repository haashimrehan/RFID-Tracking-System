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
using System.Data.SqlClient;
using Microsoft.VisualBasic.PowerPacks;

namespace View_Database_table
{
    public partial class Form1 : Form
    {
        List<RectangleShape> bench = new List<RectangleShape>();
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataSet ds = new DataSet();
        String cId, cEvent, cLocation, cUid;
        string valueToSearch;
        RectangleShape button = null;
        RectangleShape allBenches = null;

        public Form1()
        {
            InitializeComponent();

            bench.Add(C1B1); bench.Add(C1B2); bench.Add(C1B3); bench.Add(C1B4); bench.Add(C1B5); bench.Add(C1B6); bench.Add(C1B7); bench.Add(C1B8); bench.Add(C1B9); bench.Add(C1B10); bench.Add(C1B11); bench.Add(C1B12);
            bench.Add(C2B1); bench.Add(C2B2); bench.Add(C2B3); bench.Add(C2B4); bench.Add(C2B5); bench.Add(C2B6); bench.Add(C2B7); bench.Add(C2B8); bench.Add(C2B9); bench.Add(C2B10); bench.Add(C2B11); bench.Add(C2B12);
            bench.Add(C3B1); bench.Add(C3B2); bench.Add(C3B3); bench.Add(C3B4); bench.Add(C3B5); bench.Add(C3B6); bench.Add(C3B7); bench.Add(C3B8); bench.Add(C3B9); bench.Add(C3B10); bench.Add(C3B11); bench.Add(C3B12);
            bench.Add(C4B1); bench.Add(C4B2); bench.Add(C4B3); bench.Add(C4B4); bench.Add(C4B5); bench.Add(C4B6); bench.Add(C4B7); bench.Add(C4B8); bench.Add(C4B9); bench.Add(C4B10); bench.Add(C4B11); bench.Add(C4B12);
            bench.Add(C5B1); bench.Add(C5B2); bench.Add(C5B3); bench.Add(C5B4); bench.Add(C5B5); bench.Add(C5B6); bench.Add(C5B7); bench.Add(C5B8); bench.Add(C5B9); bench.Add(C5B10); bench.Add(C5B11); bench.Add(C5B12);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void emptyData()
        {
            DataTable DT = (DataTable)dataGridView1.DataSource;  // Clear Existing data
            if (DT != null)
            {
                DT.Clear();
            }
        }

        public void searchData(string valueToSearch)
        {
            try
            {
                if (IdcheckBox.CheckState == CheckState.Checked)
                {
                    cId = "`id`";
                }
                else { cId = null; }

                if (EventcheckBox.CheckState == CheckState.Checked)
                {
                    if (IdcheckBox.CheckState == CheckState.Checked)
                    {
                        cEvent = ",`event`";
                    }
                    else
                    {
                        cEvent = "`event`";
                    }
                }
                else { cEvent = null; }

                if (LocationcheckBox.CheckState == CheckState.Checked)
                {
                    if (EventcheckBox.CheckState == CheckState.Checked || IdcheckBox.CheckState == CheckState.Checked)
                    {
                        cLocation = ",`location`";
                    }
                    else
                    {
                        cLocation = "`location`";
                    }
                }
                else { cLocation = null; }

                if (UidcheckBox.CheckState == CheckState.Checked)
                {
                    if (EventcheckBox.CheckState == CheckState.Checked || IdcheckBox.CheckState == CheckState.Checked || LocationcheckBox.CheckState == CheckState.Checked)
                    {
                        cUid = ",`uid`";
                    }
                    else
                    {
                        cUid = "`uid`";
                    }
                }
                else
                {
                    cUid = null;
                }

                string query = "SELECT * FROM rfid.log WHERE CONCAT(" + cId + cEvent + cLocation + cUid + ") like '%" + valueToSearch + "%' ORDER BY event desc";
                command = new MySqlCommand(query, connection);
                adapter = new MySqlDataAdapter(command);
                adapter.Fill(ds, "log");
                dataGridView1.DataSource = ds.Tables["log"];
            }
            catch (Exception)
            {
                MessageBox.Show("No CheckBox Selected");
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                String user = userTextBox.Text.ToString();
                String pass = passTextBox.Text.ToString();

                connection = new MySqlConnection("datasource=localhost;port=3306;username=" + user + ";password=" + pass);

                emptyData();

                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM rfid.log ORDER BY event desc", connection);
                connection.Open();
                adapter.Fill(ds, "log");
                dataGridView1.DataSource = ds.Tables["log"];  // Add all data to table
                connection.Close();
                label1.Text = "Connected";
                label1.ForeColor = Color.Green;

                for (int it = 0; it < bench.Count; it++)
                {
                    allBenches = bench[it] as RectangleShape;
                    allBenches.Enabled = true;
                    allBenches.FillColor = Color.LightGray;
                }

                userTextBox.Visible = false;
                passTextBox.Visible = false;
                userLabel.Visible = false;
                passLabel.Visible = false;
                searchButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Access Denied\n\n" + ex.Message);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            // Empties old data in dataGridView
            emptyData();

            // Searches from textbox
            valueToSearch = textBoxValueToSearch.Text.ToString();
            searchData(valueToSearch);

            // Create array of table values for map
            String[,] Datavalue = new String[dataGridView1.Rows.Count, dataGridView1.Columns.Count];
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        Datavalue[row.Index, col.Index] = dataGridView1.Rows[row.Index].Cells[col.Index].Value.ToString();
                    }
                }
            }
            catch (Exception)
            {
                int i;
                for (int it = 0; it < bench.Count; it++)
                {
                    allBenches = bench[it] as RectangleShape;
                    allBenches.FillColor = Color.LightGray;
                }
                for (i = 0; i < bench.Count; i++)
                {
                    try
                    {
                        if (Datavalue[0, 2].ToString() == bench[i].Tag.ToString())
                        {
                            break;
                        }
                    }
                    catch (Exception) { }
                }
                bench[i].FillColor = Color.Lime;
            }
        }

        private void C1B2_Click(object sender, EventArgs e)
        {
            for (int it = 0; it < bench.Count; it++)
            {
                allBenches = bench[it] as RectangleShape;
                allBenches.FillColor = Color.LightGray;
            }

            emptyData();
            try
            {
                button = sender as RectangleShape;
                valueToSearch = button.Tag.ToString();
                button.FillColor = Color.Lime;
                searchData(valueToSearch);
            }
            catch (Exception)
            {
                // benches without tags
                MessageBox.Show("Bench Not Setup");
            }
        }

        private void textBoxValueToSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // if enter key is pressed press button
            if (e.KeyCode == Keys.Enter && searchButton.Enabled == true)
            {
                searchButton_Click(this, new EventArgs());
            }
        }
    }
}



