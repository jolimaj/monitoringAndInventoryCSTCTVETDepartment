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
    public partial class frmMainUsers : Form
    {
        SQLConnect r = new SQLConnect();
        String sql, uname;
        String current, o;
        public DataGridViewRow dv;
        public frmMainUsers(string username)
        {
            InitializeComponent();
            uname = username.ToString();
        }
        //dg
        private void dg()
        {
            sql = @"SELECT ID, Name, QTY, Unit, Categories, TypeEqui as 'Type'
            FROM ToolsList";
            dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }
        //search
        private void searchDg()
        {
            string ay, sp;
            sql = @"SELECT ID, Name, QTY, Unit, Categories, TypeEqui as 'Type'
            FROM ToolsList where Name like '" + txtsearch.Text + "%'";
            dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }


        private void TypeCOlor()
        {

            foreach (DataGridViewRow dgvr in dgItems.Rows)
            {

                if (dgvr.Cells[2].Value.ToString() == "0")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    if (dgvr.Cells[4].Value.ToString() == "Automotive")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.LimeGreen;
                    }
                    else if (dgvr.Cells[4].Value.ToString() == "Electrical")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                    }
                    else if (dgvr.Cells[4].Value.ToString() == "Welding")
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.SpringGreen;
                    }
                }


            }


        }
        private void Sort()
        {
            if (txtCourse.SelectedIndex == 0)
            {
                dg();
                TypeCOlor();
            }
            else if (txtCourse.SelectedIndex == 1)
            {
                sql = @"SELECT ID, Name, QTY, Unit, Categories, TypeEqui as 'Type'
                    FROM ToolsList where Categories='Automotive'";
                dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
                TypeCOlor();
            }
            else if (txtCourse.SelectedIndex == 2)
            {
                sql = @"SELECT ID, Name, QTY, Unit, Categories, TypeEqui as 'Type'
                    FROM ToolsList where Categories='Electrical'";
                dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
                TypeCOlor();
            }
            else
            {
                sql = @"SELECT ID, Name, QTY, Unit, Categories, TypeEqui as 'Type'
                    FROM ToolsList where Categories='Welding'";
                dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
                TypeCOlor();
            }
        }

        private void frmMainUsers_Load(object sender, EventArgs e)
        {
            dg();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchDg();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            searchDg();
        }

        private void txtCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {


                    if (dgItems.Rows[dgItems.CurrentRow.Index].Cells[2].Value.ToString()!="0")
                    {
                        Borrow a = new Borrow(uname.ToString());
                        a.dv = dgItems.Rows[dgItems.CurrentRow.Index];
                        a.Show();
                    }
                    else
                    {
                  
                        MessageBox.Show("This equipment is empty!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    
                
           
          
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnTools a = new ReturnTools(uname.ToString());
            a.dv = dgItems.Rows[dgItems.CurrentRow.Index];
            a.Show();
            
        }

        private void lOGOUTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Hide();
        }
    }
}
