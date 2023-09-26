namespace AirportTicketBookingTry.ResultHandler
{
    public class OperationResult
    {
        public bool IsSuccess {  get; private set; }
        public string Message  { get; private set; }

        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public static OperationResult SuccessResult(string msg)
        {
            return new OperationResult(true, msg);
        }
        public static OperationResult FailureResult(string msg)
        {
            return new OperationResult(false, msg);
        }

    }
}
