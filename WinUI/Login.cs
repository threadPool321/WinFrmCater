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
    /// 登陆窗体
    /// </summary>
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Cursor = Cursors.Default;
        }
        #region 窗体事件
        //退出
        private void btnQuit_Click(object sender, EventArgs e)
        {
            //将整个应用程序退出
            Application.Exit();           
        }
        private Bll.ManagerInfoBll bll = new Bll.ManagerInfoBll();
        //登陆
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Model.ManagerInfo manager = new Model.ManagerInfo() {
                MName=txtName.Text,
                MPwd=txtPwd.Text
            };
            if(bll.Login(manager))
            {
                //登陆成功后进入主窗体
                MainForm mainForm = new MainForm();
                //一定要注意登陆成功后登陆窗口要退出的，但是因为主窗体打开了所以不能关闭调，那么
                //要做的就是让登陆窗口隐藏调，还有就是当关闭mainform时，因为登陆窗口只是隐藏掉了，其实还在执行，所以mainform
                //退出是要执行那个事件FormClosing,窗体关闭执行事件
                mainForm.Show();
                this.Visible = false;


            }
            else
            {
                MessageBox.Show("用户名或者是密码错误");
            }
        }
        #endregion

    }
}
