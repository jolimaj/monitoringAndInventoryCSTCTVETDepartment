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
    public partial class frmfaculty : Form
    {
        SQLConnect r = new SQLConnect();
        String sql, uname;
        String current, o;
        public frmfaculty(string username)
        {
            InitializeComponent();
            uname = username.ToString();

        }

     

        //dg
        private void dg()
        {
            sql = @"SELECT FirstName as 'Name', LastName as 'Lastname',  Position, Course as 'Course', Roles as 'Role'
            FROM Users where ID<>'" + uname.ToString() + "'";
            dgUsers.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();
        }
        private void TypeCOlor()
        {

            foreach (DataGridViewRow dgvr in dgUsers.Rows)
            {

                if (dgvr.Cells[4].Value.ToString() == "Administrator")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Green;
                }
                else if (dgvr.Cells[4].Value.ToString() == "User")
                {
                    dgvr.DefaultCellStyle.BackColor = Color.LightGreen;
                }
               
            }


        }
       
        //search Users
        private void searchdg()
        {
            sql = @"SELECT FirstName as 'Name', LastName as 'Lastname',  Position, Course as 'Course Assign', Roles as 'Role'
            FROM Users where ID<>'" + uname.ToString() + "'and FirstName='" + txtsearch.Text + "'or LastName='" + txtsearch.Text + "'";

            //multiple
            dgUsers.DataSource = r.MultipleData(sql).Tables["tbl"];
            TypeCOlor();


        }
        //add users
        private void addUsers()
        {
            string fname, lname, uname;
            sql = @"SELECT FirstName,LastName,UserName FROM Users where FirstName='" + txtFname.Text +
                "' and LastName='" + txtLname.Text + "'or UserName='" + txtUname.Text + "'";
            r.DisplaySingle(sql);
            fname = r.getf1();
            lname = r.getf2();
            uname = r.getf3();


            if ((txtFname.Text != fname) && (txtLname.Text != lname) || (txtUname.Text != uname))
            {
                sql = @"INSERT into Users values('" + txtFname.Text + "','"
                + txtLname.Text + "','"
                + txtUname.Text + "','"
                + txtPass.Text + "','"
                + txtPos.Text + "','"
                + txtCourse.Text + "','"
                + txtrole.Text + "')";
                r.Modify(sql);
                clear();
                dg();
                MessageBox.Show("Users sucessfully added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtFname.Text == "" || txtLname.Text == "" || txtUname.Text == "" || txtPass.Text == "")
            {

                MessageBox.Show("Please Complete All Field!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                MessageBox.Show("Username already taken!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        //clear
        private void clear()
        {
                txtFname.Text ="";
                txtLname.Text ="";
                txtUname.Text ="";
                txtPass.Text ="";
                txtPos.Text ="Head";
                txtCourse.Text = "Automotive";
                txtrole.Text = "Administrator";
            
        }
        private void frmfaculty_Load(object sender, EventArgs e)
        {
            dg();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdmin a = new frmAdmin(uname.ToString());
            a.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmInventory a = new frmInventory(uname.ToString());
            a.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            addUsers();

        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {

        }

        private void btnInventory_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmInventory a = new frmInventory(uname.ToString());
            a.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSTockout a = new frmSTockout(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnMain_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmAdmin a = new frmAdmin(uname.ToString());
            a.Show();
        }

        private void btnReporst_Click(object sender, EventArgs e)
        {

        }

        private void btnFaculty_Click_1(object sender, EventArgs e)
        {

        }

        private void btnMain_Click_2(object sender, EventArgs e)
        {

            frmAdmin a = new frmAdmin(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnInventory_Click_2(object sender, EventArgs e)
        {

            frmInventory a = new frmInventory(uname.ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            frmEditUsers a = new frmEditUsers(uname.ToString());
            a.dv = dgUsers.Rows[dgUsers.CurrentRow.Index];
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
