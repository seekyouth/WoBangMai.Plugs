using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WoBangMaiSDK.Helpers;
using Newtonsoft.Json;
using DTcms.Model;

namespace WoBangMaiSDK.Modules.CMS
{
    public class NewsAPI
    {

        /// <summary>
        ///  获取新闻纪录
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static article Get(string access_token, int id)
        {
            var url = string.Format("https://wobangmai.cn/cgi-bin/token?access_token={0}&id={1}", "client_credential", access_token, id);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return null;
            var model = JsonConvert.DeserializeObject<article>(result.Content.ReadAsStringAsync().Result);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="title"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<article> GetList(string access_token, string title, int page, int size = 20)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?access_token={0}&id={1}&title={2}&page={3}&page={4}", "client_credential", access_token, title,page,size);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return null;
            var model = JsonConvert.DeserializeObject<List<article>>(result.Content.ReadAsStringAsync().Result);
            return model;
        }
    }
}
