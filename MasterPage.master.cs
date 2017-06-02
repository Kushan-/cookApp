﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["PageCounter"] == null)
        {
            Application["PageCounter"] = 4894;
        }
        else
        {
            Application.Lock();
            Application["PageCounter"] = (int)Application["PageCounter"] + 1;
            Application.UnLock();
        }
        PageCounter.Text = Convert.ToString(Application["PageCounter"]);
        }
}

