namespace Identity.Domain;

public class HashSettings
{
    public int Iterations { get; set; }
    public int MemorySize { get; set; }
    public int DegreeOfParallelism { get; set; }
    public int HashLength { get; set; }
}