namespace Hack.Web
{
    public interface IObjectResponse<T>
    {
        T Value { get; }
    }
}