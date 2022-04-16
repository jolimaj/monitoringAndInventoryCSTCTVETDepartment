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
    public partial class ReturnTools : Form
    {
        SQLConnect r = new SQLConnect();
        String sql;
        String current, id;
        public DataGridViewRow dv;
        public ReturnTools(string id1)
        {
            
            InitializeComponent();
            id = id1.ToString();
        }
        //dg
        private void dg()
        {
            string name;
            sql = "SELECT FirstName from Users where ID='" + id.ToString() + "'";
            r.DisplaySingle(sql);
            name = r.getf1();


            sql = @"SELECT ToolsList.Name as 'Equipment Name', 
                    BorrowTools.QTY, BorrowTools.BorrowDate as 'Borrow Date', BorrowTools.Time as 'Time'
                    FROM BorrowTools INNER JOIN
                    ToolsList ON BorrowTools.ToolsIID =ToolsList.ID INNER JOIN
                    Users ON BorrowTools.UserID = Users.ID where Users.FirstName='" + name.ToString() + "'and Returned='No'";
            dgItems.DataSource = r.MultipleData(sql).Tables["tbl"];

        }
        //return
        private void returnTools()
        {
            dg();
            int i;
            int qty, total, add;

            foreach (DataGridViewRow dgvr in dgItems.Rows)
            {

                sql = @"SELECT BorrowTools.ID,BorrowTools.QTY,ToolsList.ID,ToolsList.QTY from BorrowTools
                    Inner join ToolsList on ToolsList.ID=BorrowTools.ToolsIID
                    where ToolsList.Name='" + dgvr.Cells[0].Value.ToString() + "'";
                r.DisplaySingle(sql);


                sql = @"Insert into ReturnTools values('" + r.getf1().ToString() +
                    "','" + DateTime.Now.ToShortDateString() +
                    "','" + DateTime.Now.ToShortTimeString() + "')";
                r.Modify(sql);


                sql = @"Update BorrowTools set Returned='Yes' where ID='" + r.getf1().ToString() + "'";
                r.Modify(sql);


                i = Convert.ToInt16(r.getf4());
                qty = Convert.ToInt16(r.getf2());
                total = i + qty;

                sql = @"UPDATE ToolsList set QTY='" + total.ToString() + "'where ID='" + r.getf3().ToString() + "'";
                r.Modify(sql);


                MessageBox.Show("Equipment successfully returned!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void ReturnTools_Load(object sender, EventArgs e)
        {
            dg();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            returnTools();
            
            this.Hide();
           
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
           
            this.Hide();
           
        }
    }
}
