﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MR.SP.IdeaTracker.WebParts.IdeaViewWP {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    using System.CodeDom.Compiler;
    
    
    public partial class IdeaViewWP {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltIdeaID;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltIdeaTitle;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltAreas;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltStage;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltDescription;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltSummary;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltOriginator;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltModifiedBy;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plFeeds;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plMinutes;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plAttachments;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plHyperlinks;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plActions;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plContacts;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plSWOT;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plInnovationIndex;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Panel plDiscussion;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(IdeaViewWP target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltIdeaID() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltIdeaID = @__ctrl;
            @__ctrl.ID = "ltIdeaID";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltIdeaTitle() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltIdeaTitle = @__ctrl;
            @__ctrl.ID = "ltIdeaTitle";
            @__ctrl.Text = "Item is not exists.";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltAreas() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltAreas = @__ctrl;
            @__ctrl.ID = "ltAreas";
            @__ctrl.Text = "Empty";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltStage() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltStage = @__ctrl;
            @__ctrl.ID = "ltStage";
            @__ctrl.Text = "[Empty]";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltDescription() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltDescription = @__ctrl;
            @__ctrl.ID = "ltDescription";
            @__ctrl.Text = "[Empty]";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltSummary() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltSummary = @__ctrl;
            @__ctrl.ID = "ltSummary";
            @__ctrl.Text = "[Empty]";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltOriginator() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltOriginator = @__ctrl;
            @__ctrl.ID = "ltOriginator";
            @__ctrl.Text = "Empty";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltModifiedBy() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltModifiedBy = @__ctrl;
            @__ctrl.ID = "ltModifiedBy";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplFeeds() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plFeeds = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plFeeds";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplMinutes() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plMinutes = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plMinutes";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplAttachments() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plAttachments = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plAttachments";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplHyperlinks() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plHyperlinks = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plHyperlinks";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplActions() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plActions = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plActions";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplContacts() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plContacts = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plContacts";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplSWOT() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plSWOT = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plSWOT";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplInnovationIndex() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plInnovationIndex = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plInnovationIndex";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Panel @__BuildControlplDiscussion() {
            global::System.Web.UI.WebControls.Panel @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Panel();
            this.plDiscussion = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "plDiscussion";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::MR.SP.IdeaTracker.WebParts.IdeaViewWP.IdeaViewWP @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"

<link href=""../_layouts/15/images/MR.SP.IdeaTracker/CSS/ideaviewCSS.css"" rel=""stylesheet"" />
<link href=""../_layouts/15/images/MR.SP.IdeaTracker/CSS/jquery-ui.css"" rel=""stylesheet"" />
<script src=""../_layouts/15/images/MR.SP.IdeaTracker/Javascript/jquery-1.10.2.js""></script>
<script src=""../_layouts/15/images/MR.SP.IdeaTracker/Javascript/jquery-ui.js""></script>
	<script>
	    $(function () {
	        $(""#tabs"").tabs();
	    });
	</script>


<style type=""text/css"">
    .auto-style1 {
        width: 381px;
    }
    .auto-style2 {
        width: 447px;
    }
</style>


<div>
    <table width=""100%""; border=""0"" cellspacing=""0"" cellpadding=""0"" >
        <tr class=""ideaview-table-title-bg"">
            <td class=""auto-style1"">
                "));
            global::System.Web.UI.WebControls.Literal @__ctrl1;
            @__ctrl1 = this.@__BuildControlltIdeaID();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            </td>\r\n            <td class=\"auto-style2\">\r\n                <stron" +
                        "g>"));
            global::System.Web.UI.WebControls.Literal @__ctrl2;
            @__ctrl2 = this.@__BuildControlltIdeaTitle();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</strong>\r\n            </td>\r\n            <td class=\"ideaview-table-createdby\">\r\n" +
                        "                Therapeutic Area: "));
            global::System.Web.UI.WebControls.Literal @__ctrl3;
            @__ctrl3 = this.@__BuildControlltAreas();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            </td>\r\n        </tr>\r\n        <tr>           \r\n            <td cols" +
                        "pan=\"3\" class=\"ideaview-table-HeadTR ms-rteFontSize-4\">\r\n            Stage:     " +
                        ""));
            global::System.Web.UI.WebControls.Literal @__ctrl4;
            @__ctrl4 = this.@__BuildControlltStage();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@" 
             </td>
        </tr>
        <tr>
            <td class=""ideaview-table-HeadTR ms-rteFontSize-4"">
                Description
            </td>
              <td colspan=""2"" class=""ideaview-table-HeadTR ms-rteFontSize-4"">
                Summary
            </td>
        </tr>
        <tr>
            <td class=""auto-style1"">
                "));
            global::System.Web.UI.WebControls.Literal @__ctrl5;
            @__ctrl5 = this.@__BuildControlltDescription();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            </td>\r\n            <td colspan=\"2\">\r\n                 "));
            global::System.Web.UI.WebControls.Literal @__ctrl6;
            @__ctrl6 = this.@__BuildControlltSummary();
            @__parser.AddParsedSubObject(@__ctrl6);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
            </td>
        </tr>
        <tr>
            <td class=""ideaview-table-HeadTR ms-rteFontSize-4"">
                Idea Originator
            </td>
            <td colspan=""2"" class=""ideaview-table-HeadTR ms-rteFontSize-4"">
                Last Updated By
            </td>
        </tr>
        <tr>
            <td class=""ms-vb2 ideaview-table-ContentTR"">
                "));
            global::System.Web.UI.WebControls.Literal @__ctrl7;
            @__ctrl7 = this.@__BuildControlltOriginator();
            @__parser.AddParsedSubObject(@__ctrl7);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            </td>\r\n            <td colspan=\"2\" class=\"ms-vb2 ideaview-table-Con" +
                        "tentTR\">\r\n                "));
            global::System.Web.UI.WebControls.Literal @__ctrl8;
            @__ctrl8 = this.@__BuildControlltModifiedBy();
            @__parser.AddParsedSubObject(@__ctrl8);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n            </td>\r\n        </tr>\r\n                <tr>\r\n            <td colspan" +
                        "=\"3\" class=\"ideaview-table-HeadTR ms-rteFontSize-4\">\r\n                &nbsp;</td" +
                        ">\r\n        </tr>\r\n    </table>\r\n    </div>\r\n<p>\r\n    &nbsp;</p><div id=\"tabs\" cl" +
                        "ass=\"ideadetail-table-style ui-tabs ui-widget ui-widget-content ui-corner-all\">\r" +
                        "\n\t<ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-" +
                        "corner-all\" role=\"tablist\">\r\n\t\t<li class=\"ui-state-default ui-corner-top ui-tabs" +
                        "-active ui-state-active\" role=\"tab\" tabindex=\"0\" aria-controls=\"tabs-1\" aria-lab" +
                        "elledby=\"ui-id-1\" aria-selected=\"true\" aria-expanded=\"true\"><a href=\"#tabs-1\" cl" +
                        "ass=\"ui-tabs-anchor\" role=\"presentation\" tabindex=\"-1\" id=\"ui-id-1\">Idea Feeds</" +
                        "a></li>\r\n\t\t<li class=\"ui-state-default ui-corner-top\" role=\"tab\" tabindex=\"-1\" a" +
                        "ria-controls=\"tabs-2\" aria-labelledby=\"ui-id-2\" aria-selected=\"false\" aria-expan" +
                        "ded=\"false\"><a href=\"#tabs-2\" class=\"ui-tabs-anchor\" role=\"presentation\" tabinde" +
                        "x=\"-1\" id=\"ui-id-2\">Minutes/Notes</a></li>\r\n\t\t<li class=\"ui-state-default ui-cor" +
                        "ner-top\" role=\"tab\" tabindex=\"-1\" aria-controls=\"tabs-3\" aria-labelledby=\"ui-id-" +
                        "3\" aria-selected=\"false\" aria-expanded=\"false\"><a href=\"#tabs-3\" class=\"ui-tabs-" +
                        "anchor\" role=\"presentation\" tabindex=\"-1\" id=\"ui-id-3\">Attachments</a></li>\r\n   " +
                        "     <li class=\"ui-state-default ui-corner-top\" role=\"tab\" tabindex=\"-1\" aria-co" +
                        "ntrols=\"tabs-4\" aria-labelledby=\"ui-id-4\" aria-selected=\"false\" aria-expanded=\"f" +
                        "alse\"><a href=\"#tabs-4\" class=\"ui-tabs-anchor\" role=\"presentation\" tabindex=\"-1\"" +
                        " id=\"ui-id-4\">Hyperlinks</a></li>\r\n        <li class=\"ui-state-default ui-corner" +
                        "-top\" role=\"tab\" tabindex=\"-1\" aria-controls=\"tabs-5\" aria-labelledby=\"ui-id-5\" " +
                        "aria-selected=\"false\" aria-expanded=\"false\"><a href=\"#tabs-5\" class=\"ui-tabs-anc" +
                        "hor\" role=\"presentation\" tabindex=\"-1\" id=\"ui-id-5\">Action Items</a></li>\r\n\t    " +
                        "<li class=\"ui-state-default ui-corner-top\" role=\"tab\" tabindex=\"-1\" aria-control" +
                        "s=\"tabs-6\" aria-labelledby=\"ui-id-6\" aria-selected=\"false\" aria-expanded=\"false\"" +
                        "><a href=\"#tabs-6\" class=\"ui-tabs-anchor\" role=\"presentation\" tabindex=\"-1\" id=\"" +
                        "ui-id-6\">Contacts</a></li>\r\n\t    <li class=\"ui-state-default ui-corner-top\" role" +
                        "=\"tab\" tabindex=\"-1\" aria-controls=\"tabs-7\" aria-labelledby=\"ui-id-7\" aria-selec" +
                        "ted=\"false\" aria-expanded=\"false\"><a href=\"#tabs-7\" class=\"ui-tabs-anchor\" role=" +
                        "\"presentation\" tabindex=\"-1\" id=\"ui-id-7\">SWOT</a></li>\r\n\t    <li class=\"ui-stat" +
                        "e-default ui-corner-top\" role=\"tab\" tabindex=\"-1\" aria-controls=\"tabs-8\" aria-la" +
                        "belledby=\"ui-id-8\" aria-selected=\"false\" aria-expanded=\"false\"><a href=\"#tabs-8\"" +
                        " class=\"ui-tabs-anchor\" role=\"presentation\" tabindex=\"-1\" id=\"ui-id-8\">Innovatio" +
                        "n Index</a></li>\r\n        <li class=\"ui-state-default ui-corner-top\" role=\"tab\" " +
                        "tabindex=\"-1\" aria-controls=\"tabs-9\" aria-labelledby=\"ui-id-9\" aria-selected=\"fa" +
                        "lse\" aria-expanded=\"false\"><a href=\"#tabs-9\" class=\"ui-tabs-anchor\" role=\"presen" +
                        "tation\" tabindex=\"-1\" id=\"ui-id-9\">Discussion Board</a></li>\r\n    </ul>\r\n\t<div i" +
                        "d=\"tabs-1\" aria-labelledby=\"ui-id-1\" class=\"ui-tabs-panel ui-widget-content ui-c" +
                        "orner-bottom\" role=\"tabpanel\" aria-hidden=\"false\" style=\"display: block;\">\r\n    " +
                        "    <table width=\"90%\"><tbody><tr><td>"));
            global::System.Web.UI.WebControls.Panel @__ctrl9;
            @__ctrl9 = this.@__BuildControlplFeeds();
            @__parser.AddParsedSubObject(@__ctrl9);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table>\r\n\t</div>\r\n\t<div id=\"tabs-2\" aria-labelledby=\"ui-id-2\" " +
                        "class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" aria-hi" +
                        "dden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>"));
            global::System.Web.UI.WebControls.Panel @__ctrl10;
            @__ctrl10 = this.@__BuildControlplMinutes();
            @__parser.AddParsedSubObject(@__ctrl10);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table>       \r\n\t</div>\r\n\t<div id=\"tabs-3\" aria-labelledby=\"ui" +
                        "-id-3\" class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" " +
                        "aria-hidden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>" +
                        ""));
            global::System.Web.UI.WebControls.Panel @__ctrl11;
            @__ctrl11 = this.@__BuildControlplAttachments();
            @__parser.AddParsedSubObject(@__ctrl11);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n    </div>\r\n    <div id=\"tabs-4\" aria-labelledby=\"ui" +
                        "-id-4\" class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" " +
                        "aria-hidden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>" +
                        ""));
            global::System.Web.UI.WebControls.Panel @__ctrl12;
            @__ctrl12 = this.@__BuildControlplHyperlinks();
            @__parser.AddParsedSubObject(@__ctrl12);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n    </div>\r\n    <div id=\"tabs-5\" aria-labelledby=\"ui" +
                        "-id-5\" class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" " +
                        "aria-hidden=\"true\" style=\"display: none;\">\r\n       <table width=\"90%\"><tbody><tr" +
                        "><td>"));
            global::System.Web.UI.WebControls.Panel @__ctrl13;
            @__ctrl13 = this.@__BuildControlplActions();
            @__parser.AddParsedSubObject(@__ctrl13);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n    </div>\r\n\t<div id=\"tabs-6\" aria-labelledby=\"ui-id" +
                        "-6\" class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" ari" +
                        "a-hidden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>"));
            global::System.Web.UI.WebControls.Panel @__ctrl14;
            @__ctrl14 = this.@__BuildControlplContacts();
            @__parser.AddParsedSubObject(@__ctrl14);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n\t</div>\r\n\t<div id=\"tabs-7\" aria-labelledby=\"ui-id-7\"" +
                        " class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" aria-h" +
                        "idden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>"));
            global::System.Web.UI.WebControls.Panel @__ctrl15;
            @__ctrl15 = this.@__BuildControlplSWOT();
            @__parser.AddParsedSubObject(@__ctrl15);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n\t</div>\r\n    <div id=\"tabs-8\" aria-labelledby=\"ui-id" +
                        "-8\" class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" ari" +
                        "a-hidden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>"));
            global::System.Web.UI.WebControls.Panel @__ctrl16;
            @__ctrl16 = this.@__BuildControlplInnovationIndex();
            @__parser.AddParsedSubObject(@__ctrl16);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n    </div>\r\n    <div id=\"tabs-8\" aria-labelledby=\"ui" +
                        "-id-9\" class=\"ui-tabs-panel ui-widget-content ui-corner-bottom\" role=\"tabpanel\" " +
                        "aria-hidden=\"true\" style=\"display: none;\">\r\n\t\t<table width=\"90%\"><tbody><tr><td>" +
                        ""));
            global::System.Web.UI.WebControls.Panel @__ctrl17;
            @__ctrl17 = this.@__BuildControlplDiscussion();
            @__parser.AddParsedSubObject(@__ctrl17);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td></tr></tbody></table> \r\n    </div>\r\n</div>\r\n"));
        }
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
