namespace Middlewares.Application.Model.Kavenegar
{
 public class CountOutboxResult : CountInboxResult
 {
	public long SumPart { get; set; }
	public long Cost { get; set; }
 }
}