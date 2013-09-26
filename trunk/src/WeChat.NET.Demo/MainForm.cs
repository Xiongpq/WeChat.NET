using System.Windows.Forms;
using System.Linq;

namespace WeChat.NET.Demo
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			var weChatMp = new WeChatMp();
			weChatMp.Login();
			var allFakeIDs = weChatMp.GetAllContact("100").Data.Select(p => p.ID);
			weChatMp.SendMessage(allFakeIDs, "哈哈哈，太好了~ From WeChat.NET By Xiongpq");
		}
	}
}
