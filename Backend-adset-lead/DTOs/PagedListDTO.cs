namespace Backend_adset_lead.DTOs
{
    public class PagedListDTO<T>
    {
        public List<T>? Items { get; set; }
        public int TotalPages { get; set; }
    }
}
