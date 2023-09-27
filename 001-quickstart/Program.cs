// <imports>
using Gremlin.Net.Driver;
// </imports>

// <environment_variables>
string accountName = Environment.GetEnvironmentVariable("COSMOS_GREMLIN_ENDPOINT")!;
string accountKey = Environment.GetEnvironmentVariable("COSMOS_GREMLIN_KEY")!;
// </environment_variables>

// <authenticate_client>
var server = new GremlinServer(
    hostname: $"{accountName}.gremlin.cosmos.azure.com",
    port: 443,
    username: "/dbs/cosmicworks/colls/products",
    password: $"{accountKey}",
    enableSsl: true
);
// </authenticate_client>

// <connect_client>
using var client = new GremlinClient(
    gremlinServer: server,
    messageSerializer: new Gremlin.Net.Structure.IO.GraphSON.GraphSON2MessageSerializer()
);
// </connect_client>

// <create_vertices_1>
await client.SubmitAsync(
    requestScript: "g.addV('product').property('id', '68719518371').property('name', 'Kiama classic surfboard').property('price', 285.55).property('category', 'surfboards')"
);
// </create_vertices_1>

// <create_vertices_2>
await client.SubmitAsync(
    requestScript: "g.addV('product').property('id', '68719518403').property('name', 'Montau Turtle Surfboard').property('price', 600.00).property('category', 'surfboards')"
);
// </create_vertices_2>

// <create_vertices_3>
await client.SubmitAsync(
    requestScript: "g.addV('product').property('id', '68719518409').property('name', 'Bondi Twin Surfboard').property('price', 585.50).property('category', 'surfboards')"
);
// </create_vertices_3>

// <create_edges_1>
await client.SubmitAsync(
    requestScript: "g.V(['surfboards', '68719518403']).addE('replaces').to(g.V(['surfboards', '68719518371']))"
);
// </create_edges_1>

// <create_edges_2>
await client.SubmitAsync(
    requestScript: "g.V(['surfboards', '68719518403']).addE('replaces').to(g.V(['surfboards', '68719518409']))"
);
// </create_edges_2>

// <query_vertices_edges>
var results = await client.SubmitAsync<Dictionary<string, object>>(
    requestScript: "g.V().hasLabel('product').has('category', 'surfboards').has('name', 'Montau Turtle Surfboard').outE('replaces').inV()"
);
// </query_vertices_edges>

// <output_vertices_edges>
Console.WriteLine($"[CREATED PRODUCT]\t68719518403");
foreach (var result in results ?? Enumerable.Empty<Dictionary<string, object>>())
{
    Console.WriteLine($"[REPLACES PRODUCT]\t{result["id"]}");
}
// </output_vertices_edges>
