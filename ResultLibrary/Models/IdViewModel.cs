namespace ResultLibrary.Models;

public class IdViewModel<T>
{
    public IdViewModel(T id)
    {
        Id = id;
    }

    public T Id { get; set; }
}