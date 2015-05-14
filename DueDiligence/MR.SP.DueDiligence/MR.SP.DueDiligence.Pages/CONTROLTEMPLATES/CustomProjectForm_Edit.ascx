<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Tagprefix="wp" Namespace="MR.SP.DueDiligence.WebPart.WPViewDocument" Assembly="MR.SP.DueDiligence.WebPart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=79b2f166486ede14" %>
<%@Register TagPrefix="custom" TagName="CheckList" Src="~/_controltemplates/15/CustomPublicForm.ascx"%>

<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomProjectForm_Edit.ascx.cs" Inherits="MR.SP.DueDiligence.Pages.CONTROLTEMPLATES.CustomProjectForm_Edit" %>




<SharePoint:RenderingTemplate id="CustomProjectFormEdit" runat="server">
	<Template>
        
<SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/Form/EditForm/EditForm.js" runat="server" /> 
<SharePoint:ScriptLink ID="ScriptLink3" Name="~sitecollection/Style Library/DiligencePortal/scripts/Form/EditForm/SendMail.js" runat="server" /> 
        <SharePoint:ScriptLink ID="ScriptLink4" Name="~sitecollection/_layouts/15/datepicker.debug.js" runat="server" /> 
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Form/EditForm/EditForm.css %>' After="CONTROLS.css" runat="server"/>  
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Form/DocumentWebPart.css %>' After="CONTROLS.css" runat="server"/>        
              
        
        
        <div class="formTopBar">Edit Form</div> <br/>
			<div style="width: 100%; text-align: right;">
							<button id="btnSave" type="button">Save</button>&#160;&#160;<button id="btnCancel" type="button">Cancel</button>
					  </div> <br/>
        
		<table border="0" cellspacing="1" width="100%" class="UDEditForm">
				<custom:CheckList runat="server"></custom:CheckList>

					<tr>
                        <td colspan="4">
                            
                            <div style="float:right"><SharePoint:GoBackButton runat="server"></SharePoint:GoBackButton></div>
                                <div style="float:right"><SharePoint:SaveButton runat="server" ControlMode="Edit"></SharePoint:SaveButton></div>
                            
                        </td>
					</tr>
				</table>
        
        <wp:WPViewDocument ID="wp" runat="server" />
	</Template>
</SharePoint:RenderingTemplate>