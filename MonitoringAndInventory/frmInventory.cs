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
    public partial class frmInventory : Form
    {
        SQLConnect r = new SQLConnect();
        String sql, uname;
        String current, o;
        public frmInventory(string username)
        {
            InitializeComponent();
            uname = username.ToString();

        }
        //dg
        private void dg()
        {
            sql = @"SELECT ID, Name, Specification, AcquisitionYear, QTY, Unit, Categories, TypeEqui as 'Type'
            FROM ToolsList";
            dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }
        //search
        private void searchDg()
        {
            string ay, sp;
            sql = @"SELECT ID, Name, Specification, AcquisitionYear, QTY, Unit, Categories, TypeEqui as 'Type'
            FROM ToolsList where Name = '" + txtsearch.Text + "'";
            dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }

        private void TypeCOlor()
        {

            foreach (DataGridViewRow dgvr in dgItems.Rows)
            {

                if (dgvr.Cells[6].Value.ToString() == "Automotive")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.LimeGreen;
                }
                else if (dgvr.Cells[6].Value.ToString() == "Electrical")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.SeaGreen;
                }
                else if (dgvr.Cells[6].Value.ToString() == "Welding")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.SpringGreen;
                }
            }


        }
       
        //edit
        private void Sort()
        {
            if (comboBox1.SelectedIndex == 0)
            {
                sql = @"SELECT ID, Name, Specification, AcquisitionYear, QTY, Unit, Categories, TypeEqui as 'Type'
                FROM ToolsList where Categories='Automotive'";
                dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
                TypeCOlor();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                sql = @"SELECT ID, Name, Specification, AcquisitionYear, QTY, Unit, Categories, TypeEqui as 'Type'
                FROM ToolsList where Categories='Electrical'";
                dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
                TypeCOlor();
            }
            else
            {
                sql = @"SELECT ID, Name, Specification, AcquisitionYear, QTY, Unit, Categories, TypeEqui as 'Type'
                FROM ToolsList where Categories='Welding'";
                dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];
                TypeCOlor();
            }
        }
        //validation
        private void Validation()
        {
            if ((txtName.Text == "") || (txtUnit.Text == "") || (txtType.Text == ""))
            {
                MessageBox.Show("Please fill out the field!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                AddItems();
            }
        }
        //clear
        private void clear()
        {
            txtName.Text = "";
            txtSpecification.Text = "";
            txtAY.Text = "";
            txtUnit.Text = "";
            txtCat.Text = "";
            txtType.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            txtQty.Value = 0;
        }

        //add
        private void AddItems()
        {
            string na, spe;

            if ((checkBox1.Checked == false) && (checkBox2.Checked == true))
            {
                na = "N/A";
                txtAY.Text = na.ToString();
            }
            else if ((checkBox1.Checked == true) && (checkBox2.Checked == false))
            {
                spe = "N/A";
                txtSpecification.Text = spe.ToString();
            }
            else if ((checkBox1.Checked == true) && (checkBox2.Checked == true))
            {
                na = "N/A";
                txtAY.Text = na.ToString();
                spe = "N/A";
                txtSpecification.Text = spe.ToString();
            }


            sql = @"Insert into ToolsList values('" + txtName.Text + "','"
            + txtSpecification.Text + "','"
            + txtAY.Text + "','"
            + txtQty.Value.ToString() + "','"
            + txtUnit.Text + "','"
            + txtCat.Text + "','"
            + txtType.Text + "','"
            +DateTime.Now.ToShortDateString()+"')";
            r.Modify(sql);
            MessageBox.Show("Items sucessfully added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clear();
            dg();

        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
          
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            dg();
            TypeCOlor();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                dg();
                
            }
            else
            {
                searchDg();
               
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            searchDg();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sort();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Validation();
           
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdmin a = new frmAdmin(uname.ToString());
            a.Show();
        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmfaculty a = new frmfaculty(uname.ToString());
            a.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id;
            sql = @"Select ID,Specification,AcquisitionYear from ToolsList where Name='" + dgItems.Rows[dgItems.CurrentRow.Index].Cells[1].Value.ToString() + "'";
            r.DisplaySingle(sql);
            id = r.getf1();

            EditItems a = new EditItems(uname.ToString(), id.ToString());
            a.dv = dgItems.Rows[dgItems.CurrentRow.Index];
            a.Show();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSTockout a = new frmSTockout(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnReporst_Click(object sender, EventArgs e)
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
    }
}
