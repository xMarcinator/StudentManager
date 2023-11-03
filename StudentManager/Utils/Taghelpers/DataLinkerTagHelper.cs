using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StudentManager.Controllers;

namespace StudentManager.Utils.Taghelpers;

[HtmlTargetElement("select", Attributes = "filler-controller")]
public class DataLinkerTagHelper : TagHelper {
    private readonly IUrlHelperFactory _urlHelperFactory;
    public DataLinkerTagHelper(IUrlHelperFactory helperFactory)
    {
        _urlHelperFactory = helperFactory;
    }
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }

    //public string? searchString { get; set; }
    public string? FillerAction { get; set; } = String.Empty;
    public string FillerController { get; set; } = String.Empty;
    public string InputTarget { get; set; }
    public string EndpointParameter { get; set; }

    public override void Process(TagHelperContext context,
        TagHelperOutput output) {
        Console.WriteLine("Ran datafiller tag helper");
        
        if (ViewContext == null) {
            throw new ArgumentNullException(nameof(ViewContext));
        }
        
        FillerAction ??= ViewContext.RouteData.Values["action"]?.ToString() ?? "List";
        IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
        
        TagBuilder result = new TagBuilder("div");
        
        Console.WriteLine("Ran tag helper");
        if (ViewContext != null) {
            output.Content.AppendHtml(result.InnerHtml);
        }
        
        Dictionary<string,object> routeValues = new();

        var url = urlHelper.Action(FillerAction, FillerController, routeValues);
        //add data-filler attributes for javascript counterpart
        output.Attributes.SetAttribute("data-fill-endpoint", url);
        output.Attributes.SetAttribute("data-fill-parameter", EndpointParameter);
        output.Attributes.SetAttribute("data-fill-data-target", InputTarget);
    }
}