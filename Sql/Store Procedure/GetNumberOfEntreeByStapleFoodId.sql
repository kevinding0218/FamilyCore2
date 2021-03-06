USE [FamilyCore_Dev]
GO
/****** Object:  StoredProcedure [dbo].[GetNumberOfEntreeByVegeId]    Script Date: 1/14/2018 3:22:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
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
