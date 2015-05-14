<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomProjectForm_New.ascx.cs" Inherits="MR.SP.DueDiligence.Pages.CONTROLTEMPLATES.CustomProjectForm_New" %>




<SharePoint:RenderingTemplate id="CustomProjectFormNew" runat="server">
	<Template>
        
<SharePoint:ScriptLink ID="ScriptLink2" Name="~sitecollection/Style Library/DiligencePortal/scripts/Form/NewForm/NewForm.js" runat="server" /> 
        <SharePoint:ScriptLink ID="ScriptLink4" Name="~sitecollection/_layouts/15/datepicker.debug.js" runat="server" /> 
<SharePoint:CssRegistration name='<% $SPUrl:~sitecollection/Style Library/DiligencePortal/css/Form/NewForm/NewForm.css %>'  After="CONTROLS.css"   runat="server"/>

        <div class="formTopBar">New Form</div>  <br/>
		<table border="0" cellspacing="1" width="100%" class="UDNewForm">
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Opportunity<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField1" ControlMode="New" FieldName="Title" __designer:bind="{ddwrt:DataBind('i',concat('ff1',$Pos),'Value','ValueChanged','ID',ddwrt:EscapeDelims(string(@ID)),'@Title')}"/>
							<SharePoint:FieldDescription runat="server" id="FieldDescription1" FieldName="Title" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Form<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField2" ControlMode="New" FieldName="ProjectForm" __designer:bind="{ddwrt:DataBind('i',concat('ff3',$Pos),'Value','ValueChanged','ID',ddwrt:EscapeDelims(string(@ID)),'@ProjectForm')}"/>
							<SharePoint:FieldDescription runat="server" id="FieldDescription2" FieldName="ProjectForm" ControlMode="New"/>
						</td>
					</tr>
					
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Opportunity Description<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField3" ControlMode="New" FieldName="OpportunityDescription" __designer:bind="{ddwrt:DataBind('i',concat('ff2',$Pos),'Value','ValueChanged','ID',ddwrt:EscapeDelims(string(@ID)),'@OpportunityDescription')}"/>
							<SharePoint:FieldDescription runat="server" id="FieldDescription3" FieldName="OpportunityDescription" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Company<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField4" ControlMode="New" FieldName="Company" __designer:bind="{ddwrt:DataBind('i',concat('ff6',$Pos),'Value','ValueChanged','ID',ddwrt:EscapeDelims(string(@ID)),'@Company')}"/>
							<SharePoint:FieldDescription runat="server" id="FieldDescription4" FieldName="Company" ControlMode="New"/>
						</td>
					</tr>					
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Start<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField5" ControlMode="New" FieldName="Start" __designer:bind="{ddwrt:DataBind('i',concat('ff4',$Pos),'Value','ValueChanged','ID',ddwrt:EscapeDelims(string(@ID)),'@Start')}"/>
							<SharePoint:FieldDescription runat="server" id="FieldDescription5" FieldName="Start" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Planned End</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField6" ControlMode="New" FieldName="PlannedEnd" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription6" FieldName="PlannedEnd" ControlMode="New"/>
						</td>
					</tr>
					<tr>
					<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>BD Lead<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
					</td>
					<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField7" ControlMode="New" FieldName="BDLead" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription7" FieldName="BDLead" ControlMode="New"/>
					</td>
					<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Commercial Head<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField8" ControlMode="New" FieldName="CommercialHead" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription8" FieldName="CommercialHead" ControlMode="New"/>
						</td>
					</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>R&amp;D Executive Director<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField9" ControlMode="New" FieldName="RandDExecutiveDirector" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription9" FieldName="RandDExecutiveDirector" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Therapeutic Area</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField10" ControlMode="New" FieldName="TherapeuticArea" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription10" FieldName="TherapeuticArea" ControlMode="New"/>
						</td>

						</tr>
						<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Territories<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField11" ControlMode="New" FieldName="Territories" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription11" FieldName="Territories" ControlMode="New"/>
						</td>

						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Country</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField12" ControlMode="New" FieldName="ProjectCountry" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription12" FieldName="ProjectCountry" ControlMode="New"/>
						</td>
					</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>No of Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField13" ControlMode="New" FieldName="NoOfPaticipants" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription13" FieldName="NoOfPaticipants" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField14" ControlMode="New" FieldName="ProjectParticipants" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription14" FieldName="ProjectParticipants" ControlMode="New"/>
						</td>
					</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Need External Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField15" ControlMode="New" FieldName="NeedExternalParticipants" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription15" FieldName="NeedExternalParticipants" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>External Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField16" ControlMode="New" FieldName="ExternalParticipants" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription16" FieldName="ExternalParticipants" ControlMode="New"/>
						</td>
					</tr>				
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>e Room Details</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField17" ControlMode="New" FieldName="eRoomDetails" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription17" FieldName="eRoomDetails" ControlMode="New"/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Onsite Visit Date</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField18" ControlMode="New" FieldName="OnsiteVisitDate" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription18" FieldName="OnsiteVisitDate" ControlMode="New"/>
						</td>
					</tr>					
					<tr>
						<td width="190px" valign="top" class="ms-formlabel">
							<H3 class="ms-standardheader">
								<nobr>Comments</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody" colspan="3">
							<SharePoint:FormField runat="server" id="FormField19" ControlMode="New" FieldName="ProjectComments" />
							<SharePoint:FieldDescription runat="server" id="FieldDescription19" FieldName="ProjectComments" ControlMode="New"/>
						</td>
					</tr>
					
                    <tr>
                        <td colspan="4" style="text-align:right;">
                                <div style="float:right"><SharePoint:GoBackButton runat="server"></SharePoint:GoBackButton></div>
                                <div style="float:right"><SharePoint:SaveButton runat="server" ControlMode="New" ID="btnSave"></SharePoint:SaveButton></div>
                        </td>
                    </tr>
				</table>
        
	</Template>
</SharePoint:RenderingTemplate>