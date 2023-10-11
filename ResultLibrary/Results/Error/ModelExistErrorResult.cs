using System.Net;

namespace ResultLibrary.Results.Error;

public class ModelExistErrorResult : ErrorResult
{
    public string Description { get; set; }
    
    public ModelExistErrorResult(string description)
        : base("Conflict.", (int)HttpStatusCode.Conflict)
    {
        Description = description;
    }
}