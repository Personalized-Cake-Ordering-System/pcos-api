﻿namespace CusCake.Application;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    public JWTOptions JWTOptions { get; set; } = default!;
}

public class ConnectionStrings
{
    public string MySqlString { get; set; } = default!;
    public string MyVerifyToken { get; set; } = default!;
}


public class JWTOptions
{
    public string SecretKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}