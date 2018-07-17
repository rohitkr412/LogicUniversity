using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class ViewCollectionInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Load_Collections();
        }

        protected void Load_Collections()
        {
            gridview1.DataSource = BusinessLogic.ViewCollectionList();
            gridview1.DataBind();
        }

        protected void gridview1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}