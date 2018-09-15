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
    public partial class DishTypeInfo : Form
    {
        private DishTypeInfo()
        {
            InitializeComponent();
        }
        #region 自定义变量
        private Bll.DishTypeBll bll = new Bll.DishTypeBll();
       private static DishTypeInfo objInstance = null;
        public event Action updateTypeEvent;
        #endregion
        #region 自定义方法
        public static DishTypeInfo CreateInstacne()
        {
            if(objInstance==null||objInstance.IsDisposed)
            {
                objInstance = new DishTypeInfo();
            }
            return objInstance;
        }
        private void LoadData()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetDishTypes();
        }
        #endregion
        #region 窗体加载事件
        private void DishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        //添加事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.DishTypeModel disObj = new Model.DishTypeModel()
            {
                DTitle = txtTitle.Text
            };
            if(btnSave.Text.Equals("添加"))
            {
                if(bll.Add(disObj))
                {
                    btnSave_Click(null, null);
                    LoadData();
                    updateTypeEvent();
                }
                else
                {
                    MessageBox.Show("添加失败");
                }
            }
            else
            {
                //修改
                disObj.Did = Convert.ToInt32(txtId.Text);
                if(bll.update(disObj))
                {
                    btnCancel_Click(null, null);
                    LoadData();
                    updateTypeEvent();
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }
        //删除
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rowSelected = dgvList.SelectedRows;
            if(rowSelected.Count>0)
            {
                DialogResult result = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(result==DialogResult.OK)
                {
                    if(bll.Delete(Convert.ToInt32(rowSelected[0])))
                    {
                        LoadData();
                        updateTypeEvent();
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一行数据，进行删除");
            }
        }


        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Text = "修改";
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text=Convert.ToString(row.Cells[0].Value);
            txtTitle.Text = Convert.ToString(row.Cells[1].Value);
        }
        #endregion


    }
}
