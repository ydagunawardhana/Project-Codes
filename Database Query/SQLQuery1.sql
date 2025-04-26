CREATE DATABASE Pharmacy ManagementSystemDB;


CREATE TABLE Users (
Id INT IDENTITY (1, 1) NOT NULL,
UserRole VARCHAR (20) NOT NULL,
Name VARCHAR (20) NOT NULL,
DateofBirth DATETIME NOT NULL,
Mobile BIGINT NOT NULL,
Email VARCHAR (50) NOT NULL,
Username VARCHAR (20) NOT NULL,
Password VARCHAR (10) NOT NULL,
)

SELECT * FROM Users;


CREATE TABLE Medicine (
Id INT IDENTITY(1,1) PRIMARY KEY,
MedicineID VARCHAR (50) NOT NULL,
MedicineName VARCHAR (50) NOT NULL,
ManufacturingDate DATETIME NOT NULL,
MedicineNumber VARCHAR (50) NOT NULL,
MedicineType VARCHAR (50) NOT NULL,
TypicalDosage VARCHAR (50) NOT NULL,
ExpireDate DATETIME NOT NULL,
Quantity BIGINT NOT NULL,
PricePerUnit BIGINT NOT NULL
);

SELECT * FROM Medicine;

CREATE TABLE Customers (
Id INT IDENTITY (1, 1) NOT NULL,
UserRole VARCHAR (20) NOT NULL,
Name VARCHAR (20) NOT NULL,
DateofBirth DATETIME NOT NULL,
Mobile BIGINT NOT NULL,
Email VARCHAR (50) NOT NULL,
Username VARCHAR (20) NOT NULL,
Password VARCHAR (10) NOT NULL,
)

SELECT * FROM Customers;

CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
	MedicineID VARCHAR(50),
    MedicineName VARCHAR(50),
    MedicineType VARCHAR(50),
    TypicalDosage VARCHAR(50),
    ExpireDate DATETIME,
    PricePerUnit DECIMAL,
    NoofUnits BIGINT,
    TotalPrice DECIMAL,
    OrderDate DATETIME
);

SELECT * FROM Orders;