using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using System.IO;
namespace MonitoringAndInventory
{

    public partial class frmReports : Form
    {
        SQLConnect r = new SQLConnect();
        String sql, uname;
        String current, o;
        ReportDocument cr;
        public frmReports(string username)
        {
            InitializeComponent();
            uname = username.ToString();

        }
        private void re()
        {
            //if (dataGridView1.Rows.Count > 0)
            //{
            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Filter = "PDF(*.pdf|*.pdf)";
            //    sfd.FileName = "Reports.pdf";
            //    bool fileError = false;

            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        if(File.Exists(sfd.FileName))
            //        {
            //            try
            //            {
            //                File.Delete(sfd.FileName);
            //            }
            //            catch(IOException ex)
            //            {
            //                fileError = true;
            //                MessageBox.Show("It wasnt't possible to write the data to the disk." + ex.Message);

            //            }
            //        }
            //    }
            //    if (!fileError)
            //    {
            //        try
            //        {
            //            pdftable
            //        }
            //        catch (IOException ex)
            //        {
            //            fileError = true;
            //            MessageBox.Show("It wasnt't possible to write the data to the disk." + ex.Message);

            //        }
            //    }
            //}
            PrintDialog pr = new PrintDialog();
            printDialog1.Document = printDocument1;
            printDialog1.UseEXDialog = true;

            if (DialogResult.OK == printDialog1.ShowDialog())
            {
                printDocument1.DocumentName = "Monthly Reports";
                printDocument1.Print();
            }

        }
        //missing items reports query
        private void missingreportsQuery()
        {
            sql = @"SELECT BorrowTools.ID as 'Tools ID',ToolsList.Name, BorrowTools.QTY, BorrowTools.BorrowDate as 'Borrow Date'
            FROM BorrowTools INNER JOIN
            ToolsList ON BorrowTools.ToolsIID = ToolsList.ID INNER JOIN
            Users ON BorrowTools.UserID = Users.ID where BorrowTools.Returned='No' and BorrowDate between '" + fromDate.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "'";
            dataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];

           
        }
        private void missingreportsQuerytext()
        {


            sql = @"SELECT sum(BorrowTools.QTY)as 'Name' from BorrowTools 
            INNER JOIN ToolsList
            ToolsList ON ToolsList.ID = BorrowTools.ToolsIID where BorrowTools.Returned='No' and BorrowDate between '" + fromDate.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "'";
            r.DisplaySingle(sql);
            lblTotal.Text = r.getf1();
        }

        //inventory reports query
        private void inventoryreportsQuery()
        {
            sql = @"SELECT  ID as 'Tools ID', Name as 'Tools Name',QTY, Unit, Categories, TypeEqui as 'Type', Date
                FROM   dbo.ToolsList where Date between '" + fromDate.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "'";
            dataGridView1.DataSource = r.MultipleData(sql).Tables["tbl"];
        }
        //reports
        private void reports()
        {
            if (cmbReports.SelectedIndex == 0)
            {
                missingreportsQuery();
                missingreportsQuerytext();
                lblTotal.Visible = true;
                label5.Visible = true;
            }
            else
            {
                inventoryreportsQuery();
                lblTotal.Visible = false;
                label5.Visible = false;
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            frmAdmin a = new frmAdmin(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void lOGOUTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login a = new Login();
            a.Show();
            this.Hide();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void frmReports_Load(object sender, EventArgs e)
        {
            lblTotal.Visible = false;
            label5.Visible = false;

            
        }

        private void fromDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnReporst_Click(object sender, EventArgs e)
        {
            frmReports a = new frmReports(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (fromDate.Value == toDate.Value)
            {
                MessageBox.Show("Incorrect Date", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (fromDate.Value >toDate.Value)
            {
                MessageBox.Show("Incorrect Date", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                reports();
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            re();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSTockout a = new frmSTockout(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            frmfaculty a = new frmfaculty(uname.ToString());
            a.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            frmInventory a = new frmInventory(uname.ToString());
            a.Show();
            this.Hide();
        }
        private void printing()
        {
           

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            if (cmbReports.SelectedIndex == 0)
            {
              
                Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                dataGridView1.DrawToBitmap(bm, new Rectangle(0, 100, this.dataGridView1.Width, this.dataGridView1.Height));
                e.Graphics.DrawImage(bm, 50, 100);
                e.Graphics.DrawString("Missing Items Reports from" + " " + fromDate.Value.ToShortDateString() + " " + "to" +" "+ toDate.Value.ToShortDateString(), new Font("Verdana", 14, FontStyle.Bold), Brushes.Black, new PointF(50, 50));
                e.Graphics.DrawString(Environment.NewLine + Environment.NewLine + label5.Text + " " + lblTotal.Text, new Font("Verdana", 12, FontStyle.Regular), Brushes.Black, new PointF(50, 50));
            }
            else
            {
                Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
                dataGridView1.DrawToBitmap(bm, new Rectangle(0, 100, this.dataGridView1.Width, this.dataGridView1.Height));
                e.Graphics.DrawImage(bm, 50, 50);
                e.Graphics.DrawString("Inventory Reports from" + " " + fromDate.Value.ToShortDateString() + " " + "to" + " " + toDate.Value.ToShortDateString(), new Font("Verdana", 14, FontStyle.Bold), Brushes.Black, new PointF(50, 50));
            }
           
        }
    }
}
