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
ALTER PROCEDURE [dbo].[GetSimilarEntreeList]
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
	FROM FamilyCore_Dev.dbo.Entree e
	LEFT JOIN
			(
	Select distinct ST2.EntreeId, 
		(
			Select CAST(ed.Id AS NVARCHAR) + ', ' AS [text()]
			FROM FamilyCore_Dev.dbo.Entrees_Details eds
			INNER JOIN FamilyCore_Dev.dbo.EntreeDetail ed ON ed.Id = eds.EntreeDetailId
			Where eds.EntreeId = ST2.EntreeId
			ORDER BY eds.EntreeId
			For XML PATH ('')
		) EntreeDetails
	From dbo.Entrees_Details ST2
	) EntreeDetailMapping ON EntreeDetailMapping.EntreeId = e.Id
	WHERE LEFT(EntreeDetailMapping.EntreeDetails,Len(EntreeDetailMapping.EntreeDetails)-1) = @entreeDetailIdList
	AND e.Name <> @entreeName --AND e.StapleFoodId = @stapleFoodId
END

