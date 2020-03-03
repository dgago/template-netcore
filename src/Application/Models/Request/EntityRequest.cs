using Application.Models.Result;

namespace Application.Models.Request
{
    public abstract class EntityRequest<T> : Request<EntityResult<T>>
        where T : class
    {
    }
}