USE [master]
GO
ALTER DATABASE [tp-cuatrimestral-grupo-7] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [tp-cuatrimestral-grupo-7]    Script Date: 28/5/2024 18:44:00 ******/
DROP DATABASE [tp-cuatrimestral-grupo-7]
GO
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'tp-cuatrimestral-grupo-7'
GO
