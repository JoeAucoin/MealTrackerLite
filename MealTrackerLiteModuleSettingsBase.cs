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

namespace GIBS.Modules.MealTrackerLite
{
    public class MealTrackerLiteModuleSettingsBase : ModuleSettingsBase
    {
        public string JQueryUI
        {
            get
            {
                if (Settings.Contains("jQueryUI"))
                    return Settings["jQueryUI"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "jQueryUI", value.ToString());
            }
        }

        public string LocationsList
        {
            get
            {
                if (Settings.Contains("locationsList"))
                    return Settings["locationsList"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "locationsList", value.ToString());
            }
        }

        public string SeatingList
        {
            get
            {
                if (Settings.Contains("seatingList"))
                    return Settings["seatingList"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "seatingList", value.ToString());
            }
        }

        public string DeliveryStartTime
        {
            get
            {
                if (Settings.Contains("deliveryStartTime"))
                    return Settings["deliveryStartTime"].ToString();
                return "10:00 AM";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "deliveryStartTime", value.ToString());
            }
        }

        public string DeliveryEndTime
        {
            get
            {
                if (Settings.Contains("deliveryEndTime"))
                    return Settings["deliveryEndTime"].ToString();
                return "02:00 PM";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "deliveryEndTime", value.ToString());
            }
        }

        public string DeliveryInterval
        {
            get
            {
                if (Settings.Contains("deliveryInterval"))
                    return Settings["deliveryInterval"].ToString();
                return "15";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "deliveryInterval", value.ToString());
            }
        }

        public string CalStartDate
        {
            get
            {
                if (Settings.Contains("calStartDate"))
                    return Settings["calStartDate"].ToString();
                return "06/24/2024";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "calStartDate", value.ToString());
            }
        }

        public string CalEndDate
        {
            get
            {
                if (Settings.Contains("calEndDate"))
                    return Settings["calEndDate"].ToString();
                return "09/01/2024";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "calEndDate", value.ToString());
            }
        }


    }
}