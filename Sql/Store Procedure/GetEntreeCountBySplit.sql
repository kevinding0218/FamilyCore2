USE [FamilyCoreDev]
GO
/****** Object:  StoredProcedure [dbo].[GetEntreeCountBySplit]    Script Date: 2/7/2018 10:31:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
		, CAST((COUNT(e.Id) * 100/(SELECT CASE COUNT(*) WHEN 0 THEN 1 ELSE COUNT(*) END FROM dbo.Entree)) AS VARCHAR) AS SplitByPercentage
		, ec.Id % 5 AS StyleBgId
		FROM [dbo].EntreeCatagory ec 
		LEFT JOIN dbo.Entree e ON e.EntreeCatagoryId = ec.Id
		GROUP BY ec.Catagory, ec.Id
	END
	ELSE IF @splitBy = 'style'
	BEGIN
		SELECT COUNT(e.Id) AS SplitByCount, es.Id AS SplitId, es.Style AS SplitByName 
		, CAST((COUNT(e.Id) * 100/(SELECT CASE COUNT(*) WHEN 0 THEN 1 ELSE COUNT(*) END FROM dbo.Entree)) AS VARCHAR) AS SplitByPercentage
		, es.Id % 5 AS StyleBgId
		FROM [dbo].EntreeStyle es 
		LEFT JOIN dbo.Entree e ON e.EntreeStyleId = es.Id
		GROUP BY es.Style, es.Id
	END
END
