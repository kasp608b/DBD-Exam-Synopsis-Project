USE [master]
GO

IF DB_ID('Friends') IS NOT NULL    
BEGIN
	DROP DATABASE Friends			
END

CREATE DATABASE Friends			
GO

USE Friends						
GO

CREATE TABLE Person(
	PId			int IDENTITY(1,1) NOT NULL,
	PName			nvarchar(50)	NULL,
	CONSTRAINT PK_Person PRIMARY KEY (PId)
)
GO

CREATE TABLE PersonFriend(
	PersonID    int NOT NULL,
	FriendID    int NOT NULL,
	CONSTRAINT PK_PersonFriend PRIMARY KEY (PersonID,FriendID)
)
GO

set IDENTITY_INSERT Person ON

INSERT INTO  [Person] (PId, PName) VALUES (1, N'Alice')
INSERT INTO  [Person] (PId, PName) VALUES (2, N'Bob')
INSERT INTO  [Person] (PId, PName) VALUES (3, N'Elizabeth')
INSERT INTO  [Person] (PId, PName) VALUES (4, N'Zach')
INSERT INTO  [Person] (PId, PName) VALUES (5, N'Michael')
INSERT INTO  [Person] (PId, PName) VALUES (6, N'Joy')
INSERT INTO  [Person] (PId, PName) VALUES (7, N'Theodore')
INSERT INTO  [Person] (PId, PName) VALUES (8, N'Abner')

--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (1,2)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (1,3)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (2,1)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (2,4)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (2,3)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (4,1)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (3,2)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (5,6)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (6,7)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (7,8)
--INSERT INTO  [PersonFriend] (PersonID, FriendID) VALUES (8,4)

-- Make everyone friends with everyone.
DECLARE @i INT = 1;
DECLARE @j INT;

WHILE @i <= 8
BEGIN
    SET @j = 1;
    WHILE @j <= 8
    BEGIN
        IF @i <> @j
        BEGIN
            INSERT INTO PersonFriend (PersonID, FriendID)
            SELECT @i, @j
            FROM Person
            WHERE PId = @i;
        END;
        SET @j = @j + 1;
    END;
    SET @i = @i + 1;
END;


set IDENTITY_INSERT Person OFF

ALTER TABLE PersonFriend ADD CONSTRAINT FK_PersonFriend_Person FOREIGN KEY(PersonID)
REFERENCES Person(PId)
GO

ALTER TABLE PersonFriend ADD CONSTRAINT FK_PersonFriend_Person2 FOREIGN KEY(FriendID)
REFERENCES Person(PId)
GO

-----------------------------------------------------------------------------------------------------------------
--- Functions
-----------------------------------------------------------------------------------------------------------------
USE Friends					
GO

CREATE or alter PROCEDURE usp_CreatePerson
(
	@PName nvarchar(50),
	@PId int OUTPUT
)
AS
BEGIN 
    SET XACT_ABORT ON;

	INSERT INTO Person
	VALUES(@PName)

	
	SET @PId = SCOPE_IDENTITY();
	
	

END
GO

CREATE or alter PROCEDURE usp_CreatePersonFriend
(
	@PId1 int,
	@PId2 int
)
AS
BEGIN 
    SET XACT_ABORT ON;

	INSERT INTO PersonFriend
	VALUES(@PId1,@PId2)
END
GO

CREATE or alter PROCEDURE usp_DeletePerson
(
	@PId int
)
AS
BEGIN 
    SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT 1 FROM Person WHERE PId = @PId)
		THROW 50001, 'Person dosen''t exist', 1

    -- Delete all friendships for the person
    DELETE FROM PersonFriend
    WHERE PersonID = @PId OR FriendID = @PId;
    
	DELETE FROM Person
	WHERE PId = @PId
    
END
GO

CREATE or alter PROCEDURE usp_UpdatePersonName
(
	@PId int,
	@PName varchar(50)
	
)
AS
BEGIN 

	SET XACT_ABORT ON;
	IF NOT EXISTS (SELECT 1 FROM Person WHERE PId = @PId)
		THROW 50001, 'Person dosen''t exist', 1

	UPDATE Person SET PName = @PName WHERE PId = @PId;
END
GO

CREATE or alter PROCEDURE usp_GetPerson
(
	@PId int
)
AS
BEGIN 
	IF NOT EXISTS (SELECT 1 FROM Person WHERE PId = @PId)
		THROW 50001, 'Person dosen''t exist', 1

	SELECT DISTINCT PId, PName
	FROM Person p
	WHERE PId = @PId

END

GO
CREATE or alter PROCEDURE usp_GetAllPersons
AS
BEGIN 
	SELECT PId, PName
	FROM Person
END

GO

CREATE or alter PROCEDURE usp_GetAllFriendsOfPerson
(
	@PName varchar(50)
)
AS
BEGIN 
	SELECT p1.PName
    FROM Person p1 JOIN PersonFriend
    ON PersonFriend.FriendID = p1.PId
    JOIN Person p2
    ON PersonFriend.PersonID = p2.PId
    WHERE p2.PName = @PName
END

GO

CREATE or alter PROCEDURE usp_GetAllFriendsOfPersonReciprocal
(
	@PName varchar(50)
)
AS
BEGIN 
	SELECT p1.PName
	FROM Person p1 JOIN PersonFriend
	ON PersonFriend.PersonID = p1.PId
	JOIN Person p2
	ON PersonFriend.FriendID = p2.PId
	WHERE p2.PName = @PName
END

GO

CREATE or alter PROCEDURE usp_GetAllFriendsOfFriendsOfPerson
(
	@PName varchar(50)
)
AS
BEGIN 
	SELECT p1.PName AS PERSON, p2.PName AS FRIEND_OF_FRIEND
	FROM PersonFriend pf1 JOIN Person p1
	ON pf1.PersonID = p1.PId
	JOIN PersonFriend pf2
	ON pf2.PersonID = pf1.FriendID
	JOIN Person p2
	ON pf2.FriendID = p2.PId
	WHERE p1.PName = @PName AND pf2.FriendID <> p1.PId
END

GO