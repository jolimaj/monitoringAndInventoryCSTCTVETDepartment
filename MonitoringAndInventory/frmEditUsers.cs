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
    public partial class frmEditUsers : Form
    {
        SQLConnect r = new SQLConnect();
        String sql;
        String current, uname;
        public DataGridViewRow dv;
        public frmEditUsers(string username)
        {
            InitializeComponent();

            uname = username.ToString();

        }
        //assign
        private void assign()
        {
            txtFname.Text = dv.Cells[0].Value.ToString();
            txtLname.Text = dv.Cells[1].Value.ToString();
            txtPos.Text = dv.Cells[2].Value.ToString();
            txtCourse.Text = dv.Cells[3].Value.ToString();
            txtrole.Text = dv.Cells[4].Value.ToString();
        }
       
        //add users
        private void EditUserss()
        {
            sql = @"UPDATE Users set UserName='" + txtUname.Text + "',Password='" + txtPass.Text + "',Position='" + txtPos.Text +
                "',Course='" + txtCourse.Text + "',Roles='" + txtrole.Text +
                "' where FirstName='" + txtFname.Text + "'and LastName='" + txtLname.Text + "'";
            r.Modify(sql);

            MessageBox.Show("Users sucessfully updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }
        private void frmEditUsers_Load(object sender, EventArgs e)
        {
            assign();
           
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtUname.Text == "" && txtPass.Text == "")
            {
                MessageBox.Show("Please fill out all fields!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtUname.Text == "")
            {
                MessageBox.Show("Please enter username!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Please enter password!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EditUserss();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
