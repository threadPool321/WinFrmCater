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
    public partial class MemberInfoList : Form
    {
        public MemberInfoList()
        {
            InitializeComponent();
        }

        Bll.MemberBll bll = new Bll.MemberBll();
        #region 自定义方法
        private void LoadData()
        {
            Model.MemberInfo member = new Model.MemberInfo();
            member.MName = txtNameSearch.Text;
            member.MPhone = txtPhoneSearch.Text;
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList(member);
        }
        //绑定下拉框
        private void GetDDl()
        {
            Bll.MemberTypeInfoBll typeBll = new Bll.MemberTypeInfoBll();
            ddlType.DisplayMember = "MTitle";
            ddlType.ValueMember = "MId";
            ddlType.DataSource = typeBll.GetMemberTypeInfos();

        }
        #endregion
        #region 窗体事件
        private void MemberInfoList_Load(object sender, EventArgs e)
        {
            LoadData();
            GetDDl();
            ddlType.SelectedIndex = -1;            
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtMoney.Text = "";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            btnSave.Text = "保存";
        }
        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.MemberInfo obj = new Model.MemberInfo()
            {
                MName = txtNameAdd.Text,
                MTypeId = Convert.ToInt32(ddlType.SelectedValue),
                MPhone = txtPhoneAdd.Text,
                MMoney = Convert.ToDecimal(txtMoney.Text)
            };
            if(btnSave.Text.Equals("添加"))
            {
                if(bll.Add(obj))
                {
                    btnCancel_Click(null, null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍后重试");
                }
            }
            else
            {
                //修改的逻辑
                obj.MId = Convert.ToInt32(txtId.Text);
                if(bll.Update(obj))
                {
                    btnCancel_Click(null,null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("信息修改失败");
                }
            }
        }
        //删除
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            if(rows.Count>0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(result==DialogResult.OK)
                {
                    int id = Convert.ToInt32(rows[0].Cells[0].Value);
                    if(bll.Delete(id))
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
                MessageBox.Show("请选择要删除掉行");
            }
        }
        //双击修改
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "修改";
            var row = dgvList.Rows[e.RowIndex];
            
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();   //直接就是下拉框的值
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
        }
        //查询所有
        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            txtPhoneSearch.Text = "";
            LoadData();
        }
        //类型的管理
        private void btnAddType_Click(object sender, EventArgs e)
        {
            MemberTypeInfo info = FrmSingletonFactory.CreateMemberTypeInstance();
            info.Show();
            info.Focus();
        }
        //名字查询
        private void txtNameSearch_Leave(object sender, EventArgs e)
        {
            LoadData();
        }
        //电话查询
        private void txtPhoneSearch_Leave(object sender, EventArgs e)
        {
            LoadData();
        }
    }
    #endregion


}
