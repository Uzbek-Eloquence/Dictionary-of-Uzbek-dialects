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

-- Soz ozgartiruvchi qoshimchalar
CREATE TABLE  InflectionalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    TypesOfInflectionalAffixesId INT,
    FOREIGN KEY(TypesOfInflectionalAffixesId) REFERENCES TypesOfInflectionalAffixes(Id)
)

-- Adabiy sozlar
CREATE TABLE  LiteraryWords (
    Id INT PRIMARY KEY,
    Title VARCHAR(256),
    Description VARCHAR(1024),
    PartOfSpeech INT,
    FOREIGN KEY(PartOfSpeech) REFERENCES PartOfSpeech(Id)
)


-- Soz yasovchi qoshimchalar
CREATE TABLE  DerivationalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    PartOfSpeech INT,
    FOREIGN KEY(PartOfSpeech) REFERENCES PartOfSpeech(Id)
)

-- Shevadagi sozlar
CREATE TABLE  DialectalWords (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    LiteraryWordsId INT,
    DialectsId INT,
    FOREIGN KEY(LiteraryWordsId) REFERENCES LiteraryWords(Id),
    FOREIGN KEY(DialectsId) REFERENCES DialectalWords(Id)
)

-- Shevadagi soz ozgartiruvchi qoshimchalar
CREATE TABLE  DialectalInflectalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    InflectionalAffixesId INT,
    DialectsId INT,
    FOREIGN KEY(InflectionalAffixesId) REFERENCES InflectionalAffixes(Id),
    FOREIGN KEY(DialectsId) REFERENCES DialectalWords(Id)
)

-- Shevadagi soz yasovchi qoshimchalar
CREATE TABLE  DialectalDerivationalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    DerivationalAffixesId INT,
    DialectsId INT,
    FOREIGN KEY(DerivationalAffixesId) REFERENCES DerivationalAffixes(Id),
    FOREIGN KEY(DialectsId) REFERENCES DialectalWords(Id)
)