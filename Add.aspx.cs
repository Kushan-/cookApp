﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

public partial class Add : Page
{
    private readonly SqlConnection connection =
        new SqlConnection("Data Source = recipebook.database.windows.net; Initial Catalog = CookBook; Integrated Security = False; User ID = Satnam; Password=GURsat1313!;Connect Timeout = 15; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");//(
        //    "Data Source=DESKTOP-B5SA0JC\\SATNAM;Initial Catalog=Comp229;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    this.Master.MasterPageFile = "~/MasterSingin.master";
    //}

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
    protected void Submit_Click(object sender, EventArgs e)
    {
        string checkBox;
        connection.Open();
        var command = new SqlCommand("AddingRecipe", connection)
        {
            CommandType =CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@RecipeName", Recipebox.Text);
        command.Parameters.AddWithValue("@SubmittedBy", SubmittedBox.Text);
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
        var postedFile = ImageUpload.PostedFile;
        var filename = Path.GetFileName(postedFile.FileName);
        var fileExtension = Path.GetExtension(filename);
        var fileSize = postedFile.ContentLength;
        byte[] bytes = null;
        if ((fileExtension.ToLower() == ".jpg") || (fileExtension.ToLower() == ".gif") ||
            (fileExtension.ToLower() == ".png") || (fileExtension.ToLower() == ".bmp"))
        {
            var stream = postedFile.InputStream;
            var binaryReader = new BinaryReader(stream);
            bytes = binaryReader.ReadBytes((int) stream.Length);
        }
        else
        {
            Image.Text = "File Cannot be loade";
        }
        command.Parameters.AddWithValue("@RecipeImage", bytes);
        command.ExecuteNonQuery();
        connection.Close();
        Response.Redirect("Recipes.aspx");
    }

    
}
    
    




