<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="GIBS.Modules.MealTrackerLite.View" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<script type="text/javascript">
    $(function () {      
        var start = new Date('<%= GetStartDate() %>');
        var end = new Date('<%= GetEndDate() %>');

        $('#txtMealDate').datepicker({
            beforeShowDay: $.datepicker.noWeekends,
            minDate: start,
            maxDate: end       
        });  
    });

    function validateDamaged(source, args) {
        var delivered = parseInt(document.getElementById('<%= txtDelivered.ClientID %>').value);
        var damaged = parseInt(document.getElementById('<%= txtDamagedIncomplete.ClientID %>').value);

        if (!isNaN(delivered) && !isNaN(damaged)) {
            args.IsValid = (damaged <= delivered);
        } else {
            args.IsValid = true; // Don't invalidate if either field is not a number (let RequiredFieldValidator handle empty)
        }
    }

    function validateFirsts(source, args) {
        var delivered = parseInt(document.getElementById('<%= txtDelivered.ClientID %>').value);
        var damaged = parseInt(document.getElementById('<%= txtDamagedIncomplete.ClientID %>').value);
        var firsts = parseInt(document.getElementById('<%= txtFirstsCount.ClientID %>').value);

        if (!isNaN(delivered) && !isNaN(damaged) && !isNaN(firsts)) {
            args.IsValid = (firsts <= (delivered - damaged));
        } else {
            args.IsValid = true; // Don't invalidate if any field is not a number (let RequiredFieldValidator handle empty)
        }
    }

    function validateSeconds(source, args) {
        var delivered = parseInt(document.getElementById('<%= txtDelivered.ClientID %>').value);
        var damaged = parseInt(document.getElementById('<%= txtDamagedIncomplete.ClientID %>').value);
        var firsts = parseInt(document.getElementById('<%= txtFirstsCount.ClientID %>').value);
        var seconds = parseInt(document.getElementById('<%= txtSecondsCount.ClientID %>').value);

        if (!isNaN(delivered) && !isNaN(damaged) && !isNaN(firsts) && !isNaN(seconds)) {
            args.IsValid = (seconds <= (delivered - damaged - firsts));
        } else {
            args.IsValid = true; // Don't invalidate if any field is not a number (let RequiredFieldValidator handle empty)
        }
    }

    function validateAdults(source, args) {
        var delivered = parseInt(document.getElementById('<%= txtDelivered.ClientID %>').value);
        var damaged = parseInt(document.getElementById('<%= txtDamagedIncomplete.ClientID %>').value);
        var firsts = parseInt(document.getElementById('<%= txtFirstsCount.ClientID %>').value);
        var seconds = parseInt(document.getElementById('<%= txtSecondsCount.ClientID %>').value);
        var adults = parseInt(document.getElementById('<%= txtAdults.ClientID %>').value);

        if (!isNaN(delivered) && !isNaN(damaged) && !isNaN(firsts) && !isNaN(seconds) && !isNaN(adults)) {
            args.IsValid = (adults <= (delivered - damaged - firsts - seconds));
        } else {
            args.IsValid = true; // Don't invalidate if any field is not a number (let RequiredFieldValidator handle empty)
        }
    }

    $(function () {
        $("#hide-it").hide(15000);
    });        

</script>
<div><asp:Label ID="lblDebug" runat="server" Visible="false" />
    </div>
 <asp:HiddenField ID="HiddenMealID" runat="server" Value="0" />
<asp:HiddenField ID="hfSelecteValue" Value="0" runat="server" />

