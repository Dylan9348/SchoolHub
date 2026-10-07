
using SchoolHubApi.Models.Languages;

using FluentEmail.Core;
using SchoolHubApi.Models.Exceptions;
using System.Net;
using SchoolHubApi.Validators;

namespace SchoolHubApi.Services.Auth;

public class EmailAuthService(IFluentEmail fluentEmail) : IEmailAuthService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;

    private static string BuildEmailHtml(string title, string code, string disclaimerBold, string disclaimerRest)
    {
        var safeTitle = WebUtility.HtmlEncode(title);
        var safeCode = WebUtility.HtmlEncode(code);
        var safeBold = WebUtility.HtmlEncode(disclaimerBold);
        var safeRest = WebUtility.HtmlEncode(disclaimerRest);

        return $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>{safeTitle}</title>
        </head>
        <body style="margin:0;padding:0;background-color:#eaf2fb;">
        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="background-color:#eaf2fb;padding:32px 16px;">
            <tr>
            <td align="center">
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0"
                    style="max-width:480px;background-color:#ffffff;border:2px solid #1e3a8a;border-radius:12px;overflow:hidden;font-family:Segoe UI,Helvetica,Arial,sans-serif;">
                <tr>
                    <td style="background-color:#1d4ed8;padding:24px;text-align:center;">
                    <h1 style="margin:0;font-size:20px;color:#ffffff;font-weight:600;">{safeTitle}</h1>
                    </td>
                </tr>
                <tr>
                    <td style="padding:32px 24px;text-align:center;">
                    <p style="margin:0 0 20px;font-size:16px;color:#2563eb;">
                        <span style="font-weight:700;color:#1d4ed8;">{safeBold}</span> {safeRest}
                    </p>
                    <div style="display:inline-block;padding:14px 28px;background-color:#dbeafe;border:1px dashed #1d4ed8;border-radius:8px;
                                font-family:Consolas,Menlo,monospace;font-size:28px;letter-spacing:6px;font-weight:700;color:#1d4ed8;">
                        {safeCode}
                    </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding:16px 24px;background-color:#f0f6ff;text-align:center;font-size:12px;color:#60748c;">
                    SchoolHub
                    </td>
                </tr>
                </table>
            </td>
            </tr>
        </table>
        </body>
        </html>
        """;
    }

    public async Task<bool> SendVerificationMailAsync(string email, string language, string code)
    {
        var (subject, boldDisclaimer, disclaimer) = language switch
        {
            SupportedLanguages.Spanish => (SendVerificationMailResponses.SpanishSubject, SendVerificationMailResponses.SpanishDisBold, SendVerificationMailResponses.SpanishDisclaimer),
            SupportedLanguages.English => (SendVerificationMailResponses.EnglishSubject, SendVerificationMailResponses.EnglishDisBold, SendVerificationMailResponses.EnglishDisclaimer),
            _ => throw new LanguageNotSupportedException(language)
        };

        var response = await _fluentEmail
            .To(email)
            .Subject(subject)
            .Body(BuildEmailHtml(subject, code, boldDisclaimer, disclaimer), isHtml: true)
            .SendAsync();
        
        return response.Successful;
    }
}
