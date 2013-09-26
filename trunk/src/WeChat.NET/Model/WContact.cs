using Newtonsoft.Json;
namespace WeChat.NET.Model
{
	public class WContact
	{
		[JsonProperty(PropertyName = "id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "nick_name")]
		public string NickName { get; set; }

		[JsonProperty(PropertyName = "remark_name")]
		public string RemarkName { get; set; }

		[JsonProperty(PropertyName = "group_id")]
		public string GroupID { get; set; }
	}
}
