<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPIndicationNotes.ascx.cs" Inherits="MR.SP.DueDiligence.WebPart.WPIndicationNotes.WPIndicationNotes" %>
<table style="width:100%">
    <tr><td style="background-color:#1780C7;color:white;font-weight:bold;font-family:'Microsoft YaHei';">Notes</td></tr>
    <tr><td><asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" Height="100"></asp:TextBox></td></tr>
    <tr><td align="right"><asp:Button Text="Save" runat="server" ID="btnSave" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button Text="Cancel" runat="server" ID="btnCancel" OnClick="btnCancel_Click" /></td></tr>
</table>

<script>
    $(function () {
        $(".ms-listviewtable").css("width", "100%");
        $("#<%= txtNotes.ClientID%>").css("width", "98.5%");
    });
</script>