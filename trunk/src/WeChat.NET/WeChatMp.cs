using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Security;
using WeChat.NET.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WeChat.NET
{
	/// <summary>
	/// 微信公众平台
	/// </summary>
	public class WeChatMp
	{
		#region 字段
		private static readonly string Host = "https://mp.weixin.qq.com";
		private static readonly string LoginUrl = @"https://mp.weixin.qq.com/cgi-bin/login?lang=zh_CN";
		private static readonly string SingleSendUrl = @"https://mp.weixin.qq.com/cgi-bin/singlesend?t=ajax-response&lang=zh_CN";

		private CookieContainer _cookieContainer = new CookieContainer();
		private bool _isLogin = false;
		private string _token = string.Empty;
		#endregion

		#region 登录
		/// <summary>
		/// 登录 用户名、密码在App.config中配置
		/// </summary>
		/// <returns></returns>
		public CResult<bool> Login()
		{
			var httpClient = GetHttpClient();
			httpClient.DefaultRequestHeaders.Add("Referer", Host);

			var encryptPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(SystemInfo.MpPwd, "MD5").ToLower();
			var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"username",SystemInfo.MpUserName},
				{"pwd",encryptPwd},
				{"imgcode",""},
				{"f","json"},
			});
			var response = httpClient.PostAsync(LoginUrl, httpContent).Result;
			var strResult = response.Content.ReadAsStringAsync().Result;

			var weiXinResult = JsonConvert.DeserializeObject<WResult>(strResult);
			if (weiXinResult.ErrCode == "0") {
				_isLogin = true;
				_token = GetToken(weiXinResult.ErrMsg);

				return new CResult<bool>(true);
			}

			return new CResult<bool>(false, ErrorCode.LoginFailed);
		}
		#endregion

		#region 发送消息
		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="toFakeIDs">用户ID列表，此ID可通过GetAllContact方法获取</param>
		/// <param name="content">消息内容</param>
		/// <returns></returns>
		public CResult<SendMessageResult> SendMessage(IEnumerable<string> toFakeIDs, string content)
		{
			if (string.IsNullOrWhiteSpace(content)) {
				return new CResult<SendMessageResult>(null, ErrorCode.ParameterError);
			}

			if (!_isLogin) {
				var loginResult = Login();
				if (loginResult.Code != 0) {
					return new CResult<SendMessageResult>(null, loginResult.Code, loginResult.Msg);
				}
			}

			var httpClient = GetHttpClient();
			var result = new SendMessageResult();
			foreach (var toFakeID in toFakeIDs) {
				if (httpClient.DefaultRequestHeaders.Referrer == null) {
					var refererUrl = string.Format(@"https://mp.weixin.qq.com/cgi-bin/singlemsgpage?fromfakeid={0}&t=wxm-singlechat&token={1}&lang=zh_CN", toFakeID, _token);
					httpClient.DefaultRequestHeaders.Add("Referer", refererUrl);
				}
				var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>{
					{"type","1"},
					{"content",content},
					{"error","false"},
					{"imgcode",""},
					{"token",_token},
					{"tofakeid",toFakeID},
					{"ajax","1"},
				});

				var strResult = httpClient.PostAsync(SingleSendUrl, httpContent).Result.Content.ReadAsStringAsync().Result;
				var weiXinResult = JsonConvert.DeserializeObject<WResult>(strResult);
				if (weiXinResult.Ret == "0") {
					result.SucceedList.Add(toFakeID);
				} else {
					result.FailList.Add(toFakeID);
				}
			}

			return new CResult<SendMessageResult>(result);
		}
		#endregion

		#region 获取用户
		/// <summary>
		/// 获取用户，从用户管理页面
		/// </summary>
		/// <param name="groupid">分组ID，0为未分组，分组ID请在用户管理URL的groupid中获取</param>
		/// <param name="pageSize">分页大小</param>
		/// <returns></returns>
		public CResult<List<WContact>> GetAllContact(string groupid = "0", int pageSize = 200)
		{
			if (!_isLogin) {
				var loginResult = Login();
				if (loginResult.Code != 0) {
					return new CResult<List<WContact>>(new List<WContact>(), loginResult.Code, loginResult.Msg);
				}
			}

			var pageIdx = 0;
			var result = new List<WContact>();
			var httpClient = GetHttpClient();
			while (true) {
				var contactManageUrl = string.Format("https://mp.weixin.qq.com/cgi-bin/contactmanage?t=user/index&pagesize={2}&pageidx={3}&type=0&groupid={0}&token={1}&lang=zh_CN", groupid, _token, pageSize, pageIdx);
				var strResult = httpClient.GetStringAsync(contactManageUrl).Result;

				var match = Regex.Match(strResult, @"friendsList : \({.*:\[(?<contacts>.*?)\]}\).contacts").Groups["contacts"];
				if (match.Success && !string.IsNullOrWhiteSpace(match.Value)) {
					result.AddRange(JsonConvert.DeserializeObject<List<WContact>>(string.Format("[{0}]", match.Value)));
					pageIdx += 1;
				} else {
					break;
				}
			}

			return new CResult<List<WContact>>(result);
		}
		#endregion

		#region 私有方法
		private HttpClient GetHttpClient()
		{
			var httpClient = new HttpClient(new HttpClientHandler { CookieContainer = _cookieContainer });
			httpClient.DefaultRequestHeaders.Add("User-Agent",
				"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1626.1 Safari/537.36");
			return httpClient;
		}

		private string GetToken(string errMsg)
		{
			var match = Regex.Match(errMsg, @"\?.*?token=(?<token>.*?)(&.*)?$").Groups["token"];
			if (match.Success) {
				return match.Value;
			}
			return string.Empty;
		}
		#endregion
	}
}
