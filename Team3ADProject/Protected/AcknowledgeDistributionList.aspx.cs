using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class AcknowledgeDistributionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
               Session["SumAfterEdit"] = displayQuantity();
            }
            try
            {
                DepartmentNameLabel.Text = Session["DepartmentName"].ToString();
                DepartmentRepresentativeLabel.Text = Session["EmployeeName"].ToString();
                DateLabel.Text = Session["CollectionDate"].ToString();
                LocationLabel.Text = Session["CollectionLocation"].ToString();
                TimeLabel.Text = Session["CollectionTime"].ToString();
                int disbursement_list_id = Convert.ToInt32(Session["disbursement_list_id"]);
                myPageLoad(disbursement_list_id);
            }
            catch (Exception ex)
            {
                
                Response.Write("Exception " + ex);
            }
        }

        protected void myPageLoad(int disbursement_list_id)
        {
            gridview1.DataSource = BusinessLogic.ViewAcknowledgementList(disbursement_list_id);
            gridview1.DataBind();
            
        }
        

        protected void AcknowledgeButtonClick(object sender,EventArgs e)
        {
            int areQuantitesEditedSame = 0;
           if(PinCorrect())
            {
                Response.Write("\nPin entered is correct");
                Session["SumBeforeEdit"] = displayQuantity();
                areQuantitesEditedSame = checkQuantities();
                switch(areQuantitesEditedSame)
                {
                    case 0:
                        Response.Write("\nQuantites Edited Are Equal");
                        break;

                    case 1:
                        Response.Write("\nQuantites Edited Are Greater");
                        break;

                    case -1:
                        Response.Write("\nQuantites Edited Are Lesser");
                        break;
                }
            }
           else
            {
                Response.Write("\nPin is incorrect");
            }
        }




        protected int checkQuantities()
        {
            /*
             * Return +1 if quantity edited more
             * Return 0 if quantity edited same
             * Return -1 if quantity edited lesser
             * */

            if (Convert.ToInt32(Session["SumAfterEdit"]) > Convert.ToInt32(Session["SumBeforeEdit"])) return 1;

            else if (Convert.ToInt32(Session["SumAfterEdit"]) == Convert.ToInt32(Session["SumBeforeEdit"])) return 0;

            else return -1;
            
        }




        
        protected int displayQuantity()
        {
            int sum = 0;
            try
            {                
                foreach (GridViewRow row in gridview1.Rows)
                {
                    TextBox t = (TextBox)row.FindControl("TextBox1");
                    sum += Convert.ToInt32(t.Text.ToString());

                }
                return sum;
            }
            catch(Exception e)
            {
                Response.Write("\n\n"+e);
            }
            return sum;

        }
        





        protected Boolean PinCorrect()
        {
            try
            {
                string deptname = Session["DepartmentName"].ToString();
                int EnteredPin = Convert.ToInt32(PinTextBox.Text);
                int ActualPin = BusinessLogic.GetDepartmentPin(deptname);
                if (EnteredPin == ActualPin) return true;
            }
            catch(FormatException e)
            {
                System.FormatException ex = e;
                Response.Write("\nPin has to be numeric");
                return false;
            }
            catch(Exception e)
            {
                Response.Write("\nError occured : "+e);
            }
            return false;
        }

       
    }
}