<div class="container">

    	<div class="row form">
                <div class="form-group col-xs-11 col-sm-5 col-md-4 col-lg-4">
            
            <dnn:label id="lblSchoolLocation" runat="server" controlname="ddlLocationID" suffix=":" CssClass="control-label" />
	
	    <asp:DropDownList ID="ddlLocationID" runat="server" CssClass="form-control input-lg" OnSelectedIndexChanged="ddlLocationID_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>	
	<asp:RequiredFieldValidator runat="server" id="reqLocationID" controltovalidate="ddlLocationID" InitialValue="0" Display="Dynamic" errormessage="Required!" resourcekey="reqSchoolLocation" CssClass="NormalRed" />
            
            </div>	
            
        <div class="form-group col-xs-11 col-sm-5 col-md-4 col-lg-4"><dnn:label id="lblSeating" runat="server" controlname="ddlSeating" suffix=":" CssClass="control-label" />
			
    <asp:DropDownList ID="ddlSeating" runat="server" OnSelectedIndexChanged="ddlSeating_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-lg">
    </asp:DropDownList>	
	    <asp:RequiredFieldValidator runat="server" id="reqSeating" resourcekey="reqSeating" controltovalidate="ddlSeating" Display="Dynamic" errormessage="Required!" CssClass="NormalRed" />
        </div>
        <div class="form-group col-xs-6 col-sm-2 col-md-1 col-lg-1 checkbox-lg">
            <dnn:label id="lblDeseCbx" runat="server" controlname="CheckBoxDESE" suffix=":" CssClass="control-label" />
            <asp:CheckBox ID="CheckBoxDESE" runat="server" Enabled="false" CssClass="form-control form-check-input"  />
            </div>
            </div>


        <div class="row form">


        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-2"><dnn:label id="lblMealDate" runat="server" controlname="txtMealDate" suffix=":" for="txtMealDate" CssClass="control-label" />
			<asp:TextBox ID="txtMealDate" runat="server" MaxLength="10" AutoCompleteType="Disabled" ClientIDMode="Static" CssClass="form-control input-lg"></asp:TextBox>
	    <asp:RequiredFieldValidator runat="server" id="reqMealDate" resourcekey="reqMealDate" controltovalidate="txtMealDate" errormessage="Required!" Display="Dynamic" CssClass="NormalRed" />
        </div>

                <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-2">
            
            <dnn:label id="lblDeliveryTime" runat="server" controlname="ddlDeliveryTime" suffix=":" CssClass="control-label" />
			 <asp:DropDownList ID="ddlDeliveryTime" runat="server" CssClass="form-control input-lg">
                 
			 </asp:DropDownList>
	<asp:RequiredFieldValidator runat="server" id="reqDeliveryTime" controltovalidate="ddlDeliveryTime" InitialValue="0" Display="Dynamic" errormessage="Required!" resourcekey="reqDeliveryTime" CssClass="NormalRed" />
            
            </div>

            <div class="form-group col-xs-4 col-sm-3 col-md-3 col-lg-1">
                <dnn:label id="lblDeliveryPriorDay" runat="server" controlname="cbxDeliveryPriorDay" suffix=":" CssClass="control-label" />
                <asp:CheckBox ID="cbxDeliveryPriorDay" runat="server" CssClass="form-control checkbox-lg" />
                </div>


        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1">
            
            <dnn:label id="lblDelivered" runat="server" controlname="txtDelivered" suffix=":" CssClass="control-label" />
	<asp:TextBox ID="txtDelivered" runat="server" ClientIDMode="Static" type="number" pattern="\d*" CssClass="form-control input-lg" />
            <asp:RequiredFieldValidator runat="server" id="reqDelivered" controltovalidate="txtDelivered" Display="Dynamic" errormessage="Required!" resourcekey="reqDelivered" CssClass="NormalRed" />
             
            <asp:RangeValidator ID="rvDelivered" runat="server" ControlToValidate="txtDelivered" MinimumValue="0" MaximumValue="200" Type="Integer" ErrorMessage="Please enter a number between 1 and 200." Display="Dynamic" CssClass="NormalRed" resourcekey="rvDeliveredPositive" />	
            </div>
			        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblDamagedIncomplete" runat="server" controlname="txtDamagedIncomplete" suffix=":" />
	<asp:TextBox ID="txtDamagedIncomplete" runat="server" Text="0" type="number" pattern="\d*" CssClass="form-control input-lg" />
    <asp:RequiredFieldValidator runat="server" id="reqDamagedIncomplete" controltovalidate="txtDamagedIncomplete" Display="Dynamic" errormessage="Required!" resourcekey="reqDamagedIncomplete" CssClass="NormalRed" />
