namespace LabirentFethiye.Common.Responses
{
    public class PageInfo
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public int TotalRow { get; set; }
        public int TotalPage { get; set; }
        public bool NextPage { get; set; }
        public bool PrevPage { get; set; }
    }
}
