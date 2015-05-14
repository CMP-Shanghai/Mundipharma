<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeyContacts.aspx.cs" Inherits="MR.SP.DueDiligence.Pages.Layouts.MR.SP.DueDiligence.Pages.KeyContacts" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Assembly Name="MR.SP.DueDiligence.Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=259e7700daae3665" %>
<%@ Import Namespace="MR.SP.DueDiligence.Framework" %>
<%@ Register TagPrefix="mrkc" Namespace="MR.SP.DueDiligence.WebPart.KeyContacts" Assembly="MR.SP.DueDiligence.WebPart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=79b2f166486ede14" %>


<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/LayoutPages/keycontacts.js" runat="server" LoadAfterUI="true" />
    <SharePoint:CssRegistration Name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Layoutpages/ddkeycontacts.css %>' After="CONTROLS.css" runat="server" />
</asp:content>

<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    
    <mrkc:KeyContacts id="kcWebpart" runat="server" UserGroups="Due Diligence Administrators;BD Lead;Commercial Head;R&D Executive Director"   ViewFields="Name;Title;Department;Work Email"  UserProfilePrpoerties="DisplayName;SPS-JobTitle;Department;WorkEmail" EmailColumns="Work Email" />
</asp:content>

<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
Key Contacts
</asp:content>

<asp:content id="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
Key Contacts
</asp:content>
