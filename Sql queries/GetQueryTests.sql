/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [PersonID]
      ,[FriendID]
  FROM [Friends].[dbo].[PersonFriend]

  SELECT TOP (1000) [PId]
      ,[PName]
  FROM [Friends].[dbo].[Person]

USE Friends 
SELECT p1.PName
FROM Person p1 JOIN PersonFriend
ON PersonFriend.FriendID = p1.PId
JOIN Person p2
ON PersonFriend.PersonID = p2.PId
WHERE p2.PName = 'Bob'

SELECT p1.PName
FROM Person p1 JOIN PersonFriend
ON PersonFriend.PersonID = p1.PId
JOIN Person p2
ON PersonFriend.FriendID = p2.PId
WHERE p2.PName = 'Bob'

SELECT p1.PName AS PERSON, p2.PName AS FRIEND_OF_FRIEND
FROM PersonFriend pf1 JOIN Person p1
ON pf1.PersonID = p1.PId
JOIN PersonFriend pf2
ON pf2.PersonID = pf1.FriendID
JOIN Person p2
ON pf2.FriendID = p2.PId
WHERE p1.PName = 'Alice' AND pf2.FriendID <> p1.PId