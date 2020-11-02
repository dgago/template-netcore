namespace Kit.Domain.Validation
{
    public enum ErrorType
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        UnprocessableEntity = 422,
        Locked = 423,
    }
}