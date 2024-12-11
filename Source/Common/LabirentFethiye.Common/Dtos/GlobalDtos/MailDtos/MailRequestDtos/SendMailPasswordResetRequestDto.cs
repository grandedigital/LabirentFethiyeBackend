namespace LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailRequestDtos
{
    public class SendMailPasswordResetRequestDto
    {
        public string To { get; set; }
        public string UserId { get; set; }
        public string ResetToken { get; set; }
    }
}
