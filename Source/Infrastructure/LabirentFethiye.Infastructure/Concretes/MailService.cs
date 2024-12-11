﻿using LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.MailDtos.MailResponseDtos;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Infastructure.Abstract.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LabirentFethiye.Infastructure.Concretes
{
    public class MailService : IMailService
    {
        private readonly IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ResponseDto<SendMailResponseDto>> SendMailAsync(SendMailRequestDto model)
        {
            try
            {
                StringBuilder mail = new();
                mail.AppendLine(model.Body);

                string result = await SendMailSmtpAsync(new[] { model.To }, model.Subject, mail.ToString());

                if (result != "ok")
                    return ResponseDto<SendMailResponseDto>.Fail(new() { new() { Title = "SendMailAsync", Description = result } }, 400);



                SendMailResponseDto response = new() { Success = true };
                return ResponseDto<SendMailResponseDto>.Success(response, 200);
            }
            catch (Exception ex) { return ResponseDto<SendMailResponseDto>.Fail(new() { new() { Title = "SendMailAsync", Description = ex.Message.ToString() } }, 500); }
        }
        public async Task<ResponseDto<SendMailResponseDto>> SendMailAsync(MultiSendMailRequestDto model)
        {
            try
            {
                SendMailResponseDto response = new();
                return ResponseDto<SendMailResponseDto>.Success(response, 200);
            }
            catch (Exception ex) { return ResponseDto<SendMailResponseDto>.Fail(new() { new() { Title = "SendMailAsync", Description = ex.Message.ToString() } }, 500); }
        }
        public async Task<ResponseDto<SendMailResponseDto>> SendPasswordResetMailAsync(SendMailPasswordResetRequestDto model)
        {
            try
            {
                SendMailResponseDto response = new();
                return ResponseDto<SendMailResponseDto>.Success(response, 200);
            }
            catch (Exception ex) { return ResponseDto<SendMailResponseDto>.Fail(new() { new() { Title = "SendPasswordResetMailAsync", Description = ex.Message.ToString() } }, 500); }
        }
        public static string MailStyle(string className, string styleName, string styleProp)
        {
            string geridonenveri = "<style type=\"text/css\">";
            geridonenveri += className + "{" + styleName + ": " + styleProp + ";\n}";
            return geridonenveri + "</style>";
        }
        public async Task<string> SendMailSmtpAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(configuration["EmailSettings:Username"].ToString(), configuration["AppSettings:ApplicationName"].ToString(), Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                foreach (var to in tos)
                    mail.To.Add(to);

                string shtml = "";
                shtml += MailStyle("body", "font-size", "12px");
                shtml += MailStyle("table", "font-size", "12px");
                shtml += MailStyle("a", "color", "#1567ba");
                shtml += MailStyle("a:hover", "color", "#87c2ff");
                mail.Body = shtml + body;


                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(configuration["EmailSettings:Username"].ToString(), configuration["EmailSettings:Password"].ToString());
                smtp.Port = int.Parse(configuration["EmailSettings:Port"]);
                smtp.Host = configuration["EmailSettings:SmtpServer"].ToString();
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
                return "ok";
            }
            catch (Exception ex) { return ex.Message.ToString(); }
        }


        public async Task<ResponseDto<SendMailResponseDto>> ReservationCreateSendMailAsync(ReservationCreateMailRequestDto model)
        {
            try
            {
                StringBuilder mail = new();

                mail.AppendLine("<div style=\"width: 100%;\">");
                #region Genel Bilgiler
                mail.AppendLine("<div style=\"width: 90%; height: 80px; justify-content: space-between; display: flex;align-items: center; margin: auto;\">");
                #region Logo
                mail.AppendLine("<div>");
                mail.AppendLine("<img width=\"166\" height=\"41\" src=\"data:image/jpg;base64,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\" />");
                mail.AppendLine("</div>");
                #endregion
                #region Firma Bilgileri
                //mail.AppendLine("<div style=\"font-size: 15px; text-align: end; line-height: 6px; color: #424242;\">");
                //mail.AppendLine("<p>Ölüdeniz Mahallesi Atatürk Caddesi No :83/5</p>");
                //mail.AppendLine("<p>Fethiye / Muğla</p>");
                //mail.AppendLine("<p>0252 616 66 48</p>");
                //mail.AppendLine("<a style=\"text-decoration: none; color: #424242;\" href=\"www.labirentfethiye.com\">www.labirentfethiye.com</a>");
                //mail.AppendLine("</div>");
                //mail.AppendLine("</div>");
                #endregion
                mail.AppendLine("</div>");
                #endregion
                #region Rezervasyon Bilgileri
                mail.AppendLine("<div style=\"width: 90%; margin: auto;\">");
                mail.AppendLine("<h2 style=\"font-size: 17px; color: #424242;\">REZERVASYON BİLGİLERİ</h2>");

                #region Table
                mail.AppendLine("<table style=\"width: 100%; color: #424242; line-height: 30px; margin-top: 30px;\">");

                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">Rezervasyon Numarası</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{model.ReservationNumber}</ td > ");
                mail.AppendLine("</tr>");

                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">İsim Soyisim</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{model.ReservationInfo.Name} {model.ReservationInfo.Surname}</ td > ");
                mail.AppendLine("</tr>");

                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">Tc Kimlik Numarası</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{model.ReservationInfo.IdNo}</ td > ");
                mail.AppendLine("</tr>");

                if (model.ReservationInfo.Phone != null)
                {
                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Telefon Numarası</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine($"<td>{model.ReservationInfo.Phone}</ td > ");
                    mail.AppendLine("</tr>");
                }
                else
                {
                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Email</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine($"<td>{model.ReservationInfo.Email}</ td > ");
                    mail.AppendLine("</tr>");
                }

                if (model.Villa != null)
                {
                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Tesis Tipi</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine("<td>Villa</ td > ");
                    mail.AppendLine("</tr>");

                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Tesis Adı</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine($"<td>{model.Villa.Name}</ td > ");
                    mail.AppendLine("</tr>");

                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Kapasite</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine($"<td>{model.Villa.Person}</ td > ");
                    mail.AppendLine("</tr>");
                }
                else
                {
                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Tesis Tipi</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine("<td>Apart</ td > ");
                    mail.AppendLine("</tr>");

                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Tesis Adı</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine($"<td>{model.Room.HotelName} - {model.Room.Name}</ td > ");
                    mail.AppendLine("</tr>");

                    mail.AppendLine("<tr>");
                    mail.AppendLine("<td style=\"width: 30%;\">Kapasite</td>");
                    mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                    mail.AppendLine($"<td>{model.Room.Person}</ td > ");
                    mail.AppendLine("</tr>");
                }

                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">Giriş Tarihi</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{model.CheckIn.ToShortDateString()}</ td > ");
                mail.AppendLine("</tr>");

                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">Çıkış Tarihi</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{model.CheckOut.ToShortDateString()}</ td > ");
                mail.AppendLine("</tr>");

                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">Gece Sayısı</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{(model.CheckOut - model.CheckIn).Days}</ td > ");
                mail.AppendLine("</tr>");

                //mail.AppendLine("<tr>");
                //mail.AppendLine("<td style=\"width: 30%;\">Ekstra Ödeme</td>");
                //mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                //mail.AppendLine($"<td>{model.ExtraPrice}</ td > ");
                //mail.AppendLine("</tr>");



                mail.AppendLine("<tr>");
                mail.AppendLine("<td style=\"width: 30%;\">Toplam Ödeme</td>");
                mail.AppendLine("<td style=\"width: 5%;\">:</td>");
                mail.AppendLine($"<td>{model.Total.ToString("#,##0.00")} {model.PriceType.ToString()}</td> ");
                mail.AppendLine("</tr>");

                mail.AppendLine("</table>");
                #endregion

                mail.AppendLine("<h2 style=\"font-size: 17px; color: #424242; margin-top: 60px;\">ÖNEMLİ BİLGİLENDİRME</h2>");
                mail.AppendLine("<p>Girişlerde bir günlük kira bedeli hasar depozitosu alınır. Çıkışta yapılan hasar kontrollerinden sonra misafire iade edilir.</p>");
                mail.AppendLine("<p>Giriş saatlarimiz 16.00, çıkış saatlerimiz 10.00’dır.</p>");
                mail.AppendLine("<p>Alınan kaporalar geri iade edilmemektedir.</p>");
                mail.AppendLine("</div>");
                #endregion
                mail.AppendLine("</div> ");


                string result = await SendMailSmtpAsync(new[] { configuration["AppSettings:ReservationMail"].ToString() }, "Yeni Rezervasyon | Web Site", mail.ToString());

                if (result != "ok")
                    return ResponseDto<SendMailResponseDto>.Fail(new() { new() { Title = "SendMailAsync", Description = result } }, 400);



                SendMailResponseDto response = new() { Success = true };
                return ResponseDto<SendMailResponseDto>.Success(response, 200);
            }
            catch (Exception ex) { return ResponseDto<SendMailResponseDto>.Fail(new() { new() { Title = "SendMailAsync", Description = ex.Message.ToString() } }, 500); }
        }
    }
}
