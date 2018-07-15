use master 
create database TestSystem
go


use TestSystem
go

create table Theme
(
IdTheme int identity(1,1) primary key not null,
ThemeName nvarchar(50) not null unique,
Description text
)

create table Property
(
IdProperty int identity(1,1) primary key not null,
Difficult int not null unique,
IdTheme int not null foreign key references Theme(IdTheme)
)

create table Test
(
IdTest int identity(1,1) primary key not null,
TestName nvarchar(50) not null unique, 
QuestionsNumber int not null,
TestDescription text,
IdProperty int foreign key references Property(IdProperty)
)

create table Question
(
IdQuestion int identity(1,1) primary key not null,
QuestionText text not null,
QuestionImage image,
AnswerNumber int not null, 
Score int not null, 
IdProperty int foreign key references Property(IdProperty)
)

create table Answer
(
IdAnswer  int identity(1,1) primary key not null,
AnswerText text not null,
Correct bit not null
)


create table UserInfo
(
IdUserInfo  int identity(1,1) primary key not null,
UserLogin nvarchar(20)  unique not null,
UserFirstName nvarchar(20) not null,
UserLastName nvarchar(50) not null,
UserEmail text not null,
)

create table UserSystem
(
IdUser int identity(1,1) primary key not null,
IdUserInfo int not null foreign key references UserInfo(IdUserInfo),
UserPassword nvarchar(50),
UserRights bit
)

create table Result
(
IdResult int identity(1,1) primary key not null,
UserLogin nvarchar(20) not null foreign key references UserInfo(UserLogin),
IdTest int not null foreign key references Test(IdTest),
ResultScore float not null,
ResultDescription  text
)

create table TestQuestion
(
IdTest int not null foreign key references Test(IdTest),
IdQuestion int not null foreign key references Question(IdQuestion)
)

create table QuestionAnswer
(
IdQuestion int not null foreign key references Question(IdQuestion),
IdAnswer int not null foreign key references Answer(IdAnswer)
)