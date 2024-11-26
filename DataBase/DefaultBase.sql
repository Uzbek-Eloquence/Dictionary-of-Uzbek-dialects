USE  UzbekDialectsDB;



INSERT INTO PartOfSpeech (Title)VALUES
('Ot'),('Sifat'),('Son'),('Fel'),('Olmosh'),('Ravish');



INSERT INTO TypesOfInflectionalAffixes(Title) VALUES
('Nisbat qo`shimchalari'),
('Kelishik qo`shimchalari'),
('Egalik qo`shimchalari'),
('Shaxs-son qo`shimchalari');




INSERT INTO Dialects (Title) VALUES
('Parket shevasi'),('Piskent shevasi');


INSERT INTO DerivationalAffixes(Title,IsSuffix,FirstPartOfSpeachId,LastPartOfSpeechId) VALUES
('chi',1,1,1),
('kor',1,1,1),
('gar',1,1,1),
('dor',1,1,1),
('boz',1,1,1),
('xon',1,1,1),
('sil',1,1,1),
('soz',1,1,1),
('go`y',1,1,1),
('vchi',1,4,1),
('uvchi',1,4,1),
('ham',0,1,1),
('lik',1,1,1),
('kash',1,1,1),
('kar',1,1,1),
('gor',1,1,1),
('bon',1,1,1),
('paz',1,1,1),
('shunos',1,1,1),
('do`z',1,1,1),
('xo`r',1,1,1),
('parast',1,1,1),
('ovchi',1,4,1),
('dosh',1,1,1),
('furush',1,1,1);




SELECT * FROM DerivationalAffixes;
 

SELECT * FROM Dialects; 
