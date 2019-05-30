namespace Hack.Service
{
    public class SuccessResponse
    {
        public bool IsSuccess => FailureMessage is null;
        public string FailureMessage { get; set; }
    }
}