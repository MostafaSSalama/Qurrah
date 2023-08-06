namespace Qurrah.Business
{
    public enum ActionResult : int
    {
        Success,
        ResourceNotFound,
        Empty,
        InternalServerError,
        BadRequest,
        GeneralFailure
    }
}