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
    public partial class OrderInfoList : Form
    {
        public OrderInfoList()
        {
            InitializeComponent();
        }
        Bll.OrderInfoBll bll = new Bll.OrderInfoBll();
        int _oId;
        private void OrderInfoList_Load(object sender, EventArgs e)
        {
            //获取传递过来的餐桌编号
            int tableId = Convert.ToInt32(this.Tag);

            //根据餐桌编号，获取订单编号
            _oId = bll.GetOrderIdByTid(tableId);
            //加载菜品分类和菜品详情
            LoadDishType();
            LoadDish();
            //刷线点过的菜(为了点过菜后又重现增加菜)
            LoadOrderDishList(_oId);

            //如果已经有下订单，重新加载总价
            GetAllCount();
        }

        private void LoadDish()
        {
            //查询的时候
            Model.DishInfo dishObj = new Model.DishInfo();
            dishObj.DChar = txtTitle.Text;
            dishObj.DTypeId = Convert.ToInt32(ddlType.SelectedValue);

            Bll.DishInfoBll infoBll = new Bll.DishInfoBll();
            var list = infoBll.GetDishInfos(dishObj);
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = list;
        }

        private void LoadDishType()
        {
            Bll.DishTypeBll dishTypeBll = new Bll.DishTypeBll();
            ddlType.DisplayMember = "DTitle";
            ddlType.ValueMember = "DId";
            var list = dishTypeBll.GetDishTypes();
            list.Insert(0, new Model.DishTypeModel()
            {
                Did = 0,
                DTitle = "全部"
            });
            ddlType.DataSource = list;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDish();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDish();
        }
        //开始进行点菜功能
        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //首先实现双击菜品  然后点菜栏中就会有刚才点的菜（实现）
            var dishId = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);
            if (bll.OrderDish(_oId, dishId)) //点菜成功了，然后就要重新刷线出来刚才点的菜
            {
                LoadOrderDishList(_oId);
            }
        }
       
        //刷新这个点过的菜
        public void  LoadOrderDishList(int _oId)
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = bll.GetOrderDetailInfos(_oId);
        }
        //修改数量
        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvOrderDetail.Rows[e.RowIndex];
            int id = Convert.ToInt32(row.Cells[0].Value);
            int count = Convert.ToInt32(row.Cells[e.ColumnIndex].Value);
            //修改数量后开始
            if(bll.UpdateCountOrder(id, count))
            {
                GetAllCount();
            }
        }
        //计算所有的价格
        public void GetAllCount()
        {
            var row = dgvOrderDetail.Rows;
            decimal total=0;
            for (int i = 0; i < row.Count; i++)
            {
                int count = Convert.ToInt32(row[i].Cells[2].Value);
                decimal price = Convert.ToDecimal(row[i].Cells[3].Value);
                total += count * price;
            }
            lblMoney.Text = total.ToString();
        }
        //删除已经点过的菜品
        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvOrderDetail.SelectedRows;
            if(row.Count>0)
            {
                DeleteOrderInfo();
            }
            else
            {
                MessageBox.Show("请选择要删除的菜品！");
            }
        }
    }
}
