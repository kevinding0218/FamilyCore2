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
ALTER PROCEDURE [dbo].[GetWeekOrderPrepare]
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
	EO.Count AS EntreeCount, EO.ScheduledDate, EO.Note AS Note, PrimaryPhoto.FileName AS EntreeImgUrl
	FROM FamilyCore_Dev.dbo.[Order] O
	INNER JOIN FamilyCore_Dev.dbo.Entrees_Orders EO ON EO.OrderId = O.Id
	INNER JOIN FamilyCore_Dev.dbo.Entree E ON EO.EntreeId = E.Id
	INNER JOIN FamilyCore_Dev.dbo.EntreeStyle ES ON ES.Id = E.EntreeStyleId
	INNER JOIN FamilyCore_Dev.dbo.EntreeCatagory EC ON EC.Id = E.EntreeCatagoryId
	LEFT JOIN (
		SELECT TOP 1 ep.FileName, ep.EntreeId FROM dbo.Entree e
		INNER JOIN dbo.EntreePhoto ep ON ep.EntreeId = e.Id
	)PrimaryPhoto ON PrimaryPhoto.EntreeId = e.Id
	WHERE O.StartDate <= ISNULL(@StartDate, GETDATE()) AND O.EndDate > ISNULL(@EndDate, GETDATE())
END

