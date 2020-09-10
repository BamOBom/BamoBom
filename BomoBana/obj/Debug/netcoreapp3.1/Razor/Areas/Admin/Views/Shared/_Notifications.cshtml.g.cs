#pragma checksum "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bc6711dbbaa7ebda9664304e56b10bcb4ad85a7e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Shared__Notifications), @"mvc.1.0.view", @"/Areas/Admin/Views/Shared/_Notifications.cshtml")]
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
#line 1 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
using EnumValue;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc6711dbbaa7ebda9664304e56b10bcb4ad85a7e", @"/Areas/Admin/Views/Shared/_Notifications.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"270f508fdfb23f1ce87b694860f0ec721e2a109d", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Shared__Notifications : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
  
    //success messages
    var successMessages = new List<string>();
    if (TempData[string.Format("rata.notifications.{0}", NotifyType.Success)] != null)
    {
        successMessages.AddRange(TempData[string.Format("rata.notifications.{0}", NotifyType.Success)] as IList<string>);
    }
    if (ViewData[string.Format("rata.notifications.{0}", NotifyType.Success)] != null)
    {
        successMessages.AddRange(ViewData[string.Format("rata.notifications.{0}", NotifyType.Success)] as IList<string>);
    }



    //error messages
    var errorMessages = new List<string>();
    if (TempData[string.Format("rata.notifications.{0}", NotifyType.Error)] != null)
    {
        errorMessages.AddRange(TempData[string.Format("rata.notifications.{0}", NotifyType.Error)] as IList<string>);
    }
    if (ViewData[string.Format("rata.notifications.{0}", NotifyType.Error)] != null)
    {
        errorMessages.AddRange(ViewData[string.Format("rata.notifications.{0}", NotifyType.Error)] as IList<string>);
    }

    var warningMessages = new List<string>();
    if (TempData[string.Format("rata.notifications.{0}", NotifyType.Warning)] != null)
    {
        warningMessages.AddRange(TempData[string.Format("rata.notifications.{0}", NotifyType.Warning)] as IList<string>);
    }
    if (ViewData[string.Format("rata.notifications.{0}", NotifyType.Warning)] != null)
    {
        warningMessages.AddRange(ViewData[string.Format("rata.notifications.{0}", NotifyType.Warning)] as IList<string>);
    }

    var infoMessages = new List<string>();
    if (TempData[string.Format("rata.notifications.{0}", NotifyType.Info)] != null)
    {
        infoMessages.AddRange(TempData[string.Format("rata.notifications.{0}", NotifyType.Info)] as IList<string>);
    }
    if (ViewData[string.Format("rata.notifications.{0}", NotifyType.Info)] != null)
    {
        infoMessages.AddRange(ViewData[string.Format("rata.notifications.{0}", NotifyType.Info)] as IList<string>);
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 48 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
 foreach (var message in successMessages)
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        Notification_success(\'");
#nullable restore
#line 52 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
                         Write(message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
            WriteLiteral("    </script>\r\n");
#nullable restore
#line 55 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 57 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
 foreach (var message in errorMessages)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        Notification_error(\'");
#nullable restore
#line 60 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
                       Write(message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
            WriteLiteral("    </script>\r\n");
#nullable restore
#line 63 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 65 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
 foreach (var message in warningMessages)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        Notification_warning(\'");
#nullable restore
#line 68 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
                         Write(message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
            WriteLiteral("    </script>\r\n");
#nullable restore
#line 71 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 73 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
 foreach (var message in infoMessages)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        Notification_info(\'");
#nullable restore
#line 76 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
                      Write(message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
            WriteLiteral("    </script>\r\n");
#nullable restore
#line 79 "D:\New folder\BamoBom\BomoBana\Areas\Admin\Views\Shared\_Notifications.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
