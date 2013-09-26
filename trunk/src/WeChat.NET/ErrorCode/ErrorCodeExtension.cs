using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WeChat.NET
{
	internal static class ErrorCodeExtension
	{
		static Dictionary<ErrorCode, ErrorCodeDescriptionAttribute> _descriptionMap = new Dictionary<ErrorCode, ErrorCodeDescriptionAttribute>();

		public static ErrorCodeDescriptionAttribute GetErrorCodeDescription(ErrorCode errorCode)
		{
			if (_descriptionMap.ContainsKey(errorCode)) {
				return _descriptionMap[errorCode];
			}

			FieldInfo provider = errorCode.GetType().GetField(errorCode.ToString());
			ErrorCodeDescriptionAttribute[] attributes = (ErrorCodeDescriptionAttribute[])provider.GetCustomAttributes(typeof(ErrorCodeDescriptionAttribute), false);

			ErrorCodeDescriptionAttribute errorCodeDescription = attributes[0];

			_descriptionMap[errorCode] = errorCodeDescription;

			return errorCodeDescription;
		}
	}
}
