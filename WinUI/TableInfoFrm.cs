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
    }
}
