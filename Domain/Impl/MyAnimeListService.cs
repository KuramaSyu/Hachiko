using System.Net.Http.Json;
using Hachiko.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.ObjectPool;
namespace Hachiko.Domain.Impl;


class MyAnimeListService
{
    private string clientId;
    private readonly HttpClient _httpClient;
    private readonly ILogger<MyAnimeListService> _logger;
    private readonly string _baseApi;
    public MyAnimeListService(string clientId, HttpClient httpClient, ILogger<MyAnimeListService> logger, string baseApi)
    {
        this.clientId = clientId;
        this._httpClient = httpClient;
        this._logger = logger;
        this._baseApi = baseApi.TrimEnd('/');

    }

    public async Task<T?> GetAsync<T>(string baseUrl, IDictionary<string, string?> queryParams, CancellationToken ct = default)
    {
        var url = QueryHelpers.AddQueryString(baseUrl, queryParams);
        var response = await _httpClient.GetAsync(url, ct);

        response.EnsureSuccessStatusCode();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning($"Request failed with {response.StatusCode}");
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public Task<ICollection<Anime>> SearchAnime(string name, bool includeNsfw)
    {
        string fields = string.Join(",", new[]
        {
            "id","title","main_picture","alternative_titles",
            "start_date","end_date","synopsis","mean","rank","popularity",
            "num_list_users","num_scoring_users","nsfw","created_at",
            "updated_at","media_type","status","genres","my_list_status",
            "num_episodes","start_season","broadcast","source",
            "average_episode_duration","rating","pictures","background",
            "related_anime","related_manga","recommendations","studios","statistics",
            "opening_themes","ending_themes"
        });

        Dictionary<string, string> args = new()
        {
            ["nsfw"] = includeNsfw ? "true" : "false",
            ["limit"] = "50",
            ["fields"] = fields,
            ["q"] = name,
        };

    }
}


// -----
// query : str
//     the query to search for
// include_nsfw : bool
//     whether to include nsfw results
// fallback : bool
//     whether or not to limit the query to 50 chars
// Returns:
// --------
// Dict[str, Any]
//     the response json
// """
// try:
//     resp = self.response_cache[query]
//     return deepcopy(resp)
// except KeyError:
//     pass
// fields = (
//     "id,title,main_picture,alternative_titles,"
//     "start_date,end_date,synopsis,mean,rank,popularity,"
//     "num_list_users,num_scoring_users,nsfw,created_at,"
//     "updated_at,media_type,status,genres,my_list_status,"
//     "num_episodes,start_season,broadcast,source,"
//     "average_episode_duration,rating,pictures,background,"
//     "related_anime,related_manga,recommendations,studios,statistics,"
//     "average_episode_duration,opening_themes,ending_themes"
// )
// a = datetime.now()
// kwargs = {"nsfw": "true" if include_nsfw else "false"}
// try:
//     resp = await self._make_request(
//         endpoint="anime", 
//         optional_query={
//             "q": query, 
//             "fields":fields, 
//             "limit":"50", 
//             **kwargs
//     })
// except RuntimeError as e:
//     if fallback:
//         log.warning(f"Error while fetching anime - title len = {len(query)}")
//         log.warning(traceback.format_exc())
//         return None
//     else:
//         log.warning(f"fallback search for title {query}")
//         return await self.search_anime(query[:50], include_nsfw, True)
// log.info(f"fetched {len(resp['data'])} anime in {(datetime.now() - a).total_seconds():.2f}s")
// self.response_cache.ttl(query, deepcopy(resp), self.TTL)
// return deepcopy(resp)