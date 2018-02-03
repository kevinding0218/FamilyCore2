USE [FamilyCore_Dev]
GO
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
ALTER PROCEDURE [dbo].[GetNavigationBadge]
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

