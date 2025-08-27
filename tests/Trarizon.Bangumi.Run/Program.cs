// See https://aka.ms/new-console-template for more information
// TODO:
// CalendarSubject 同时有Episode和EpisodeCount值
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Exceptions;
using Trarizon.Bangumi.Api.Models.PersonModels;
using Trarizon.Bangumi.Api.Models.UserModels;
using Trarizon.Bangumi.Api.Routes;

#pragma warning disable BgmExprApi // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable CS0618 // 类型或成员已过时

Console.WriteLine("Hello, World!");

const string UserAgent = "Trarizon/Traizon.Bangumi";

var accessToken = File.ReadAllText("access_token.priv");

var client = new BangumiClient(UserAgent, accessToken);

try {
    var me = await client.GetSelfAsync();

    var col= await client.GetPagedUserSubjectCollectionsAsync(me.UserName,
        collectionType: SubjectCollectionType.Doing);

    col.Datas.Select(s =>
    {
        Console.WriteLine(s.Subject.Name);
        return default(ValueTuple);
    }).ToList();
}
catch (BangumiApiException e) {
    Console.WriteLine("Exception");
    Console.WriteLine(e);
}
