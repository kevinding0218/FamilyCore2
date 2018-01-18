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
	WHERE edt.DetailType = @EntreeDetailType
	AND
	NOT ExistS 
	(
	SELECT * FROM dbo.Entrees_Details esds
	WHERE ed.Id = esds.EntreeDetailId and esds.EntreeId = @Id
	)
END

