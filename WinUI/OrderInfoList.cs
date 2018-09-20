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
        private void OrderInfoList_Load(object sender, EventArgs e)
        {
            //获取传递过来的餐桌编号
            int tableId = Convert.ToInt32(this.Tag);

            //根据餐桌编号，获取订单编号
            int oId = bll.GetOrderIdByTid(tableId);
            //加载菜品分类和菜品详情
            LoadDishType();
            LoadDish();
        }

        private void LoadDish()
        {
            //查询的时候
            Model.DishInfo dishObj = new Model.DishInfo();
            dishObj.DChar = txtTitle.Text;
            dishObj.DTypeId = Convert.ToInt32(ddlType.SelectedValue);

            Bll.DishInfoBll infoBll = new Bll.DishInfoBll();
            var list=infoBll.GetDishInfos(dishObj);
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = list;
        }

        private void LoadDishType()
        {
            Bll.DishTypeBll dishTypeBll = new Bll.DishTypeBll();
            ddlType.DisplayMember = "DTitle";
            ddlType.ValueMember = "DId";
            var list= dishTypeBll.GetDishTypes();
            list.Insert(0,new Model.DishTypeModel() {
                Did=0,
                DTitle="全部"
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
    }
}
