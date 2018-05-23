﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser.Plug
{
    public static class QueryExtensions
    {
        public static string Where<TSource>(this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            var expression = Expression.Call(null, ((MethodInfo)MethodBase.GetCurrentMethod())
            .MakeGenericMethod(new Type[] { typeof(TSource) }),
            new Expression[] { source.Expression, Expression.Quote(predicate) });

            var translator = new QueryTranslator();
            return translator.Translate(expression);
        }
    }
}
