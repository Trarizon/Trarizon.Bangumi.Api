# Trarizon.Bangumi

基于C#的[Bangumi API](https://github.com/bangumi/api)实现。

由于[公开API](https://github.com/bangumi/api)未稳定，本库主要以[bgm.tv后端源码](https://github.com/bangumi/server)为参考，且本库内容可能会随API变化（以及我看懂了更多源码）而发生破坏性变更

## Project

Project|Summary
:--|:--
Trarizon.Bangumi.Api|[公开API](https://github.com/bangumi/api)的直接实现
Trarizon.Bangumi|扩展了`Trarizon.Bangumi.Api`的功能。

## How to Use

完整的API参考请参阅[Examples.cs](./tests/Trarizon.Bangumi.Run/Examples.cs)

``` C#
usint Trarizon.Bangumi.Api;

var client = new BangumiClient(UserAgent, AccessToken);

// API对应方法返回值为Task<BangumiApiResult<T>>，使用Unwrap()方法可以直接返回Task<T>，并将错误结果转为异常抛出
var subject = await client.GetSubjectAsync(200763).Unwrap();

Console.WriteLine(subject.Name)
```