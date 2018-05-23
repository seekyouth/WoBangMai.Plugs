using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using ExpressionParser.Plug.Modules;
using ExpressionParser.Plug;
using System.Reflection;
using System.ComponentModel;


namespace ExpresssionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Expression<Func<User, bool>> expressionUser = u => u.Age > 2 & u.Name == "王大力";
            //// Console.WriteLine(new QueryTranslator().Translate(expressionUser));
            //var Name = "王大力";
            //Console.WriteLine(Get(m => m.Name == Name));
            List<QueryItem> list = new List<QueryItem>();
            Em_AS_Condition ff = QueryItem.GetEnum("aa");
            list.Add(new QueryItem { PropName = "Name", Condition =ff, Keyword = "王大力" });
            list.Add(new QueryItem { PropName = "Age", Condition = Em_AS_Condition.GtEqual, Keyword = "17" });
            list.Add(new QueryItem { PropName = "Birthday", Condition = Em_AS_Condition.GtEqual, Keyword = "2010-12-5" });
            list.Add(new QueryItem { PropName = "Birthday", Condition = Em_AS_Condition.LtEqual, Keyword = "2015-12-6" });
            list.Add(new QueryItem { PropName = "IsOpen", Condition = Em_AS_Condition.Equal, Keyword = "true" });
            var  aa= JsonConvert.SerializeObject(list);
            list = JsonConvert.DeserializeObject<List<QueryItem>>(aa);
            Expression<Func<User, bool>> expreUser = ExpressionExt.ConvertToExpression<User>(list);
            var cc = new QueryTranslator().Translate(expreUser);
            Console.WriteLine(cc);
            Console.ReadLine();
        }


      

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string Get(Expression<Func<User, bool>> express)
        {
            return new QueryTranslator().Translate(express);
        }

    }
}
