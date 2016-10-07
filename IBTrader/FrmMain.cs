using System.Windows.Forms;
using IB.Db.Function;

namespace IBTrader
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var item = FnIntraDayData.GetAllByTickerIdWithDate(200, date1.Value, date2.Value);
            IntraDayDatabds.DataSource = item;
          
        }
    }
}
