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
    public partial class ManagerInfoList : Form
    {
        public ManagerInfoList()
        {
            InitializeComponent();
        }
        ManagerInfoBll managerInfoBll = new ManagerInfoBll();

        #region 自定义方法
        private void LoadData()
        {
            gvList.AutoGenerateColumns = false;  //取消自动生成列
            gvList.DataSource = managerInfoBll.GetManagerInfos();
        }
        #endregion



        #region 窗体事件
        private void ManagerInfoList_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        //对datagridvie单元格进行格式化
        private void gvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                switch (e.Value.ToString())
                {
                    case "0":
                        e.Value = "店员";
                        break;
                    case "1":
                        e.Value = "经理";
                        break;
                }

            }
        }
        /// <summary>
        /// 添加功能，添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.ManagerInfo managerInfo = new Model.ManagerInfo()
            {
                MName = txtName.Text,
                MPwd = txtPwd.Text,
                MType = rb1.Checked ? 1 : 0   //1代表经理，2代表店员
            };
            if (btnSave.Text == "修改")
            {
                managerInfo.MId = Convert.ToInt32(txtId.Text);
                if (managerInfoBll.Update(managerInfo))
                {
                    btnCancel_Click(null,null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后再试！");
                }
            }
            else
            {
                if (managerInfoBll.InserData(managerInfo))
                {
                    //执行完毕后执行一下清楚功能
                    btnCancel_Click(null, null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("添加成员失败，请稍后再试");
                }
            }

        }
        //清楚功能
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "保存";
        }
        /// <summary>
        /// 删除店员操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //gvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  选择一个单元格就是选择一行
            var rows = gvList.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要进行删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    int id = Convert.ToInt32(rows[0].Cells[0].Value);
                    if (managerInfoBll.Delete(id))
                    {
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("删除失败，请稍后再试");
                    }
                }
            }
            else
            {
                MessageBox.Show("你还没有要删除的选中行");
            }
        }


        #endregion
        /// <summary>
        /// 进行双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                var row = gvList.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtPwd.Text = "******";
                if (row.Cells[2].Value.ToString() == "1")
                {
                    rb1.Checked = true;
                }
                else
                {
                    rb2.Checked = true;
                }
                btnSave.Text = "修改";
            }
            
        }
    }
}
