#pragma checksum "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "893bddb56c37da6029890572413281f00a2cca94"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Blog__ContentPage), @"mvc.1.0.view", @"/Areas/Admin/Views/Blog/_ContentPage.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\_ViewImports.cshtml"
using BomoBana;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\_ViewImports.cshtml"
using BomoBana.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\_ViewImports.cshtml"
using BomoBana.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml"
using Common;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"893bddb56c37da6029890572413281f00a2cca94", @"/Areas/Admin/Views/Blog/_ContentPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"270f508fdfb23f1ce87b694860f0ec721e2a109d", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Blog__ContentPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BomoBana.Areas.Admin.DetailBlogDto>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div id=""DetailPageLoad"" class=""modal fade in"" tabindex=""-1""
     role=""dialog"" aria-labelledby=""myModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog"" style=""max-width: 650px!important;"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""myModalLabel"">جزئیات مطلب</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">×</button>
            </div>
            <div class=""modal-body"">
                <div class=""card-body"">
                    <div class="" row"">
                        <div class="" col-md-8"">
                            <h4 class=""card-title"">");
#nullable restore
#line 15 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml"
                                              Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                            <h6 class=\"card-subtitle mb-2 text-muted\" style=\"margin: 15px auto;text-align: justify;\">");
#nullable restore
#line 16 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml"
                                                                                                                Write(Model.Caption);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                        </div>\r\n                        <div class=\" col-md-4\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 1025, "\"", 1109, 1);
#nullable restore
#line 19 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml"
WriteAttributeValue("", 1031, Url.Content(string.Format(ApplicationPathes.Blog.VirtualFolder, Model.Image)), 1031, 78, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1110, "\"", 1128, 1);
#nullable restore
#line 19 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml"
WriteAttributeValue("", 1116, Model.Title, 1116, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"100%\" />\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"card-text\" style=\"margin-top: 20px;text-align: justify;\">\r\n                        ");
#nullable restore
#line 23 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Blog\_ContentPage.cshtml"
                   Write(Html.Raw(Model.Content));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-default waves-effect"" data-dismiss=""modal"">انصراف</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>


");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BomoBana.Areas.Admin.DetailBlogDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
