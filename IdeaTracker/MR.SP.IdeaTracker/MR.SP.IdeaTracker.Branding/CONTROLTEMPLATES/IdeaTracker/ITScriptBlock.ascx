<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ITScriptBlock.ascx.cs" Inherits="Merck.GTAS.SharePoint.CONTROLTEMPLATES.GTAS.GlobalDelegate" %>

<script type="text/javascript" src='<asp:Literal runat="server"  Text="<% $SPUrl:~sitecollection/Style%20Library/IdeaTracker/scripts/jquery-1.11.1.min.js %>" />'></script>
<script type="text/javascript" src='<asp:Literal runat="server"  Text="<% $SPUrl:~sitecollection/Style%20Library/IdeaTracker/scripts/jquery-ui.min.js %>" />'></script>
<script type="text/javascript" src='<asp:Literal runat="server"  Text="<% $SPUrl:~sitecollection/Style%20Library/IdeaTracker/scripts/knockout-3.2.0.js %>" />'></script>
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/IdeaTracker/css/jquery-ui.min.css %>' After="CONTROLS.css" runat="server"/>
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/IdeaTracker/css/jquery-ui.structure.min.css %>' After="CONTROLS.css" runat="server"/>
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/IdeaTracker/css/jquery-ui.theme.min.css %>' After="CONTROLS.css" runat="server"/>