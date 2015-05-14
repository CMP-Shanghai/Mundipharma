<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPublicForm.ascx.cs" Inherits="MR.SP.DueDiligence.Pages.CONTROLTEMPLATES.CustomPublicForm" %>



        
		
				<tr>
				<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Project ID</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody" colspan="3">
							<%= itemID %>
						</td>				
				</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Opportunity<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField1"  FieldName="Title" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription1" FieldName="Title" />
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Form<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField2"  FieldName="Form" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription2" FieldName="Form" />
						</td>
						
					</tr>
					<tr>
					<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Opportunity Description<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField3"  FieldName="Opportunity_x0020_Description" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription3" FieldName="Opportunity_x0020_Description" 

/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Company<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField4"  FieldName="Company" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription4" FieldName="Company" />
						</td>
						
					</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Next Step</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField5"  FieldName="Next_x0020_Step" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription5" FieldName="Next_x0020_Step" />
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Next Deadline</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField6"  FieldName="Next_x0020_Deadline" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription6" FieldName="Next_x0020_Deadline" 

/>
						</td>
					</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Status</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField7"  FieldName="Status"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription7" FieldName="Status" />
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Outcome</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField8"  FieldName="Outcome" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription8" FieldName="Outcome" />
						</td>
					</tr>
					<tr>
					<td colspan="4"><input style="background-color:red;color:white; width:95%;margin-top:10px;margin-bottom:10px;" onclick="javascript: sendEmail();" type="button" value="Email Team"/> </td>
					</tr>
					<tr>
					<td colspan="4">




<table width="100%" class="docDateTab" cellspacing="1">
					<tr>
					<td class="docDateTabTdLeft">Process Checklist</td><td class="docDateTabTdLeft">Planned</td><td class="docDateTabTdLeft">Actual</td>
					</tr>
					<!--Check list start-->
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>KO Meeting</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField11" FieldName="KO_x0020_Meeting_x0020_Planned" />
				<SharePoint:FieldDescription runat="server" id="FieldDescription11" FieldName="KO_x0020_Meeting_x0020_Planned"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField12" FieldName="KO_x0020_Meeting_x0020_Actual" />
				<SharePoint:FieldDescription runat="server" id="FieldDescription12" FieldName="KO_x0020_Meeting_x0020_Actual"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>BC1 complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField15" FieldName="BC1_x0020_complete_x0020_Planned" />
				<SharePoint:FieldDescription runat="server" id="FieldDescription15" FieldName="BC1_x0020_complete_x0020_Planned"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField16" FieldName="BC1_x0020_complete_x0020_Actual" />
				<SharePoint:FieldDescription runat="server" id="FieldDescription16" FieldName="BC1_x0020_complete_x0020_Actual"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>CFPA complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField19" FieldName="CFPA_x0020_complete_x0020_Planne" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription19" FieldName="CFPA_x0020_complete_x0020_Planne"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField20" FieldName="CFPA_x0020_complete_x0020_Actual" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription20" FieldName="CFPA_x0020_complete_x0020_Actual"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>TPP complete</nobr>
				</H3>
				</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField23" FieldName="TPP_x0020_complete_x0020_Planned" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription23" FieldName="TPP_x0020_complete_x0020_Planned"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField24" FieldName="TPP_x0020_complete_x0020_Actual" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription24" FieldName="TPP_x0020_complete_x0020_Actual"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>SWOT complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
					<SharePoint:FormField runat="server" id="FormField27" FieldName="SWOT_x0020_complete_x0020_Planne" />

					<SharePoint:FieldDescription runat="server" id="FieldDescription27" FieldName="SWOT_x0020_complete_x0020_Planne" 

