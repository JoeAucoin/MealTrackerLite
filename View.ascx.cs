/*
' Copyright (c) 2024  GIBS.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System;
using GIBS.Modules.MealTracker;
using GIBS.Modules.MealTracker.Components;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common.Lists;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.UI.Skins;

namespace GIBS.Modules.MealTrackerLite
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from MealTrackerLiteModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : MealTrackerLiteModuleBase
    {
        
        public string _SeatingList = "MTMealSeating";
        public bool _DESE_Breakfast;
        public bool _DESE_Lunch;
        public bool _DESE_Snack;
        public bool _DESE_Snack_PM;
        public string _DeliveryStartTime = "10:00 AM";
        public string _DeliveryEndTime = "02:00 PM";
        public string _DeliveryInterval = "30";

        static string _startDate = "06/24/2024";
        static string _endDate = "09/01/2024";
        static int _mealFloat = 0;

        string _deleteRole = "";


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);

            // ADD STYLESHEET FROM SETTINGS
            HtmlGenericControl css1 = new HtmlGenericControl("link");
            css1.Attributes["type"] = "text/css";
            if (Settings.Contains("jQueryUI"))
            {
                css1.Attributes["href"] = Settings["jQueryUI"].ToString();
            }
            else
            {
                css1.Attributes["href"] = "https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/redmond/jquery-ui.css";
            }
            css1.Attributes["rel"] = "stylesheet";
            css1.Attributes["media"] = "screen";
            Page.Header.Controls.Add(css1);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadSettings();
                    FillDeliveryTimeDropdowns();

                    GridView1.DataSource = MealController.GetAllMealsLite(0, this.PortalId, _startDate.ToString(), _endDate.ToString());
                    GridView1.DataBind();

                    GetDropDownLists();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected string GetStartDate()
        {
            //  return ;

            return _startDate.ToString();
        }

        protected string GetEndDate()
        {
            //  return ;

            return _endDate.ToString();
        }

        protected int GetMealFloat()
        {
            return _mealFloat;

        }

        public void LoadSettings()
        {
            try
            {
              //  GridView1.Rows[1].Visible = false;
             //   GridView1.Columns[1].Visible = false;

                if (Settings.Contains("deleteRole"))
                {
                    _deleteRole = (Settings["deleteRole"].ToString());

                    if (UserId > 0)
                    {
                        DotNetNuke.Entities.Users.UserInfo USERINFO = DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, this.UserId);
                        if (!USERINFO.IsInRole(_deleteRole.ToString()))
                        {
                            GridView1.Columns[1].Visible = false;
                            
                        }
                        else
                        {
                            GridView1.Columns[1].Visible = true;
                        }
                    }

                }



                if (Settings.Contains("seatingList"))
                {
                    _SeatingList = Settings["seatingList"].ToString();

                }


                if (Settings.Contains("deliveryStartTime"))
                {
                    _DeliveryStartTime = Settings["deliveryStartTime"].ToString();

                }
                if (Settings.Contains("deliveryEndTime"))
                {
                    _DeliveryEndTime = Settings["deliveryEndTime"].ToString();

                }
                if (Settings.Contains("deliveryInterval"))
                {
                    _DeliveryInterval = Settings["deliveryInterval"].ToString();

                }

                if (Settings.Contains("calStartDate"))
                {
                    _startDate = Settings["calStartDate"].ToString();

                }
                if (Settings.Contains("calEndDate"))
                {
                    _endDate = Settings["calEndDate"].ToString();

                }

                if (Settings.Contains("mealFloat"))
                {
                    _mealFloat = Int32.Parse(Settings["mealFloat"].ToString());
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void LbCancelClick(object sender, EventArgs e)
        {

        }

        public void FillDeliveryTimeDropdowns()
        {

            try
            {

                DateTime start = DateTime.ParseExact(_DeliveryStartTime.ToString(), "hh:mm tt", null);
                DateTime end = DateTime.ParseExact(_DeliveryEndTime.ToString(), "hh:mm tt", null);

                int interval = Int16.Parse(_DeliveryInterval.ToString());
                List<string> lstTimeIntervals = new List<string>();
                for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
                    lstTimeIntervals.Add(i.ToString("hh:mm tt"));

                ddlDeliveryTime.DataSource = lstTimeIntervals;
                ddlDeliveryTime.DataBind();
                ddlDeliveryTime.Items.Insert(0, new ListItem("-- Select --", "0"));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetDropDownLists()

        {

            try
            {
                           
                List<MealInfo> items;
                MealController controller = new MealController();

                items = MealController.GetLocations("1");

                ddlLocationID.DataTextField = "Location";
                ddlLocationID.DataValueField = "LocationID";
                ddlLocationID.DataSource = items;
                ddlLocationID.DataBind();
                ddlLocationID.Items.Insert(0, new ListItem("-- Please Select --", "0"));
                //  ddlLocationID.SelectedValue = "MA";

                //MTMealSeating  ddlSeating
                var seating = new ListController().GetListEntryInfoItems(_SeatingList, "", this.PortalId);
                ddlSeating.DataTextField = "Text";
                ddlSeating.DataValueField = "Value";
                ddlSeating.DataSource = seating;
                ddlSeating.DataBind();
                ddlSeating.Items.Insert(0, new ListItem("-- Please Select --", ""));

                ddlSeating.Enabled = false;
            }

            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //   int itemID = (int)GridView1.DataKeys[e.RowIndex].Value;

            int itemID = (int)GridView1.DataKeys[e.RowIndex].Value;

            MealController.DeleteMeal(itemID);
            FillGrid();
            

        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int itemID = (int)GridView1.DataKeys[e.RowIndex].Value;


            //     Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "SkinSrc=[G]" + Globals.QueryStringEncode(SkinController.RootSkin + "/" + Globals.glbHostSkinFolder + "/" + "No Skin") + "&mid=" + ModuleId.ToString() + "&ItemId=" + itemID), true);

            // string MyURL =  Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "Edit", "SkinSrc=[G]" + Globals.QueryStringEncode(SkinController.RootSkin + "/" + Globals.glbHostSkinFolder + "/" + "No Skin") + "&mid=" + ModuleId.ToString() + "&ItemId=" + itemID);

        }

        protected void LbSaveClick(object sender, EventArgs e)

        {
            hfSelecteValue.Value = ddlLocationID.SelectedValue.ToString();
            int _hidMealID = Convert.ToInt32(HiddenMealID.Value.ToString());
        //    lblDebug.Text = "Step 1 <br />";
            MealInfo mi;

            if (_hidMealID > 0)
            // UPDATE RECORD
            {


                //lblDebug.Text += "MealID = " + _hidMealID.ToString();
                //lblDebug.Visible=true;

                string _notes = txtMealNotes.Text.ToString();
                PortalSecurity cleanup = new PortalSecurity();
                _notes = cleanup.InputFilter(_notes.ToString(), PortalSecurity.FilterFlag.NoScripting);
                _notes = cleanup.InputFilter(_notes.ToString(), PortalSecurity.FilterFlag.NoMarkup);

                string mealDate = txtMealDate.Text.ToString();
                string deliveredDate = "";

                if (cbxDeliveryPriorDay.Checked)
                {
                    // First, parse the string into a DateTime object
                    if (DateTime.TryParse(mealDate, out DateTime originalDate))
                    {
                        // Now, subtract one day using the AddDays() method
                        DateTime previousDay = originalDate.AddDays(-1);

                        // If you need the result back as a string in a specific format:
                        deliveredDate = previousDay.ToString("MM/dd/yyyy"); // Or any other desired format

                    }

                }
                else
                {
                    deliveredDate = mealDate.ToString();
                }



                MealInfo mi_update;
                mi_update = new MealInfo

                {
                    MealID = Int32.Parse(HiddenMealID.Value.ToString()),
                    DeliveredCount = Convert.ToInt32(txtDelivered.Text.ToString()),
                    FirstsCount = Convert.ToInt32(txtFirstsCount.Text.ToString()),
                    SecondsCount = Convert.ToInt32(txtSecondsCount.Text.ToString()),
                    Adults = Convert.ToInt32(txtAdults.Text.ToString()),
                    DamagedIncomplete = Convert.ToInt16(txtDamagedIncomplete.Text.ToString()),
                    DeliveryTime = deliveredDate.ToString() + " " + ddlDeliveryTime.SelectedValue.ToString(),
                    Short = Convert.ToInt32(txtShort.Text.ToString()),
                    Notes = _notes.ToString()

                };

                mi_update.Update();
                txtDelivered.Text = "";
            }

            else
            // INSERT NEW RECORD
            {
              //  lblDebug.Text += "MealID = not exists <br />";
                


                if (Int32.Parse(txtDelivered.Text.ToString()) > 0)
                {

                    string _notes = txtMealNotes.Text.ToString();
                    PortalSecurity cleanup = new PortalSecurity();
                    _notes = cleanup.InputFilter(_notes.ToString(), PortalSecurity.FilterFlag.NoScripting);
                    _notes = cleanup.InputFilter(_notes.ToString(), PortalSecurity.FilterFlag.NoMarkup);


                    string mealDate = txtMealDate.Text.ToString();
                    string deliveredDate = "";

                    if (cbxDeliveryPriorDay.Checked) 
                    {
                        // First, parse the string into a DateTime object
                        if (DateTime.TryParse(mealDate, out DateTime originalDate))
                        {
                            // Now, subtract one day using the AddDays() method
                            DateTime previousDay = originalDate.AddDays(-1);

                            // If you need the result back as a string in a specific format:
                            deliveredDate = previousDay.ToString("MM/dd/yyyy"); // Or any other desired format

                        }
                      
                    }
                    else 
                    {
                        deliveredDate = mealDate.ToString();
                    }

                    

                    mi = new MealInfo

                    {

                        MealDate = Convert.ToDateTime(txtMealDate.Text.ToString()),
                        Seating = ddlSeating.SelectedValue.ToString(),
                        DeliveredCount = Convert.ToInt32(txtDelivered.Text.ToString()),
                        FirstsCount = Convert.ToInt32(txtFirstsCount.Text.ToString()),
                        SecondsCount = Convert.ToInt32(txtSecondsCount.Text.ToString()),
                        Location = ddlLocationID.SelectedItem.Text.ToString(),
                        LocationID = Int32.Parse(ddlLocationID.SelectedValue.ToString()),
                        Notes = _notes.ToString(),
                        CreatedByUserID = this.UserId,
                        MTPortalID = this.PortalId,
                        Adults = Convert.ToInt32(txtAdults.Text.ToString()),
                        DESE = CheckBoxDESE.Checked
                        ,DeliveryTime = deliveredDate.ToString() + " " + ddlDeliveryTime.SelectedValue.ToString()
                        ,DamagedIncomplete = Int32.Parse(txtDamagedIncomplete.Text.ToString())
                        ,Short = Convert.ToInt32(txtShort.Text.ToString())
                    };

                    mi.Save();

                    LabelResults.Visible = true;
                    LabelResults.Text = "Success";

                }




            }
            ClearForm();
            FillGrid();


        }

        public void ClearForm()
        {

            try
            {
                HiddenMealID.Value = "0";
                ddlLocationID.Enabled = true;
                ddlSeating.Enabled = true;
                txtMealDate.Enabled = true;
                txtMealDate.Text = string.Empty;
                ddlSeating.SelectedValue = null;
                txtFirstsCount.Text = "";
                txtSecondsCount.Text = "";
                txtAdults.Text = "";
                cbxDeliveryPriorDay.Checked = false;
                CheckBoxDESE.Checked = false;
                txtMealNotes.Text = string.Empty;
               
                ddlDeliveryTime.SelectedIndex = -1;
               
                txtDamagedIncomplete.Text = "0";
                txtShort.Text = "0";
               
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void ClearDelivered()
        {

            try
            {

                txtDelivered.Text = "";
                CheckBoxDESE.Checked = false;
                _DESE_Breakfast = false;
                _DESE_Lunch = false;
                _DESE_Snack = false;
                _DESE_Snack_PM = false;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ddlLocationID_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelResults.Text = "";
            
            ddlSeating.Enabled = true;

            ClearDelivered();
            ddlSeating.ClearSelection();
            hfSelecteValue.Value = ddlLocationID.SelectedValue.ToString();
            
            FillGrid();
         //   LoadDeseSettings();


        }

        protected void ddlSeating_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string whatsSelected = ddlSeating.SelectedValue.ToString().Trim();
                LoadDeseSettings();
              
                switch (whatsSelected.ToString())
                {
                    case "Breakfast Seating":
                        CheckBoxDESE.Checked = _DESE_Breakfast;
                        break;
                    case "AM Snack":
                        CheckBoxDESE.Checked = _DESE_Snack;
                        break;

                    case "Lunch Seating":
                        CheckBoxDESE.Checked = _DESE_Lunch;
                        break;
                    case "PM Snack":
                        CheckBoxDESE.Checked = _DESE_Snack_PM;
                        break;

                    default:
                        CheckBoxDESE.Checked = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void LoadDeseSettings()
        {
            try
            {

                MealController controller = new MealController();
                MealInfo item = controller.GetLocationByID(Int32.Parse(hfSelecteValue.Value.ToString()));

                if (item != null)
                {

                    _DESE_Breakfast = item.DESE_Breakfast;
                    _DESE_Lunch = item.DESE_Lunch;
                    _DESE_Snack = item.DESE_Snack;
                    _DESE_Snack_PM = item.DESE_Snack_PM;


                }
                 //  lblDebug.Visible = true;
                 //  lblDebug.Text = "_DESE_Breakfast: " + _DESE_Breakfast;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void LinkButtonCancelUpdate_Click(object sender, EventArgs e)
        {
           
            ClearForm();
            FillGrid();
            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                //  FillInvoiceGrid();
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataSource = MealController.GetAllMeals(Int32.Parse(ddlLocationID.SelectedValue.ToString()), this.PortalId);
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }


        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

           // int itemID = (int)GridView1.DataKeys[e.NewEditIndex].Value;
           //// Panel1.Visible = true;


           // MealController controller = new MealController();
           // MealInfo item = controller.GetMeal(itemID);

           // if (item != null)
           // {
           //     HiddenMealID.Value = item.MealID.ToString();
           //     txtDelivered.Text = item.DeliveredCount.ToString();
           //     txtMealNotes.Text = item.Notes.ToString();
           //     ddlLocationID.SelectedValue = item.LocationID.ToString();
           //     ddlSeating.SelectedValue = item.Seating.ToString();
           // //    LabelLocation.Text = item.Location.ToString();
           // //     LabelMeal.Text = item.Seating.ToString();
           //     txtMealDate.Text = item.MealDate.ToShortDateString();
           //     txtMealDate.Enabled = false;
           //     txtFirstsCount.Text = item.FirstsCount.ToString();
           //     txtSecondsCount.Text = item.SecondsCount.ToString();
           //     txtAdults.Text = item.Adults.ToString();
           //     txtShort.Text = item.Short.ToString();
                
           // }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DataCommand")
            {
                // Get the value of command argument
                var _mealID = e.CommandArgument;

                if (_mealID != null)
                {
                   // Panel1.Visible = true;


                    MealController controller = new MealController();
                    MealInfo item = controller.GetMeal(Int32.Parse(_mealID.ToString()));

                    if (item != null)
                    {
                        HiddenMealID.Value = item.MealID.ToString();
                        txtDelivered.Text = item.DeliveredCount.ToString();
                        txtMealNotes.Text = item.Notes.ToString();
                        ddlLocationID.SelectedValue = item.LocationID.ToString();
                        ddlSeating.SelectedValue = item.Seating.ToString();
                        txtMealDate.Text = item.MealDate.ToShortDateString();
                        txtMealDate.Enabled = false;
                        ddlLocationID.Enabled = false;
                        ddlSeating.Enabled = false;
                        txtFirstsCount.Text = item.FirstsCount.ToString();
                        txtSecondsCount.Text = item.SecondsCount.ToString();
                        txtAdults.Text = item.Adults.ToString();
                        txtDamagedIncomplete.Text = item.DamagedIncomplete.ToString();
                        CheckBoxDESE.Checked = item.DESE;
                      //  ddlDeliveryTime.SelectedValue = item.DeliveryTime.ToString();
                        // ddlDeliveryTimeEdit.SelectedValue = item.DeliveryTime.ToString();
                        if (item.DeliveryTime.ToString().Length > 0)
                        {
                            ListItem lisource = ddlDeliveryTime.Items.FindByValue(item.DeliveryTime);
                            if (lisource != null)
                            {
                                // value found - select it
                                ddlDeliveryTime.SelectedValue = item.DeliveryTime;
                            }
                            else
                            {
                                //Value not found - add it and then select it
                                ddlDeliveryTime.Items.Insert(1, new ListItem(item.DeliveryTime, item.DeliveryTime));
                                ddlDeliveryTime.SelectedValue = item.DeliveryTime;
                            }
                        }
                        if(item.MealDate.ToShortDateString() != item.DeliveryDateTime.ToShortDateString())
                        {
                            cbxDeliveryPriorDay.Checked = true;
                        }


                    }
                }
                
            }
        }

        public void FillGrid()
        {

            try
            {

                GridView1.DataSource = MealController.GetAllMealsLite(Int32.Parse(ddlLocationID.SelectedValue.ToString()), this.PortalId, _startDate.ToString(), _endDate.ToString());
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

       
    }
}