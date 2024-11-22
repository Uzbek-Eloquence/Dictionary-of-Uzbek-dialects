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


CREATE TABLE  LiteraryWords (
    Id INT PRIMARY KEY,
    Title VARCHAR(256),
    Description VARCHAR(1024),
    PartOfSpeech INT,
    FOREIGN KEY(PartOfSpeech) REFERENCES PartOfSpeech(Id)
)


CREATE TABLE  DerivationalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    PartOfSpeech INT,
    FOREIGN KEY(PartOfSpeech) REFERENCES PartOfSpeech(Id)
)

CREATE TABLE  DialectalWords (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    LiteraryWordsId INT,
    DialectsId INT,
    FOREIGN KEY(LiteraryWordsId) REFERENCES LiteraryWords(Id),
    FOREIGN KEY(DialectsId) REFERENCES DialectalWords(Id)
)

CREATE TABLE  DialectalInflectalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    InflectionalAffixesId INT,
    DialectsId INT,
    FOREIGN KEY(InflectionalAffixesId) REFERENCES InflectionalAffixes(Id),
    FOREIGN KEY(DialectsId) REFERENCES DialectalWords(Id)
)

CREATE TABLE  DialectalDerivationalAffixes (
    Id INT PRIMARY KEY,
    Title VARCHAR(16),
    IsSuffix BIT,
    DerivationalAffixesId INT,
    DialectsId INT,
    FOREIGN KEY(DerivationalAffixesId) REFERENCES DerivationalAffixes(Id),
    FOREIGN KEY(DialectsId) REFERENCES DialectalWords(Id)
)