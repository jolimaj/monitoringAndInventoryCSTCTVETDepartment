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
    public partial class EditItems : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        String current, uname;
        public DataGridViewRow dv;
        public EditItems(string username, string id)
        {
            InitializeComponent();
            current = id.ToString();
            uname = username.ToString();
         
        }
        //from dg
        string id;
        private void fromDG()
        {
            
            id = dv.Cells[0].Value.ToString();
            txtName.Text = dv.Cells[1].Value.ToString();
            txtSpecification.Text = dv.Cells[2].Value.ToString();
            txtAY.Text = dv.Cells[3].Value.ToString();
            txtQty.Value = Convert.ToDecimal(dv.Cells[4].Value);
            txtUnit.Text = dv.Cells[5].Value.ToString();
            txtCat.Text= dv.Cells[6].Value.ToString();
            txtType.Text = dv.Cells[7].Value.ToString();
        }
  
        //edit code
        private void edit()
        {

            sql = @"UPDATE ToolsList set QTY='" + txtQty.Value.ToString() + "'where ID='" + dv.Cells[0].Value.ToString() + "'";
            r.Modify(sql);
            MessageBox.Show("Items sucessfully updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmInventory a = new frmInventory(uname.ToString());
            this.Hide();
            a.Show();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
           
            this.Hide();
           
        }

        private void EditItems_Load(object sender, EventArgs e)
        {
            fromDG();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventory a = new frmInventory(uname.ToString());
            this.Hide();
            a.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void txtAY_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
