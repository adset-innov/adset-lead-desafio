namespace Backend_adset_lead.DTOs
{
    public class PagedListDTO<T>
    {
        public List<T>? Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalCarros { get; set; }
        public int TotalCarrosComFotos { get; set; }
        public int TotalCarrosSemFotos { get; set; }
    }
}
