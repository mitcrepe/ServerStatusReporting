# Description
This solution will allow you to quickly add status report for your server. The report will contain information about the connectivity with configured dependent services. This report can be then consumed by automation systems to automatically detect application connection problems.

# Example usage
```c#
public void ConfigureServices(IServiceCollection services)
{
    var dependencies = Configuration.GetSection("Dependencies");

    services.AddServerStatusReport(options =>
    {
        options.TestPath = "/Test";
        options.AddHttpServiceTest(dependencies.GetSection("HttpService").Value, HttpMethod.Get);
        options.AddDatabaseTest(dependencies.GetSection("Database").Value);
        options.AddTcpTest(dependencies.GetSection("Tcp:Host").Value, int.Parse(dependencies.GetSection("Tcp:Port").Value));
    });
}
```

# Example output
```json
{
    "Type": "Full",
    "Status": 2,
    "Services": [{
            "Type": "Http",
            "Ok": true,
            "Details": {
                "Method": "GET",
                "URL": "http://google.com"
            },
            "IsCritical": false
        }, {
            "Type": "Database",
            "Ok": true,
            "Details": {
                "Host": "(LocalDb)\\MSSQLLocalDB",
                "Port": "",
                "DbName": "TodoList"
            },
            "IsCritical": false
        }, {
            "Type": "Tcp",
            "Ok": false,
            "Details": {
                "Host": "localhost",
                "Port": "1234"
            },
            "IsCritical": false
        }
    ]
}
```

Serialization for `Status` and `Type` can be change to string or int depending on consuming service.

