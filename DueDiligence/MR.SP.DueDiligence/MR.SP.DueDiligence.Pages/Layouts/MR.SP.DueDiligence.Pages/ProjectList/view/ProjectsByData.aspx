<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Debug="true" Language="C#" AutoEventWireup="true" CodeBehind="ProjectsByData.aspx.cs" Inherits="MR.SP.DueDiligence.Pages.Layouts.MR.SP.DueDiligence.Pages.ProjectList.view.ProjectsByData" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Layoutpages/ddreports.css %>' After="CONTROLS.css" runat="server"/>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="searchCondition" class="dd_report_search_codition">
        <table width="90%">

            <tr>
                <td class="dd_report_search_field_title">Therapeutic area:</td>
                <td class="dd_report_search_field_area">
                    <asp:DropDownList runat="server" ID="selectArea" />
                </td>
                <td class="dd_report_search_field_title">Outcome:</td>
                <td class="dd_report_search_field_outcome" >
                    <asp:DropDownList runat="server" ID="selectOutcome" />
                </td>
                <td class="dd_report_search_show_button">
                    <asp:Button runat="server" ID="btnShow" Text="Show" OnClick="btnShow_Click" />
                </td>
            </tr>


        </table>
    </div>
    <br />


    <SharePoint:SPGridView ID="gvReport" runat="server" AllowGroupCollapse="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="30">
        <AlternatingRowStyle CssClass="ms-alternating" />
        <SelectedRowStyle CssClass="ms-selectednav" Font-Bold="True" />
    </SharePoint:SPGridView>
    <div style="text-align: center">
        <SharePoint:SPGridViewPager ID="gvPager" runat="server" GridViewId="gvReport" ViewStateMode="Inherit" OnClickNext="SPGridViewPager1_ClickNext">
        </SharePoint:SPGridViewPager>
    </div>


</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    KPI's
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    KPI's
</asp:Content>
