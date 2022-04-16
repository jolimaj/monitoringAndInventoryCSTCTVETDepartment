using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace MonitoringAndInventory
{
    public partial class frmAdmin : Form
    {
        SQLConnect r = new SQLConnect();
        String sql, uname;
        Series sr = new Series();
        public frmAdmin(string username)
        {
            InitializeComponent();
            uname = username.ToString();
        }
       

        //chart code
        private void missingchartDisplay()
        {
            sql = @"SELECT sum(BorrowTools.QTY)as 'Missing Items' from BorrowTools 
            LEFT JOIN ToolsList
            ToolsList ON ToolsList.ID = BorrowTools.ToolsIID where Returned='No'";
            chart1.DataSource = r.MultipleData(sql).Tables["tbl"];
            chart1.Series[0].XValueMember = "Missing Items";
            chart1.Series[0].YValueMembers = "Missing Items";
        }
        private void borrowers()
        {
            sql = @"SELECT Users.FirstName as 'Name', sum(BorrowTools.QTY) as 'QTY'
            FROM   BorrowTools  LEFT JOIN
            Users ON BorrowTools.UserID = Users.ID where BorrowDate='"+DateTime.Now.ToShortDateString()+"'Group By Users.FirstName";
            chart2.DataSource = r.MultipleData(sql).Tables["tbl"];
            chart2.Series[0].XValueMember = "Name";
            chart2.Series[0].YValueMembers = "QTY";
        }
        private void dailyStockout()
        {
            sql = @"SELECT sum(BorrowTools.QTY)as 'Name' from BorrowTools 
            LEFT JOIN ToolsList
            ToolsList ON ToolsList.ID = BorrowTools.ToolsIID where Returned='No' and BorrowDate='" + DateTime.Now.ToShortDateString()+"'";
            chart3.DataSource = r.MultipleData(sql).Tables["tbl"];
            chart3.Series[0].XValueMember = "Name";
            chart3.Series[0].YValueMembers = "Name";
        }
        private void frmAdmin_Load(object sender, EventArgs e)
        {

            missingchartDisplay();
            borrowers();
            dailyStockout();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            frmInventory a = new frmInventory(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            frmfaculty a = new frmfaculty(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSTockout a = new frmSTockout(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnReporst_Click(object sender, EventArgs e)
        {

        }

        private void btnMain_Click_1(object sender, EventArgs e)
        {

        }

        private void btnInventory_Click_1(object sender, EventArgs e)
        {
            frmInventory a = new frmInventory(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnFaculty_Click_1(object sender, EventArgs e)
        {

            frmfaculty a = new frmfaculty(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            frmSTockout a = new frmSTockout(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnReporst_Click_1(object sender, EventArgs e)
        {

            frmReports a = new frmReports(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void lOGOUTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
          
        }
    }
}
