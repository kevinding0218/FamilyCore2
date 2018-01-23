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
ALTER PROCEDURE [dbo].[GetEntreeCountBySplit]
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

