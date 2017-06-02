using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class moreInfo : System.Web.UI.Page
{
    private string cookie;
    private readonly SqlConnection connection =
       new SqlConnection("Data Source=recipebook.database.windows.net;Initial Catalog=CookBook;Integrated Security=False;User ID=Satnam;Password=GURsat1313!;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
    //    "Data Source=DESKTOP-B5SA0JC\\SATNAM;Initial Catalog=Comp229;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
    protected void Page_Load(object sender, EventArgs e)
    {

        HttpCookie myCookie = Request.Cookies["addingId"];
        // Read the cookie information and display it.
        if (myCookie != null) { 
            cookie = myCookie.Value;
        }
        else { 
            Response.Write("not found");
                Response.Redirect("Recipes.aspx");
        }

        connection.Open();
        var query = "Select * from AddRecipe,Category,Cuisine where AddRecipe.RecipeNumbers="+cookie+" and Category.RecipeNumber="+cookie+" and Cuisine.RecipeNumber="+cookie;
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@RecipeNumbers",cookie);
        var reader = command.ExecuteReader();
        int j = 1;
        if (reader.HasRows)
            while (reader.Read())
            {
                //int count =(int)command2.ExecuteScalar();
                //for (int j = 0; j<count ; j++)
                //{
                var image = "Select RecipeImage from AddRecipe where  RecipeNumbers=" + cookie;
                var imagedata = new SqlCommand(image, connection);
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
                    //var innerDiv=new HtmlGenericControl("DIV");
                    //innerDiv.Attributes["class"] = "btn-toolbar pull-right";
                    //Button deleteButton = new Button();
                    //deleteButton.Attributes["class"] = "btn btn-danger";

                    //deleteButton.Attributes["data-target"] = "#Delete";
                    //deleteButton.Text = "Delete Recipe";
                    //deleteButton.ID = "Delete";
                    //deleteButton.ClientIDMode = ClientIDMode.Static;
                    //deleteButton.Click += new EventHandler(Update_Click);
                    //Button editRecipe = new Button();
                    //editRecipe.Attributes["class"] = "btn btn-primary";

                    //editRecipe.Attributes["data-target"] = "#Edit";
                    //editRecipe.Text = "Edit Recipe";
                    //editRecipe.ID = "Edit";
                    //editRecipe.ClientIDMode = ClientIDMode.Static;
                    //editRecipe.Click += new EventHandler(Update_Click);
                    string reacipeName = (string)reader["RecipeName"];

                    createDiv.ClientIDMode = ClientIDMode.Static;
                    
                    createDiv.Attributes["class"] = "col-sm-12 col-xs-12 col-md-12 ";
                    createDiv.InnerHtml = "<img src=" + imageUrl +
                                          " alt='Recipe Image' class='img-responsive img-rounded img-thumbnail center block ' style:'max-width:100%'  > <div class='jumbotron' > <h2 ID='RecipeName' class='text-center' runat='server'>" +
                                          reader["RecipeName"] + "</h2>" + "<p> Category :- " + reader["Category"] + "</p></br><p> Cooking Time :- "+reader["CookingTime"] + "</p></br><p>Cuisine :- "+reader["cusine"] + "</p></br><p> Recipe Description :- "+reader["RecipeDescription"]+ "</p></br><p>Recipe Steps :- " +reader["RecipeSteps"]+ "</p></div>";
                    createDiv.ID = "createDiv" + cookie;
                    //innerDiv.ID = "innerDiv" + cookie;
                    dynamic.Controls.Add(createDiv);
                    //dynamic.Controls.Add(innerDiv);
                    //innerDiv.Controls.Add(deleteButton);
                    //innerDiv.Controls.Add(editRecipe);
                    j++;
                }


            }
        else { 
            dynamic.InnerHtml = "<div style='font-family: 'Pacifico', cursive;'> Sorry, This Recipe has been deleted by Admin</div>";
            buttons.Visible = false;
        }
        connection.Close();
    }
    
    protected void Delete_click(object sender, EventArgs e)
    {

       
        int convert = Convert.ToInt32(cookie);
        connection.Open();
        var command = new SqlCommand("Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@recipeNumber", cookie);
        var reader = command.ExecuteNonQuery();
        connection.Close();
        Response.RedirectPermanent("Recipes.aspx");
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        int recipeNumber = Convert.ToInt32(cookie);
        //update.ID = cookie;
        string checkBox;
        connection.Open();
        var command = new SqlCommand("Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@RecipeNumber", recipeNumber);
        command.Parameters.AddWithValue("@RecipeName", Recipebox.Text);
        command.Parameters.AddWithValue("@Category", CategoryList.Text);
        command.Parameters.AddWithValue("@CookingTime", CookingTimeBox.Text);
        command.Parameters.AddWithValue("@Cuisine", CuisineList.Text);
        if (Private.Checked)
            checkBox = "Private";
        else
            checkBox = "Public";
        command.Parameters.AddWithValue("@Limits", checkBox);
        command.Parameters.AddWithValue("@RecipeDescription", Steps.Text);
        command.Parameters.AddWithValue("@RecipeSteps", RecipeStep.Text);

        var reader = command.ExecuteNonQuery();
        
        connection.Close();
        Response.Redirect("Recipes.aspx");
    }
}