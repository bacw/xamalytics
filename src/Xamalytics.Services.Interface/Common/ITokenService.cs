namespace Xamalytics.Services.Interface.Common
{
    public interface ITokenService
    {
        string CreateJwtSecurityToken(string id);
    }
}
