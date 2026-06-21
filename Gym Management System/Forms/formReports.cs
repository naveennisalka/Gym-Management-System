using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace Gym_Management_System.Forms
{
    public partial class formReports : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        PrintDocument printDoc = new PrintDocument();


        DataTable dtActiveReport = null;

        public formReports()
        {
            InitializeComponent();
            LoadReportTypes();
        }

        private void LoadReportTypes()
        {
            cbReportType.Items.Clear();
            cbReportType.Items.AddRange(new string[] { "Inventory Alerts", "Membership Expiry", "Maintenance Ledger", "Monthly Sales & Revenue Summary" });
            cbReportType.SelectedIndex = 0;
        }

        private DataTable FetchReportData(string reportType)
        {
            DataTable dt = new DataTable();
            string query = "";

            if (reportType == "Inventory Alerts")
            {
                query = @"SELECT id AS [Item ID], name AS [Item Name], stock AS [Stock], 
                                 min_stock_level AS [Min Level], category AS [Category] 
                          FROM store_items 
                          WHERE stock <= min_stock_level AND category = 'GymItem'";
            }
            else if (reportType == "Membership Expiry")
            {
                query = @"SELECT u.id AS [Member ID], u.name AS [Name], gp.name AS [Plan], 
                                 m.start_date AS [Start Date], m.end_date AS [Expiry Date]
                          FROM memberships m
                          INNER JOIN users u ON m.user_id = u.id
                          INNER JOIN gym_plans gp ON m.plan_id = gp.id
                          WHERE m.end_date >= GETDATE()
                          ORDER BY m.end_date ASC";
            }
            else if (reportType == "Maintenance Ledger")
            {
                query = @"SELECT s.service_id AS [Service ID], e.name AS [Equipment], 
                                 s.service_date AS [Service Date], s.service_type AS [Type], 
                                 s.cost AS [Cost (Rs.)]
                          FROM service_log s
                          INNER JOIN equipment e ON s.equipment_id = e.id
                          ORDER BY s.service_date DESC";
            }
            else if (reportType == "Monthly Sales & Revenue Summary")
            {
                
                query = @"SELECT 
                FORMAT(order_date, 'yyyy - MMMM') AS [Billing Period],
                COUNT(id) AS [Total Transactions],
                SUM(total_amount) AS [Gross Revenue (Rs.)],
                AVG(total_amount) AS [Average Ticket Size]
              FROM orders 
              WHERE status = 1
              GROUP BY FORMAT(order_date, 'yyyy - MMMM'), YEAR(order_date), MONTH(order_date)
              ORDER BY YEAR(order_date) DESC, MONTH(order_date) DESC";
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Load Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontTitle = new Font("Arial", 16, FontStyle.Bold);
            Font fontSub = new Font("Arial", 10, FontStyle.Italic);
            Font fontHeader = new Font("Arial", 10, FontStyle.Bold);
            Font fontBody = new Font("Arial", 9, FontStyle.Regular);

            int xStart = 40;
            int yStart = 40;

            // Dynamically adjust column spacing allocations to fit wide financial numbers
            int colWidth = (e.PageBounds.Width - 80) / dtActiveReport.Columns.Count;

            //Draw Report Header Typography Block
            g.DrawString("MAHARAJA GYM NETWORK", fontTitle, Brushes.Black, xStart, yStart);
            yStart += 25;
            g.DrawString($"Official System Registry Report: {cbReportType.SelectedItem}", fontHeader, Brushes.DarkGray, xStart, yStart);
            yStart += 20;
            g.DrawString($"Generated on: {DateTime.Now:F}", fontSub, Brushes.Gray, xStart, yStart);
            yStart += 30;

            // Draw horizontal dividing bar
            g.DrawLine(Pens.Black, xStart, yStart, e.PageBounds.Width - 40, yStart);
            yStart += 20;

            //Render Data Table Headers Grid Rows
            int currentX = xStart;
            for (int i = 0; i < dtActiveReport.Columns.Count; i++)
            {
                // Dark theme color header matching high-end administrative layouts
                g.FillRectangle(Brushes.DarkSlateGray, currentX, yStart, colWidth, 25);
                g.DrawRectangle(Pens.Black, currentX, yStart, colWidth, 25);
                g.DrawString(dtActiveReport.Columns[i].ColumnName, fontHeader, Brushes.White, currentX + 4, yStart + 5);
                currentX += colWidth;
            }
            yStart += 25;

            //Render Data Table Rows loop logic
            for (int r = 0; r < dtActiveReport.Rows.Count; r++)
            {
                currentX = xStart;
                for (int c = 0; c < dtActiveReport.Columns.Count; c++)
                {
                    string cellText = dtActiveReport.Rows[r][c] != DBNull.Value ? dtActiveReport.Rows[r][c].ToString() : "";

                    // Format dates safely
                    if (dtActiveReport.Rows[r][c] is DateTime dVal)
                    {
                        cellText = dVal.ToString("yyyy-MM-dd");
                    }
                    // Format monetary decimal values beautifully
                    else if (dtActiveReport.Rows[r][c] is decimal decVal)
                    {
                        cellText = decVal.ToString("N2");
                    }

                    // Alternating zebra pattern rows background
                    if (r % 2 != 0)
                    {
                        g.FillRectangle(Brushes.WhiteSmoke, currentX, yStart, colWidth, 22);
                    }

                    g.DrawRectangle(Pens.LightGray, currentX, yStart, colWidth, 22);
                    g.DrawString(cellText, fontBody, Brushes.Black, currentX + 4, yStart + 4);

                    currentX += colWidth;
                }
                yStart += 22;

                // Check page overflow boundaries
                if (yStart > e.PageBounds.Height - 60)
                {
                    g.DrawString("[Report truncated due to length limits]", fontSub, Brushes.Red, xStart, yStart + 10);
                    break;
                }
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string selectedReport = cbReportType.SelectedItem.ToString();
            dtActiveReport = FetchReportData(selectedReport);

            if (dtActiveReport == null || dtActiveReport.Rows.Count == 0)
            {
                dgvReportPreview.DataSource = null;
                btnExportPdf.Enabled = false;
                MessageBox.Show("No records found matching the specified report criteria.", "Empty Dataset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Bind the dataset right into your UI preview grid
            dgvReportPreview.DataSource = dtActiveReport;
            btnExportPdf.Enabled = true; 
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (dtActiveReport == null || dtActiveReport.Rows.Count == 0) return;

            using (PrintDialog printDlg = new PrintDialog())
            {
                printDlg.Document = printDoc;

                // Instruct the system to automatically look for 'Microsoft Print to PDF'
                MessageBox.Show("Please select 'Microsoft Print to PDF' in the upcoming dialog to save your document as a PDF.", "Exporting Report", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print(); 
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
