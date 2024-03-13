CREATE DATABASE SoftTradePlus;
GO

USE SoftTradePlus;
GO

-- �������� ������� ��� �������� ��������
CREATE TABLE ClientStatus (
    StatusId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL
);
GO

-- ���������� ���������������� �������� ��������
INSERT INTO ClientStatus (Name) VALUES ('�������� ������'), ('������� ������');
GO

-- �������� ������� ��� ����������
CREATE TABLE Manager (
    ManagerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
GO

-- �������� ������� ��� �������
CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    SubscriptionPeriod NVARCHAR(50)
);
GO

-- �������� ������� ��� ��������
CREATE TABLE Client (
    ClientId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
	ProductId INT FOREIGN KEY REFERENCES Product(ProductId),
    StatusId INT FOREIGN KEY REFERENCES ClientStatus(StatusId),
    ManagerId INT FOREIGN KEY REFERENCES Manager(ManagerId)
);
GO
