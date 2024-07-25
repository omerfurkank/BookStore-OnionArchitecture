using Newtonsoft.Json;

namespace Application.Exceptions;

public class ExceptionModel
{
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
