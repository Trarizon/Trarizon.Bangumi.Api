// See https://aka.ms/new-console-template for more information
// TODO:
// CalendarSubject 同时有Episode和EpisodeCount值
using System.Text.Json;
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Api.Http.Requests;
using Trarizon.Bangumi.Api.Http.Responses;
using Trarizon.Bangumi.Api.Models;
using Trarizon.Bangumi.Api.Models.EpisodeModels;
using Trarizon.Bangumi.Api.Models.SubjectModels;
using Trarizon.Bangumi.Api.Serialization;

#pragma warning disable BgmExprApi // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable CS0618 // 类型或成员已过时

Console.WriteLine("Hello, World!");

const string UserAgent = "Trarizon/Traizon.Bangumi";

var accessToken = File.ReadAllText("access_token.priv");

var client = new BangumiClient(UserAgent, accessToken);

var res = await client.RemoveSubjectFromIndexAsync(79713, 393037);

Console.WriteLine(res);