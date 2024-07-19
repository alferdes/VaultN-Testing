using NUnit.Framework;
using RestSharp;
using System.Net;

[TestFixture]
public class TestAPIUserAuth
{
    private RestClient client;

    [SetUp]
    public void Setup()
    {
        client = new RestClient("https://api-base-url.com");
    }

    [Test]
    public void Login_ValidCredentials_ReturnsOk()
    {
        var request = new RestRequest("api/auth/login", Method.POST);
        request.AddJsonBody(new { username = "validuser", password = "validpass" });

        var response = client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Content);
        // More assertions can be added to verify token, user info, etc.
    }

    [Test]
    public void Login_InvalidCredentials_ReturnsBadRequest()
    {
        var request = new RestRequest("api/auth/login", Method.POST);
        request.AddJsonBody(new { username = "invaliduser", password = "invalidpass" });

        var response = client.Execute(request);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual("Invalid user name", response.message);
        // More assertions can be added if any other part of the error response needs verifying
    }

    [Test]
    public void Register_User_ReturnsOK()
    {
        var request = new RestRequest("api/auth/register", Method.POST);
        request.AddJsonBody(new { username = "testuser1", email = "testuser1@example.com", password = "testpass123"});

        var response = client.Execute(request);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Content);
        // More assertions can be added as needed
    }

    [Test]
    public void Register_User_ReturnsBadRequest()
    {
        var request = new RestRequest("api/auth/register", Method.POST);
        request.AddJsonBody(new { username = "testuser1", email = "testuser1_notValidEmail", password = "testpass123"});

        var response = client.Execute(request);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual("Invalid email", response.message);
        // More assertions can be added as needed
    }
    
}