CREATE PROCEDURE [dbo].[Update]
	@RecipeNumber int,
	@RecipeName nvarchar(90),
	@Category nvarchar(90),
	@CookingTime nvarchar(90),
	@Cuisine nvarchar(90),
	@Limits nvarchar(90),
	@RecipeDescription nvarchar(90),
	@RecipeSteps text 
AS
	UPDATE Category set category=@Category where RecipeNumber=@RecipeNumber;
	UPDATE Cuisine set cusine=@Cuisine where RecipeNumber=@RecipeNumber;
	UPDATE AddRecipe set RecipeName=@RecipeName, CookingTime=@CookingTime,Limits=@Limits,RecipeDescription=@RecipeDescription,RecipeSteps=@RecipeSteps where RecipeNumbers=@RecipeNumber
RETURN 

