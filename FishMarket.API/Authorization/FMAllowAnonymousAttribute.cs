namespace FishMarket.API.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FMAllowAnonymousAttribute : Attribute
    {
    }
}
