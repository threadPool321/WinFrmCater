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
    public partial class MemberTypeInfo : Form
    {
        public MemberTypeInfo()
        {
            InitializeComponent();
        }
        Bll.MemberTypeInfoBll bll = new Bll.MemberTypeInfoBll();
        private void MemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        #region 自定义方法
        private void LoadData()
        {
            dgvList.DataSource = bll.GetMemberTypeInfos();
        }
        #endregion
        #region 窗体事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.MemberTypeInfo memberType = new Model.MemberTypeInfo()
            {
                MTitle = txtTitle.Text,
                MDiscount = txtDiscount.Text.Length > 0 ? Convert.ToDecimal(txtDiscount.Text) : Convert.ToDecimal(0)
            };
            if (btnSave.Text.Equals("添加"))
            {
                if (bll.Insert(memberType))
                {
                    btnCancel_Click(null, null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("添加错误，你稍候重试！");
                }

            }
            else
            {
                //修改操作
                memberType.Mid = Convert.ToInt32(txtId.Text);
                if(bll.Update(memberType))
                {
                    btnSave.Text = "添加";
                    LoadData();
                }
                else
                {
                    MessageBox.Show("修改错误，请稍后重试");
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var ids = dgvList.SelectedRows;
            if (ids.Count > 0)
            {
                int id = Convert.ToInt32(ids[0].Cells[0].Value);
                DialogResult result = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result==DialogResult.OK)
                {
                    if(bll.Delete(id))
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("选中一行删除");
            }
        }
        //双击修改操作
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "修改";
             var row=dgvList.SelectedRows;
           //var row = dgvList.SelectedRows[e.RowIndex];
            if(row.Count>0)
            {
                txtId.Text = row[0].Cells[0].Value.ToString();
                txtTitle.Text = row[0].Cells[1].Value.ToString();
                txtDiscount.Text = row[0].Cells[2].Value.ToString();
            }
        }

        #endregion


    }
}
