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

