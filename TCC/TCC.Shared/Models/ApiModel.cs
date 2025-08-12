namespace TCC.Shared.Models;

public class ApiModel
{
    public string Endpoint { get; set; } = "http://localhost:40123/api/";
    public double Tolerance { get; set; } = 0.02;
}