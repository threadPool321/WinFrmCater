using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bll;
namespace WinUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ManagerInfoBll bll = new ManagerInfoBll();
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 旧
            //string sql = "select * from ManagerInfo";
            //dataGridView1.DataSource = bll.GetManagerInfos(sql); 
            #endregion
            dataGridView1.DataSource = bll.GetManagerInfos();
        }
    }
}
