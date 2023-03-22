using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Programowanie_wizualne_3
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void SetDataGridView(string tb1, string tb2, string tb3)
        {
            dataGridView1.Rows.Add(tb1, tb2, tb3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Tytuł";
            dataGridView1.Columns[1].Name = "Autor";
            dataGridView1.Columns[2].Name = "Kategoria";

            Form2 form2 = new Form2(this);
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;

                var lines = File.ReadAllLines(path);
                var headers = lines.First().Split(',').Select(h => h.Trim('"')).ToArray();
                var data = lines.Skip(1)
                                .Where(row => !string.IsNullOrWhiteSpace(row))
                                .Select(row => row.Split(',').Select(c => c.Trim('"')).ToArray());

                dataGridView1.Columns.Clear();
                foreach (var header in headers)
                {
                    dataGridView1.Columns.Add(header, header);
                }

                foreach (var row in data)
                {
                    dataGridView1.Rows.Add(row);
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var headers = dataGridView1.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
            }

            string path = string.Empty;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                File.WriteAllText(path, sb.ToString().TrimEnd('\r', '\n'));
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !dataGridView1.SelectedRows[0].IsNewRow)
    {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }
    }
}
