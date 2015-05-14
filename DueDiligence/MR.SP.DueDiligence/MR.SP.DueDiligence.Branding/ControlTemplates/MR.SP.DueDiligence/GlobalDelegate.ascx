<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GlobalDelegate.ascx.cs" Inherits="MR.SP.DueDiligence.Branding.ControlTemplates.MR.SP.DueDiligence.Branding.GlobalDelegate" %>

<SharePoint:ScriptLink ID="ScriptLink1" Name="~sitecollection/Style Library/DiligencePortal/scripts/jquery-1.11.1.min.js" runat="server" />
<SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/publicMethod.js" runat="server" />
<SharePoint:ScriptLink ID="ScriptLink3" Name="~sitecollection/Style Library/DiligencePortal/scripts/jquery.SPServices-2014.01.min.js" runat="server" />
<SharePoint:ScriptLink ID="ScriptLink4" Name="~sitecollection/Style Library/DiligencePortal/scripts/view/RemoveLookupFieldLink.js" runat="server" />

<SharePoint:CssRegistration Name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/ddglobal.css %>' After="CONTROLS.css" runat="server" />
