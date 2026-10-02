
using System.Net;
using System.Net.Mail;

namespace SchoolHubApi.Extensions;

public static class EmailExtension
{
    private static string GetVariable(string variableName) => 
        Environment.GetEnvironmentVariable(variableName)
            ?? throw new InvalidOperationException($"ENVIRONMENT VARIABLE \"{variableName}\" NOT FOUND");
    public static IServiceCollection AddEmail(this IServiceCollection services)
    {
        var host = GetVariable("EMAIL_HOST");
        var port = int.Parse(GetVariable("EMAIL_PORT"));
        var user = GetVariable("EMAIL_USER");
        var password = GetVariable("EMAIL_PASSWORD");
        var fromName = GetVariable("EMAIL_FROMNAME");
        var from = GetVariable("EMAIL_FROM");

        var smtpClient = new SmtpClient(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(user, password)
        };

        services
            .AddFluentEmail(from, fromName)
            .AddSmtpSender(smtpClient);

        return services;
    }
}
