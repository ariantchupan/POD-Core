using Kavenegar.Core.Models.Enums;
namespace Middlewares.Application.Model.Kavenegar
{
 public class StatusResult
 {
	public long Messageid { get; set; }
	public MessageStatus Status { get; set; }
	public string Statustext { get; set; }
 }
}