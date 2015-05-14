<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@Register TagPrefix="custom" TagName="CheckList" Src="~/_controltemplates/15/CustomPublicForm.ascx"%>

<%@ Register Tagprefix="wp" Namespace="MR.SP.DueDiligence.WebPart.WPViewDocument" Assembly="MR.SP.DueDiligence.WebPart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=79b2f166486ede14" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomProjectForm_Display.ascx.cs" Inherits="MR.SP.DueDiligence.Pages.CONTROLTEMPLATES.CustomProjectForm_Display" %>


 

<SharePoint:RenderingTemplate id="CustomProjectFormDisplay" runat="server">
	<Template>
<SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/Form/DisplayForm/DisplayForm.js" runat="server" /> 
<SharePoint:ScriptLink ID="ScriptLink3" Name="~sitecollection/Style Library/DiligencePortal/scripts/Form/DisplayForm/SendMailButtonAndEditButton.js" runat="server" />   
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Form/DocumentWebPart.css %>' After="CONTROLS.css"  runat="server"/>  
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Form/DisplayForm/DisplayForm.css %>' After="CONTROLS.css"  runat="server"/>  

        <div class="formTopBar">Display Form</div> <br/>
		<table border="0" cellspacing="1" width="100%" class="UDDisplayForm">
				<custom:CheckList runat="server"></custom:CheckList>
					<tr>
                        <td colspan="4">
                            
                            <div style="float:right"><SharePoint:SaveButton runat="server" ControlMode="Display"></SharePoint:SaveButton></div>
                                <div style="float:right"><SharePoint:GoBackButton runat="server"></SharePoint:GoBackButton></div>
                            
                        </td>
					</tr>
				</table>
        <wp:WPViewDocument ID="wp" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>
