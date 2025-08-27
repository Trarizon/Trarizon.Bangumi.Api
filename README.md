# Trarizon.Bangumi

基于C#的[Bangumi API](https://github.com/bangumi/api)实现。

由于[公开API](https://github.com/bangumi/api)未稳定，本库主要以[bgm.tv后端源码](https://github.com/bangumi/server)为参考

## Project

## How to Use

完整的API参考请参阅[Examples.cs](./tests/Trarizon.Bangumi.Run/Examples.cs)

``` C#
using Trarizon.Bangumi.Api;
using Trarizon.Bangumi.Routes; // API直接实现的方法位置

var client = new BangumiClient(UserAgent, AccessToken);

try {
    Subject subject = await client.GetSubjectAsync(200763);
    Console.WriteLine(subject.Name)
}
catch (BangumiApiException ex) {
    Console.WriteLine(ex.Error);
}
```