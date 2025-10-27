using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameGK
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        public void DisplayHistory(List<Tuple<int, DateTime>> history)
        {
            lstHistory.Items.Clear();
            if (history.Count == 0)
            {
                lstHistory.Items.Add("Chưa có lịch sử chơi.");
                return;
            }

            foreach (var entry in history)
            {
                string line = string.Format("Điểm: {0, -8} | Ngày: {1}", entry.Item1, entry.Item2.ToString("dd/MM/yyyy HH:mm"));
                lstHistory.Items.Add(line);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
