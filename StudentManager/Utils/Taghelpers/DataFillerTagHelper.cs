using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StudentManager.Controllers;

namespace StudentManager.Utils.Taghelpers;

[HtmlTargetElement("select", Attributes = "initial-data")]
public class DataFillerTagHelper : TagHelper {
    public IEnumerable<(int id,object data)>? InitialData { get; set; }
    public int? InitiallySelected { get; set; }

    public override void Process(TagHelperContext context,
        TagHelperOutput output) {
        
        //add default option
        var defaultTag = new TagBuilder("option");
        defaultTag.Attributes.Add("value", string.Empty);
        defaultTag.InnerHtml.Append(InitialData != null ? "None Selected": "No Data");
        output.Content.AppendHtml(defaultTag);
        
        //add initial data
        if (InitialData != null)
        {
            foreach (var (id, data) in InitialData)
            {
                var tag = new TagBuilder("option");
                if (Equals(id, InitiallySelected))
                {
                    tag.Attributes.Add("selected", string.Empty);
                }
                tag.Attributes.Add("value", id.ToString());
                tag.InnerHtml.Append(data.ToString() ?? string.Empty);
                output.Content.AppendHtml(tag);
            }
        }
    }
}