namespace Trarizon.Bangumi.Api.Utilities;
internal interface IConstructable<T>
{
    static abstract T Construct();
}
