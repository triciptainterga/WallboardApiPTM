using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/report")]
public class CmsReportController : ControllerBase
{
    private readonly IConfiguration _config;

    public CmsReportController(IConfiguration config)
    {
        _config = config;
    }

    private async Task<IActionResult> FetchCmsReport(string reportKey)
    {
        string baseUrl = _config["CmsReportConfig:BaseUrl"];
        string sessionId = _config["CmsReportConfig:SessionId"];
        string reportId = _config[$"CmsReportConfig:ReportIds:{reportKey}"];

        if (string.IsNullOrEmpty(reportId))
            return BadRequest($"Report ID untuk '{reportKey}' tidak ditemukan di konfigurasi.");

        string url = $"{baseUrl}{reportId}";

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Cookie", "JSESSIONID=" + sessionId);

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        using var client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "DotNetClient");

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "Failed to fetch report");

        var json = await response.Content.ReadAsStringAsync();
        return Content(json, "application/json");
    }

    [HttpGet("detail-data-voice")]
    public async Task<IActionResult> GetDetailDataVoice()
    {
        return await FetchCmsReport("voice");
    }

    [HttpGet("detail-data-email")]
    public async Task<IActionResult> GetDetailDataEmail()
    {
        return await FetchCmsReport("email");
    }

    [HttpGet("detail-data-sosmed")]
    public async Task<IActionResult> GetDetailDataSosmed()
    {
        return await FetchCmsReport("sosmed");
    }

    [HttpGet("detail-data-livechat")]
    public async Task<IActionResult> GetDetailDataLivechat()
    {
        return await FetchCmsReport("livechat");
    }

    [HttpGet("detail-data-video")]
    public async Task<IActionResult> GetDetailDataVideo()
    {
        return await FetchCmsReport("video");
    }


   
}
