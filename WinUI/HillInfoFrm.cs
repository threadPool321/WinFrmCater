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
    public partial class HillInfoFrm : Form
    {
        public HillInfoFrm()
        {
            InitializeComponent();
        }
        #region 自定义方法
        Bll.HillInfoBll bll = new Bll.HillInfoBll();
        private void LoadData()
        {
            dgvList.AllowUserToOrderColumns = false;
            dgvList.DataSource = bll.GetDishInfos();
        }
        #endregion
        private void HillInfoFrm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.HallInfo hiObj = new Model.HallInfo();
            hiObj.HTitle = txtTitle.Text.Trim();
            if(btnSave.Text.Equals("添加"))
            {
                if(bll.Add(hiObj))
                {
                    btnCancel_Click(null, null); //把事件当作一个方法使用
                    LoadData();
                }
            }
            else
            {
                hiObj.Hid = Convert.ToInt32(txtId.Text);
                if(bll.Update(hiObj))
                {
                    btnCancel_Click(null,null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("更新失败");
                }
            }
        }
        //完成修改操作
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows;
            if(row.Count>0)
            {
                DialogResult result = MessageBox.Show("确定要删除选中行","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(result==DialogResult.OK)
                {
                    if(bll.Delete(Convert.ToInt32(row[0].Cells[0].Value)))
                    {
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                }
            }
            else
            {
                MessageBox.Show("选择删除选中行");
            }
        }
    }
}
