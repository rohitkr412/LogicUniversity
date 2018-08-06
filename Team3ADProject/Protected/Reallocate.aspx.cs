﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Windows.Forms;

namespace Team3ADProject.Protected
{
    //JOEL START

    public partial class Reallocate : System.Web.UI.Page
    {
        string itemNum;
        string description;
        int collectedItemCount;
        static List<spReallocateQty_Result> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["itemNum"] != null)
            {
                itemNum = Request.QueryString["itemNum"];
            }

            if (Request.QueryString["description"] != null)
            {
                description = Request.QueryString["description"];
            }

            if (!IsPostBack)
            {
                list = BusinessLogic.GetReallocateList(itemNum);

                gridview_Reallocate.DataSource = list;
                gridview_Reallocate.DataBind();

                Label_itemNum.Text = itemNum;
                Label_Description.Text = description;
                Label_warning.Visible = false;

                collectedItemCount = BusinessLogic.GetTotalCollectedVolumeForChosenItem(itemNum);
                Label_collectedAmount.Text = collectedItemCount.ToString();
            }
        }

        // on loading gridview, finds total collected amount collected for this item for all departments.
        protected void gridview_Reallocate_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;
                System.Web.UI.WebControls.TextBox text = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                foreach (var q in list)
                {
                    if (dpt_id == q.department_id)
                    {
                        int qty = (int)q.item_distributed_quantity;
                        text.Text = qty.ToString();
                    }
                }
            }
        }

        //if yes button is clicked when qty reallocated is different from what was collected. items are returned to inventory.
        protected void Yes_Click(object sender, EventArgs e)
        {
            ReallocateItems();
            int totalEnterQty = 0;
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                System.Web.UI.WebControls.TextBox textbox = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int distriQty = Convert.ToInt32(textbox.Text);
                totalEnterQty += distriQty;
            }
            int collectedQty = Convert.ToInt32(Label_collectedAmount.Text);
            int returnBalance = collectedQty - totalEnterQty;
            BusinessLogic.ReturnToInventory(returnBalance, itemNum);
            Response.Redirect("~/Protected/DisbursementSorting.aspx");

        }

        //if no button is clicked when qty reallocated is different from what was collected. returns to where user left off. user will need to re-enter qtys. 
        protected void No_Click(object sender, EventArgs e)
        {
            PanelMsg.Visible = false;
            Button_Reallocate.Visible = true;
        }

        //if reallocate button is clicked. 
        protected void Button_Reallocate_Click(object sender, EventArgs e)
        {
            Label_warning.Visible = false;


            if (ValidatePreparedQty() < 0)
            {
                return;
            }

            // getting total input
            int totalEnterQty = 0;
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                System.Web.UI.WebControls.TextBox textbox = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int distriQty = Convert.ToInt32(textbox.Text);
                totalEnterQty += distriQty;
            }

            int collectedQty = Convert.ToInt32(Label_collectedAmount.Text);

            // if what is insert > total collected, error msg
            if (totalEnterQty > collectedQty)
            {
                Label_warning.Visible = true;
                Label_warning.Text = "You entered more quantity than collected";
                return;
            }

            // if what is inserted < total, show message that the extra will be returned to inventory
            if (totalEnterQty < collectedQty)
            {
                PanelMsg.Visible = true;
                Button_Reallocate.Visible = false;
                WarningMsg.Text = "You entered less items than collected. Do you want to return items to warehouse / inventory?";
                //tried using windows forms dialog box, but doesn't work when published on IIS
                //System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("You entered less items than collected. Do you want to return items to warehouse / inventory?", "Return to warehouse?", MessageBoxButtons.YesNo);
                //if no, return.
                //if (result == DialogResult.No)
                //{
                //    return;
                //}

                //if (result == DialogResult.Yes)
                //{
                //    ReallocateItems();
                //    int returnBalance = collectedQty - totalEnterQty;
                //    BusinessLogic.ReturnToInventory(returnBalance, itemNum);
                //    Response.Redirect("~/Protected/DisbursementSorting.aspx");
                //}
            }

            else
            {
                ReallocateItems();
                Response.Redirect("~/Protected/DisbursementSorting.aspx");
            }
        }

        //if items need to be reallocated
        protected void ReallocateItems()
        {
            //Reset ROD Values

            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;

                BusinessLogic.ResetRODTable(dpt_id, itemNum);
            }

            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;
                System.Web.UI.WebControls.TextBox textbox = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int distriQty = Convert.ToInt32(textbox.Text);

                BusinessLogic.UpdateRODTableOnReallocate(dpt_id, itemNum, distriQty);
            }
        }

        //validates if prepared qtys are less than ordered qty, and less than what was collected overall.
        protected int ValidatePreparedQty()
        {
            bool flag = false;
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                int qtyOrder = Convert.ToInt32(gvr.Cells[1].Text);

                System.Web.UI.WebControls.TextBox tb = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int qtyToPrep = Convert.ToInt32(tb.Text);

                System.Web.UI.WebControls.Label validator = (System.Web.UI.WebControls.Label)gvr.FindControl("Label1");
                validator.Visible = false;

                if (qtyToPrep > qtyOrder)
                {
                    validator.Visible = true;
                    validator.Text = "Amount is more than Ordered Qty";
                    flag = true;
                    break;
                }
            }
            if (flag == true)
                return -1;

            else
                return 1;
        }
    }
}