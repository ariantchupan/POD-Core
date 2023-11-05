using System;
using Kavenegar.Core.Utils;

namespace Middlewares.Application.Model.Kavenegar
{
 public class AccountInfoResult
 {
	public long RemainCredit { get; set; }
	public long Expiredate { get; set; }
	public DateTime GregorianExpiredate
	{
	 get { return DateHelper.UnixTimestampToDateTime(Expiredate); }
	}
	public string Type { get; set; }
 }
}