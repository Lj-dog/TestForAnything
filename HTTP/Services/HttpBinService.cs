using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HTTP.Models;
using Microsoft.Extensions.Logging;

namespace HTTP.Services
{
    internal class HttpBinService
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<HttpBinService> _logger;

        public HttpBinService(HttpClient httpClient, ILogger<HttpBinService> logger) =>
            (_httpClient, _logger) = (httpClient, logger);

        public async Task<HTTPResult> GetIPAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("get");
                response.EnsureSuccessStatusCode();
                //var content = await response.Content.ReadAsStreamAsync();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                //var ip = await JsonSerializer.DeserializeAsync<IPModel>(content, options);
                var res = JsonSerializer.Deserialize<HTTPResult>(content, options);

                if (res == null)
                    throw new NullReferenceException("解析失败");

                _logger.LogInformation(
                    "Successfully retrieved IP information: {Content} \n\r" + "Url: {Url}",
                    res.Origin,
                    res.Url
                );

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making the GET request to /ip");
                throw;
            }
        }

        public async Task<HTTPResult> DeleteAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync("delete");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var res = JsonSerializer.Deserialize<HTTPResult>(content, options);
                if (res == null)
                    throw new NullReferenceException("解析失败");

                _logger.LogInformation(
                    "Successfully made DELETE request: {Content}\n\r" + "Url: {Url}",
                    res.Origin,
                    res.Url
                );
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An error occurred while making the DELETE request to /delete"
                );
                throw;
            }
        }

        public async Task<HTTPResult> PatchAsync(string? info = null)
        {
            try
            {
                var infoStream = new StringContent(
                    info ?? string.Empty,
                    Encoding.UTF8,
                    "application/json"
                );
                HttpResponseMessage response = await _httpClient.PatchAsync("patch", infoStream);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var res = JsonSerializer.Deserialize<HTTPResult>(content, options);
                if (res == null)
                    throw new NullReferenceException("解析失败");
                _logger.LogInformation(
                    "Successfully made PATCH request: {Content}\n\r" + "Url: {Url}",
                    res.Origin,
                    res.Url
                );
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making the PATCH request to /patch");
                throw;
            }
        }

        public async Task<HTTPResult> PostAsync(string? info = null)
        {
            try
            {
                var infoStream = new StringContent(
                    info ?? string.Empty,
                    Encoding.UTF8,
                    "application/json"
                );
                HttpResponseMessage response = await _httpClient.PostAsync("post", infoStream);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var res = JsonSerializer.Deserialize<HTTPResult>(content, options);
                if (res == null)
                    throw new NullReferenceException("解析失败");
                _logger.LogInformation(
                    "Successfully made POST request: {Content}\n\r" + "Url: {Url}",
                    res.Origin,
                    res.Url
                );
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making the POST request to /post");
                throw;
            }
        }

        public async Task<HTTPResult> PutAsync(string? info = null)
        {
            try
            {
                var infoStream = new StringContent(
                    info ?? string.Empty,
                    Encoding.UTF8,
                    "application/json"
                );
                HttpResponseMessage response = await _httpClient.PutAsync("put", infoStream);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var res = JsonSerializer.Deserialize<HTTPResult>(content, options);
                if (res == null)
                    throw new NullReferenceException("解析失败");
                _logger.LogInformation(
                    "Successfully made PUT request: {Content}\n\r" + "Url: {Url}",
                    res.Origin,
                    res.Url
                );
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making the PUT request to /put");
                throw;
            }
        }

        public async Task<AuthResult> GetAuthAsync(string username, string password)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))
                    );

                var requestUri = $"basic-auth/{username}/{password}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var res = JsonSerializer.Deserialize<AuthResult>(content, options);
                if (res == null)
                    throw new NullReferenceException("解析失败");

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An error occurred while making the GET request to /basic-auth"
                );
                throw;
            }
        }

        public async Task<string> GetDenyRobotTxtAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("deny");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while making the GET request to /deny");
                throw;
            }
        }

        public async Task GetImageAsync(string imgType)
        {
            try
            {
                string savePicdFloderPath =
                    AppDomain.CurrentDomain.BaseDirectory + "imgs";
                if (!Directory.Exists(savePicdFloderPath))
                    Directory.CreateDirectory(savePicdFloderPath);

                string savePicPath = Path.Combine(savePicdFloderPath, $"httpbin_image.{imgType}");

                string requestUri = $"image/{imgType}";

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                //var content = await response.Content.ReadAsByteArrayAsync();

                //using (var fs = new FileStream(savePicPath, FileMode.Create))
                //{
                //    await fs.WriteAsync(content, 0, content.Length);
                //}

                using (var content = await response.Content.ReadAsStreamAsync())
                {
                    using (var fs = new FileStream(savePicPath,FileMode.Create))
                    {
                        await content.CopyToAsync(fs);
                        await fs.FlushAsync();
                    }
                }
            }
            catch (Exception)
            {
                _logger.LogError(
                    $"An error occurred while making the GET request to /image/{imgType}"
                );
                throw;
            }
        }

    }
}
