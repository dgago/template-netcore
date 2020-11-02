using Kit.Application.Models.Responses;

namespace Kit.Application.Models.Requests
{
    public abstract class EntityRequest<T> : RequestBase<EntityResult<T>>
        where T : class
    {
        public EntityResult<T> GetResult(T item)
        {
            return new EntityResult<T>(Notifications, item);
        }
    }
}