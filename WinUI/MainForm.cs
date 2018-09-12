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
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        #region 窗体事件
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //窗体加载时确定订单
          
            if(this.Tag!= null && this.Tag.ToString() == "0")
            {
                //表示是店员的权限
                menuManager.Visible = false;
            }

        }
        //退出
        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void menuManager_Click(object sender, EventArgs e)
        {
            //显示出管理员列表
            ManagerInfoList mif = FrmSingletonFactory.CreateInstance();
            mif.Show();
            mif.Activate();
        }
        //会员类型表
        private void menuMember_Click(object sender, EventArgs e)
        {
            MemberInfoList mti = FrmSingletonFactory.CreateMemberInstance();
            mti.Show();
            mti.Activate();
           // mti.Focus();
        }
    }
}
