#WeChat.NET
**WeChat.NET**是微信公共平台 .NET SDK

##环境：
- .NET Framework 4.0;
- Newtonsoft.Json.dll;
- System.Net.Http.dll;

##目录说明：
- trunk  ---------主目录
-  bin   ---------引用的DLL和生成的DLL
-  src   ---------源代码
-   WeChat.NET            ---------主工程
-   WeChat.NET.Demo       ---------示例工程
-   WeChat.NET.UnitTest   ---------单元测试工程

##使用方法：
1. 下载trunk\bin\目录下所有DLL，并添加这些DLL的引用；
2. 或者下载源代码，编译代码后引用trunk\bin\目录下所有DLL;
2. 示例代码参考WeChat.NET.Demo项目;

##主要功能：
1. 主动向用户推送消息，由于微信官方API中不提供主动向用户推送消息API，所以该接口模拟登陆微信公众平台，使用实时消息功能发送单条信息，可循环实现群发功能；

##捐赠：
如果您觉得这个项目对您有帮助，非常感谢你的捐赠

[![捐赠WeChat.NET](https://img.alipay.com/sys/personalprod/style/mc/btn-index.png)](https://me.alipay.com/xiongpq)
