using System.Configuration;

namespace WeChat.NET
{
	public class SystemInfo
	{
		/// <summary>
		/// 微信公众平台用户名
		/// </summary>
		public static string MpUserName { get { return ConfigurationManager.AppSettings["MpUserName"]; } }

		/// <summary>
		/// 微信公众平台密码
		/// </summary>
		public static string MpPwd { get { return ConfigurationManager.AppSettings["MpPwd"]; } }
	}
}
