using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitoringAndInventory
{
    public partial class frmSTockout : Form
    {
        SQLConnect r = new SQLConnect();
        String sql, uname;
        public frmSTockout(string username)
        {
            InitializeComponent();
            uname = username.ToString();
        }
      
      
        private void TypeCOlor()
        {

            foreach (DataGridViewRow dgvr in usersList.Rows)
            {

                if (dgvr.Cells[5].Value.ToString() == "No")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Turquoise;
                }

            }


        }
        //dg
        private void dg()
        {
            sql = @"SELECT Users.FirstName+ ' '+ Users.LastName as 'Instructor Name',ToolsList.Name as 'Equipment Name', 
                    BorrowTools.QTY, BorrowTools.BorrowDate as 'Borrow Date', BorrowTools.Time as 'Time',Returned
                    FROM BorrowTools INNER JOIN
                    ToolsList ON BorrowTools.ToolsIID =ToolsList.ID INNER JOIN
                    Users ON BorrowTools.UserID = Users.ID order by [Borrow Date] desc ";
            usersList.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }
       

        private void frmSTockout_Load(object sender, EventArgs e)
        {
           
            dg();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            frmAdmin a = new frmAdmin(uname.ToString());
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

        private void btnReporst_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnMain_Click_1(object sender, EventArgs e)
        {
            frmAdmin a = new frmAdmin(uname.ToString());
            a.Show();
            this.Hide();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sql = @"SELECT Users.FirstName+ ' '+ Users.LastName as 'Instructor Name',ToolsList.Name as 'Equipment Name', 
                    BorrowTools.QTY, BorrowTools.BorrowDate as 'Borrow Date', BorrowTools.Time as 'Time',Returned
                    FROM BorrowTools INNER JOIN
                    ToolsList ON BorrowTools.ToolsIID =ToolsList.ID INNER JOIN
                    Users ON BorrowTools.UserID = Users.ID where BorrowDate='"+dateTimePicker1.Value.ToShortDateString()+"' order by [Borrow Date] desc  ";
            usersList.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          
        }
    }
}
