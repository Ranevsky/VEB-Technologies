namespace ResultLibrary.Models.CatalogInfo;

public class PageInfo
{
    public long Limit { get; set; }
    public long Items { get; set; }
    public long CurrentPage { get; set; }
    public long LastPage { get; set; }
}