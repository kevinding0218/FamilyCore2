USE [FamilyCoreDev]
GO
/****** Object:  StoredProcedure [dbo].[GetWeekOrderPrepare]    Script Date: 1/29/2018 3:12:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEntreeOrderMappingScheduleDate]
	@EntreeId INT,
	@OrderId INT,
	@ScheduleDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.Entrees_Orders
	SET ScheduledDate = @ScheduleDate
	WHERE OrderId = @OrderId AND EntreeId = @EntreeId
END
go
/****** Object:  StoredProcedure [dbo].[GetEntreeInfoById]    Script Date: 1/26/2018 5:22:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWeekOrderPrepare]
	@StartDate DATETIME,
	@EndDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT O.Id AS OrderId, E.Id AS EntreeId, 
	ES.Style, EC.Catagory, E.Name AS EntreeName, 
	EO.Count AS EntreeCount, EO.ScheduledDate, EO.Note AS Note, 'assets/images/logo.png' AS EntreeImgUrl
	FROM dbo.[Order] O
	INNER JOIN dbo.Entrees_Orders EO ON EO.OrderId = O.Id
	INNER JOIN dbo.Entree E ON EO.EntreeId = E.Id
	INNER JOIN dbo.EntreeStyle ES ON ES.Id = E.EntreeStyleId
	INNER JOIN dbo.EntreeCatagory EC ON EC.Id = E.EntreeCatagoryId
	WHERE O.StartDate <= ISNULL(@StartDate, GETDATE()) AND O.EndDate > ISNULL(@EndDate, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[GetWeekOrderPrepare]    Script Date: 1/27/2018 1:58:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetWeekOrderEntreeDetails]
	@StartDate DATETIME,
	@EndDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ed.Name AS EntreeDetailName, SUM(ESDS.Quantity) AS EntreeDetailQty, EDT.DetailName AS EntreeDetailTypeName, SF.Name AS StapleFood
	FROM dbo.[Order] O
	INNER JOIN dbo.Entrees_Orders EO ON EO.OrderId = O.Id
	INNER JOIN dbo.Entree E ON E.Id = EO.EntreeId
	INNER JOIN dbo.StapleFood SF ON SF.Id = E.StapleFoodId
	INNER JOIN dbo.Entrees_Details ESDS ON ESDS.EntreeId = E.Id
	INNER JOIN dbo.EntreeDetail ED ON ED.Id = ESDS.EntreeDetailId
	INNER JOIN dbo.EntreeDetailType EDT ON EDT.Id = ED.EntreeDetailTypeId
	WHERE O.StartDate <= ISNULL(@StartDate, GETDATE()) AND O.EndDate > ISNULL(@EndDate, GETDATE())
	GROUP BY ed.Name, EDT.DetailName, SF.Name, EDT.Id
	ORDER BY EDT.Id
END
GO
CREATE PROCEDURE [dbo].[GetSimilarEntreeList]
	@entreeName NVARCHAR(50) = NULL,
	@stapleFoodId INT = NULL,
	@entreeDetailIdList NVARCHAR(50) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  e.Id AS Id, e.Name AS Name
	FROM dbo.Entree e
	LEFT JOIN
			(
	Select distinct ST2.EntreeId, 
		(
			Select CAST(ed.Id AS NVARCHAR) + ', ' AS [text()]
			FROM dbo.Entrees_Details eds
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			Where eds.EntreeId = ST2.EntreeId
			ORDER BY eds.EntreeId
			For XML PATH ('')
		) EntreeDetails
	From dbo.Entrees_Details ST2
	) EntreeDetailMapping ON EntreeDetailMapping.EntreeId = e.Id
	WHERE LEFT(EntreeDetailMapping.EntreeDetails,Len(EntreeDetailMapping.EntreeDetails)-1) = @entreeDetailIdList
	AND e.Name <> @entreeName AND e.StapleFoodId = @stapleFoodId
END
GO
CREATE PROCEDURE [dbo].[GetNumberOfEntreeByStapleFoodId]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) AS TotalEntrees FROM dbo.Entree e
	INNER JOIN dbo.StapleFood sf ON sf.Id = e.StapleFoodId 
	WHERE sf.Id = @Id
END
GO
CREATE PROCEDURE [dbo].[GetNumberOfEntreeByEntreeDetailId]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(*) AS TotalEntrees FROM Entrees_Details WHERE EntreeDetailId = @Id
END
GO
CREATE PROCEDURE [dbo].[GetNavigationBadge]
	@NavName NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @NavName = 'Dashboard'
	BEGIN
	    SELECT 'info' AS Variant, 'NEW' AS Text
	END
	ELSE IF @NavName = 'Entrees'
	BEGIN
	    SELECT  'success' AS Variant, 
		CAST(COUNT(*) AS VARCHAR) AS Text
		FROM dbo.Entree
	END
	ELSE IF @NavName = 'Vegetables'
	BEGIN
	    SELECT  'primary' AS Variant, 
		CAST(COUNT(*) AS VARCHAR) AS Text
		FROM dbo.EntreeDetail ED
		INNER JOIN	dbo.EntreeDetailType edt ON edt.Id = ED.EntreeDetailTypeId
		WHERE edt.DetailName = 'Vegetable'
	END
	ELSE IF @NavName = 'Meats'
	BEGIN
	    SELECT  'warning' AS Variant, 
		CAST(COUNT(*) AS VARCHAR) AS Text
		FROM dbo.EntreeDetail ED
		INNER JOIN	dbo.EntreeDetailType edt ON edt.Id = ED.EntreeDetailTypeId
		WHERE edt.DetailName = 'Meat'
	END
	ELSE IF @NavName = 'Seafood'
	BEGIN
	    SELECT  'danger' AS Variant, 
		CAST(COUNT(*) AS VARCHAR) AS Text
		FROM dbo.EntreeDetail ED
		INNER JOIN	dbo.EntreeDetailType edt ON edt.Id = ED.EntreeDetailTypeId
		WHERE edt.DetailName = 'Seafood'
	END
	ELSE IF @NavName = 'Ingredient'
	BEGIN
	    SELECT  'info' AS Variant, 
		CAST(COUNT(*) AS VARCHAR) AS Text
		FROM dbo.EntreeDetail ED
		INNER JOIN	dbo.EntreeDetailType edt ON edt.Id = ED.EntreeDetailTypeId
		WHERE edt.DetailName = 'Ingredient'
	END	
	ELSE IF @NavName = 'Sauce'
	BEGIN
	    SELECT  'success' AS Variant, 
		CAST(COUNT(*) AS VARCHAR) AS Text
		FROM dbo.EntreeDetail ED
		INNER JOIN	dbo.EntreeDetailType edt ON edt.Id = ED.EntreeDetailTypeId
		WHERE edt.DetailName = 'Sauce'
	END	
	ELSE IF @NavName = 'Order'
	BEGIN
	    SELECT  'primary' AS Variant, 
		CAST(SUM(EO.Count) AS VARCHAR) AS Text
		FROM dbo.[Order] O
		INNER JOIN dbo.Entrees_Orders EO ON EO.OrderId = O.Id
		WHERE O.StartDate <= GETDATE() AND O.EndDate > GETDATE()
	END	
END
GO
CREATE PROCEDURE [dbo].[GetEntreeInfoBySplit]
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
		FROM dbo.Entree e
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
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
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
		FROM dbo.Entree e
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
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
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
		FROM dbo.Entree e
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
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
			GROUP BY e.Id, edt.DetailType
		) SauceCountTable ON SeafoodCountTable.Id = e.Id
		ORDER BY e.LastUpdatedByOn DESC, e.CurrentRank DESC
	END
END
GO
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
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
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
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
			GROUP BY e.Id, edt.DetailType
		) VegeCountTable ON VegeCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
			GROUP BY e.Id, edt.DetailType
		) MeatCountTable ON MeatCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
			GROUP BY e.Id, edt.DetailType
		) SeafoodCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
			GROUP BY e.Id, edt.DetailType
		) IngredientCountTable ON SeafoodCountTable.Id = e.Id
		LEFT JOIN
		(
			SELECT COUNT(*) AS DetailCount, e.Id, edt.DetailType
			FROM dbo.Entree e
			INNER JOIN dbo.Entrees_Details eds ON eds.EntreeId = e.Id
			INNER JOIN dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
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
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = ' ﬂ≤À'
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
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '»‚¿‡'
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
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '∫£œ '
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
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = 'Ω¥÷≠'
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
					INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId AND edt.DetailType = '≈‰¡œ'
					Where eds.EntreeId = ST2.EntreeId
					ORDER BY eds.EntreeId
					For XML PATH ('')
				) Ingredients
			From dbo.Entrees_Details ST2
		) Entree_Ingredient ON Entree_Ingredient.EntreeId = e.Id
		WHERE e.Id = @Id
	END
END
GO
CREATE PROCEDURE [dbo].[GetEntreeCountBySplit]
	@splitBy NVARCHAR(50) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @splitBy = 'catagory'
	BEGIN
		SELECT COUNT(e.Id) AS SplitByCount, ec.Id AS SplitId, ec.Catagory AS SplitByName 
		, CAST((COUNT(e.Id) * 100/(SELECT COUNT(*) FROM dbo.Entree)) AS VARCHAR) AS SplitByPercentage
		, ec.Id % 5 AS StyleBgId
		FROM [dbo].EntreeCatagory ec 
		LEFT JOIN dbo.Entree e ON e.EntreeCatagoryId = ec.Id
		GROUP BY ec.Catagory, ec.Id
	END
	ELSE IF @splitBy = 'style'
	BEGIN
		SELECT COUNT(e.Id) AS SplitByCount, es.Id AS SplitId, es.Style AS SplitByName 
		, CAST((COUNT(e.Id) * 100/(SELECT COUNT(*) FROM dbo.Entree)) AS VARCHAR) AS SplitByPercentage
		, es.Id % 5 AS StyleBgId
		FROM [dbo].EntreeStyle es 
		LEFT JOIN dbo.Entree e ON e.EntreeStyleId = es.Id
		GROUP BY es.Style, es.Id
	END
END
GO
CREATE PROCEDURE [dbo].[GetAvailableEntreeDetailByDetailType]
	@Id INT = NULL,
	@EntreeDetailType NVARCHAR(20) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ed.Id, ed.Name FROM dbo.EntreeDetail ed
	INNER JOIN dbo.EntreeDetailType edt ON edt.Id = ed.EntreeDetailTypeId
	WHERE LOWER(edt.DetailName) = @EntreeDetailType
	--AND
	--NOT ExistS 
	--(
	--SELECT * FROM dbo.Entrees_Details esds
	--WHERE ed.Id = esds.EntreeDetailId and esds.EntreeId = @Id
	--)
END
GO
