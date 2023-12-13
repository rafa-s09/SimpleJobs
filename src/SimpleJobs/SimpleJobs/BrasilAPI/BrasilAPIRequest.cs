using System.Net;


namespace SimpleJobs.BrasilAPI;

public class BrasilAPIRequest
{

    private static async Task<BrasilAPIResponse<TEntity>> GetAsync<TEntity>(string url) where TEntity : class
    {       
        try
        {
            const string baseUrl = "https://brasilapi.com.br/api/";

            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(baseUrl);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return new BrasilAPIResponse<TEntity>() { Success = true, Message = "Ok", Content = JsonSerializer.Deserialize<TEntity>(responseBody) };
                }
            }

        }
#if DEBUG
        catch (HttpRequestException ex)
        {
            return new BrasilAPIResponse<TEntity>() { Success = false, Message = ex.ToString() };
        }
        catch (Exception ex)
        {
            return new BrasilAPIResponse<TEntity>() { Success = false, Message = ex.ToString() };
        }
#else
        catch
        {
            return new BrasilAPIResponse<TEntity>() { Success = false, Message = "ocorreu um erro durante a consulta na API, verifique se os dados estão corretos." };
        }
#endif
    }
}

