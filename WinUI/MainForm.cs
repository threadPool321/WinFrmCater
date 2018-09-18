﻿using System;
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
            //加载标签页
            LoadHallTab();
        }
        /// <summary>
        /// 动态的添加tabPage,一个tabControl包含多个tabPage
        /// </summary>
        private void LoadHallTab()
        {
            Bll.HillInfoBll bll = new Bll.HillInfoBll();
            var items = bll.GetDishInfos();
            foreach (var item in items)
            {
                TabPage tabPage = new TabPage(item.HTitle);
                tabPage.Tag = item.Hid;             //拿到这个听包到时查找餐桌
                tabHill.TabPages.Add(tabPage);
            }
            tabControl1_SelectedIndexChanged(null,null);
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
        //菜单
        private void menudish_Click(object sender, EventArgs e)
        {
            //使用单例模式
            DishInfo dish = FrmSingletonFactory.CreateInstacne();

            dish.Show();
            dish.Activate();
        }

        private void menuTable_Click(object sender, EventArgs e)
        {
            TableInfoFrm table = FrmSingletonFactory.CreateInstace();
            table.Show();
            table.Activate();
        }
        //tabcontrol，移除选项卡手动添加标签
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //操作过程:选择一个tabPage,然后根据当前选中的TabPage存储的厅包编号,查找里面的餐桌,然后创建ListView,加入所有的餐桌,
            //再将ListView加到当前选中的TagPage
            //1\获取选中的tabPage
            var tabPage = tabHill.SelectedTab;
            //查出所有餐桌的信息
            Bll.TableInfoBll bll = new Bll.TableInfoBll();
            Model.TableInfo table = new Model.TableInfo();
            table.THallId = Convert.ToInt32(tabHill.SelectedTab.Tag);   //厅包的条件
            table.TIsFree = -1;                                         //得到是否空闲-1表示全部
            var list = bll.GetTableInfos(table);


            //把这个集合放到listview中
            //构建listView
            ListView listView = new ListView();
            listView.LargeImageList = imageList1;
            listView.Dock = DockStyle.Fill;
            listView.MultiSelect = false; //不能进行多选
            foreach (var item in list)
            {
                ListViewItem lItem = new ListViewItem(item.TTitle, item.TIsFree);
                listView.Items.Add(lItem);
            }
           
            //4\将ListView加入当前选中的TabPage
            tabPage.Controls.Add(listView);
        }
    }
}
//开单的餐桌，我们双击可以进行加菜操作