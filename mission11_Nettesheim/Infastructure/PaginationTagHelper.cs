using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using mission11_Nettesheim.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace mission11_Nettesheim.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]

    public class PaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory ?? throw new ArgumentNullException(nameof(urlHelperFactory));
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = default!;

        public string PageAction { get; set; } = default!;

        public PaginationInfo PageModel { get; set; } = default!;

        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } = String.Empty;
        public string PageClassNormal { get; set; } = String.Empty;
        public string PageClassSelected { get; set; } = String.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");

                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }

                    tag.InnerHtml.Append(i.ToString());

                    result.InnerHtml.AppendHtml(tag);
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
