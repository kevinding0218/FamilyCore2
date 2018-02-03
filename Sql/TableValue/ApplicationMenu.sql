INSERT INTO dbo.ApplicationMenu
(
    Icon,
    Name,
    Url,
	MenuPosition
)
VALUES
(   
    N'icon-speedometer', -- Icon - nvarchar(max)
    N'Dashboard', -- Name - nvarchar(max)
    N'/dashboard',  -- Url - nvarchar(max)
	1
 )

 INSERT INTO dbo.ApplicationMenu
(
    Icon,
    Name,
    Url,
	MenuPosition
)
VALUES
(   
    N'icon-fire', -- Icon - nvarchar(max)
    N'Meal', -- Name - nvarchar(max)
    N'/meal',  -- Url - nvarchar(max)
	2
 )

INSERT INTO dbo.ApplicationMenu
(
    Icon,
    Name,
    Url,
	ShowBadge,
	MenuPosition
)
VALUES
(   
    N'icon-calculator', -- Icon - nvarchar(max)
    N'Order', -- Name - nvarchar(max)
    N'/order',  -- Url - nvarchar(max)
	1,
	3
 )

 INSERT INTO dbo.ApplicationMenu
(
    Icon,
    Name,
    Url,
	MenuPosition
)
VALUES
(   
    N'icon-calculator', -- Icon - nvarchar(max)
    N'Calendar', -- Name - nvarchar(max)
    N'/order/setupCalendar',  -- Url - nvarchar(max)
	4
 )

 INSERT INTO dbo.ApplicationMenu
(
    Icon,
    Name,
    Url,
	MenuPosition
)
VALUES
(   
    N'icon-star', -- Icon - nvarchar(max)
    N'Pages', -- Name - nvarchar(max)
    N'/pages',  -- Url - nvarchar(max)
	5
 )

 ---------------Meal Children pages
 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'fa fa-eercast', -- Icon - nvarchar(max)
     N'Entrees', -- Name - nvarchar(max)
     N'/meal/entreeSummary',  -- Url - nvarchar(max)
	 1,
	 1
     )

 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'fa fa-snowflake-o', -- Icon - nvarchar(max)
     N'Vegetables', -- Name - nvarchar(max)
     N'/meal/vegetableList/vegetable',  -- Url - nvarchar(max)
	 1,
	 2
     )

 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'fa fa-superpowers', -- Icon - nvarchar(max)
     N'Meats', -- Name - nvarchar(max)
     N'/meal/meatList/meat',  -- Url - nvarchar(max)
	 1,
	 3
     )

 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'fa fa-telegram', -- Icon - nvarchar(max)
     N'Seafood', -- Name - nvarchar(max)
     N'/meal/seafoodList/seafood',  -- Url - nvarchar(max)
	 1,
	 4
     )

 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'fa fa-bandcamp', -- Icon - nvarchar(max)
     N'Ingredient', -- Name - nvarchar(max)
     N'/meal/ingredientList/ingredient',  -- Url - nvarchar(max)
	 1,
	 5
     )

 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'fa fa-free-code-camp', -- Icon - nvarchar(max)
     N'Sauce', -- Name - nvarchar(max)
     N'/meal/sauceList/sauce',  -- Url - nvarchar(max)
	 1,
	 6
     )

 INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 ShowBadge,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Meal'),   -- ApplicationMenuId - int
     N'icon-star', -- Icon - nvarchar(max)
     N'Staple Food', -- Name - nvarchar(max)
     N'/meal/staplefoodList',  -- Url - nvarchar(max)
	 1,
	 7
     )
 ---------------Page Children pages
  INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Pages'),   -- ApplicationMenuId - int
     N'icon-star', -- Icon - nvarchar(max)
     N'Login', -- Name - nvarchar(max)
     N'/pages/login',  -- Url - nvarchar(max)
	 1
     )
  INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Pages'),   -- ApplicationMenuId - int
     N'icon-star', -- Icon - nvarchar(max)
     N'Register', -- Name - nvarchar(max)
     N'/pages/register',  -- Url - nvarchar(max)
	 2
     )
  INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Pages'),   -- ApplicationMenuId - int
     N'icon-star', -- Icon - nvarchar(max)
     N'Error 404', -- Name - nvarchar(max)
     N'/pages/404',  -- Url - nvarchar(max)
	 3
     )
  INSERT INTO dbo.ApplicationMenu
 (
     ApplicationMenuId,
     Icon,
     Name,
     Url,
	 MenuPosition
 )
 VALUES
 (   (SELECT ID FROM dbo.ApplicationMenu WHERE Name = 'Pages'),   -- ApplicationMenuId - int
     N'icon-star', -- Icon - nvarchar(max)
     N'Error 500', -- Name - nvarchar(max)
     N'/pages/500',  -- Url - nvarchar(max)
	 4
     )
