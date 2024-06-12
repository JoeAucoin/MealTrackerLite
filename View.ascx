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
</script>

<asp:Label ID="lblDebug" runat="server" Visible="false" CssClass="alert alert-warning" />
 <asp:HiddenField ID="HiddenMealID" runat="server" Value="0" />
<asp:HiddenField ID="hfSelecteValue" Value="" runat="server" />

<div class="container">

    	<div class="row form">
                <div class="form-group col-xs-10 col-sm-4 col-md-4 col-lg-4 col-md-offset-1">
            
            <dnn:label id="lblSchoolLocation" runat="server" controlname="ddlLocationID" suffix=":" CssClass="control-label" />
	
	    <asp:DropDownList ID="ddlLocationID" runat="server" CssClass="form-control input-lg" OnSelectedIndexChanged="ddlLocationID_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>	
	<asp:RequiredFieldValidator runat="server" id="reqLocationID" controltovalidate="ddlLocationID" InitialValue="0" Display="Dynamic" errormessage="Required!" resourcekey="reqSchoolLocation" CssClass="NormalRed" />
            
            </div>	
            
        <div class="form-group col-xs-10 col-sm-4 col-md-4 col-lg-4"><dnn:label id="lblSeating" runat="server" controlname="ddlSeating" suffix=":" CssClass="control-label" />
			
    <asp:DropDownList ID="ddlSeating" runat="server" OnSelectedIndexChanged="ddlSeating_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-lg">
    </asp:DropDownList>	
	    <asp:RequiredFieldValidator runat="server" id="reqSeating" resourcekey="reqSeating" controltovalidate="ddlSeating" Display="Dynamic" errormessage="Required!" CssClass="NormalRed" />
        </div>
        <div class="form-group col-xs-6 col-sm-1 col-md-1 col-lg-1 checkbox-lg">
            <dnn:label id="lblDeseCbx" runat="server" controlname="CheckBoxDESE" suffix=":" CssClass="control-label" />
            <asp:CheckBox ID="CheckBoxDESE" runat="server" Enabled="false" CssClass="form-control form-check-input"  />
            </div>
            </div>


        <div class="row form">


        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-2 col-md-offset-1"><dnn:label id="lblMealDate" runat="server" controlname="txtMealDate" suffix=":" for="txtMealDate" CssClass="control-label" />
			<asp:TextBox ID="txtMealDate" runat="server" MaxLength="10" AutoCompleteType="Disabled" ClientIDMode="Static" CssClass="form-control input-lg"></asp:TextBox>
	    <asp:RequiredFieldValidator runat="server" id="reqMealDate" resourcekey="reqMealDate" controltovalidate="txtMealDate" errormessage="Required!" Display="Dynamic" CssClass="NormalRed" />
        </div>

                <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-2">
            
            <dnn:label id="lblDeliveryTime" runat="server" controlname="ddlDeliveryTime" suffix=":" CssClass="control-label" />
			 <asp:DropDownList ID="ddlDeliveryTime" runat="server" CssClass="form-control input-lg">
                 
			 </asp:DropDownList>
	<asp:RequiredFieldValidator runat="server" id="reqDeliveryTime" controltovalidate="ddlDeliveryTime" InitialValue="0" Display="Dynamic" errormessage="Required!" resourcekey="reqDeliveryTime" CssClass="NormalRed" />
            
            </div>

        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1">
            
            <dnn:label id="lblDelivered" runat="server" controlname="txtDelivered" suffix=":" CssClass="control-label" />
	<asp:TextBox ID="txtDelivered" runat="server" ClientIDMode="Static" type="number" pattern="\d*" CssClass="form-control input-lg" /><asp:RequiredFieldValidator runat="server" id="reqDelivered" controltovalidate="txtDelivered" Display="Dynamic" errormessage="Required!" resourcekey="reqDelivered" CssClass="NormalRed" />
            
            </div>
			        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblDamagedIncomplete" runat="server" controlname="txtDamagedIncomplete" suffix=":" />
	<asp:TextBox ID="txtDamagedIncomplete" runat="server" Text="0" type="number" pattern="\d*" CssClass="form-control input-lg" /><asp:RequiredFieldValidator runat="server" id="reqDamagedIncomplete" controltovalidate="txtDamagedIncomplete" Display="Dynamic" errormessage="Required!" resourcekey="reqDamagedIncomplete" CssClass="NormalRed" />
            </div>

        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblFirstsCount" runat="server" controlname="txtFirstsCount" suffix=":" />
	<asp:TextBox ID="txtFirstsCount" runat="server" type="number" pattern="\d*" CssClass="form-control input-lg" /><asp:RequiredFieldValidator runat="server" id="reqFirstsCount" controltovalidate="txtFirstsCount" Display="Dynamic" errormessage="Required!" resourcekey="reqFirstsCount" CssClass="NormalRed" />
            </div>
		
        <div class="form-group col-xs-5 col-sm-3 col-md-3 col-lg-1"><dnn:label id="lblSecondsCount" runat="server" controlname="txtSecondsCount" suffix=":" CssClass="control-label" />
	<asp:TextBox ID="txtSecondsCount" runat="server" type="number" pattern="\d*" CssClass="form-control input-lg" />
	<asp:RequiredFieldValidator runat="server" id="reqSecondsCount" controltovalidate="txtSecondsCount" errormessage="Required!" Display="Dynamic" resourcekey="reqSecondsCount" CssClass="NormalRed" />
            </div>
         <div class="form-group col-xs-5 col-sm-2 col-md-2 col-lg-1"><dnn:label id="lblAdults" runat="server" controlname="txtAdults" suffix=":" CssClass="control-label" />
	<asp:TextBox ID="txtAdults" runat="server" type="number" pattern="\d*" CssClass="form-control input-lg" />
	<asp:RequiredFieldValidator runat="server" id="reqAdults" controltovalidate="txtAdults" errormessage="Required!" Display="Dynamic" resourcekey="reqAdults" CssClass="NormalRed" />
            </div>

    </div>


    	<div class="row form">
	       <div class="form-group col-xs-7 col-sm-7 col-md-7 col-lg-7 col-md-offset-2"><dnn:label id="lblMealNotes" runat="server" controlname="txtMealNotes" suffix=":" CssClass="control-label" />
			<asp:TextBox ID="txtMealNotes" runat="server" TextMode="MultiLine" CssClass="form-control input-lg"></asp:TextBox>      
            </div> 
        

        </div>

    <div class="row form">

            <div class="col-xs-10 col-sm-4 col-md-4 col-lg-4" style="text-align:center;">
        <asp:LinkButton ID="lbSave" runat="server" resourcekey="lbSave" OnClick="LbSaveClick" CssClass="dnnPrimaryAction" />
        <asp:LinkButton ID="lbCancel" runat="server" resourcekey="lbCancel" OnClick="LbCancelClick" CausesValidation="false" CssClass="dnnSecondaryAction" Visible="false"/>
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

        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false" >
         <ItemTemplate>
             <asp:LinkButton ID="LinkButtonUpdate" CausesValidation="false" runat="server" CommandName="DataCommand" CommandArgument='<%# Eval("MealID" )%>'><img src="/Icons/Sigma/Edit_16X16_Standard_2.png" alt="Edit" /></asp:LinkButton>
           
         </ItemTemplate>
            <ItemStyle Width="20px"></ItemStyle>
       </asp:TemplateField>

        <asp:TemplateField HeaderText="" meta:resourcekey="TemplateFieldResource1" ItemStyle-Width="20px">
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

        <asp:BoundField HeaderText="Delivery Time" DataField="DeliveryTime" NullDisplayText="" ItemStyle-Width="80px">
<ItemStyle Width="80px"></ItemStyle>
        </asp:BoundField>

      <asp:BoundField HeaderText="Location" DataField="Location" ItemStyle-Width="170px" />

        <asp:BoundField HeaderText="Seating" DataField="Seating" ItemStyle-Width="140px" />

        <asp:BoundField HeaderText="Delivered" DataField="DeliveredCount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
        </asp:BoundField>

                <asp:BoundField HeaderText="Damaged" DataField="DamagedIncomplete" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
        </asp:BoundField>

		<asp:BoundField HeaderText="Firsts" DataField="FirstsCount" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>
		<asp:BoundField HeaderText="Seconds" DataField="SecondsCount" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center" Width="65px"></ItemStyle>
        </asp:BoundField>
<asp:BoundField HeaderText="Adults" DataField="Adults" ItemStyle-Width="65px" ItemStyle-HorizontalAlign="Center">
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