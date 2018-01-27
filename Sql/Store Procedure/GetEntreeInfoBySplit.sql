USE [FamilyCore_Dev]
GO
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
ALTER PROCEDURE [dbo].[GetEntreeInfoBySplit]
	@SplitBy NVARCHAR(20) = NULL,
	@Id INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @SplitBy = 'style'
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
		e.AddedById, addedUser.FirstName + ' ' + addedUser.LastName AS AddedByUserName, 
		CONVERT(varchar(25), e.AddedOn, 120)  as AddedOn
		FROM FamilyCore_Dev.dbo.Entree e
		LEFT JOIN FamilyCore_Dev.dbo.StapleFood sf ON sf.Id = e.StapleFoodId
		INNER JOIN FamilyCore_Dev.dbo.EntreeStyle es ON es.Id = e.EntreeStyleId
		INNER JOIN FamilyCore_Dev.dbo.EntreeCatagory ec ON ec.Id = e.EntreeCatagoryId
		LEFT JOIN FamilyCore_Dev.dbo.Users addedUser ON addedUser.UserID = e.AddedById
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
			GROUP BY e.Id, edt.DetailType
		) SauceCountTable ON SeafoodCountTable.Id = e.Id
		WHERE es.Id = @Id
		ORDER BY e.LastUpdatedByOn DESC, e.CurrentRank DESC
	END
	ELSE IF @SplitBy = 'catagory'
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
		e.AddedById, addedUser.FirstName + ' ' + addedUser.LastName AS AddedByUserName, 
		CONVERT(varchar(25), e.AddedOn, 120)  as AddedOn
		FROM FamilyCore_Dev.dbo.Entree e
		LEFT JOIN FamilyCore_Dev.dbo.StapleFood sf ON sf.Id = e.StapleFoodId
		INNER JOIN FamilyCore_Dev.dbo.EntreeStyle es ON es.Id = e.EntreeStyleId
		INNER JOIN FamilyCore_Dev.dbo.EntreeCatagory ec ON ec.Id = e.EntreeCatagoryId
		LEFT JOIN FamilyCore_Dev.dbo.Users addedUser ON addedUser.UserID = e.AddedById
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
			GROUP BY e.Id, edt.DetailType
		) SauceCountTable ON SeafoodCountTable.Id = e.Id
		WHERE ec.Id = @Id
		ORDER BY e.LastUpdatedByOn DESC, e.CurrentRank DESC
	END
	ELSE
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
		e.AddedById, addedUser.FirstName + ' ' + addedUser.LastName AS AddedByUserName, 
		CONVERT(varchar(25), e.AddedOn, 120)  as AddedOn
		FROM FamilyCore_Dev.dbo.Entree e
		LEFT JOIN FamilyCore_Dev.dbo.StapleFood sf ON sf.Id = e.StapleFoodId
		INNER JOIN FamilyCore_Dev.dbo.EntreeStyle es ON es.Id = e.EntreeStyleId
		INNER JOIN FamilyCore_Dev.dbo.EntreeCatagory ec ON ec.Id = e.EntreeCatagoryId
		LEFT JOIN FamilyCore_Dev.dbo.Users addedUser ON addedUser.UserID = e.AddedById
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM FamilyCore_Dev.dbo.Entree e
			INNER JOIN FamilyCore_Dev.dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
			GROUP BY e.Id, edt.DetailType
		) SauceCountTable ON SeafoodCountTable.Id = e.Id
		ORDER BY e.LastUpdatedByOn DESC, e.CurrentRank DESC
	END
END

