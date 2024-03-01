CREATE TABLE Conta (
    Email VARCHAR(255) PRIMARY KEY,
    Nome VARCHAR(255)
)

INSERT INTO Conta Values ('ploomes@email.com', 'Ploomes')

CREATE TABLE Cliente (
    Nome VARCHAR(255),
    Created_At DATETIME,
    Email_Conta VARCHAR(255),
    UrlAvatar VARCHAR(255),
    CONSTRAINT FK_EmailClient_Conta FOREIGN KEY (Email_Conta)
     REFERENCES Conta(Email),
    CONSTRAINT PK_Nome_Email PRIMARY KEY(Email_Conta, Nome)
)