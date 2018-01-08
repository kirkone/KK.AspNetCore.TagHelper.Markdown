# KK.AspNetCore.TagHelper.Markdown

This TagHelper act like a `pre` tag but renders the contend as html.  
The Markdown processor is [Markdig](processor) and most of all advanced extensions (except Emoji, SoftLine as HardLine, JiraLinks and SmartyPants) are enabled.

This TagHelper can be used as the following:

```
<markdown text="# With surrounding tag!" surrounding-tag="div" />
```
```
<markdown text="# With surrounding tag and css class!" surrounding-tag="div" surrounding-class="sample-class" />
```
```
<markdown text="# Without surrounding tag!" />
```

```
<markdown>
# Inline Markdown  
- List 1
- List 2
> text

| abc | def | 
|---|---|
| cde| ddd| 
| eee| fff|
| fff | fffff   | 
|gggg  | ffff |
</markdown>
```