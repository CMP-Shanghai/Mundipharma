<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdeaViewWP.ascx.cs" Inherits="MR.SP.IdeaTracker.WebParts.IdeaViewWP.IdeaViewWP" %>

<link href="../_layouts/15/images/MR.SP.IdeaTracker/CSS/ideaviewCSS.css" rel="stylesheet" />
<link href="../_layouts/15/images/MR.SP.IdeaTracker/CSS/jquery-ui.css" rel="stylesheet" />
<script src="../_layouts/15/images/MR.SP.IdeaTracker/Javascript/jquery-1.10.2.js"></script>
<script src="../_layouts/15/images/MR.SP.IdeaTracker/Javascript/jquery-ui.js"></script>
	<script>
	    $(function () {
	        $("#tabs").tabs();
	    });
	</script>


<style type="text/css">
    .auto-style1 {
        width: 381px;
    }
    .auto-style2 {
        width: 447px;
    }
</style>


<div>
    <table width="100%"; border="0" cellspacing="0" cellpadding="0" >
        <tr class="ideaview-table-title-bg">
            <td class="auto-style1">
                <asp:Literal ID="ltIdeaID" runat="server"></asp:Literal>
            </td>
            <td class="auto-style2">
                <strong><asp:Literal ID="ltIdeaTitle" runat="server" Text="Item is not exists."></asp:Literal></strong>
            </td>
            <td class="ideaview-table-createdby">
                Therapeutic Area: <asp:Literal ID="ltAreas" runat="server" Text="Empty"></asp:Literal>
            </td>
        </tr>
        <tr>           
            <td colspan="3" class="ideaview-table-HeadTR ms-rteFontSize-4">
            Stage:     <asp:Literal ID="ltStage" runat="server" Text="[Empty]"></asp:Literal> 
             </td>
        </tr>
        <tr>
            <td class="ideaview-table-HeadTR ms-rteFontSize-4">
                Description
            </td>
              <td colspan="2" class="ideaview-table-HeadTR ms-rteFontSize-4">
                Summary
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Literal ID="ltDescription" runat="server" Text="[Empty]"></asp:Literal>
            </td>
            <td colspan="2">
                 <asp:Literal ID="ltSummary" runat="server" Text="[Empty]"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="ideaview-table-HeadTR ms-rteFontSize-4">
                Idea Originator
            </td>
            <td colspan="2" class="ideaview-table-HeadTR ms-rteFontSize-4">
                Last Updated By
            </td>
        </tr>
        <tr>
            <td class="ms-vb2 ideaview-table-ContentTR">
                <asp:Literal ID="ltOriginator" runat="server" Text="Empty"></asp:Literal>
            </td>
            <td colspan="2" class="ms-vb2 ideaview-table-ContentTR">
                <asp:Literal ID="ltModifiedBy" runat="server"></asp:Literal>
            </td>
        </tr>
                <tr>
            <td colspan="3" class="ideaview-table-HeadTR ms-rteFontSize-4">
                &nbsp;</td>
        </tr>
    </table>
    </div>
<p>
    &nbsp;</p><div id="tabs" class="ideadetail-table-style ui-tabs ui-widget ui-widget-content ui-corner-all">
	<ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all" role="tablist">
		<li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active" role="tab" tabindex="0" aria-controls="tabs-1" aria-labelledby="ui-id-1" aria-selected="true" aria-expanded="true"><a href="#tabs-1" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-1">Idea Feeds</a></li>
		<li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-2" aria-labelledby="ui-id-2" aria-selected="false" aria-expanded="false"><a href="#tabs-2" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-2">Minutes/Notes</a></li>
		<li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-3" aria-labelledby="ui-id-3" aria-selected="false" aria-expanded="false"><a href="#tabs-3" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-3">Attachments</a></li>
        <li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-4" aria-labelledby="ui-id-4" aria-selected="false" aria-expanded="false"><a href="#tabs-4" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-4">Hyperlinks</a></li>
        <li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-5" aria-labelledby="ui-id-5" aria-selected="false" aria-expanded="false"><a href="#tabs-5" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-5">Action Items</a></li>
	    <li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-6" aria-labelledby="ui-id-6" aria-selected="false" aria-expanded="false"><a href="#tabs-6" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-6">Contacts</a></li>
	    <li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-7" aria-labelledby="ui-id-7" aria-selected="false" aria-expanded="false"><a href="#tabs-7" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-7">SWOT</a></li>
	    <li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-8" aria-labelledby="ui-id-8" aria-selected="false" aria-expanded="false"><a href="#tabs-8" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-8">Innovation Index</a></li>
        <li class="ui-state-default ui-corner-top" role="tab" tabindex="-1" aria-controls="tabs-9" aria-labelledby="ui-id-9" aria-selected="false" aria-expanded="false"><a href="#tabs-9" class="ui-tabs-anchor" role="presentation" tabindex="-1" id="ui-id-9">Discussion Board</a></li>
    </ul>
	<div id="tabs-1" aria-labelledby="ui-id-1" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="false" style="display: block;">
        <table width="90%"><tbody><tr><td><asp:Panel ID="plFeeds" runat="server" /></td></tr></tbody></table>
	</div>
	<div id="tabs-2" aria-labelledby="ui-id-2" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plMinutes" runat="server" /></td></tr></tbody></table>       
	</div>
	<div id="tabs-3" aria-labelledby="ui-id-3" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plAttachments" runat="server" /></td></tr></tbody></table> 
    </div>
    <div id="tabs-4" aria-labelledby="ui-id-4" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plHyperlinks" runat="server" /></td></tr></tbody></table> 
    </div>
    <div id="tabs-5" aria-labelledby="ui-id-5" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
       <table width="90%"><tbody><tr><td><asp:Panel ID="plActions" runat="server" /></td></tr></tbody></table> 
    </div>
	<div id="tabs-6" aria-labelledby="ui-id-6" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plContacts" runat="server" /></td></tr></tbody></table> 
	</div>
	<div id="tabs-7" aria-labelledby="ui-id-7" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plSWOT" runat="server" /></td></tr></tbody></table> 
	</div>
    <div id="tabs-8" aria-labelledby="ui-id-8" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plInnovationIndex" runat="server" /></td></tr></tbody></table> 
    </div>
    <div id="tabs-8" aria-labelledby="ui-id-9" class="ui-tabs-panel ui-widget-content ui-corner-bottom" role="tabpanel" aria-hidden="true" style="display: none;">
		<table width="90%"><tbody><tr><td><asp:Panel ID="plDiscussion" runat="server" /></td></tr></tbody></table> 
    </div>
</div>