/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField28" FieldName="SWOT_x0020_complete_x0020_Actual" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription28" FieldName="SWOT_x0020_complete_x0020_Actual"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>Document requests complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField31" FieldName="Document_x0020_requests_x0020_co" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription31" FieldName="Document_x0020_requests_x0020_co"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField32" FieldName="Document_x0020_requests_x0020_co0" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription32" FieldName="Document_x0020_requests_x0020_co0"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>Questions complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField35" FieldName="Questions_x0020_complete_x0020_P" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription35" FieldName="Questions_x0020_complete_x0020_P"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField36" FieldName="Questions_x0020_complete_x0020_A" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription36" FieldName="Questions_x0020_complete_x0020_A"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>F2F Agenda complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField39" FieldName="F2F_x0020_Agenda_x0020_complete_" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription39" FieldName="F2F_x0020_Agenda_x0020_complete_"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField40" FieldName="F2F_x0020_Agenda_x0020_complete_0" /> 

				<SharePoint:FieldDescription runat="server" id="FieldDescription40" FieldName="F2F_x0020_Agenda_x0020_complete_0"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>Meeting minutes complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField43" FieldName="Meeting_x0020_minutes_x0020_comp" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription43" FieldName="Meeting_x0020_minutes_x0020_comp"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField44" FieldName="Meeting_x0020_minutes_x0020_comp0" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription44" FieldName="Meeting_x0020_minutes_x0020_comp0"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>DD Report Draft 1 complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField47" FieldName="DD_x0020_Report_x0020_Draft_x002" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription47" FieldName="DD_x0020_Report_x0020_Draft_x002"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField48" FieldName="DD_x0020_Report_x0020_Draft_x0021" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription48" FieldName="DD_x0020_Report_x0020_Draft_x0021"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>DD Report Draft 2 complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField51" FieldName="DD_x0020_Report_x0020_Draft_x0020" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription51" FieldName="DD_x0020_Report_x0020_Draft_x0020"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField52" FieldName="DD_x0020_Report_x0020_Draft_x0022" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription52" FieldName="DD_x0020_Report_x0020_Draft_x0022"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>DD Report Final Draft completecomplete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField55" FieldName="DD_x0020_Report_x0020_Final_x002" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription55" FieldName="DD_x0020_Report_x0020_Final_x002"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField56" FieldName="DD_x0020_Report_x0020_Final_x0021" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription56" FieldName="DD_x0020_Report_x0020_Final_x0021"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>DD Report FINAL</nobr>
				</H3>
				</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField59" FieldName="DD_x0020_Report_x0020_FINAL_x0020" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription59" FieldName="DD_x0020_Report_x0020_FINAL_x0020"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField60" FieldName="DD_x0020_Report_x0020_FINAL_x0022" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription60" FieldName="DD_x0020_Report_x0020_FINAL_x0022"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>S&amp;TC presentation draft 1 complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField63" FieldName="S_x0026_TC_x0020_presentation_x0" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription63" FieldName="S_x0026_TC_x0020_presentation_x0"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField64" FieldName="S_x0026_TC_x0020_presentation_x01" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription64" FieldName="S_x0026_TC_x0020_presentation_x01"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>S&amp;TC Presentation FINAL</nobr>
			</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField67" FieldName="S_x0026_TC_x0020_Presentation_x00" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription67" FieldName="S_x0026_TC_x0020_Presentation_x00"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField68" FieldName="S_x0026_TC_x0020_Presentation_x02" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription68" FieldName="S_x0026_TC_x0020_Presentation_x02"/>
			</td>
			</tr>
		
			<tr>
			<td width="190px" valign="top" class="ms-formlabel">
				<H3 class="ms-standardheader">
				<nobr>BC2 complete</nobr>
				</H3>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField71" FieldName="BC2_x0020_complete_x0020_Planned" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription71" FieldName="BC2_x0020_complete_x0020_Planned"/>
			</td>
			<td width="400px" valign="top" class="ms-formbody">
				<SharePoint:FormField runat="server" id="FormField72" FieldName="BC2_x0020_complete_x0020_Planned" />

				<SharePoint:FieldDescription runat="server" id="FieldDescription72" FieldName="BC2_x0020_complete_x0020_Planned"/>
			</td>
			</tr>
						
					<!--Check list end-->		
					</table>








</td>				
					</tr>										
					<tr>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Start<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField73"  FieldName="Start" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription73" FieldName="Start" />
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Planned End</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField74"  FieldName="Plan_x0020_End" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription74" FieldName="Plan_x0020_End" />
						</td>
					</tr>
					<tr>
					<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Onsite Visit Date</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField75"  FieldName="Onsite_x0020_Visit_x0020_Date" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription75" FieldName="Onsite_x0020_Visit_x0020_Date" 

/>
						</td>
					<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Actual End</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField76"  FieldName="Actual_x0020_End" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription76" FieldName="Actual_x0020_End" />
						</td>	
					</tr>
					<tr>
						
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>BD Lead<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField77"  FieldName="BD_x0020_Lead" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription77" FieldName="BD_x0020_Lead" />
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Commercial Head<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField78"  FieldName="Commercial_x0020_Head" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription78" FieldName="Commercial_x0020_Head" 

/>
						</td>
						</tr>
						<tr>				
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>R&amp;D Executive Director<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField79"  FieldName="R_x0026_D_x0020_Executive_x0020_" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription79" FieldName="R_x0026_D_x0020_Executive_x0020_" 

/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Therapeutic Area<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField80"  FieldName="Therapeutic_x0020_Area"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription80" FieldName="Therapeutic_x0020_Area" 

/>
						</td>
					</tr>
					<tr>
					<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>No of Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField81"  FieldName="No_x0020_of_x0020_Participants"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription81" FieldName="No_x0020_of_x0020_Participants" 

/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField82"  FieldName="Participants" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription82" FieldName="Participants" />
						</td>
						
					</tr>					
					<tr>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Need External Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField83"  FieldName="Need_x0020_External_x0020_Partic" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription83" FieldName="Need_x0020_External_x0020_Partic" 

/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>External Participants</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField84"  FieldName="External_x0020_Participants"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription84" FieldName="External_x0020_Participants" 

/>
						</td>
					</tr>
					<tr>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Territories<span class="ms-formvalidation"> *</span>
								</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField85"  FieldName="Territories"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription85" FieldName="Territories" />
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Country</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField86"  FieldName="Country" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription86" FieldName="Country" />
						</td>
					</tr>
					
					<tr>
					<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>e Room Details</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField87"  FieldName="e_x0020_Room_x0020_Details"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription87" FieldName="e_x0020_Room_x0020_Details" 

/>
						</td>
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Comments</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField88"  FieldName="Comments" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription88" FieldName="Comments" />
						</td>
						
					</tr>
					<tr>
					<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Additional Comments</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField89"  FieldName="Additional_x0020_Comments" ></SharePoint:FormField>

							<SharePoint:FieldDescription runat="server" id="FieldDescription89" FieldName="Additional_x0020_Comments" 

/>
						</td>
						
						<td width="190px" valign="top" class="ms-formlabel tdLeft">
							<H3 class="ms-standardheader">
								<nobr>Review Date</nobr>
							</H3>
						</td>
						<td width="400px" valign="top" class="ms-formbody">
							<SharePoint:FormField runat="server" id="FormField90"  FieldName="Review_x0020_Date"></SharePoint:FormField> 

							<SharePoint:FieldDescription runat="server" id="FieldDescription90" FieldName="Review_x0020_Date" 

/>
						</td>
					</tr>

					
			