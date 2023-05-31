using ConsoleTables;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
namespace DBD_Exam_Synopsis_Project
{



    public class GraphQueries
    {
        public async Task<Dictionary<string, object>> CreatePerson(string name, int id)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Write));

            // Create a node within a write transaction
            var result = await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync("CREATE (p:Person {id: $id,name: $name}) RETURN p",
                    new { id, name });
                return await cursor.SingleAsync();
            });

            // Get the `p` value from the first record
            var node = result["p"].As<INode>();

            // Return the properties of the node
            return node.Properties
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task CreatePersonFriend(int PId1, int PId2)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Write));

            // Create a node within a write transaction
            await session.ExecuteWriteAsync(async tx =>
           {
               var cursor = await tx.RunAsync("MATCH (n:Person {id: $PId1})" +
                   "MATCH (m:Person {id: $PId2})" +
                   "CREATE (n)-[:FRIENDS_WITH]->(m)",
                   new { PId1, PId2 });
           });



        }

        public async Task DeletePerson(int PId)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Write));

            // Create a node within a write transaction
            await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync("MATCH (p:Person) " +
                    "WHERE p.id = $PId " +
                    "DETACH DELETE p ",
                    new { PId });
            });



        }

        public async Task GetAllPersons()
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Read));

            // Run a query within a Read Transaction
            await session.ExecuteReadAsync(async tx =>
           {
               var cursor = await tx.RunAsync(@"
                        MATCH(p:Person)
                        RETURN p.id AS id, p.name AS name");

               List<IRecord> rows = await cursor.ToListAsync();

               var table = new ConsoleTable();

               if (rows.Any())
               {
                   var columns = new List<string>();

                   rows.First().Keys.ToList().ForEach(key =>
                   {
                       columns.Add(key);
                   });

                   table.AddColumn(columns);

                   rows.ForEach(row =>
                   {
                       var values = new List<string>();

                       row.Values.ToList().ForEach(value =>
                       {
                           values.Add(value.Value.ToString());
                       });

                       table.AddRow(values.ToArray());
                   });

                   table.Write();
               }
               else
               {
                   Console.WriteLine("No rows found.");
               }
           });

        }
        public async Task GetPerson(int PId)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Read));

            // Run a query within a Read Transaction
            await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(@"
                        MATCH(p:Person)
                        WHERE p.id = $PId
                        RETURN p.id AS id, p.name AS name",
                        new { PId });

                List<IRecord> rows = await cursor.ToListAsync();

                var table = new ConsoleTable();

                if (rows.Any())
                {
                    var columns = new List<string>();

                    rows.First().Keys.ToList().ForEach(key =>
                    {
                        columns.Add(key);
                    });

                    table.AddColumn(columns);

                    rows.ForEach(row =>
                    {
                        var values = new List<string>();

                        row.Values.ToList().ForEach(value =>
                        {
                            values.Add(value.Value.ToString());
                        });

                        table.AddRow(values.ToArray());
                    });

                    table.Write();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            });

        }
        public async Task GetAllFriendsOfPerson(string PName)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Read));

            // Run a query within a Read Transaction
            await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(@"
                        MATCH (n:Person {name: $PName})-[:FRIENDS_WITH]->(m)
                        RETURN  m.id AS id, m.name AS name"
                ,
                        new { PName });

                List<IRecord> rows = await cursor.ToListAsync();

                var table = new ConsoleTable();

                if (rows.Any())
                {
                    var columns = new List<string>();

                    rows.First().Keys.ToList().ForEach(key =>
                    {
                        columns.Add(key);
                    });

                    table.AddColumn(columns);

                    rows.ForEach(row =>
                    {
                        var values = new List<string>();

                        row.Values.ToList().ForEach(value =>
                        {
                            values.Add(value.Value.ToString());
                        });

                        table.AddRow(values.ToArray());
                    });

                    table.Write();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            });



        }

        public async Task GetAllFriendsOfPersonReciprocal(string PName)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Read));

            // Run a query within a Read Transaction
            await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(@"
                        MATCH (n:Person {name: $PName})<-[:FRIENDS_WITH]-(m)
                        RETURN  m.id AS id, m.name AS name"
                ,
                        new { PName });

                List<IRecord> rows = await cursor.ToListAsync();

                var table = new ConsoleTable();

                if (rows.Any())
                {
                    var columns = new List<string>();

                    rows.First().Keys.ToList().ForEach(key =>
                    {
                        columns.Add(key);
                    });

                    table.AddColumn(columns);

                    rows.ForEach(row =>
                    {
                        var values = new List<string>();

                        row.Values.ToList().ForEach(value =>
                        {
                            values.Add(value.Value.ToString());
                        });

                        table.AddRow(values.ToArray());
                    });

                    table.Write();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            });



        }
        public async Task UpdatePersonName(int PId, string PName)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Write));

            // Create a node within a write transaction
            await session.ExecuteWriteAsync(async tx =>
            {
                var cursor = await tx.RunAsync(@"MATCH (p:Person)" +
                    "WHERE p.id = $PId" +
                    " SET p.name = $PName",
                    new { PId, PName });
            });


        }

        public async Task GetAllFriendsOfFriendsOfPerson(string PName)
        {
            // Create a Session for the `people` database
            using var session = Neo4jDriver.Driver.AsyncSession(configBuilder =>
                configBuilder
                    .WithDefaultAccessMode(AccessMode.Read));

            // Run a query within a Read Transaction
            await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(@"
                        MATCH (n:Person {name: $PName})-[:FRIENDS_WITH]->(m)-[:FRIENDS_WITH]->(o)
                        RETURN  o.id AS id, o.name AS name"
                ,
                        new { PName });

                List<IRecord> rows = await cursor.ToListAsync();

                var table = new ConsoleTable();

                if (rows.Any())
                {
                    var columns = new List<string>();

                    rows.First().Keys.ToList().ForEach(key =>
                    {
                        columns.Add(key);
                    });

                    table.AddColumn(columns);

                    rows.ForEach(row =>
                    {
                        var values = new List<string>();

                        row.Values.ToList().ForEach(value =>
                        {
                            values.Add(value.Value.ToString());
                        });

                        table.AddRow(values.ToArray());
                    });

                    table.Write();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            });
        }


    }
}


