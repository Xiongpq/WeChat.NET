using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChat.NET
{
	/// <summary>
	/// ErrorCode的错误描述
	/// </summary>
	public class ErrorCodeDescriptionAttribute : Attribute
	{
		public string ErrorCodeDescription { get; set; }

		public ErrorCodeDescriptionAttribute(string errorCodeDescription)
		{
			this.ErrorCodeDescription = errorCodeDescription;
		}
	}
}