<asp:RangeValidator ID="rvDamaged" runat="server" ControlToValidate="txtDamagedIncomplete" MinimumValue="0" MaximumValue="200" Type="Integer" ErrorMessage="Please enter a number zero or greater." Display="Dynamic" CssClass="NormalRed" resourcekey="rvDamagedPositive" />	
           <asp:CustomValidator ID="cvDamagedVsDelivered" runat="server" ControlToValidate="txtDamagedIncomplete" ClientValidationFunction="validateDamaged" ErrorMessage="Damaged cannot be greater than Delivered." Display="Dynamic" CssClass="NormalRed" resourcekey="cvDamagedVsDelivered" />

			        </div>

        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblFirstsCount" runat="server" controlname="txtFirstsCount" suffix=":" />
	<asp:TextBox ID="txtFirstsCount" runat="server" type="number" pattern="\d*" CssClass="form-control input-lg" /><asp:RequiredFieldValidator runat="server" id="reqFirstsCount" controltovalidate="txtFirstsCount" Display="Dynamic" errormessage="Required!" resourcekey="reqFirstsCount" CssClass="NormalRed" />
            <asp:CustomValidator ID="cvFirstsVsTotal" runat="server" ControlToValidate="txtFirstsCount" ClientValidationFunction="validateFirsts" ErrorMessage="Firsts Count cannot be greater than Delivered - Damaged." Display="Dynamic" CssClass="NormalRed" resourcekey="cvFirstsVsTotal" />
<asp:RangeValidator ID="rvFirsts" runat="server" ControlToValidate="txtFirstsCount" MinimumValue="0" MaximumValue="200" Type="Integer" ErrorMessage="Please enter a number greater than zero and less then 200." Display="Dynamic" CssClass="NormalRed" resourcekey="rvFirstsPositive" />	            
        </div>
		
        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblSecondsCount" runat="server" controlname="txtSecondsCount" suffix=":" CssClass="control-label" />
	<asp:TextBox ID="txtSecondsCount" runat="server" type="number" pattern="\d*" CssClass="form-control input-lg" />
	<asp:RequiredFieldValidator runat="server" id="reqSecondsCount" controltovalidate="txtSecondsCount" errormessage="Required!" Display="Dynamic" resourcekey="reqSecondsCount" CssClass="NormalRed" />
             <asp:CustomValidator ID="cvSecondsVsRemaining" runat="server" ControlToValidate="txtSecondsCount" ClientValidationFunction="validateSeconds" ErrorMessage="Seconds Count cannot be greater than Delivered - Damaged - Firsts." Display="Dynamic" CssClass="NormalRed" resourcekey="cvSecondsVsRemaining" />
<asp:RangeValidator ID="rvSeconds" runat="server" ControlToValidate="txtSecondsCount" MinimumValue="0" MaximumValue="99" Type="Integer" ErrorMessage="Please enter 0 or a positive number." Display="Dynamic" CssClass="NormalRed" resourcekey="rvSecondsPositive" />           
            </div>
         <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblAdults" runat="server" controlname="txtAdults" suffix=":" CssClass="control-label" />
	<asp:TextBox ID="txtAdults" runat="server" type="number" pattern="\d*" CssClass="form-control input-lg" />
	<asp:RequiredFieldValidator runat="server" id="reqAdults" controltovalidate="txtAdults" errormessage="Required!" Display="Dynamic" resourcekey="reqAdults" CssClass="NormalRed" />
             <asp:CustomValidator ID="cvAdultsVsRemaining" runat="server" ControlToValidate="txtAdults" ClientValidationFunction="validateAdults" ErrorMessage="Adults Count cannot be greater than Delivered - Damaged - Firsts - Seconds." Display="Dynamic" CssClass="NormalRed" resourcekey="cvAdultsVsRemaining" />
<asp:RangeValidator ID="rvAdults" runat="server" ControlToValidate="txtAdults" MinimumValue="0" MaximumValue="99" Type="Integer" ErrorMessage="Please enter 0 or a positive number." Display="Dynamic" CssClass="NormalRed" resourcekey="rvAdultsPositive" />
            </div>
     <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblShort" runat="server" controlname="txtShort" suffix=":" CssClass="control-label" />
