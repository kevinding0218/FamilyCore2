
/****** Object:  StoredProcedure [dbo].[GetEntreeInfoById]    Script Date: 1/15/2018 8:25:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEntreeInfoById]
	@Id INT = NULL,
	@Type NVARCHAR(20) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @Type = 'EntreeDetail'
	BEGIN
		SELECT e.Id AS EntreeId, e.Name AS EntreeName, 
		ISNULL(sf.Name, '') AS StapleFood,
		ISNULL(VegeCountTable.DetailCount, 0) AS VegetableCount,
		ISNULL(MeatCountTable.DetailCount, 0) AS MeatCount,
		ISNULL(SeafoodCountTable.DetailCount, 0) AS SeafoodCount,
		ISNULL(IngredientCountTable.DetailCount, 0) AS IngredientCount,
		ISNULL(SauceCountTable.DetailCount, 0) AS SauceCount,
		es.Style,
		ec.Catagory,
		e.CurrentRank AS Rank,
		ISNULL(e.Note, '') AS Note,
		e.AddedById, addedUser.FirstName + ' ' + addedUser.LastName AS addedByUserName, convert(varchar(25), e.AddedOn, 120)  as AddedOn
		FROM dbo.Entree e
		INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
		LEFT JOIN dbo.StapleFood sf ON sf.Id = e.StapleFoodId
		INNER JOIN dbo.EntreeStyle es ON es.Id = e.EntreeStyleId
		INNER JOIN dbo.EntreeCatagory ec ON ec.Id = e.EntreeCatagoryId
		LEFT JOIN dbo.Users addedUser ON addedUser.UserID = e.AddedById
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'蔬菜'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'肉类'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'海鲜'
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'配料'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'酱汁'
			GROUP BY e.Id, edt.DetailType
		) SauceCountTable ON SeafoodCountTable.Id = e.Id
		WHERE eds.EntreeDetailId = @Id
	END
	ELSE IF @Type = 'StapleFood'
	BEGIN
		SELECT e.Id AS EntreeId, e.Name AS EntreeName, 
		ISNULL(sf.Name, '') AS StapleFood,
		ISNULL(VegeCountTable.DetailCount, 0) AS VegetableCount,
		ISNULL(MeatCountTable.DetailCount, 0) AS MeatCount,
		ISNULL(SeafoodCountTable.DetailCount, 0) AS SeafoodCount,
		ISNULL(IngredientCountTable.DetailCount, 0) AS IngredientCount,
		ISNULL(SauceCountTable.DetailCount, 0) AS SauceCount, 
		es.Style,
		ec.Catagory,
		e.CurrentRank AS Rank,
		ISNULL(e.Note, '') AS Note,
		e.AddedById, addedUser.FirstName + ' ' + addedUser.LastName AS addedByUserName, convert(varchar(25), e.AddedOn, 120)  as AddedOn
		FROM dbo.Entree e
		INNER JOIN dbo.StapleFood sf ON sf.Id = e.StapleFoodId
		INNER JOIN dbo.EntreeStyle es ON es.Id = e.EntreeStyleId
		INNER JOIN dbo.EntreeCatagory ec ON ec.Id = e.EntreeCatagoryId
		LEFT JOIN dbo.Users addedUser ON addedUser.UserID = e.AddedById
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'蔬菜'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'肉类'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'海鲜'
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'配料'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'酱汁'
			GROUP BY e.Id, edt.DetailType
		) SauceCountTable ON SeafoodCountTable.Id = e.Id
		WHERE sf.Id = @Id
	END
	ELSE IF @Type = 'Entree'
	BEGIN
	    Select	Entree_Vegetable.EntreeId,
		e.Name AS EntreeName,
		LEFT(Entree_Vegetable.Vegetables,Len(Entree_Vegetable.Vegetables)-1) AS Vegetable,
		LEFT(Entree_Meat.Meats,Len(Entree_Meat.Meats)-1) AS Meat,
		LEFT(Entree_Seafood.Seafoods,Len(Entree_Seafood.Seafoods)-1) AS Seafood,
		LEFT(Entree_Sauce.Sauces,Len(Entree_Sauce.Sauces)-1) AS Sauce,
		LEFT(Entree_Ingredient.Ingredients,Len(Entree_Ingredient.Ingredients)-1) AS Ingredient,
		ISNULL(e.Note, '') AS Note
		FROM	dbo.Entree e
		LEFT JOIN
		(
			Select distinct ST2.EntreeId, 
				(
					Select ISNULL(ed.Name, '') + ' x ' + LTRIM(STR(ISNULL(eds.Quantity, 0))) + ', ' AS [text()]
					FROM dbo.Entrees_Details eds
					INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'蔬菜'
					Where eds.EntreeId = ST2.EntreeId
					ORDER BY eds.EntreeId
					For XML PATH ('')
				) Vegetables
			From dbo.Entrees_Details ST2
		) Entree_Vegetable	ON Entree_Vegetable.EntreeId = e.Id
		LEFT JOIN
		(
			SELECT distinct ST2.EntreeId, 
				(
					Select ISNULL(ed.Name, '') + ' x ' + LTRIM(STR(ISNULL(eds.Quantity, 0))) + ', ' AS [text()]
					FROM dbo.Entrees_Details eds
					INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'肉类'
					Where eds.EntreeId = ST2.EntreeId
					ORDER BY eds.EntreeId
					For XML PATH ('')
				) Meats
			From dbo.Entrees_Details ST2
		) Entree_Meat ON Entree_Meat.EntreeId = e.Id
		LEFT JOIN
		(
			SELECT distinct ST2.EntreeId, 
				(
					Select ISNULL(ed.Name, '') + ' x ' + LTRIM(STR(ISNULL(eds.Quantity, 0))) + ', ' AS [text()]
					FROM dbo.Entrees_Details eds
					INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'海鲜'
					Where eds.EntreeId = ST2.EntreeId
					ORDER BY eds.EntreeId
					For XML PATH ('')
				) Seafoods
			From dbo.Entrees_Details ST2
		) Entree_Seafood ON Entree_Seafood.EntreeId = e.Id
		LEFT JOIN
		(
			SELECT distinct ST2.EntreeId, 
				(
					Select ISNULL(ed.Name, '') + ' x ' + LTRIM(STR(ISNULL(eds.Quantity, 0))) + ', ' AS [text()]
					FROM dbo.Entrees_Details eds
					INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'酱汁'
					Where eds.EntreeId = ST2.EntreeId
					ORDER BY eds.EntreeId
					For XML PATH ('')
				) Sauces
			From dbo.Entrees_Details ST2
		) Entree_Sauce ON Entree_Sauce.EntreeId = e.Id
		LEFT JOIN
		(
			SELECT distinct ST2.EntreeId, 
				(
					Select ISNULL(ed.Name, '') + ' x ' + LTRIM(STR(ISNULL(eds.Quantity, 0))) + ', ' AS [text()]
					FROM dbo.Entrees_Details eds
					INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = N'配料'
					Where eds.EntreeId = ST2.EntreeId
					ORDER BY eds.EntreeId
					For XML PATH ('')
				) Ingredients
			From dbo.Entrees_Details ST2
		) Entree_Ingredient ON Entree_Ingredient.EntreeId = e.Id
		WHERE e.Id = @Id
	END
END

