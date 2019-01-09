USE [master]
GO
/****** Object:  Database [SerwisMuzyczny]    Script Date: 04.01.2019 13:00:41 ******/
CREATE DATABASE [SerwisMuzyczny]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SerwisMuzyczny', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SerwisMuzyczny.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SerwisMuzyczny_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SerwisMuzyczny_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SerwisMuzyczny] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SerwisMuzyczny].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SerwisMuzyczny] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET ARITHABORT OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SerwisMuzyczny] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SerwisMuzyczny] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SerwisMuzyczny] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SerwisMuzyczny] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET RECOVERY FULL 
GO
ALTER DATABASE [SerwisMuzyczny] SET  MULTI_USER 
GO
ALTER DATABASE [SerwisMuzyczny] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SerwisMuzyczny] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SerwisMuzyczny] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SerwisMuzyczny] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SerwisMuzyczny] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SerwisMuzyczny] SET QUERY_STORE = OFF
GO
USE [SerwisMuzyczny]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SerwisMuzyczny]
GO
/****** Object:  UserDefinedFunction [dbo].[najczesciej_odtwarzany_utwor_danego_gatunku]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[najczesciej_odtwarzany_utwor_danego_gatunku]( @gatunekId int ) returns varchar(60) as begin
declare @id int;
select @id = utworId from (select top 1 utworId, ((select count(odsluchId) from odsluch where utworId = tmp.utworId) + 0.0 )/
(datediff(dd, minDate, Convert(date, getdate())) +0.0) as sredniaNaDzien from (select min(o.dataOdtworzenia) as minDate, 
 u.utworId from album alb join przynaleznosc p on p.albumId = alb.albumId join utwor u on u.utworId = p.utworId join odsluch 
 o on o.utworId = u.utworId where alb.gatunekId = @gatunekId group by u.utworId) as tmp order by sredniaNaDzien desc) as
podzapytanie 
declare @n varchar(60);
select @n = nazwa from utwor where utworId = @id
return @n
end

GO
/****** Object:  UserDefinedFunction [dbo].[piosenka_zyskujaca_najbardziej_popularność_30_dni]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 -- Piosenka która zyskuje popularność (ostatnie 30 dni)
 create function [dbo].[piosenka_zyskujaca_najbardziej_popularność_30_dni]() returns varchar(60) as begin
declare @nazwa varchar(60);
select @nazwa = nazwa from (
 select top 1 count(o.odsluchId) / (datediff(dd, -30, Convert(date, getdate()))) as WskaznikPopularnosci, ut.nazwa from uzytkownik u join odsluch o on 
 o.uzytkownikId = u.uzytkownikId join utwor ut
 on ut.utworId = o.utworId group by o.utworId, ut.nazwa order by WskaznikPopularnosci desc) as poz
 return @nazwa
 end

 --select dbo.piosenka_zyskujaca_nabardziej_popularność_30_dni()

GO
/****** Object:  UserDefinedFunction [dbo].[wylicz_wartosc]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[wylicz_wartosc] (@id int ) returns float as begin
declare @to_return float;
select @to_return = p.cena*((100-pu.rabat)/100.0) from planUzytkownik pu join plany p on p.planId=pu.planId where pu.planUzytkownikid= @id
return(@to_return)
end
GO
/****** Object:  UserDefinedFunction [dbo].[wylicz_wartosc_rabatu]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[wylicz_wartosc_rabatu] (@id int ) returns float as begin
declare @to_return float;
select @to_return = p.cena-(p.cena*((100-pu.rabat)/100.0)) from planUzytkownik pu join plany p on p.planId=pu.planId where pu.planUzytkownikid= @id
return(@to_return)
end


GO
/****** Object:  Table [dbo].[planUzytkownik]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[planUzytkownik](
	[planUzytkownikid] [int] IDENTITY(1,1) NOT NULL,
	[uzytkownikId] [varchar](30) NOT NULL,
	[planId] [int] NOT NULL,
	[dataKupnaPakietu] [date] NOT NULL,
	[rabat] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[planUzytkownikid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[najlepsi_klienci]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[najlepsi_klienci] as 
select top(3) uzytkownikId as [Id uzytkownik], COUNT(planUzytkownikid) as [Kupil] from planUzytkownik 
group by uzytkownikId order by Kupil desc
GO
/****** Object:  Table [dbo].[album]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[album](
	[albumId] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [varchar](60) NOT NULL,
	[dataWydania] [date] NOT NULL,
	[gatunekId] [int] NOT NULL,
	[artystaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[albumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[artysta]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[artysta](
	[artystaId] [int] IDENTITY(1,1) NOT NULL,
	[pseudonim] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[artystaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gatunek]    Script Date: 04.01.2019 13:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gatunek](
	[gatunekId] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[gatunekId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[odsluch]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[odsluch](
	[odsluchId] [int] IDENTITY(1,1) NOT NULL,
	[uzytkownikId] [varchar](30) NOT NULL,
	[utworId] [int] NOT NULL,
	[dataOdtworzenia] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[odsluchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plany]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plany](
	[planId] [int] IDENTITY(1,1) NOT NULL,
	[iloscPiosenek] [int] NOT NULL,
	[cena] [money] NOT NULL,
	[nazwa] [varchar](30) NOT NULL,
 CONSTRAINT [PK_plany] PRIMARY KEY CLUSTERED 
(
	[planId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[przynaleznosc]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[przynaleznosc](
	[przynaleznoscId] [int] IDENTITY(1,1) NOT NULL,
	[albumId] [int] NOT NULL,
	[utworId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[przynaleznoscId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utwor]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utwor](
	[utworId] [int] IDENTITY(1,1) NOT NULL,
	[nazwa] [varchar](60) NOT NULL,
	[dlugosc] [varchar](8) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[utworId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[uzytkownik]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[uzytkownik](
	[uzytkownikId] [varchar](30) NOT NULL,
	[imie] [varchar](20) NOT NULL,
	[nazwisko] [varchar](20) NOT NULL,
	[email] [varchar](40) NULL,
	[kraj] [varchar](30) NOT NULL,
	[dataUrodzenia] [date] NOT NULL,
	[miejscowosc] [varchar](40) NOT NULL,
	[rodzajMiejscowosci] [varchar](6) NOT NULL,
	[plec] [varchar](9) NOT NULL,
	[PozostalaIlosc] [int] NOT NULL,
	[row_version] [varchar](23) NULL,
PRIMARY KEY CLUSTERED 
(
	[uzytkownikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wykonanie]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wykonanie](
	[wykonanieId] [int] IDENTITY(1,1) NOT NULL,
	[artystaId] [int] NOT NULL,
	[utworId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[wykonanieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[odsluch] ADD  DEFAULT (getdate()) FOR [dataOdtworzenia]
GO
ALTER TABLE [dbo].[planUzytkownik] ADD  DEFAULT (getdate()) FOR [dataKupnaPakietu]
GO
ALTER TABLE [dbo].[planUzytkownik] ADD  CONSTRAINT [DF_planUzytkownik_rabat]  DEFAULT ((0)) FOR [rabat]
GO
ALTER TABLE [dbo].[uzytkownik] ADD  DEFAULT ((0)) FOR [PozostalaIlosc]
GO
ALTER TABLE [dbo].[album]  WITH CHECK ADD FOREIGN KEY([artystaId])
REFERENCES [dbo].[artysta] ([artystaId])
GO
ALTER TABLE [dbo].[album]  WITH CHECK ADD FOREIGN KEY([gatunekId])
REFERENCES [dbo].[gatunek] ([gatunekId])
GO
ALTER TABLE [dbo].[odsluch]  WITH CHECK ADD FOREIGN KEY([utworId])
REFERENCES [dbo].[utwor] ([utworId])
GO
ALTER TABLE [dbo].[odsluch]  WITH CHECK ADD FOREIGN KEY([uzytkownikId])
REFERENCES [dbo].[uzytkownik] ([uzytkownikId])
GO
ALTER TABLE [dbo].[planUzytkownik]  WITH CHECK ADD FOREIGN KEY([uzytkownikId])
REFERENCES [dbo].[uzytkownik] ([uzytkownikId])
GO
ALTER TABLE [dbo].[planUzytkownik]  WITH CHECK ADD  CONSTRAINT [fk_plan_uzytkownik] FOREIGN KEY([planId])
REFERENCES [dbo].[plany] ([planId])
GO
ALTER TABLE [dbo].[planUzytkownik] CHECK CONSTRAINT [fk_plan_uzytkownik]
GO
ALTER TABLE [dbo].[przynaleznosc]  WITH CHECK ADD FOREIGN KEY([albumId])
REFERENCES [dbo].[album] ([albumId])
GO
ALTER TABLE [dbo].[przynaleznosc]  WITH CHECK ADD FOREIGN KEY([utworId])
REFERENCES [dbo].[utwor] ([utworId])
GO
ALTER TABLE [dbo].[wykonanie]  WITH CHECK ADD FOREIGN KEY([artystaId])
REFERENCES [dbo].[artysta] ([artystaId])
GO
ALTER TABLE [dbo].[wykonanie]  WITH CHECK ADD FOREIGN KEY([utworId])
REFERENCES [dbo].[utwor] ([utworId])
GO
ALTER TABLE [dbo].[uzytkownik]  WITH CHECK ADD CHECK  (([rodzajMiejscowosci]='miasto' OR [rodzajMiejscowosci]='wies'))
GO
ALTER TABLE [dbo].[uzytkownik]  WITH CHECK ADD CHECK  (([plec]='mezczyzna' OR [plec]='kobieta'))
GO
/****** Object:  StoredProcedure [dbo].[dodaj_album]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_album] @nazwa varchar(60), @data date, @nazwa_gatunku varchar(30), @pseudonim varchar(40)
as 
declare @gatunekid int, @artystaid int
select @gatunekid = gatunekId from gatunek where nazwa = @nazwa_gatunku
select @artystaid = artystaId from artysta where @pseudonim = pseudonim
insert into album(nazwa, dataWydania, gatunekId, artystaId) values(
@nazwa, @data, @gatunekid, @artystaid)

GO
/****** Object:  StoredProcedure [dbo].[dodaj_artyste]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_artyste] @pseudonim varchar(40)
as 
insert into artysta(pseudonim) values (@pseudonim)

GO
/****** Object:  StoredProcedure [dbo].[dodaj_gatunek]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_gatunek] @nazwa varchar(30)
as insert into gatunek(nazwa) values (@nazwa)

GO
/****** Object:  StoredProcedure [dbo].[dodaj_odsluch]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_odsluch] @uztkownikId varchar(30), @utworId int
as 
insert into odsluch(uzytkownikId, utworId, dataOdtworzenia) values(@uztkownikId, @utworId,Convert(date, getdate()))

GO

/****** Object:  StoredProcedure [dbo].[dodaj_przynaleznosc]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_przynaleznosc] @albumId int, @utworId int
as 
insert into przynaleznosc(albumId, utworId) values(@albumId, @utworId)

GO
/****** Object:  StoredProcedure [dbo].[dodaj_utwor]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_utwor] @nazwa varchar(60), @dlugosc varchar(8)
as 
insert into utwor(nazwa, dlugosc) values(@nazwa, @dlugosc);

GO
/****** Object:  StoredProcedure [dbo].[dodaj_uzytkownika]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_uzytkownika] @nick varchar(30), @imie varchar(20), @email varchar(30),
@nazwisko varchar(20), @kraj varchar(20), @dataUrodzenia date, @miejscowosc varchar(30),
@rodzajMiejscowosci varchar(10), @plec varchar(10)
as 
insert into uzytkownik(uzytkownikID, imie, nazwisko, email, kraj, dataUrodzenia, miejscowosc, rodzajMiejscowosci,
plec, PozostalaIlosc) values 
(@nick, @imie, @email, @nazwisko, @kraj, @dataUrodzenia, @miejscowosc, @rodzajMiejscowosci, @plec, 0)

GO
/****** Object:  StoredProcedure [dbo].[dodaj_wykonanie]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodaj_wykonanie] @artystaId int, @utworId int
as 
insert into wykonanie(artystaId,utworId) values(@artystaId,@utworId)

GO
/****** Object:  StoredProcedure [dbo].[dodajPlan]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodajPlan] @iloscPiosenek int, @cena money, @nazwa varchar(20)
as 
insert into plany(iloscPiosenek, cena, nazwa) values
(@iloscPiosenek, @cena, @nazwa)

GO
/****** Object:  StoredProcedure [dbo].[dodajPlanUzytkownik]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[dodajPlanUzytkownik] @idUzytkowni varchar(30), @idPlan int
as insert into planUzytkownik(uzytkownikId, planId, dataKupnaPakietu) values (@idUzytkowni, @idPlan,Convert(date, getdate()))

GO
/****** Object:  StoredProcedure [dbo].[edycja_uzytkownik]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[edycja_uzytkownik] @uzytkownikId varchar(30), @imie varchar(20), @nazwisko varchar(20), @email varchar(40), @kraj varchar(30), 
@dataUrodzenia date, @miejscowosc varchar(40), @rodzajMiejscowosci varchar(6), @plec varchar(9), @PozostalaIlosc int, @dataEdycji varchar(23) as 
declare @row_date varchar(23);
select @row_date = row_version from uzytkownik where uzytkownikId = @uzytkownikId
if (select uzytkownikId from uzytkownik where uzytkownikId = @uzytkownikId) is null
		begin
			RAISERROR ('*** Ten wpis został usunięty przez innego uzytkownika ***',16,1) 
		end

else if @row_date > @dataEdycji
		begin
			RAISERROR ('*** Ten wpis został zmodyfikowany przez innego uzytkownika ***',16,1) 
		end
else 
update uzytkownik set imie = @imie,
nazwisko = @nazwisko,
email = @email,
kraj = @kraj,
dataUrodzenia = @dataUrodzenia, 
miejscowosc = @miejscowosc,
rodzajMiejscowosci = @rodzajMiejscowosci,
plec = @plec, 
PozostalaIlosc = @PozostalaIlosc,
row_version = convert(nvarchar(23), GETDATE(), 25)
where uzytkownikId = @uzytkownikId
GO
/****** Object:  StoredProcedure [dbo].[liczba_albumow_artysty]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_albumow_artysty] @pseudonim varchar(40) as 
declare @to_return int; 
select @to_return = count(distinct alb.albumId) from artysta a join album alb on 
a.artystaId = alb.artystaId where a.pseudonim = @pseudonim
return(@to_return)

GO
/****** Object:  StoredProcedure [dbo].[liczba_dni_odtwarzania_utworu]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_dni_odtwarzania_utworu] @utworId int as 
declare @begin date;
select top 1 @begin = dataOdtworzenia from odsluch where utworId = @utworId order by dataOdtworzenia 
return datediff(dd, @begin, Convert(date, getdate()))

GO
/****** Object:  StoredProcedure [dbo].[liczba_odsluchan]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[liczba_odsluchan] @id varchar(60), @days int as 
declare @odsluchan int
SELECT @odsluchan =  COUNT(*) FROM odsluch WHERE uzytkownikId=@id AND DATEDIFF(day, dataOdtworzenia, GETDATE())<=@days
return(@odsluchan)
GO
/****** Object:  StoredProcedure [dbo].[liczba_utworow_album]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_utworow_album] @nazwa varchar(60), @pseudonim varchar(40) as
declare @to_return int, @albumId int;
select @albumId = a.albumId from album a join artysta ar on ar.artystaId=a.artystaId where ar.pseudonim = @pseudonim
select @to_return = count(utworId) from przynaleznosc where albumId = @albumId
return(@to_return)

GO
/****** Object:  StoredProcedure [dbo].[liczba_utworow_artysty]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_utworow_artysty] @pseudonim varchar(40) as 
declare @to_return int, @artystaId int;
select @artystaId = artystaId from artysta where pseudonim = @pseudonim
select @to_return = count(utworId) from wykonanie where artystaId = @artystaId
return @to_return

GO
/****** Object:  StoredProcedure [dbo].[liczba_utworow_danego_gatunku]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_utworow_danego_gatunku] @nazwa varchar(40) as 
declare @gatunekId int, @to_return int;
select @gatunekId = gatunekId from gatunek where nazwa = @nazwa
select @to_return = count(distinct u.utworId) from gatunek g join album a on a.gatunekId = g.gatunekId join 
przynaleznosc p on a.albumId = p.albumId join utwor u on u.utworId = p.utworId where
a.gatunekId = @gatunekId
return(@to_return)

GO
/****** Object:  StoredProcedure [dbo].[liczba_utworow_odtowrzonych_przez_uzytkownika]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_utworow_odtowrzonych_przez_uzytkownika] @imie varchar(30) 
as 
declare @to_return int;
select @to_return = count(DISTINCT  o.utworId) from uzytkownik u join odsluch o on o.uzytkownikId=@imie
return(@to_return)

GO
/****** Object:  StoredProcedure [dbo].[liczba_uzytkownikow_ktorzy_odtworzyli_utwor]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[liczba_uzytkownikow_ktorzy_odtworzyli_utwor] @nazwa varchar(60),
@pseudonim varchar(40) as 
declare @to_return int;
select @to_return = count(distinct o.uzytkownikId) from odsluch o join utwor u on u.utworId=o.utworId join wykonanie w on 
w.utworId=u.utworId join artysta a on a.artystaId = w.artystaId where u.nazwa=@nazwa and 
a.pseudonim = @pseudonim
return(@to_return)

GO
/****** Object:  StoredProcedure [dbo].[najlepszePlany]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[najlepszePlany] as
select 
pu.planUzytkownikid as planUzytkownikid,
pu.uzytkownikId as uzytkownikId,
pu.planId as planId,
pu.dataKupnaPakietu as dataKupnaPakietu,
pu.rabat as rabat,
p.planId as planId1,
p.iloscPiosenek as iloscPiosenek,
p.cena as cena,
p.nazwa as nazwa
 from planUzytkownik pu join plany p on p.planId=pu.planId
GO
/****** Object:  StoredProcedure [dbo].[najpopularniejsze_w_kraju]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  -- Najpopularniejsze w utwory w kraju
 create procedure [dbo].[najpopularniejsze_w_kraju] as
 select count(o.odsluchId) / (datediff(dd, min(o.dataOdtworzenia), Convert(date, getdate()))) as WskaznikPopularnosci, ut.nazwa, u.kraj from uzytkownik u join odsluch o on 
 o.uzytkownikId = u.uzytkownikId join utwor ut
 on ut.utworId = o.utworId group by u.kraj, ut.nazwa order by WskaznikPopularnosci desc
 
GO
/****** Object:  StoredProcedure [dbo].[napopularniejsi_artysci]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--select dbo.najczesciej_odtwarzany_utwor_danego_datunku(5)

-- Najpopularniejsi artyści tygodnia/miesiąca/wszechczasów
-- Sterowanie za pomocą parametru okreslajacego liczbę dni 
create procedure [dbo].[napopularniejsi_artysci] @liczbaDni int as 
 select count(distinct o.odsluchId) as liczbaOdtworzen, ar.pseudonim from wykonanie w join utwor u on 
 u.utworId = w.utworId join odsluch o on o.utworId = u.utworId join artysta ar on ar.artystaId = w.artystaId where o.dataOdtworzenia > dateadd(day,-@liczbaDni, cast(getdate() as date))
 group by ar.pseudonim order by liczbaOdtworzen desc

 
GO
/****** Object:  StoredProcedure [dbo].[planyIUzytkownik]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[planyIUzytkownik] @idUzytkownika varchar(30) AS
SELECT 
	pu.dataKupnaPakietu AS DataZakupu,
	pu.planId AS IDPlanu,
	p.nazwa AS NazwaPlanu,
	p.cena AS Cena,
	pu.planUzytkownikid AS IDZakupu, 
	pu.rabat AS Rabat,
	pu.uzytkownikId AS Nickname,
	u.dataUrodzenia AS DataUrodzenia,
	u.email AS Email,
	u.imie AS Imie,
	u.nazwisko AS Nazwisko, 
	u.kraj AS Kraj,
	u.miejscowosc AS Miejscowosc,
	u.plec AS Plec,
	u.rodzajMiejscowosci AS Typ,
	u.PozostalaIlosc AS PozostaloDoOdsluchania,
	dbo.wylicz_wartosc(pu.planUzytkownikid) AS Wartosc,
	dbo.wylicz_wartosc_rabatu(pu.planUzytkownikid) AS WartoscRabatu
FROM planUzytkownik pu 
JOIN uzytkownik u ON u.uzytkownikId=pu.uzytkownikId 
JOIN plany p ON p.planId=pu.planId
WHERE pu.uzytkownikId=@idUzytkownika

GO
/****** Object:  StoredProcedure [dbo].[pobierzObrotyZaOkres]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pobierzObrotyZaOkres] @beginDate date, @endDate date as 
if @beginDate > @endDate
		begin
			RAISERROR ('*** Data początkowa musi być mniejsza od daty końcowej. ***',16,1) 
		end
select up.planId, p.nazwa, sum(p.cena - (p.cena * (up.rabat)/100)) as Suma, 
count(distinct up.planUzytkownikid) as [Sprzedanych sztuk] 
from planUzytkownik up  join plany p on p.planId = up.planId
where up.dataKupnaPakietu >= @beginDate and up.dataKupnaPakietu <= @endDate group by up.planId, p.nazwa
GO
/****** Object:  StoredProcedure [dbo].[srednia_odtworzen]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Wypisz utwory które były odsłuchiwane więcej raz niż średnia liczba odtworzeń.
create procedure [dbo].[srednia_odtworzen] as 
declare @liczba_utworow float;
declare @liczba_odsluchan float;
select @liczba_utworow = count(distinct utworId) from utwor 
select @liczba_odsluchan = count(distinct odsluchId) from odsluch
return (round(@liczba_utworow/@liczba_odsluchan, 0))

GO
/****** Object:  StoredProcedure [dbo].[statystykaSprzedazy]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[statystykaSprzedazy] @beginDate date, @endDate date as
if @beginDate > @endDate
		begin
			RAISERROR ('*** Data początkowa musi być mniejsza od daty końcowej. ***',16,1) 
		end
select sum(p.cena - cast((p.cena * (CAST(pu.rabat as money)/100)) as money)) as [Wartosc sprzedazy],
count(distinct pu.uzytkownikId) as [Klientow],
round((sum(p.cena - (p.cena * (CAST(pu.rabat as money)/100) )) / count(distinct pu.uzytkownikId)), 2) as [Sredni wydatek klienta],
max((p.cena - (p.cena * (CAST(pu.rabat as money)/100) ))) as [Maksymalny wydatek],
min((p.cena - (p.cena * (CAST(pu.rabat as money)/100) ))) as [Minimalny wydatek],
count(*) as [Wszystkie sprzedaze]
from planUzytkownik pu join plany p on p.planId = pu.planId where  
pu.dataKupnaPakietu >= @beginDate and pu.dataKupnaPakietu <= @endDate
GO
/****** Object:  StoredProcedure [dbo].[usun_album]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usun_album] @nazwa int
AS
declare @tmp TABLE(
	utworID int
)
insert into @tmp(utworID)
	select utworID from przynaleznosc
	where albumId = @nazwa;

delete from wykonanie where utworID in (select utworID from @tmp)
delete from przynaleznosc where albumId = @nazwa
delete from odsluch where utworID in (select utworID from @tmp)
delete from utwor where utworID in (select utworID from @tmp)
delete from album where albumId = @nazwa
GO
/****** Object:  StoredProcedure [dbo].[usun_artyste]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usun_artyste] @nazwa int
AS
declare @tmp TABLE(
	utworID int
)
insert into @tmp(utworID)
	select utworID from wykonanie
	where artystaID = @nazwa;

delete from wykonanie where artystaID = @nazwa
delete from przynaleznosc where albumId in (select albumId from album where artystaId = @nazwa)
delete from odsluch where utworID in (select utworID from @tmp)
delete from wykonanie where artystaId = @nazwa
delete from utwor where utworID in (select utworID from @tmp)
delete from album where artystaID = @nazwa
delete from artysta where artystaID = @nazwa

GO
/****** Object:  StoredProcedure [dbo].[usun_utwor]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usun_utwor] @nazwa int
AS
delete from wykonanie where utworID = @nazwa
delete from przynaleznosc where utworID = @nazwa
delete from odsluch where utworID = @nazwa
delete from utwor where utworID = @nazwa
GO
/****** Object:  StoredProcedure [dbo].[usun_uzytkownika]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usun_uzytkownika] @nazwa varchar(30)
AS
delete from planUzytkownik where uzytkownikId = @nazwa
delete from odsluch where uzytkownikId = @nazwa
delete from uzytkownik where uzytkownikId = @nazwa

GO
/****** Object:  StoredProcedure [dbo].[utwory_z_albumu]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[utwory_z_albumu] @idAlbumu int as 
 select U.nazwa as Nazwa, U.dlugosc as Długość  from utwor U join przynaleznosc P on 
 P.utworId = U.utworID join album A on A.albumId= P.albumId
 WHERE A.albumId=@idAlbumu
 order by U.utworId asc
GO
/****** Object:  StoredProcedure [dbo].[wiecej_niz_srednia]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[wiecej_niz_srednia] as 
declare @srednia int;
exec @srednia = srednia_odtworzen
select * from utwor u where ((select count(distinct odsluchId) from odsluch where utworId = u.utworId) > @srednia)

-- Najczęściej odtwarzane utwory danego gatunku trzeba naprawic
/****** Object:  StoredProcedure [dbo].[dodaj_plan_uzytkownik_transakcja]    Script Date: 04.01.2019 13:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[dodaj_plan_uzytkownik_transakcja] @uzytkownikId varchar(30), @planId int as 
begin 
	set transaction isolation level SERIALIZABLE
	begin try
		begin transaction 
		declare @ileUtworowKupiono int;

		DECLARE @wartosc tinyint;
		DECLARE @odsluchan int;

		EXEC @odsluchan = liczba_odsluchan @uzytkownikId, 30	 

		if (@odsluchan>1000) SET @wartosc = 25
		else if @odsluchan>500 SET @wartosc = 15
		else if @odsluchan>100 SET @wartosc = 5
		else SET @wartosc = 0

		insert into planUzytkownik(uzytkownikId, planId, dataKupnaPakietu, rabat) values (@uzytkownikId, @planId, Convert(date, getdate()), @wartosc)
		select @ileUtworowKupiono = iloscPiosenek from plany where planId = @planId
		update uzytkownik set PozostalaIlosc += @ileUtworowKupiono where uzytkownikId = @uzytkownikId

		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback tran
			RAISERROR ('*** Błąd transakcji! ***',16,1)
	end catch
end 
GO
GO
USE [master]
GO
ALTER DATABASE [SerwisMuzyczny] SET  READ_WRITE 
GO
