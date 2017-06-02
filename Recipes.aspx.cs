using System;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Recipes : Page
{


    private readonly SqlConnection connection =
            new SqlConnection(
                "Data Source=recipebook.database.windows.net;Initial Catalog=CookBook;Integrated Security=False;User ID=Satnam;Password=GURsat1313!;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
        //(

    //"Data Source=DESKTOP-B5SA0JC\\SATNAM;Initial Catalog=Comp229;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");

    protected void Page_Load(object sender, EventArgs e)
    {
        connection.Open();
        var query3 = "SELECT TOP 1 * FROM AddRecipe ORDER BY RecipeNumbers DESC";
        var command3 = new SqlCommand(query3, connection);
        var i = (int)command3.ExecuteScalar();
        for (; i >= 1; i--)
        {
            var query = "Select * from AddRecipe where Limits=@Private and RecipeNumbers="+i ;
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Private", "Public");

        var reader = command.ExecuteReader();
            if (reader.Read())
            {
                

                    var image = "Select RecipeImage  from AddRecipe  where  Limits=@Private  and RecipeNumbers=" + i;
                    var imagedata = new SqlCommand(image, connection);
                    //var query2 = "Select * from AddRecipe where Limits=@Private and RecipeNumbers=" + i;
                    //var command2 = new SqlCommand(query2, connection);

                    //command2.Parameters.AddWithValue("@Private", "Public");
                    imagedata.Parameters.AddWithValue("@Private", "Public");
                    var bytes = (byte[]) imagedata.ExecuteScalar();
                    //var reader = command2.ExecuteReader();
                    if ((byte[]) imagedata.ExecuteScalar() == null)
                    {

                    }
                    else
                    {
                        var strBase64 = Convert.ToBase64String(bytes);
                        var imageUrl = "data:Image;base64," + strBase64;
                        var createDiv = new HtmlGenericControl("DIV");
                        var innerdiv = new HtmlGenericControl("DIV");
                        innerdiv.Attributes["class"] = "thumbnail";
                        //string reacipeName = (string)reader["RecipeName"];

                        Button moreInfoButton = new Button();
                        moreInfoButton.Text = "More Info";
                        moreInfoButton.ID = Convert.ToString(i);
                        moreInfoButton.ClientIDMode = ClientIDMode.Static;
                        moreInfoButton.Click += new EventHandler(moreInfo_Click);
                        moreInfoButton.Attributes["class"] = "btn btn-info";
                        createDiv.Attributes["class"] = "col-md-4 col-sm-6 col-xs-12";
                        innerdiv.InnerHtml = "<img src=" + imageUrl +
                                             " alt='Recipe Image' class='img-responsive img-rounded img-thumbnail' style='height: 250px'> <div class='caption' > <h2 ID='RecipeName' runat='server'>" +
                                             reader["RecipeName"] + "</h2></div>" + "<p>" + reader["RecipeDescription"] +
                                             "</p>";
                        createDiv.ID = "createDiv" + i;
                        innerdiv.ID = "innerDiv" + i;
                        dynamic.Controls.Add(createDiv);
                        createDiv.Controls.Add(innerdiv);
                        innerdiv.Controls.Add(moreInfoButton);
                        
                    }

                }
        

            
        }
        connection.Close();
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
    
    private void moreInfo_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string buttonId = button.ID;
        HttpCookie myCookie = new HttpCookie("addingId");
        myCookie.Value = buttonId;
        Response.Cookies.Add(myCookie);
       Response.Redirect("moreInfo.aspx");
    }

    
}