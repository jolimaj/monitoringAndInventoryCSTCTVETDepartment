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
    public partial class Login : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        public Login()
        {
            InitializeComponent();
        }


        //validation code
        private void validate()
        {

            if (txtuname.Text == "" && txtpass.Text == "")
            {
                MessageBox.Show("Please complete all fields!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtuname.Text == "")
            {
                MessageBox.Show("Please Enter your username!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtpass.Text == "")
            {
                MessageBox.Show("Please Enter your password!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                login();
            }
        }

        
        //login code
        private void login()
        {
            string unmae, pass,role,id;
            sql = "Select Username,Password,Roles,ID from Users where Username='" + txtuname.Text + "'and Password='" + txtpass.Text + "'";
            r.DisplaySingle(sql);


            unmae = r.getf1();
            pass = r.getf2();
            role = r.getf3();
            id = r.getf4();


            if (unmae == txtuname.Text && pass == txtpass.Text)
            {

                if (role.Equals("User"))
                {
                    MessageBox.Show("Access Granted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Hide();
                    frmMainUsers a = new frmMainUsers(id.ToString());
                    a.Show();

                }
                else
                {
                    MessageBox.Show("Access Granted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    this.Hide();
                    frmAdmin a = new frmAdmin(id.ToString());
                    a.Show();

                }
                
            }
            else
            {
                MessageBox.Show("Access Denied", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtuname.Clear();
                txtpass.Clear();

            }




        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            validate();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
