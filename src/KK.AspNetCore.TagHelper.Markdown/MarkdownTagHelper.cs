namespace KK.AspNetCore.TagHelper.Markdown
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Markdig;

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

        public async override Task ProcessAsync(
            TagHelperContext context,
            TagHelperOutput output
        )
        {
            if (this.Source != null)
            {
                this.Text = this.Source.Model.ToString();
            }

            if (string.IsNullOrWhiteSpace(this.Text))
            {
                this.Text = await GetContent(output);
            }

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

            var result = Markdown.ToHtml(this.Text, pipeline);

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

        private async Task<string> GetContent(TagHelperOutput output)
        {
            return (await output.GetChildContentAsync()).GetContent().Trim();
        }
    }
}
