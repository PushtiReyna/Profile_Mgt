
CREATE DATABASE UserProfileDB

CREATE TABLE UserMst(
	Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Firstname Varchar(100) Not null,
	Middlename Varchar(100) null,
	Lastname Varchar(100) Not null,
	Email Nvarchar(100) Not null,
	DOB datetime NOT NULL,
	Address Nvarchar(max) NOT NULL,
	Pincode int Not null,
	Username Nvarchar(50) Not null,
	Password Nvarchar(50) Not null,
	ProfileImage Nvarchar(max) Not Null,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
)



CREATE TABLE CategoryMst(
	CategoryId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CategoryName [Varchar](100) NOT NULL,
	CategoryImage nvarchar(max) not null,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
)

	CREATE TABLE SubcategoryMst(
	SubcategoryId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	SubcategoryName [Varchar](100) NOT NULL,
	CategoryId Int Not Null,
	SubcategoryImage nvarchar(max) not null,
	IsActive bit not null,
	IsDelete bit not null,
	CreatedBy int not null,
	CreatedOn datetime null,
	UpdateBy int not null,
	UpdatedOn datetime null
	)