USE [FamilyCore_Dev]
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
ALTER PROCEDURE [dbo].[GetWeekOrderEntreeDetails]
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

