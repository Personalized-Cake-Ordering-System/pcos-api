Note for CLI
dotnet ef migrations remove -s .\CusCake.WebApi\ -p .\CusCake.Infrastructures\
dotnet ef migrations add Initial -s .\CusCake.WebApi\ -p .\CusCake.Infrastructures\  
dotnet ef database update -s .\CusCake.WebApi\ -p .\CusCake.Infrastructures\
dotnet ef database drop -s .\CusCake.WebApi\ -p .\CusCake.Infrastructures\
dotnet run --project .\APIs\CusCake.WebApi\
