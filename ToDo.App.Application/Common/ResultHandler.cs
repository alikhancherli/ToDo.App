namespace ToDo.App.Application.Common;

public class ResultHandler<TResult>
{
    private readonly TResult _result;
    private string _message;

    public TResult Result
    {
        get { return _result; }
    }

    public string Message
    {
        get { return _message; }
    }

    public ResultHandler()
    {

    }

    public ResultHandler(TResult result)
    {
        _result = result;
    }

    public void WithMessage(string message) =>
        _message = message;

}