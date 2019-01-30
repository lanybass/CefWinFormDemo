using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YwCEF
{
    public class BrowserManager
    {
        public CefSharp.WinForms.ChromiumWebBrowser Browser = null;

        InteractiveManager im = new InteractiveManager();

        public static readonly BrowserManager Instance = new BrowserManager();
        private BrowserManager()
        {
            InitCef();
            Browser = new CefSharp.WinForms.ChromiumWebBrowser("about:blank")
            {
                BrowserSettings =
                {
                    DefaultEncoding = "UTF-8",
                    WebGl = CefState.Disabled
                }
            };

            //Browser.JavascriptObjectRepository.Register("im", im, true, null);//新版本cef
            Browser.RegisterAsyncJsObject("im", im); //老版本的cef使用
            Browser.FrameLoadEnd += OnFrameLoadEnd;
            Browser.MenuHandler = new MenuHandler();
        }


        private static void InitCef()
        {
            //var settings = new CefSharp.WinForms.CefSettings();//新版本cef
            var settings = new CefSharp.CefSettings();//老版本的cef使用
            //settings.CachePath = "cache";
            settings.CefCommandLineArgs.Add("enable-media-stream", "0");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2661.102 Safari/537.36";//老版本无默认，自己加
            settings.UserAgent = "Yunwang-" + settings.UserAgent;
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            Cef.Initialize(settings);
        }


        private void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                Browser.ExecuteScriptAsync(@"(async () =>{await CefSharp.BindObjectAsync('im');})();");
                
                //以下为测试代码,正式使用时请注释掉
                Browser.ExecuteScriptAsync(@"
                    var testFunc = function(){im.getHphm().then(function(ret){console.log(ret); im.vehicleForm(ret);});}
                    setTimeout('testFunc()',3000);
                ");
            }
        }

    }
}
