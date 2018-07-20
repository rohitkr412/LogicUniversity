using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using System.Drawing;

namespace Team3ADProject.Protected
{


    public partial class AcknowledgeDistributionList : System.Web.UI.Page
    {




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DepartmentNameLabel.Text = Session["DepartmentName"].ToString();
                    DepartmentRepresentativeLabel.Text = Session["EmployeeName"].ToString();
                    DateLabel.Text = Session["CollectionDate"].ToString();
                    LocationLabel.Text = Session["CollectionLocation"].ToString();
                    TimeLabel.Text = Session["CollectionTime"].ToString();
                    int collection_id = Convert.ToInt32(Session["collection_id"]);
                    myPageLoad(collection_id);
                }
                catch (Exception ex)
                {

                    Response.Write("Exception " + ex);
                }

            }
        }



        protected void myPageLoad(int collection_id)
        {
            gridview1.DataSource = BusinessLogic.ViewAcknowledgementList(collection_id);
            gridview1.DataBind();

        }


        protected void AcknowledgeButton_Click(object sender, EventArgs e)
        {

        }




        protected void VerifyPasswordButtonClick(object sender, EventArgs e)
        {

            if (PinCorrect())
            {
                Response.Write("\nPin entered is correct");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Pin is incorrect')", true);
            }
        }



        protected Boolean PinCorrect()
        {
            try
            {
                string deptname = Session["DepartmentName"].ToString();
                int EnteredPin = Convert.ToInt32(PinTextBox.Text);
                int ActualPin = BusinessLogic.GetDepartmentPin(deptname);
                if (EnteredPin == ActualPin)
                {
                    Session["Pin"] = true;
                    AcknowledgeButton.Enabled = true;
                    AcknowledgeButton.BackColor = Color.Green;
                    return true;
                }
                else
                {
                    Session["Pin"] = false;
                    AcknowledgeButton.Enabled = false;
                    AcknowledgeButton.BackColor = Color.Red;
                }
            }
            catch (FormatException e)
            {
                System.FormatException ex = e;
                Response.Write("\nPin has to be numeric");
                return false;
            }
            catch (Exception e)
            {
                Response.Write("\nError occured : " + e);
            }
            return false;
        }


    }
}