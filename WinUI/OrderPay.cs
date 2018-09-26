using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUI
{
    public partial class OrderPay : Form
    {
        public OrderPay()
        {
            InitializeComponent();
        }

        private void OrderPay_Load(object sender, EventArgs e)
        {
            gbMember.Enabled = false;
        }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            //if(cbkMember.Checked==true)
            //{
            //    gbMember.Enabled = true;
            //}
            //建议写法
            gbMember.Enabled = cbkMember.Checked;  //这就表示单选框选中的结果赋值组控件
        }
    }
}
