// See https://aka.ms/new-console-template for more information
// TODO:
// CalendarSubject 同时有Episode和EpisodeCount值
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Exceptions;
using Trarizon.Bangumi.Api.Requests.Payloads;
using Trarizon.Bangumi.Api.Routes;
using Trarizon.Bangumi.Api.Toolkit;

#pragma warning disable BgmExprApi // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable CS0618 // 类型或成员已过时

Console.WriteLine("Hello, World!");

const string UserAgent = "Trarizon/Traizon.Bangumi";

var AccessToken = File.ReadAllText("access_token.priv");

var client = new BangumiHttpClient(UserAgent, AccessToken);

//var subjects = client.SearchSubjects(new SearchSubjectsRequestBody { Keyword = "异国日记" }, 5);

var eps= client.GetUserSubjectEpisodeCollections(493016);

await foreach (var item in eps) {
    Console.WriteLine(item.Episode.Name);
}

//try {

//    var subject = await client.GetSubjectAsync(200763);
//    Console.WriteLine(subject.Name);
//}
//catch (BangumiApiException ex) {
//    Console.WriteLine(ex.HttpStatusCode);
//    Console.WriteLine(ex.Error.Title);
//    Console.WriteLine(ex.Error.Description);
//	throw;
//}



static class Ext
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source) {
            action(item);
        }
    }
}