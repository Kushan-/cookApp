using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Search : Page
{
    private readonly SqlConnection connection =
        new SqlConnection("Data Source = recipebook.database.windows.net; Initial Catalog = CookBook; Integrated Security = False; User ID = Satnam; Password=GURsat1313!;Connect Timeout = 15; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
    //"Data Source=DESKTOP-B5SA0JC\\SATNAM;Initial Catalog=Comp229;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True");

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    
    

    protected void Submit_Click(object sender, EventArgs e)
    {

        string query;
        SqlCommand command;
        string checkBox;
        string check;
        connection.Open();
        var query3 = "SELECT TOP 1 * FROM AddRecipe ORDER BY RecipeNumbers DESC";
        var command3 = new SqlCommand(query3, connection);
        var i = (int)command3.ExecuteScalar();
        for (; i >= 1; i--)
        {
            if (Private.Checked)
                checkBox = "Private";
            else
                checkBox = "Public";
            if (RecipeBox.Text == "")
            {
                query = "Select RecipeName,RecipeNumbers,RecipeDescription ,Cusine c from AddRecipe ,Cuisine c  where Limits=@Private and c.RecipeNumber=" + i + " and RecipeNumbers=" + i + " and c.cusine=@Cuisine";


            }
            else
            {
                query = "Select RecipeName,RecipeNumbers,RecipeDescription ,Cusine c from AddRecipe,Cuisine c where  RecipeName like"+" '%'+"+"@RecipeName"+"+'%'"+"  and  Limits=@Private and c.RecipeNumber=" + i + " and RecipeNumbers=" + i + " and c.cusine=@Cuisine";

            }
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RecipeName", RecipeBox.Text);

            command.Parameters.AddWithValue("@Private", checkBox);

            command.Parameters.AddWithValue("@Cuisine", CuisineList.Text);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {



                string image = null;

                if (RecipeBox.Text == "")
                {
                    image = "Select RecipeImage from AddRecipe where Limits=@Private and @RecipeNumbers=RecipeNumbers";
                }
                else
                {
                    image = "Select RecipeImage from AddRecipe where RecipeName like" + " '%'+" + "@RecipeName" + "+'%'" + " and Limits=@Private and RecipeNumbers=@RecipeNumbers";
                }

                var imagedata = new SqlCommand(image, connection);
                imagedata.Parameters.AddWithValue("@RecipeName", RecipeBox.Text);
                if (Private.Checked)
                    checkBox = "Private";
                else
                    checkBox = "Public";
                imagedata.Parameters.AddWithValue("@Private", checkBox);
                imagedata.Parameters.AddWithValue("@RecipeNumbers", i);

                imagedata.Parameters.AddWithValue("@Cuisine", CuisineList.Text);


                // imagedata.Parameters.AddWithValue("@Limit", "Public");
                var bytes = (byte[])imagedata.ExecuteScalar();
                if ((byte[])imagedata.ExecuteScalar() == null)
                {
                    //do nothing
                }
                else
                {
                    var strBase64 = Convert.ToBase64String(bytes);
                    var imageUrl = "data:Image;base64," + strBase64;
                    var createDiv = new HtmlGenericControl("DIV");
                    var innerdiv = new HtmlGenericControl("DIV");
                    innerdiv.Attributes["class"] = "thumbnail";

                    Button moreInfoButton = new Button();
                    moreInfoButton.Text = "More Info";
                    moreInfoButton.ID = Convert.ToString(i);
                    moreInfoButton.ClientIDMode = ClientIDMode.Static;
                    moreInfoButton.EnableViewState = true;
                    moreInfoButton.Click += new EventHandler(moreInfo_Click);
                    moreInfoButton.Attributes["class"] = "btn btn-info";
                    createDiv.Attributes["class"] = "col-md-4 col-sm-6 col-xs-12";
                    innerdiv.InnerHtml = "<a href='moreInfo.aspx'id="+i+"><img src=" + imageUrl +
                                          " alt='Recipe Image' class='img-responsive img-rounded img-thumbnail' style='height: 250px'> <div class='caption' > <h2 ID='RecipeName' runat='server'>" + reader["RecipeName"] + "</h2></div>" + "<p>" + reader["RecipeDescription"] + "</p></a>";
                    createDiv.ID = "createDiv" + i;
                    innerdiv.ID = "innerDiv" + i;
                    search.Controls.Add(createDiv);
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

    protected void moreInfo_Click(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string buttonId = button.ID;
        HttpCookie myCookie = new HttpCookie("addingId");
        myCookie.Value = buttonId;
        Response.Cookies.Add(myCookie);
       Response.Redirect("moreInfo.aspx");
    }
}

