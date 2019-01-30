要使用此库的exe项目先用nuget安装CefSharp.WinForms 和 CefSharp.Common  两个包：
Install-Package CefSharp.WinForms -Version 49.0.1
Install-Package CefSharp.Common -Version 49.0.1

注意，版本49.0.1为支持XP的最后一个版本，且.net环境为4.0，而较新的版本至少需要4.5.2的环境
=============================

app.manifest 需要放在exe的同目录

====================

解决方案的“配置属性”和exe项目的生成属性，均需要设置为x86

====================

exe的main函数里，添加退出事件来关闭浏览器:

Application.ApplicationExit += Application_ApplicationExit;

private static void Application_ApplicationExit(object sender, EventArgs e)
{
        //程序退出时记得调用shutdown
        Cef.Shutdown();
}

====================================
BrowserManager.Instance.Browser就是浏览器控件，这样添加浏览器到一个pannel里面:

BrowserManager.Instance.Browser.Dock = DockStyle.Fill;
BrowserManager.Instance.Browser.Load("https://www.baidu.com/");//浏览器加载的网址
panel1.Controls.Add(BrowserManager.Instance.Browser);

====================================
.net和js交互

在c#代码中通知web页面执行js代码：
BrowserManager.Instance.Browser.ExecuteScriptAsync("var a=1;b=2;alert(a+b);console.log(document.location.href);document.getElementById('myBtn');");

在web页面调用c#代码:
InteractiveManager类已经被注册到了web的js环境中，赋值给了变量im

在web环境直接调用 im.xxx即可调用到InteractiveManager类对应的函数（注意，js中调用时函数名首字母变小写）如:
<script>
$("#myBtn").click(function(){ im.vehicleForm();});
</script>
由于这都是异步调用，要取返回结果的话，需要传入一个回调函数到.then中，如：
<script>
$("#myBtn").click(function(){ 
	im.getHphm().then(function(ret){ alert(ret); }); //这里的ret就是getHphm的返回值
});
</script>

注意：im对象是在页面加载完成后才注入的，如果你在页面刚加载完就直接调用im可能这时im还未被赋值，则需要这样调用：
<script>
(async () =>{
	await CefSharp.BindObjectAsync('im');//等待im注册完成
	//上面句完了之后这里调用就有im了
	im.vehicleForm();
})();
</script>

在InteractiveManager中添加的public的属性和函数都可以在js中被调用，但字段不可以。