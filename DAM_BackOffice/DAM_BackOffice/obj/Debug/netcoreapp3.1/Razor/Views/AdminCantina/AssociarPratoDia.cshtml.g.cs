#pragma checksum "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e33ffb9a8941fd9f6247c6d5ac3154b158b71148"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AdminCantina_AssociarPratoDia), @"mvc.1.0.view", @"/Views/AdminCantina/AssociarPratoDia.cshtml")]
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
#line 1 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\_ViewImports.cshtml"
using DAM_BackOffice;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\_ViewImports.cshtml"
using DAM_BackOffice.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
using DAM_BackOffice.Models.Dtos;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e33ffb9a8941fd9f6247c6d5ac3154b158b71148", @"/Views/AdminCantina/AssociarPratoDia.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78a7c30f9bfb95e614da6873e040959fbe8d973a", @"/Views/_ViewImports.cshtml")]
    public class Views_AdminCantina_AssociarPratoDia : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ReturnPratosDto>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AssociarPratoDia", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "AdminCantina", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AdicionarPrato", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PratosDia", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 4 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
  
    ViewData["Title"] = "Pratos";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Escolha prato para o dia ");
#nullable restore
#line 8 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                        Write(ViewBag.data);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n\n");
#nullable restore
#line 10 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
 if (Model.Pratos.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table class=\"table table-borderless\">\n        <tbody>\n");
#nullable restore
#line 14 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
             foreach (var item in Model.Pratos)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\n                    <td>\n                        <div style=\"position:relative; padding:10px;\">   \n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e33ffb9a8941fd9f6247c6d5ac3154b158b711486826", async() => {
                WriteLiteral("\n                                <div>\n                                    <button type=\"submit\" class=\"btn btn-success\" style=\"text-decoration: none;\">");
#nullable restore
#line 21 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                                                                                                            Write(Html.DisplayFor(modelItem => item.Descricao));

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\n                                </div>\n                                <br />\n                                <div>\n                                    <img style=\"border-radius:10%; box-shadow:2px 2px 4px grey;\"");
                BeginWriteAttribute("src", " src=\"", 1040, "\"", 1105, 2);
                WriteAttributeValue("", 1046, "http://localhost:56069/api/users/getimage/pratos/", 1046, 49, true);
#nullable restore
#line 25 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
WriteAttributeValue("", 1095, item.Foto, 1095, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" height=\"70\" />\n                                </div>\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-IdPrato", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                                                                                                     WriteLiteral(item.IdPrato);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["IdPrato"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-IdPrato", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["IdPrato"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                                                                                                                                         WriteLiteral(ViewBag.IdCantina);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["IdCantina"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-IdCantina", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["IdCantina"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                                                                                                                                                                             WriteLiteral(ViewBag.Data);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["Data"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Data", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["Data"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                            </div>\n                    </td>\n             \n                </tr>\n");
#nullable restore
#line 32 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n        </tbody>\n    </table>\n");
#nullable restore
#line 37 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\n    <p>Ainda n??o existe nenhum prato, clique ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e33ffb9a8941fd9f6247c6d5ac3154b158b7114813490", async() => {
                WriteLiteral("aqui");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" para adicionar um prato. </p>\n</div>\n");
#nullable restore
#line 43 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<br />\n<div>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e33ffb9a8941fd9f6247c6d5ac3154b158b7114815156", async() => {
                WriteLiteral("Voltar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-IdCantina", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 47 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                                                                                             WriteLiteral(ViewBag.IdCantina);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["IdCantina"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-IdCantina", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["IdCantina"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 47 "C:\Users\jmfer\OneDrive\Ambiente de Trabalho\DAM_Project-main\DAM_BackOffice\DAM_BackOffice\Views\AdminCantina\AssociarPratoDia.cshtml"
                                                                                                                                 WriteLiteral(ViewBag.Data);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Data"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Data", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Data"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ReturnPratosDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
