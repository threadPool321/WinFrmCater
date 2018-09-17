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
    public partial class TableInfoFrm : Form
    {
        public TableInfoFrm()
        {
            InitializeComponent();
        }
        #region 自定义方法
        
        private Bll.TableInfoBll bll = new Bll.TableInfoBll();
        private void LoadData()
        {
            Model.TableInfo table = new Model.TableInfo()
            {
                TIsFree=Convert.ToInt32(ddlFreeSearch.SelectedValue),
                THallId=Convert.ToInt32(ddlHallSearch.SelectedValue)
            };
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetTableInfos(table);
        }
        private Bll.HillInfoBll hillBll = new Bll.HillInfoBll();
        private void LoadSearch()
        {
            //根据包房的
            var listHill = hillBll.GetDishInfos();
            listHill.Insert(0, new Model.HallInfo() {
                Hid=0,
                HTitle="全部"
            });
            ddlHallSearch.DisplayMember = "HTitle";
            ddlHallSearch.ValueMember = "Hid";
            ddlHallSearch.DataSource = listHill;
            //根据是否空闲来看
            List<Model.TableState> listTable = new List<Model.TableState>();
            listTable.Insert(0,new Model.TableState("-1","全部"));  //给这个类赋值用的是构造
            listTable.Insert(1,new Model.TableState("0","非空闲"));
            listTable.Insert(2, new Model.TableState("1","空闲"));

            ddlFreeSearch.DisplayMember = "StateName";
            ddlFreeSearch.ValueMember = "StateId";
            ddlFreeSearch.DataSource = listTable;

            //添加时的
            ddlHallAdd.DisplayMember = "Htitle";
            ddlHallAdd.ValueMember = "Hid";
            ddlHallAdd.DataSource= hillBll.GetDishInfos();
        }
        #endregion

        private void TableInfoFrm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadSearch();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex==3)
            {
                if(Convert.ToString(e.Value)=="0")
                {
                    e.Value = "否";
                }
                else
                {
                    e.Value = "是";
                }
            }
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlHallSearch.SelectedIndex = 0;
            ddlFreeSearch.SelectedIndex = 0;
            LoadData(); //直接是加载全部
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Model.TableInfo table = new Model.TableInfo();
            table.TTitle = txtTitle.Text;
            table.THallId = Convert.ToInt32(ddlHallAdd.SelectedValue);
            table.TIsFree = rbFree.Checked ? 1 : rbUnFree.Checked?0:1;
            
            if(btnSave.Text.Equals("添加"))
            {
                if(bll.Add(table))
                {
                    btnCancel_Click(null,null);
                    LoadData();
                }
            }
            else
            {
                //执行更新
                table.TId = Convert.ToInt32(txtId.Text);
                if(bll.Update(table))
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
            txtTitle.Text = "";
            btnSave.Text = "添加";
            rbFree.Checked = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows;
            if(row.Count>0)
            {
                DialogResult result = MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                MessageBox.Show("选中要删除行");
            }
        }
        //双击进行修改
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text=row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            if(row.Cells[3].FormattedValue=="是")
            {
                rbFree.Checked = true;
            }
            else
            {
                rbUnFree.Checked = true;
            }
            btnSave.Text = "修改";
        }
        //听包管理
        private void btnAddHall_Click(object sender, EventArgs e)
        {
            HillInfoFrm hill = HillInfoFrm.CreateInstacne();
            hill.HillInfoEvent += UpdataTable;
            hill.Show();
            hill.Activate();
        }
        public void UpdataTable()
        {
            LoadData();
            LoadSearch();
        }
    }
}
