Ҫʹ�ô˿��exe��Ŀ����nuget��װCefSharp.WinForms �� CefSharp.Common  ��������
Install-Package CefSharp.WinForms -Version 49.0.1
Install-Package CefSharp.Common -Version 49.0.1

ע�⣬�汾49.0.1Ϊ֧��XP�����һ���汾����.net����Ϊ4.0�������µİ汾������Ҫ4.5.2�Ļ���
=============================

app.manifest ��Ҫ����exe��ͬĿ¼

====================

��������ġ��������ԡ���exe��Ŀ���������ԣ�����Ҫ����Ϊx86

====================

exe��main���������˳��¼����ر������:

Application.ApplicationExit += Application_ApplicationExit;

private static void Application_ApplicationExit(object sender, EventArgs e)
{
        //�����˳�ʱ�ǵõ���shutdown
        Cef.Shutdown();
}

====================================
BrowserManager.Instance.Browser����������ؼ�����������������һ��pannel����:

BrowserManager.Instance.Browser.Dock = DockStyle.Fill;
BrowserManager.Instance.Browser.Load("https://www.baidu.com/");//��������ص���ַ
panel1.Controls.Add(BrowserManager.Instance.Browser);

====================================
.net��js����

��c#������֪ͨwebҳ��ִ��js���룺
BrowserManager.Instance.Browser.ExecuteScriptAsync("var a=1;b=2;alert(a+b);console.log(document.location.href);document.getElementById('myBtn');");

��webҳ�����c#����:
InteractiveManager���Ѿ���ע�ᵽ��web��js�����У���ֵ���˱���im

��web����ֱ�ӵ��� im.xxx���ɵ��õ�InteractiveManager���Ӧ�ĺ�����ע�⣬js�е���ʱ����������ĸ��Сд����:
<script>
$("#myBtn").click(function(){ im.vehicleForm();});
</script>
�����ⶼ���첽���ã�Ҫȡ���ؽ���Ļ�����Ҫ����һ���ص�������.then�У��磺
<script>
$("#myBtn").click(function(){ 
	im.getHphm().then(function(ret){ alert(ret); }); //�����ret����getHphm�ķ���ֵ
});
</script>

ע�⣺im��������ҳ�������ɺ��ע��ģ��������ҳ��ռ������ֱ�ӵ���im������ʱim��δ����ֵ������Ҫ�������ã�
<script>
(async () =>{
	await CefSharp.BindObjectAsync('im');//�ȴ�imע�����
	//���������֮��������þ���im��
	im.vehicleForm();
})();
</script>

��InteractiveManager����ӵ�public�����Ժͺ�����������js�б����ã����ֶβ����ԡ