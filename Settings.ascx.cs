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

using DotNetNuke.Common.Lists;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System;
using System.Web.UI.WebControls;

namespace GIBS.Modules.MealTrackerLite
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from MealTrackerLiteSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : MealTrackerLiteModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    GetRoles();

                    var ctlLists = new ListController();
                    var myLists = ctlLists.GetListInfoCollection(string.Empty, string.Empty, PortalSettings.ActiveTab.PortalID);
                    //ddlLocationList.Items.Clear();

                    ddlLocationList.DataTextField = "Name";
                    ddlLocationList.DataValueField = "Name";
                    ddlLocationList.DataSource = myLists;
                    ddlLocationList.DataBind();
                    ddlLocationList.Items.Insert(0, new ListItem("-Select-", ""));

                    //ddlSeatingList
                    ddlSeatingList.DataTextField = "Name";
                    ddlSeatingList.DataValueField = "Name";
                    ddlSeatingList.DataSource = myLists;
                    ddlSeatingList.DataBind();
                    ddlSeatingList.Items.Insert(0, new ListItem("-Select-", ""));

                }

                if (Settings.Contains("deleteRole"))
                    ddlRoles.SelectedValue = Settings["deleteRole"].ToString();

                if (Settings.Contains("locationsList"))
                    ddlLocationList.SelectedValue = Settings["locationsList"].ToString();

                if (Settings.Contains("seatingList"))
                    ddlSeatingList.SelectedValue = Settings["seatingList"].ToString();


                if (Settings.Contains("jQueryUI"))
                    txtjQueryUI.Text = Settings["jQueryUI"].ToString();

                if (Settings.Contains("deliveryStartTime"))
                    txtDeliveryStartTime.Text = Settings["deliveryStartTime"].ToString();

                if (Settings.Contains("deliveryEndTime"))
                    txtDeliveryEndTime.Text = Settings["deliveryEndTime"].ToString();

                if (Settings.Contains("deliveryInterval"))
                    txtDeliveryInterval.Text = Settings["deliveryInterval"].ToString();

                if (Settings.Contains("calStartDate"))
                    txtCalStartDate.Text = Settings["calStartDate"].ToString();

                if (Settings.Contains("calEndDate"))
                    txtCalEndDate.Text = Settings["calEndDate"].ToString();

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public void GetRoles()
        {
            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();

            var myRoles = rc.GetRoles(this.PortalId);
            //  myRoles
            ddlRoles.DataSource = myRoles;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleName";
            ddlRoles.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem item = new ListItem();
            item.Text = "-- Select Role to Assign --";
            item.Value = "";
            ddlRoles.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlRoles.Items.Remove("Administrators");
            ddlRoles.Items.Remove("Registered Users");
            ddlRoles.Items.Remove("Subscribers");

            //// REPORTS ROLE
            //ddlReportsRoles.DataSource = myRoles;
            //ddlReportsRoles.DataBind();

            //// ADD FIRST (NULL) ITEM
            //ListItem item1 = new ListItem();
            //item1.Text = "-- Select Role to View Reports --";
            //item1.Value = "";
            //ddlReportsRoles.Items.Insert(0, item1);
            //// REMOVE DEFAULT ROLES
            //ddlReportsRoles.Items.Remove("Administrators");
            //ddlReportsRoles.Items.Remove("Registered Users");
            //ddlReportsRoles.Items.Remove("Subscribers");

            //// MERGE ROLE

            //ddlMergeRoles.DataSource = myRoles;
            //ddlMergeRoles.DataBind();

            //// ADD FIRST (NULL) ITEM
            //item1.Value = "Select Role to Allow Merge";
            //ddlMergeRoles.Items.Insert(0, item1);
            //// REMOVE DEFAULT ROLES
            //ddlMergeRoles.Items.Remove("Administrators");
            //ddlMergeRoles.Items.Remove("Registered Users");
            //ddlMergeRoles.Items.Remove("Subscribers");

        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                //module settings

                modules.UpdateModuleSetting(ModuleId, "deleteRole", ddlRoles.SelectedValue.ToString()); 

                modules.UpdateModuleSetting(ModuleId, "jQueryUI", txtjQueryUI.Text);
                modules.UpdateModuleSetting(ModuleId, "locationsList", ddlLocationList.SelectedValue.ToString());
                modules.UpdateModuleSetting(ModuleId, "seatingList", ddlSeatingList.SelectedValue.ToString());
                modules.UpdateModuleSetting(ModuleId, "deliveryStartTime", txtDeliveryStartTime.Text);
                modules.UpdateModuleSetting(ModuleId, "deliveryEndTime", txtDeliveryEndTime.Text);
                modules.UpdateModuleSetting(ModuleId, "deliveryInterval", txtDeliveryInterval.Text);

                modules.UpdateModuleSetting(ModuleId, "calStartDate", txtCalStartDate.Text);
                modules.UpdateModuleSetting(ModuleId, "calEndDate", txtCalEndDate.Text);


            }
            
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}