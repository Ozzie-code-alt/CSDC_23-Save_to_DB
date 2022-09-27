using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CSDC23SaveToDB
{
    public partial class studentForm : Form
    {
        public studentForm()
        {
            InitializeComponent();
        }

        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;
        DataSet DS = new DataSet();
        DataTable DT =  new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void setConnection()
        {
            sql_con = new SQLiteConnection("Data Source = StudentProfileDB.db");
        }

        private void loadData()
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from Student";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dgvStudent.DataSource = DT;
            sql_con.Close();
        }

        private void executeQuery(string txtQuery)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string txtQuery = "insert into Student (studentID,firstName, lastName, mdName, gender, birthDate) values('"+txtStudID.Text+"','"+txtFF.Text+ "','" + txtLN.Text + "','" + txtMN.Text + "','" + cmbGender.Text + "','" + dtpBirthdate.Value.ToShortDateString() + "')";

            executeQuery(txtQuery);
            loadData();

            MessageBox.Show("Student profile has been successfully stored");
            txtStudID.Clear();
            txtFF.Clear();
            txtLN.Clear();
            txtMN.Clear();
            dtpBirthdate.Value = DateTime.Now;
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStudID.Text = dgvStudent.SelectedRows[0].Cells[1].Value.ToString();
            txtFF.Text = dgvStudent.SelectedRows[0].Cells[2].Value.ToString();
            txtLN.Text = dgvStudent.SelectedRows[0].Cells[3].Value.ToString();
            txtMN.Text = dgvStudent.SelectedRows[0].Cells[4].Value.ToString();
            cmbGender.Text = dgvStudent.SelectedRows[0].Cells[5].Value.ToString();
            dtpBirthdate.Text = dgvStudent.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string txtQuery = "update student set firstName = '" + txtFF.Text + "',lastName = '" + txtLN.Text + "',mdName= '" + txtMN.Text + "',gender = '" + cmbGender.Text + "',birthDate = '" + dtpBirthdate.Value.ToShortDateString() + "' where studentID = '"+txtStudID.Text+"'";

            executeQuery(txtQuery);
            loadData();

            MessageBox.Show("Student profile has been successfully updated");
            txtStudID.Clear();
            txtFF.Clear();
            txtLN.Clear();
            txtMN.Clear();
            dtpBirthdate.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStudID.Clear();
            txtFF.Clear();
            txtLN.Clear();
            txtMN.Clear();
            dtpBirthdate.Value = DateTime.Now;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string txtQuery = "delete from student where studentID = '" + txtStudID.Text + "'";
            executeQuery(txtQuery);
          
            loadData();

            MessageBox.Show("Student profile has been successfully deleted");
            txtStudID.Clear();
            txtFF.Clear();
            txtLN.Clear();
            txtMN.Clear();
            dtpBirthdate.Value = DateTime.Now;
        }
    }
}
