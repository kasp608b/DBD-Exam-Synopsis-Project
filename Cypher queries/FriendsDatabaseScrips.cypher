//CreateFriendsDatabase
MERGE (Alice:Person {id: 1, name: 'Alice'})
MERGE (Bob:Person {id: 2, name: 'Bob'})
MERGE (Elizabeth:Person {id: 3, name: 'Elizabeth'})
MERGE (Zach:Person {id: 4, name: 'Zach'})
MERGE (Michael:Person {id: 5, name: 'Michael'})
MERGE (Joy:Person {id: 6, name: 'Joy'})
MERGE (Theodore:Person {id: 7, name: 'Theodore'})
MERGE (Abner:Person {id: 8, name: 'Abner'})

MERGE (Alice)-[:FRIENDS_WITH]->(Bob)
MERGE (Alice)-[:FRIENDS_WITH]->(Elizabeth)
MERGE (Bob)-[:FRIENDS_WITH]->(Alice)
MERGE (Bob)-[:FRIENDS_WITH]->(Zach)
MERGE (Zach)-[:FRIENDS_WITH]->(Alice)
MERGE (Elizabeth)-[:FRIENDS_WITH]->(Bob)
MERGE (Bob)-[:FRIENDS_WITH]->(Elizabeth)
MERGE (Michael)-[:FRIENDS_WITH]->(Joy)
MERGE (Joy)-[:FRIENDS_WITH]->(Theodore)
MERGE (Theodore)-[:FRIENDS_WITH]->(Abner)
MERGE (Abner)-[:FRIENDS_WITH]->(Zach)


//CreateFriendsDatabaseShemaScript
CREATE CONSTRAINT person_id_unique_constraint IF NOT EXISTS
FOR (p:Person)
REQUIRE p.id IS UNIQUE

// CreateFriendship
MATCH (n:Person {id: 9})
MATCH (m:Person {id: 8})
CREATE (n)-[:FRIENDS_WITH]->(m)



// CreatePerson
CREATE (n:Person {id: 9, name: 'Jake'})



//Delete database
MATCH (n)
DETACH DELETE n

//DeletePerson
MATCH (p:Person)
WHERE p.id = 9
DETACH DELETE p


// GetAllFriendsOfFriendsOfPerson
MATCH (n:Person {name: 'Alice'})-[:FRIENDS_WITH*2]->(m)
WHERE m.id <> n.id
RETURN n.name AS PERSON, m.name AS FRIEND_OF_FRIEND


//GetAllFriendsOfPerson
MATCH (n:Person {name: 'Bob'})-[:FRIENDS_WITH]->(m)
RETURN m.name


// GetAllFriendsOfPersonReciprocal
MATCH (n:Person {name: 'Bob'})<-[:FRIENDS_WITH]-(m)
RETURN m.name


// GetALLPersons
MATCH (n:Person)
RETURN n.id, n.name



//GetPerson
MATCH (p:Person)
WHERE p.id = 8
RETURN p.id,p.name

// MakeEveryoneFriendsWithEveryone
MATCH (n:Person)
MATCH (m:Person)
WHERE n <> m
CREATE (n)-[:FRIENDS_WITH]->(m)



//UpdatePersonName
MATCH (p:Person)
WHERE p.id = 8
SET p.name = "Jared"