using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnPreInit(EventArgs e)
    {
        if (Session["userName"] != null)
        {
            this.MasterPageFile = "~/MasterSingin.master";
            base.OnPreInit(e);
        }
        else
        {
            this.MasterPageFile = "~/MasterPage.master";
            base.OnPreInit(e);
        }

    }
}