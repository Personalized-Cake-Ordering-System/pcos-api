﻿namespace CusCake.Application;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = default!;
    public JWTOptions JWTOptions { get; set; } = default!;
    public FirebaseSettings FirebaseSettings { get; set; } = default!;
    public SepayOptions SepayOptions { get; set; } = default!;
    public string GoongAPIKey { get; set; } = default!;

}

public class ConnectionStrings
{
    public string MySqlString { get; set; } = default!;
    public string MongoDbConnection { get; set; } = default!;
}

public class SepayOptions
{
    public string BankId { get; set; } = default!;
    public string AccountNo { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
}

public class JWTOptions
{
    public string SecretKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}

public class FirebaseSettings
{
    public string SenderId { get; set; } = default!;
    public string ServerKey { get; set; } = default!;
    public string ApiKeY { get; set; } = default!;
    public string Bucket { get; set; } = default!;
    public string AuthEmail { get; set; } = default!;
    public string AuthPassword { get; set; } = default!;
}
