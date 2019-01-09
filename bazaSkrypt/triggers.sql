USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[blokowanie_dodania_2_albumów]    Script Date: 04.01.2019 15:47:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE trigger [dbo].[blokowanie_dodania_2_albumów] on [dbo].[album]
after insert as 
if((select count(*) from album where nazwa = (select nazwa from inserted where
artystaId = (select artystaId from inserted))) > 1)
begin
rollback
RAISERROR ('*** Ten artysta ma już album o takiej nazwie! ***',16,1) 
end

--alter trigger blokowanie_wprowadzania_dwoch_uzytkownikow on uzytkownik after insert as
--if ((select count(*) from uzytkownik where imie = (select imie from inserted) and nazwisko = (select 
--nazwisko from inserted))>1)
--begin 
--rollback
--end
GO

ALTER TABLE [dbo].[album] ENABLE TRIGGER [blokowanie_dodania_2_albumów]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[powiadomienieMailowe]    Script Date: 04.01.2019 15:48:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[powiadomienieMailowe]
ON [dbo].[planUzytkownik]
FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @mail varchar(50), @imie varchar(30), @nazwisko varchar(30), @id int, @kwota float;
	SELECT @mail = email, @imie = imie, @nazwisko = nazwisko, @id=planUzytkownikid FROM inserted JOIN uzytkownik ON inserted.uzytkownikId=uzytkownik.uzytkownikId
	SET @kwota = dbo.wylicz_wartosc(@id)
    DECLARE @body NVARCHAR(MAX) = N'';
	DECLARE @NewLineChar AS CHAR(2) = CHAR(13) + CHAR(10)
    SELECT @body += 'Witaj '+@imie+' '+@nazwisko+@NewLineChar+'Twoje zamówienie nr '+CONVERT(varchar(10), @id)+' opiewa na kwotę '+CONVERT(varchar(20), @kwota);

    IF @mail is not NULL
    BEGIN
        EXEC msdb.dbo.sp_send_dbmail
          @recipients = @mail, 
          @profile_name = 'TubaService',
          @subject = 'Potwierdzenie zamówienia', 
          @body = @body
    END	
END
GO

ALTER TABLE [dbo].[planUzytkownik] DISABLE TRIGGER [powiadomienieMailowe]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[pustyEmailPlany]    Script Date: 04.01.2019 15:49:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create trigger [dbo].[pustyEmailPlany] on [dbo].[planUzytkownik]
after insert as 
DECLARE @email varchar(60);
SELECT @email = email FROM uzytkownik u JOIN inserted i ON i.uzytkownikId=u.uzytkownikId

if (@email is NULL)
BEGIN
	ROLLBACK
	RAISERROR('Nie podano adresu email, nie można sfinalizować zakupu.',16,1);
END
GO

ALTER TABLE [dbo].[planUzytkownik] ENABLE TRIGGER [pustyEmailPlany]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[zabezpieczenieRabat]    Script Date: 04.01.2019 15:49:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [dbo].[zabezpieczenieRabat] on [dbo].[planUzytkownik]
after insert, update as 
DECLARE @wartosc tinyint
SELECT @wartosc=rabat FROM inserted
if (@wartosc<0 OR @wartosc>100)
begin
rollback
RAISERROR ('*** Rabat musi mieścić się w zakresie <0,100>! ***',16,1) 
end
GO

ALTER TABLE [dbo].[planUzytkownik] ENABLE TRIGGER [zabezpieczenieRabat]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[zaktualizuj_rowversion]    Script Date: 04.01.2019 15:49:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[zaktualizuj_rowversion] ON [dbo].[uzytkownik]
AFTER INSERT, UPDATE AS
DECLARE @id varchar(30);
SELECT @id = uzytkownikId FROM inserted;
UPDATE uzytkownik SET row_version = convert(nvarchar(23), GETDATE(), 25) WHERE uzytkownikId=@id;

GO

ALTER TABLE [dbo].[uzytkownik] ENABLE TRIGGER [zaktualizuj_rowversion]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[blokowanie_przyszla_data_album]    Script Date: 04.01.2019 15:47:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [dbo].[blokowanie_przyszla_data_album] on [dbo].[album]
after insert as 
if (select dataWydania from inserted)>Convert(date, getdate())
begin 
rollback
RAISERROR ('*** Podano przyszłą datę! ***',16,1) 
end

GO

ALTER TABLE [dbo].[album] ENABLE TRIGGER [blokowanie_przyszla_data_album]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[blokowanie_dodania_artystów]    Script Date: 04.01.2019 15:48:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE trigger [dbo].[blokowanie_dodania_artystów] on [dbo].[artysta]
after insert as 
if ((select count(*) from artysta where pseudonim = (select pseudonim from inserted)) > 1)begin 
rollback 
RAISERROR ('*** Artysta o takim pseudonimie już istnieje! ***',16,1) 
end

GO

ALTER TABLE [dbo].[artysta] ENABLE TRIGGER [blokowanie_dodania_artystów]
GO

USE [SerwisMuzyczny]
GO

/****** Object:  Trigger [dbo].[darmowyDlaMlodychPlany]    Script Date: 04.01.2019 15:48:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [dbo].[darmowyDlaMlodychPlany] on [dbo].[planUzytkownik]
after insert as 
DECLARE @id varchar(60);
DECLARE @wiek int, @idZakupu int, @ilePlanowBasic int, @jakiPlan int;

SELECT @id = uzytkownikId, @idZakupu = planUzytkownikid, @jakiPlan=planId FROM inserted
SELECT @wiek = DATEDIFF(year, (SELECT dataUrodzenia FROM uzytkownik u JOIN inserted i ON u.uzytkownikId=i.uzytkownikId), GETDATE())
SELECT @ilePlanowBasic = COUNT(*) FROM planUzytkownik p JOIN inserted i ON i.uzytkownikId=p.uzytkownikId WHERE p.planId=3
if (@wiek<18 AND @jakiPlan=3 AND @ilePlanowBasic=1)
BEGIN
UPDATE planUzytkownik SET rabat=100 WHERE uzytkownikId=@id AND planUzytkownikid=@idZakupu
END
GO

ALTER TABLE [dbo].[planUzytkownik] ENABLE TRIGGER [darmowyDlaMlodychPlany]
GO

