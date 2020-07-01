--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
drop table tblCharges
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployee')


create table tblEmployee
(
UserID int primary key IDENTITY(1,1),
FirstName varchar(100) not null,
LastName varchar(100) not null,
JMBG char(13) unique not null,
DateOfBirth date not null,
Gender char(1) check(Gender in ('M', 'F', 'X')) not null,
RegistrationNumber char(9) unique not null,
Location varchar(100) not null
)

create table tblSector
(
UserID int primary key IDENTITY(1,1),
FirstName varchar(100) not null,
LastName varchar(100) not null,
JMBG char(13) unique not null,
DateOfBirth date not null,
Gender char(1) check(Gender in ('M', 'F', 'X')) not null,
RegistrationNumber char(9) unique not null,
Location varchar(100) not null
)


--Constraints regarding user are realised here, JMBG is validated by date of birth.
alter table tblUser add constraint DateOfBirth_c check (FLOOR(DATEDIFF(DAY, DateOfBirth, GETDATE()) / 365.25) >= 16 and DateOfBirth > '1900-1-1')
alter table tblUser add constraint JMBG_c check (LEN(JMBG) = 13 and ISNUMERIC(JMBG) = 1 and 
SUBSTRING(CONVERT(varchar, DateOfBirth, 112), 7,2)+SUBSTRING(CONVERT(varchar, DateOfBirth, 112), 5,2) + SUBSTRING(CONVERT(varchar, DateOfBirth, 112), 2, 3) = SUBSTRING(JMBG, 1, 7));

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
--Optional inserts.
insert into tblLocation (Adress, Town, Country) values ('Adress 1', 'Town 1', 'Srbija')
insert into tblIdentityCard (RegistrationNumber, DateOfIssue, ExpirationDate, TheIssuer) values ('111111111', '1-1-1992', '1-1-2002', 'Issuer 1');
insert into tblEmployee (FirstName, LastName, JMBG, DateOfBirth, Gender, IdentityCardID, LocationID) values ('NameA','SurnameA', '0101990111111', '1-1-1990', 'M', 1, 1);