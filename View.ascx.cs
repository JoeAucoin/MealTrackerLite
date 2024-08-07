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
        static string _LocationsList = "MTLocations";
        static string _SeatingList = "MTMealSeating";
        static bool _DESE_Breakfast = false;
        static bool _DESE_Lunch = false;
        static bool _DESE_Snack = false;
        static bool _DESE_Snack_PM = false;
        public string _DeliveryStartTime = "10:00 AM";
        public string _DeliveryEndTime = "02:00 PM";
        public string _DeliveryInterval = "30";

        static string _startDate = "06/24/2024";
        static string _endDate = "09/01/2024";


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

        public void LoadSettings()
        {
            try
            {

                if (Settings.Contains("locationsList"))
                {
                    _LocationsList = Settings["locationsList"].ToString();

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
                // Get State Dropdown from DNN Lists

                // var regions = new ListController().GetListEntryInfoItems(_LocationsList, "", this.PortalId);
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
            //  Response.Redirect(Globals.NavigateURL(TabId));
            //FillGrid();

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
            lblDebug.Text = "Step 1 <br />";
            MealInfo mi;

            if (_hidMealID > 0)

            {
                // DO UPDATE HERE 
                lblDebug.Text += "MealID = 0 <br />";
                lblDebug.Visible=true; 
              //  mi = new MealInfo();
                //    mi = ArticleController.GetArticle(ArticleId);
                //mi.MealDate = Convert.ToDateTime(txtMealDate.Text.ToString());
                //mi.Seating = txtPlatesServed.Text.ToString();
                //mi.ChildCount = txtChildCount.Text.ToString();
                //mi.LastModifiedOnDate = DateTime.Now;
                //mi.LastModifiedByUserId = UserInfo.UserID;
                //mi.ModuleId = ModuleId;

            }

            else

            {
                lblDebug.Text += "MealID = not exists <br />";
                


                if (Int32.Parse(txtDelivered.Text.ToString()) > 0)
                {

                    string _notes = txtMealNotes.Text.ToString();
                    PortalSecurity cleanup = new PortalSecurity();
                    _notes = cleanup.InputFilter(_notes.ToString(), PortalSecurity.FilterFlag.NoScripting);
                    _notes = cleanup.InputFilter(_notes.ToString(), PortalSecurity.FilterFlag.NoMarkup);

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
                        ,DeliveryTime = txtMealDate.Text.ToString() + " " + ddlDeliveryTime.SelectedValue.ToString()
                        ,DamagedIncomplete = Int32.Parse(txtDamagedIncomplete.Text.ToString())

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
                txtMealDate.Text = string.Empty;
                ddlSeating.SelectedValue = null;
                txtFirstsCount.Text = "";
                txtSecondsCount.Text = "";
                txtAdults.Text = "";
              
                CheckBoxDESE.Checked = false;
                txtMealNotes.Text = string.Empty;
               
                ddlDeliveryTime.SelectedIndex = -1;
               
                txtDamagedIncomplete.Text = "0";
               
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
            ClearDelivered();
            hfSelecteValue.Value = ddlLocationID.SelectedValue.ToString();
            //ddlLocationID.Items.FindByValue(hfSelecteValue.Value.ToString()).Selected = true;
            FillGrid();
            LoadDeseSettings();


        }

        protected void ddlSeating_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string whatsSelected = ddlSeating.SelectedValue.ToString();
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
                //   lblDebug.Visible = true;
                //   lblDebug.Text = "_DESE_Breakfast: " + _DESE_Breakfast;
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

            int itemID = (int)GridView1.DataKeys[e.NewEditIndex].Value;
           // Panel1.Visible = true;


            MealController controller = new MealController();
            MealInfo item = controller.GetMeal(itemID);

            if (item != null)
            {
                HiddenMealID.Value = item.MealID.ToString();
                txtDelivered.Text = item.DeliveredCount.ToString();
                txtMealNotes.Text = item.Notes.ToString();
                ddlLocationID.SelectedValue = item.LocationID.ToString();
                ddlSeating.SelectedValue = item.Seating.ToString();
            //    LabelLocation.Text = item.Location.ToString();
            //     LabelMeal.Text = item.Seating.ToString();
                txtMealDate.Text = item.MealDate.ToShortDateString();
                txtFirstsCount.Text = item.FirstsCount.ToString();
                txtSecondsCount.Text = item.SecondsCount.ToString();
                txtAdults.Text = item.Adults.ToString();
            }

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

                    }
                }
                // Do whatever operation you want.  
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

        //public ModuleActionCollection ModuleActions
        //{
        //    get
        //    {
        //        var actions = new ModuleActionCollection
        //            {
        //                {
        //                    GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
        //                    EditUrl(), false, SecurityAccessLevel.Edit, true, false
        //                }
        //            };
        //        return actions;
        //    }
        //}
    }
}