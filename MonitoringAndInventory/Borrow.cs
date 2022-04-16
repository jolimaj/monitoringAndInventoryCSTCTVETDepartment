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
    public partial class Borrow : Form
    {

        SQLConnect r = new SQLConnect();
        String sql;
        String current, uname;
        public DataGridViewRow dv;
        public Borrow(string username)
        {
            InitializeComponent();
            
            uname = username.ToString();
        }

        //from datagrid
        private void fromDG()
        {
            int i;

            txtID.Text = dv.Cells[0].Value.ToString();
            txtName.Text = dv.Cells[1].Value.ToString();
            i = Convert.ToInt16(dv.Cells[2].Value);
            txtUnit.Text = dv.Cells[3].Value.ToString();
            txtCat.Text = dv.Cells[4].Value.ToString();
            txtType.Text = dv.Cells[5].Value.ToString();
        }

        //submit borrow
        private void Borrows()
        {
            fromDG();
            int i;
            int qty, total;
            i = Convert.ToInt16(dv.Cells[2].Value);


            if (txtQty.Value > i)
            {
                MessageBox.Show("Tools quantity is insufficient!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sql = @"Insert into BorrowTools values('" + txtID.Text +
                              "','" + uname.ToString() +
                              "','" + txtQty.Value.ToString() +
                              "','" + DateTime.Now.ToShortDateString() +
                              "','" + DateTime.Now.ToShortTimeString() + "','No')";
                r.Modify(sql);

                qty = Convert.ToInt16(txtQty.Value);
                total = i - qty;


                sql = @"UPDATE ToolsList set QTY='" + total.ToString() + "',Date='"+ DateTime.Now.ToShortDateString()+"' where ID='" + txtID.Text + "'";
                r.Modify(sql);


                MessageBox.Show("Done!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Hide();
            }

          

        }
        private void Borrow_Load(object sender, EventArgs e)
        {
            fromDG();

           
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

          
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Borrows();
        }
    }
}
