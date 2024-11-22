CREATE DATABASE UzbekDialects ;
USE UzbekDialects;


-- So'z o'zgartiruvchi qo'shimchalar turlari :
CREATE TABLE TypesOfInflectionalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(100)
)

-- Shevalar 
CREATE TABLE Dialects (
    Id INT PRIMARY KEY,
    Title VARCHAR(100)
)

-- So'z turkumlari
CREATE TABLE PartOfSpeech (
    Id INT PRIMARY KEY,
    Title VARCHAR(100)
)

CREATE TABLE  InflectionalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    TypesOfInflectionalAffixesId INT,
    FOREIGN KEY(TypesOfInflectionalAffixesId) REFERENCES TypesOfInflectionalAffixes(Id)
)
