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
    public partial class DishInfo : Form
    {
        public DishInfo()
        {
            InitializeComponent();
        }
        #region 自定义变量
        private Bll.DishInfoBll bll = new Bll.DishInfoBll();
        Bll.DishTypeBll typeBll = new Bll.DishTypeBll();
        #endregion

        #region 自定义方法
        private void LoadData()
        {
            Model.DishInfo dishInfo = new Model.DishInfo();
            dishInfo.DTitle = txtTitleSearch.Text;
            dishInfo.DTypeId = ddlTypeSearch.SelectedIndex;

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetDishInfos(dishInfo);
        }
        private void LoadDishType()
        {
            //1.
          
            ddlTypeAdd.DisplayMember = "DTitle";
            ddlTypeAdd.ValueMember = "DId";
            ddlTypeAdd.DataSource = typeBll.GetDishTypes();
            ddlTypeAdd.SelectedIndex = -1;
            //2.
            var list = typeBll.GetDishTypes();
            ddlTypeSearch.DisplayMember = "DTitle";
            ddlTypeSearch.ValueMember = "DId";
            list.Insert(0, new Model.DishTypeModel()
            {
                Did = 0,
                DTitle = "全部"
            });
            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.SelectedIndex = -1;
        }
        #endregion

        #region 窗体事件


        private void DishInfo_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDishType();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.DishInfo dishObj = new Model.DishInfo()
            {
                DTitle = txtTitleSave.Text,
                DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue),
                DPrice = Convert.ToDecimal(txtPrice.Text),
                DChar = txtChar.Text

            };
            if (btnSave.Text.Equals("添加"))
            {
                if (bll.Add(dishObj))
                {
                    btnCancel_Click(null, null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                //更新操作
                dishObj.DId = Convert.ToInt32(txtId.Text);
                if (bll.Update(dishObj))
                {
                    btnCancel_Click(null, null);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            ddlTypeAdd.SelectedIndex = -1;
            txtPrice.Text = "";
            txtChar.Text = "";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows;
            if (row.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    if (bll.Delete(Convert.ToInt32(row[0].Cells[0].Value)))
                    {
                        LoadData();
                    }
                }
            }
        }
        //更新操作
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "修改";
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = Convert.ToString((row.Cells[0].Value));
            txtTitleSave.Text = Convert.ToString(row.Cells[1].Value);
            ddlTypeAdd.Text = Convert.ToString(row.Cells[2].Value);
            txtPrice.Text = Convert.ToString(row.Cells["3"].Value);
            txtChar.Text = Convert.ToString(row.Cells[4].Value);
        }
       

        #endregion

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSave.Text = "";
            ddlTypeSearch.SelectedIndex = 0;
            LoadData();
        }

        private void txtTitleSearch_Leave(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTitleSave_TextChanged(object sender, EventArgs e)
        {
            string txt = txtTitleSave.Text;
            txtChar.Text = Common.PinyinHelper.GetPinYin(txt);
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            DishTypeInfo dishTypeInfo = DishTypeInfo.CreateInstacne();
            //绑定事件
            dishTypeInfo.updateTypeEvent += UpdateLoad;
            dishTypeInfo.Show();
            dishTypeInfo.Activate();
        }
        private void UpdateLoad()
        {
            LoadData();
            LoadDishType();
        }
    }
}
