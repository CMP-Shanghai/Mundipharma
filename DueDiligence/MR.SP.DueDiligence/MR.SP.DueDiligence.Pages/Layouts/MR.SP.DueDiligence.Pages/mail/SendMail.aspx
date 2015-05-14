<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="MR.SP.DueDiligence.Pages.Layouts.MR.SP.DueDiligence.Pages.mail.SendMail" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

    <SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/LayoutPages/SendMail.js" LoadAfterUI="true" runat="server" />
    <SharePoint:CssRegistration Name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Layoutpages/ddsendmail.css %>' After="CONTROLS.css" runat="server" />

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table width="100%" id="tabSendmail">
        <tr class="dd_send_mail_template_row">
            <td class="tdFirst">Template Type</td>

            <td>

                <asp:DropDownList runat="server" ID="ddlTempTyle" AutoPostBack="true" OnLoad="ddlTempTyle_Load" OnSelectedIndexChanged="ddlTempTyle_SelectedIndexChanged"></asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td class="tdFirst">To</td>
            <td>

                <table>

                    <tr>
                        <td>
                            <SharePoint:PeopleEditor runat="server" ID="peMilTo" MultiSelect="true" Width="500px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtTo" TextMode="MultiLine" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>

        <tr>
            <td class="tdFirst">Subject</td>
            <td>
                <asp:TextBox ID="txtSubject" runat="server" Width="500px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdFirst">Body</td>
            <td>
                <SharePoint:InputFormTextBox Title="" class="ms-input" Width="500px" ID="txtBody" runat="server" TextMode="MultiLine" Rows="15" RichText="true" AllowHyperlink="true" RichTextMode="FullHtml" /></td>
        </tr>

        <tr>
            <td colspan="2" style="height: 50px; text-align: left;">
                <asp:Button runat="server" Text="Send" ID="btnSend" OnClick="Send_Click" />
                <button runat="server" onclick="rueurl()" type="button">Cancel</button>

            </td>

        </tr>

    </table>



</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Send Mail
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Send Mail
</asp:Content>
