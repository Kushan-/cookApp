CREATE PROCEDURE [dbo].[Delete]
	@recipeNumber int
AS
	Delete from Category where RecipeNumber=@recipeNumber
	Delete from Cuisine where RecipeNumber=@recipeNumber	
	Delete from AddRecipe Where RecipeNumbers=@recipeNumber
RETURN 0

