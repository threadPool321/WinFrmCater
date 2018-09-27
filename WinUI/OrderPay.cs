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
    public partial class OrderPay : Form
    {
        public OrderPay()
        {
            InitializeComponent();
            //设置窗体的开始位置
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        Bll.OrderInfoBll orderInfoBll = new Bll.OrderInfoBll();
        private void OrderPay_Load(object sender, EventArgs e)
        {
            gbMember.Enabled = false;
            this.Text = "这是" + this.Tag + "号桌";
            //窗体界面一开始加载的时候直接带过来的（消费金额）
            lblPayMoney.Text = orderInfoBll.GetOrderMoneyByTid(Convert.ToInt32(this.Tag)).ToString();
            lblPayMoneyDiscount.Text = lblPayMoney.Text;
        }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            //if(cbkMember.Checked==true)
            //{
            //    gbMember.Enabled = true;
            //}
            //建议写法
            gbMember.Enabled = cbkMember.Checked;  //这就表示单选框选中的结果赋值组控件
            //如果选择后又突然不在选择那么就要恢复到原值
            if(!cbkMember.Checked)
            {
                txtId.Text = "";
                txtPhone.Text = "";
                lblMoney.Text = "0";
                lblTypeTitle.Text = "普通会员";
                lblDiscount.Text = "1";
                cbkMoney.Checked = false;
                //不在选择后消费金额应该回复的原值
                lblPayMoneyDiscount.Text = lblPayMoney.Text;
            }
        }
        //会员编号的值改变
        private void txtId_TextChanged(object sender, EventArgs e)
        {
            BindData();
        }
        //电话号码的值改变事件
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            //根据我输入的值检索的客户的会员信息
            Model.MemberInfo memberInfo = new Model.MemberInfo();
            if (!string.IsNullOrEmpty(txtId.Text))              //这里是为了值改变后清空了，就会触发这textchang事件
            {
                memberInfo.MId = Convert.ToInt32(txtId.Text);  
            }
            //memberInfo.MId = Convert.ToInt32(txtId.Text);
            memberInfo.MPhone = txtPhone.Text;   //如果我们拿到的没有值的化那么得到就是null
            Bll.MemberBll memberBll = new Bll.MemberBll();
            var list = memberBll.GetList(memberInfo);
            if(list.Count==1)  //说明确实就只有一个会员并且能够使用
            {
                memberInfo = list[0];
                txtPhone.Text = memberInfo.MPhone;
                lblMoney.Text = memberInfo.MMoney.ToString();
                lblTypeTitle.Text = memberInfo.TypeTitle;
                lblDiscount.Text = memberInfo.Discount.ToString();

                //根据会员的id,查出来折扣后就应该
                lblPayMoneyDiscount.Text = (Convert.ToDecimal(lblDiscount.Text) * Convert.ToDecimal(lblPayMoney.Text)).ToString();


            }
            else
            {
                MessageBox.Show("会员支付已经取消");
            }
        }
        public event Action UpdateTableEvent;
        //确认结账 拿到的信息的餐座编号
        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            //已知的数据，餐座编号
            //需要知道的数据，会员的编号，折扣金额，折扣

            //1、更改订单状态
            //1.1如果是会员则记录下来
            //2.将餐桌更改为空闲 1
            //3.如果使用余额结账，那么就要更新会员的余额
            int tableid = Convert.ToInt32(this.Tag);
            int memberId = 0;
            decimal discount = 0;
            decimal payMoney = 0;
            if (!string.IsNullOrEmpty(txtId.Text))  //不选择会员
            {
                memberId = Convert.ToInt32(txtId.Text);
                discount = Convert.ToDecimal(lblDiscount.Text);

            }
            if(cbkMoney.Checked)
            {
                if (Convert.ToDecimal(lblMoney.Text) > Convert.ToDecimal(lblPayMoneyDiscount.Text))
                {
                    payMoney = Convert.ToDecimal(lblPayMoneyDiscount.Text);
                }
                else
                {
                    payMoney = Convert.ToDecimal(lblMoney.Text);
                }
            }
            
            if(orderInfoBll.SettleAccounts(tableid,memberId,discount,payMoney))
            {
                UpdateTableEvent();
                this.Close();
            }
            //4.结完账后更改餐桌的转态

        }
    }
}
