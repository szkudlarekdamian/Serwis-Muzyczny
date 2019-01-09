USE [master]
USE [SerwisMuzyczny]
GO

INSERT INTO uzytkownik
values('rybak21','Piotr','Kontowicz','splawik@o2.pl','Polska','02.01.1937','Wachock','wies','mezczyzna',0,1),
('leniuszek','Mikolaj','Walkowiak','whatever@gmail.com','Polska','02.01.1997','Poznan','miasto','mezczyzna',8, 1),
('hejter44','Dominika','Damianowska','fake@fake.com','Polska','12.14.2015','Warszawa','wies','kobieta',97, 1),
('milygosc','Damian','Szkudlarek','szkudi@gmail.com','Polska','12.14.1997','Kostrzyn','miasto','mezczyzna',1994, 1)

INSERT INTO plany
values (0,0,'darmowy'),
(10,1,'Testowy'),
(100,5,'Basic'),
(2000,20,'Advanced'),
(20000,100,'Meloman')


INSERT INTO planUzytkownik values('rybak21',1,'10.27.2018', 0)
INSERT INTO planUzytkownik values('leniuszek',2,'11.2.2018', 0)
INSERT INTO planUzytkownik values('hejter44',3,'1.22.2018', 0)
INSERT INTO planUzytkownik values('milygosc',4,'5.17.2018', 0)


insert into artysta values ('Kendrick Lamar')
insert into artysta values ('System Of A Down')
insert into artysta values ('Death')
insert into artysta values ('The Prodigy')
insert into artysta values ('Rihanna')



insert into gatunek values ('Rap'),('Rock'),('Melodic Death Metal'),('Elektroniczna'),('Pop'),('Thrash'),('Klasyczna')


SET IDENTITY_INSERT album ON
insert into album(albumId, nazwa, dataWydania, gatunekId, artystaId) values (1,'DAMN.','04.14.2017',1,1)
insert into album(albumId, nazwa, dataWydania, gatunekId, artystaId) values (2,'Mezmerize','05.16.2005',2,2)
insert into album(albumId, nazwa, dataWydania, gatunekId, artystaId) values (3,'Hypnotize','11.22.2005',2,2)
insert into album(albumId, nazwa, dataWydania, gatunekId, artystaId) values (4,'Symbolic','03.21.1995',3,3)
insert into album(albumId, nazwa, dataWydania, gatunekId, artystaId) values (5,'Spiritual Healing','02.16.1990',3,3)
insert into album(albumId, nazwa, dataWydania, gatunekId, artystaId) values (6,'Invaders Must Die','02.20.2009',4,4)


insert into utwor values ('Blood.','1:58'),('DNA.','3:05'),('Yah.','2:40'),('Element.','3:28'),('Feel.','3:34'),('Loyalty.','3:47'),
('Pride.','4:35'),('Humble.','2:57'),('Lust.','5:07'),('Love.','3:33'),('XXX.','4:14'),('Fear.','7:40'),('God.','4:08'),('Duckworth.','4:08'),
('Soldier Side - Intro','1:03'),('B.Y.O.B.','4:15'),('Revenga','3:50'),('Cigaro','2:11'),('Radio/Video','4:13'),
('This Cocaine Makes Me Feel Like Im On This Song','2:08'),('Violent Pornography','3:31'),
('Question!','3:20'),('Sad Statue','3:27'),('Old School Hollywood','2:58'),('Lost In Hollywood','5:20'),
('Attack','3:06'),('Dreaming','3:59'),('Kill Rock N Roll','2:27'),('Hypnotize','3:09'),('Stealing Society','2:58'),
('Tentative','3:37'),('U-Fig','2:55'),('„Holy Mountains','5:28'),('Vicinity of Obscenity','2:51'),
('Shes Like Heroin','2:44'),('Lonely Day','2:47'),('Soldier Side','3:40'),('Symbolic','6:33'),('Zero Tolerance','4:48'),
('Empty Words','6:22'),('Sacred Serenity','4:27'),('1,000 Eyes','4:28'),('Without Judgement','5:28'),('Crystal Mountain','5:07'),('Misanthrope','5:03'),
('Perennial Quest','8:21'),('Living Monstrosity','5:08'),('Altering the Future','5:34'),('Defensive Personalities','4:45'),
('Within the Mind','5:34'),('Spiritual Healing','7:44'),('Low Life','5:23'),('Genetic Reconstruction','4:52'),('Killing Spree','4:16'),
('Invaders Must Die','4:55'),('Omen','3:36'),('Thunder','4:08'),('Colours','3:27'),('Take Me to the Hospital','3:39'),('Warriors Dance','5:12'),
('Run with the Wolves','4:24'),('Omen Reprise','2:14'),('Worlds on Fire','4:50'),('Piranha','4:05'),('Stand Up','5:30')

insert into przynaleznosc values (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),
(2,15),(2,16),(2,17),(2,18),(2,19),(2,20),(2,21),(2,22),(2,23),(2,24),(2,25),
(3,26),(3,27),(3,28),(3,29),(3,30),(3,31),(3,32),(3,33),(3,34),(3,35),(3,36),(3,37),
(4,38),(4,39),(4,40),(4,41),(4,42),(4,43),(4,44),(4,45),(4,46),
(5,47),(5,48),(5,49),(5,50),(5,51),(5,52),(5,53),(5,54),
(6,55),(6,56),(6,57),(6,58),(6,59),(6,60),(6,61),(6,62),(6,63),(6,64),(6,65)


insert into wykonanie values (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(5,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),
(2,15),(2,16),(2,17),(2,18),(2,19),(2,20),(2,21),(2,22),(2,23),(2,24),(2,25),
(2,26),(2,27),(2,28),(2,29),(2,30),(2,31),(2,32),(2,33),(2,34),(2,35),(2,36),(2,37),
(3,38),(3,39),(3,40),(3,41),(3,42),(3,43),(3,44),(3,45),(3,46),
(3,47),(3,48),(3,49),(3,50),(3,51),(3,52),(3,53),(3,54),
(4,55),(4,56),(4,57),(4,58),(4,59),(4,60),(4,61),(4,62),(4,63),(4,64),(4,65)

insert into odsluch values ('hejter44',55,'10.27.2018'),('hejter44',56,'10.27.2018'),('hejter44',57,'10.27.2018'),
('leniuszek',55,'10.27.2018'),('leniuszek',26,'10.27.2018'),
('milygosc',1,'10.27.2018'),('milygosc',2,'10.27.2018'),('milygosc',3,'10.28.2018'),('milygosc',4,'10.28.2018'),('milygosc',5,'10.28.2018'),
('milygosc',6,'10.28.2018')

insert into odsluch values ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018')
, ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018')
, ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018')
, ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018')
, ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018'), ('hejter44',2,'10.27.2018')