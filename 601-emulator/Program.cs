// <imports>
using Gremlin.Net.Driver;
// </imports>

// <client>
var server = new GremlinServer(
    hostname: "localhost",
    port: 65400,
    username: "/dbs/db1/colls/coll1",
    password: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
);

using var client = new GremlinClient(
    gremlinServer: server,
    messageSerializer: new Gremlin.Net.Structure.IO.GraphSON.GraphSON2MessageSerializer()
);
// </client>

// <graph>
await client.SubmitAsync(
    requestScript: "g.V().drop()"
);
// </graph>

// <insert>
await client.SubmitAsync(
    requestScript: "g.addV('product').property('id', prop_id).property('name', prop_name)",
    bindings: new Dictionary<string, object>
    {
        { "prop_id", "68719518371" },
        { "prop_name", "Kiama classic surfboard" }
    }
);
// </insert>
