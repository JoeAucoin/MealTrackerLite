<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.MealTrackerLite.Settings" %>


<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
    <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lbljQueryUI" ControlName="txtjQueryUI" ResourceKey="lbljQueryUI" Suffix=":" />
        <asp:Textbox ID="txtjQueryUI" runat="server" />
           
     </div>	

	<div class="dnnFormItem">
    
        <dnn:label id="lblDeleteRole" runat="server" controlname="ddlRoles" suffix=":"></dnn:label>
        <asp:DropDownList ID="ddlRoles" runat="server" datavaluefield="RoleName" datatextfield="RoleName" CssClass="form-control input-lg">
        </asp:DropDownList>
	</div>


	<div class="dnnFormItem" style="display:none;">
        <dnn:Label runat="server" ID="lblLocationsList" ControlName="Locations" ResourceKey="lblLocationsList" Suffix=":" />
        <asp:DropDownList ID="ddlLocationList" runat="server" CssClass="form-control input-lg">
    </asp:DropDownList>	
           
     </div>
	<div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblSeatingList" ControlName="ddlSeatingList" ResourceKey="lblSeatingList" Suffix=":" />
            <asp:DropDownList ID="ddlSeatingList" runat="server" CssClass="form-control input-lg">
    </asp:DropDownList>	
           
     </div>	

    <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblDeliveryStartTime" ControlName="txtDeliveryStartTime" ResourceKey="lblDeliveryStartTime" Suffix=":" />
        <asp:Textbox ID="txtDeliveryStartTime" runat="server" />
           
     </div>	

    <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblDeliveryEndTime" ControlName="txtDeliveryEndTime" ResourceKey="lblDeliveryEndTime" Suffix=":" />
        <asp:Textbox ID="txtDeliveryEndTime" runat="server" />
           
     </div>	

    <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblDeliveryInterval" ControlName="txtDeliveryInterval" ResourceKey="lblDeliveryInterval" Suffix=":" />
        <asp:Textbox ID="txtDeliveryInterval" runat="server" />
           
     </div>	


    <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblCalStartDate" ControlName="txtCalStartDate" ResourceKey="lblCalStartDate" Suffix=":" />
        <asp:Textbox ID="txtCalStartDate" runat="server" />
           
     </div>	

    <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblCalEndDate" ControlName="txtCalEndDate" ResourceKey="lblCalEndDate" Suffix=":" />
        <asp:Textbox ID="txtCalEndDate" runat="server" />
           
     </div>	

    </fieldset>


