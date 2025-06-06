CREATE DATABASE ClientesDB;
GO

USE ClientesDB;
GO

-- Tabela de Clientes
CREATE TABLE Cliente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Sexo NVARCHAR(20) NOT NULL,
    Endereco NVARCHAR(200)
);

-- Tabela de Telefones
CREATE TABLE Telefone (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero NVARCHAR(20) NOT NULL,
    Ativo BIT NOT NULL,
    ClienteId INT NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id) ON DELETE CASCADE
);
