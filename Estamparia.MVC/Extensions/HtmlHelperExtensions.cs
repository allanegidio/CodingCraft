using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Estamparia.MVC.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString FoundationValidationMessageFor<TModel, TProperty>
            (this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            // Check if an error exists, otherwise return empty.
            if (helper.ValidationMessageFor(expression) != null)
            {
                TagBuilder span = new TagBuilder("span");
                
                span.AddCssClass("form-error is-visible");

                span.InnerHtml = helper.ValidationMessageFor(expression).ToHtmlString();

                return MvcHtmlString.Create(span.ToString());
            }

            return MvcHtmlString.Create(String.Empty);
        }
    }
}