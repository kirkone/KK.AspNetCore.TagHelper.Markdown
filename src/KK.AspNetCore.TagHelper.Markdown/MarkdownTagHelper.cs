namespace KK.AspNetCore.TagHelper.Markdown
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    [HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : TagHelper
    {
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        [HtmlAttributeName("surrounding-tag")]
        public string SurroundingTag { get; set; }

        [HtmlAttributeName("surrounding-class")]
        public string SurroundingClass { get; set; }

        [HtmlAttributeName("source")]
        public ModelExpression Source { get; set; }

        public override void Process(
            TagHelperContext context,
            TagHelperOutput output
        )
        {
            if (this.Source != null)
            {
                this.Text = this.Source.Model.ToString();
            }

            var result = Markdig.Markdown.ToHtml(this.Text);

            if (!string.IsNullOrWhiteSpace(SurroundingTag))
            {
                output.TagName = SurroundingTag;
                if (!string.IsNullOrWhiteSpace(SurroundingClass))
                {
                    output.Attributes.Add("class", SurroundingClass);
                }
                output.TagMode = TagMode.StartTagAndEndTag;
            }
            else
            {
                output.TagName = null;
            }
            output.Content.SetHtmlContent(result);
        }
    }
}
