using System.Collections.Generic;

namespace WeChat.NET.Model
{
	public class SendMessageResult
	{
		private List<string> _succeedList = new List<string>();
		private List<string> _failList = new List<string>();

		public List<string> SucceedList { get { return _succeedList; } set { _succeedList = value; } }
		public List<string> FailList { get { return _failList; } set { _failList = value; } }
	}
}
