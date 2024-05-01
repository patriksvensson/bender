namespace Bender.Tests.Data;

public static class Manifests
{
    public const string Simple =
        """
        {
          "resources": {
            "apiservice": {
              "type": "project.v0",
              "path": "AspireTest/AspireTest.ApiService/AspireTest.ApiService.csproj",
              "env": {
                "OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES": "true",
                "OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES": "true",
                "OTEL_DOTNET_EXPERIMENTAL_OTLP_RETRY": "in_memory",
                "ASPNETCORE_FORWARDEDHEADERS_ENABLED": "true"
              },
              "bindings": {
                "http": {
                  "scheme": "http",
                  "protocol": "tcp",
                  "transport": "http"
                },
                "https": {
                  "scheme": "https",
                  "protocol": "tcp",
                  "transport": "http"
                }
              }
            },
            "sqlserver": {
              "type": "container.v0",
              "connectionString": "Server={sqlserver.bindings.tcp.host},{sqlserver.bindings.tcp.port};User ID=sa;Password={sqlserver-password.value};TrustServerCertificate=true",
              "image": "mcr.microsoft.com/mssql/server:2022-latest",
              "env": {
                "ACCEPT_EULA": "Y",
                "MSSQL_SA_PASSWORD": "{sqlserver-password.value}"
              },
              "bindings": {
                "tcp": {
                  "scheme": "tcp",
                  "protocol": "tcp",
                  "transport": "tcp",
                  "targetPort": 1433
                }
              }
            },
            "TestDatabase": {
              "type": "value.v0",
              "connectionString": "{sqlserver.connectionString};Database=TestDatabase"
            },
            "webfrontend": {
              "type": "project.v0",
              "path": "AspireTest/AspireTest.Web/AspireTest.Web.csproj",
              "env": {
                "OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES": "true",
                "OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES": "true",
                "OTEL_DOTNET_EXPERIMENTAL_OTLP_RETRY": "in_memory",
                "ASPNETCORE_FORWARDEDHEADERS_ENABLED": "true",
                "services__apiservice__http__0": "{apiservice.bindings.http.url}",
                "services__apiservice__https__0": "{apiservice.bindings.https.url}",
                "ConnectionStrings__TestDatabase": "{TestDatabase.connectionString}"
              },
              "bindings": {
                "http": {
                  "scheme": "http",
                  "protocol": "tcp",
                  "transport": "http",
                  "external": true
                },
                "https": {
                  "scheme": "https",
                  "protocol": "tcp",
                  "transport": "http",
                  "external": true
                }
              }
            },
            "sqlserver-password": {
              "type": "parameter.v0",
              "value": "{sqlserver-password.inputs.value}",
              "inputs": {
                "value": {
                  "type": "string",
                  "secret": true,
                  "default": {
                    "generate": {
                      "minLength": 22,
                      "minLower": 1,
                      "minUpper": 1,
                      "minNumeric": 1
                    }
                  }
                }
              }
            }
          }
        }
        """;
}