<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="moreInfo.aspx.cs" Inherits="moreInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="padd" style="padding-top: 90px">
    <div class="container ">
    <div runat="server" id="dynamic">


    </div>
    <div class=" btn-toolbar " id="buttons" runat="server">
        <button type="button" id="editRecipe" class="btn btn-primary pull-right" data-target="#Edit" data-toggle="modal">Edit Recipe</button>
        <button type="button" id="deleteRecipe" class="btn btn-danger pull-right" data-target="#Delete" data-toggle="modal">Delete Recipe</button>
    </div>
    <div class="modal fade" tabindex="-1" id="Delete" role="dialog" aria-labelledby="modalLabel">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span class="pull-right" style="padding-top: 20px">&times;</span></button>
                    <h2 class="modal-title text-danger text-capitalize" id="modalLabel">
                        Danger Zone <i class="fi-skull" style="font-size: 50px"></i>
                    </h2>
                </div>
                <div class="modal-body text-danger">
                    Are you sure you want to delete the Current Recipe. You should be aware about the concequenceses of your action and you are responsible for deleting this recipe.
                </div>
                <div class="modal-footer">
                    <asp:Button type="button" CssClass="btn btn-danger text-danger fa fa-trash" runat="server" Text="Yes I, agree to delete the current recipe" OnClick="Delete_click"/> <%--runat="server"--%>
               
                </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" id="Edit" role="dialog" aria-labelledby="modalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span class="pull-right" style="padding-top: 20px">&times;</span></button>
                    <h2 class="modal-title text-primary text-capitalize" id="modalLabelEdit">
                        Edit Recipe <i class="glyphicon glyphicon-edit"></i>
                    </h2>
                </div>
                <div class="modal-body text">
                    <div class="container formtext text padd">
                        <div class="row">
                            <section class="col-sm-12 col-md-10">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label CssClass="col-sm-3 control-label " ID="RecipeName" runat="server" Text="Recipe Name"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox CssClass="form-control" placeholder="Recipe Name" ID="Recipebox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label CssClass="col-sm-3 control-label " ID="Category" runat="server" Text="Category"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList CssClass="dropdown form-control text" ID="CategoryList" runat="server">
                                            <asp:ListItem>Vegeterian</asp:ListItem>
                                            <asp:ListItem>Non-Veg</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label CssClass="col-sm-3 control-label " ID="CookingTime" runat="server" Text="Cooking Time"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox CssClass="form-control" placeholder="hh:mm" ID="CookingTimeBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label CssClass="col-sm-3 control-label " ID="Cuisine" runat="server" Text="Cuisine"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList CssClass="dropdown  form-control  text" ID="CuisineList" runat="server">
                                            <asp:ListItem>Snack</asp:ListItem>
                                            <asp:ListItem>Meal</asp:ListItem>
                                            <asp:ListItem>Dessert</asp:ListItem>
                                            <asp:ListItem>Appetizers</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group ">

                                    <div class="col-sm-9 col-sm-offset-3">
                                        <asp:CheckBox CssClass="control-label" ID="Private" runat="server"/><asp:Label CssClass="" ID="PrivateLabel" runat="server" Text="Mark as private"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label CssClass="col-sm-3 control-label " ID="RecipeDescription" runat="server" Text="Recipe Description"></asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox ID="Steps" CssClass="form-control" placeholder="Write a small description for your recipe" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="RecipeSteps" CssClass="col-sm-3 control-label" runat="server" Text="Recipe Steps">
                                    </asp:Label>
                                    <div class="col-sm-6 ">
                                        <asp:TextBox ID="RecipeStep" placeholder="Write the steps in well organised manner" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </section>
                        </div>
                      </div>
                        <div class="modal-footer">
                            <asp:Button type="button" class="btn btn-success text-danger" runat="server" Text="Update the recipe" OnClick="Update_Click"/>
                        </div>
                    
                </div>  
            </div>
        </div>
    </div>
      </div>
    
</asp:Content>

