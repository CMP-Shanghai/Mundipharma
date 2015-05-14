<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DueDiligenceReport.aspx.cs" Inherits="MR.SP.DueDiligence.Pages.DueDiligenceReport" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Assembly Name="MR.SP.DueDiligence.Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=259e7700daae3665" %>
<%@ Import Namespace="MR.SP.DueDiligence.Framework" %>
<%@ Register Tagprefix ="mrdd" Namespace="MR.SP.DueDiligence.WebPart" Assembly="MR.SP.DueDiligence.WebPart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=79b2f166486ede14"%>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
        
        
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <mrdd:ListViewWithRole ID="viewRole" runat="server" AdminViewKey="ViewWithDDReportLink4Admin" AdminViewDefault="" ParticipantViewKey="ViewWithDDReportLink4Participant" ParticipantViewDefault="" NormalUserViewKey="ViewWithDDReportLink4NormalUser" NormalUserViewDefault=""/>
    <SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/View/LinkToReport.js" LoadAfterUI="true" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Due Diligence Report
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Due Diligence Report
</asp:Content>