<asp:TextBox ID="txtShort" runat="server" Text="0" type="number" pattern="\d*" CssClass="form-control input-lg" />
<asp:RangeValidator ID="rvShort" runat="server" ControlToValidate="txtShort" MinimumValue="0" MaximumValue="99" Type="Integer" ErrorMessage="Please enter 0 or a positive number." Display="Dynamic" CssClass="NormalRed" resourcekey="rvShortPositive" />
        </div>

    </div>


    	<div class="row form">
	       <div class="form-group col-xs-11 col-sm-10 col-md-7 col-lg-7 col-md-offset-1"><dnn:label id="lblMealNotes" runat="server" controlname="txtMealNotes" suffix=":" CssClass="control-label" />
			<asp:TextBox ID="txtMealNotes" runat="server" TextMode="MultiLine" CssClass="form-control input-lg"></asp:TextBox>      
            </div> 
        

        </div>

    <div class="row form">

            <div class="col-xs-10 col-sm-4 col-md-4 col-lg-4" style="text-align:center;">
        <asp:LinkButton ID="lbSave" runat="server" resourcekey="lbSave" OnClick="LbSaveClick" CssClass="dnnPrimaryAction" />
        <asp:LinkButton ID="lbCancel" runat="server" resourcekey="lbCancel" OnClick="LbCancelClick" CausesValidation="false" CssClass="dnnSecondaryAction" Visible="false"/>
              <div id="hide-it"><asp:label runat="server" CssClass="msgSuccess" ID="LabelResults"></asp:label></div>
        </div>
          <div class="clearfix"></div>

	</div>

</div>


<div class="table-responsive">
<asp:GridView ID="GridView1" runat="server" EnableModelValidation="True" 
    DataKeyNames="MealID" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"    
    resourcekey="GridView1Resource1" OnPageIndexChanging="GridView1_PageIndexChanging" 
     AllowPaging="True" PageSize="20" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list" PagerStyle-CssClass="pagination-ys"   
    GridLines="None">
   
    <Columns>

        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true" >
         <ItemTemplate>
             <asp:LinkButton ID="LinkButtonUpdate" CausesValidation="false" runat="server" CommandName="DataCommand" CommandArgument='<%# Eval("MealID" )%>'><img src="/Icons/Sigma/Edit_32X32_Standard.png" alt="Edit" /></asp:LinkButton>
           
         </ItemTemplate>
            <ItemStyle Width="20px"></ItemStyle>
       </asp:TemplateField>

        <asp:TemplateField HeaderText="" meta:resourcekey="TemplateFieldResource1" ItemStyle-Width="20px" Visible="false">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="false"     
             CommandArgument='<%# Eval("MealID") %>' 
             CommandName="Delete" runat="server" meta:resourcekey="LinkButtonEditResource1">
             <img src="/Icons/Sigma/Delete_32x32_Standard.png" alt="Delete" /></asp:LinkButton>
         </ItemTemplate>

<ItemStyle Width="20px"></ItemStyle>
       </asp:TemplateField>


      <asp:BoundField HeaderText="Date" DataField="MealDate" DataFormatString="{0:dddd, MM/dd/yyyy}"  ItemStyle-Width="90px">
<ItemStyle Width="90px"></ItemStyle>
        </asp:BoundField>

        <asp:BoundField HeaderText="Delivery Time" DataField="DeliveryTime" NullDisplayText="" HtmlEncode="false" ItemStyle-Width="80px">
<ItemStyle Width="105px"></ItemStyle>
        </asp:BoundField>

      <asp:BoundField HeaderText="Location" DataField="Location" ItemStyle-Width="170px" />

        <asp:BoundField HeaderText="Seating" DataField="Seating" ItemStyle-Width="140px" />

        <asp:BoundField HeaderText="Delivered" DataField="DeliveredCount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
        </asp:BoundField>

                <asp:BoundField HeaderText="Damaged" DataField="DamagedIncomplete" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
        </asp:BoundField>

		<asp:BoundField HeaderText="Firsts" DataField="FirstsCount" ItemStyle-Width="55px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>
		<asp:BoundField HeaderText="Seconds" DataField="SecondsCount" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>
<asp:BoundField HeaderText="Adults" DataField="Adults" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>

<asp:BoundField HeaderText="Left Overs" DataField="LeftOvers" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>

<asp:BoundField HeaderText="Short" DataField="Short" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>

<asp:CheckBoxField DataField="DESE" HeaderText="DESE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="55px" />
        <asp:TemplateField HeaderText="Notes">
            <ItemTemplate>
            <asp:Label ID="lblNotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Notes").ToString().TrimEnd() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
    
</asp:GridView>
    </div>

<div><small>(1) - Prior Day Delivery</small></div>