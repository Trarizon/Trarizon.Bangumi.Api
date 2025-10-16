// See https://aka.ms/new-console-template for more information
// TODO:
// CalendarSubject 同时有Episode和EpisodeCount值
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Exceptions;
using Trarizon.Bangumi.Api.Routes;

#pragma warning disable BgmExprApi // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable CS0618 // 类型或成员已过时

Console.WriteLine("Hello, World!");

const string UserAgent = "Trarizon/Traizon.Bangumi";

var AccessToken = File.ReadAllText("access_token.priv");

var client = new BangumiHttpClient(UserAgent, AccessToken);

try {
    var subject = await client.GetSubjectAsync(200763);
    Console.WriteLine(subject.Name);
}
catch (BangumiApiException ex) {
    Console.WriteLine(ex.HttpStatusCode);
    Console.WriteLine(ex.Error.Title);
    Console.WriteLine(ex.Error.Description);
	throw;
}