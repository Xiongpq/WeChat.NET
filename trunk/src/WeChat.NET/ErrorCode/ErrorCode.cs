using System;

namespace WeChat.NET
{
	/// <summary>
	/// 记录所有Web服务的错误信息
	/// 示例：10123
	/// 1系统错误；2为服务级错误
	/// 01服务模块代码
	///		01 	
	/// 23具体错误代码
	/// </summary>
	public enum ErrorCode
	{
		/// <summary>
		/// 未知错误
		/// </summary>
		[ErrorCodeDescription("未知错误")]
		UnknownError = 1,

		/// <summary>
		/// 错误:{0}
		/// </summary>
		[ErrorCodeDescription("错误:{0}")]
		CustomErrorWithParam = 2,

		/// <summary>
		/// 系统错误
		/// </summary>
		[ErrorCodeDescription("系统错误")]
		SystemError = 10001,

		/// <summary>
		/// 参数错误
		/// </summary>
		[ErrorCodeDescription("参数错误")]
		ParameterError = 20001,

		/// <summary>
		/// 登录失败
		/// </summary>
		[ErrorCodeDescription("登录失败")]
		LoginFailed = 20002,
	}
}
