using UnityEngine;

namespace UI
{
    public class UIUtility
    {
        
        public static bool IsThisDeviceTablet()
        {
            float screenWidthInches = Screen.width / Screen.dpi;
            float screenHeightInches = Screen.height / Screen.dpi;

            float tabletScreenWidthThreshold = 4.5f; 
            float tabletScreenHeightThreshold = 4.5f;

            if (screenWidthInches > tabletScreenWidthThreshold && screenHeightInches > tabletScreenHeightThreshold)
            {
                return true;
            }

            return false;
        }
        
    }
}