namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientCommentCreateRequestDto
    {
        public string? Title { get; set; }
        public string CommentText { get; set; }
        public decimal Rating { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public Guid? VillaId { get; set; }
        public Guid? HotelId { get; set; }
    }
}
