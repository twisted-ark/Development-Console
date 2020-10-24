using UnityEngine;

namespace TwistedArk.Development.Console.DefaultControls
{
    public static class DevControls
    {
        private static string[] QualityLevelNames;
        
        [RuntimeInitializeOnLoadMethod]
        private static void RegisterControls ()
        {
            var tab = DevelopmentConsole.GetOrCreateTab ("Graphics");
            var group = tab.GetOrCreateGroup ("Quality");

            QualityLevelNames = QualitySettings.names;
            RegisterQualityControls (group);
        }

        private static void RegisterQualityControls (IGuiElementGroup elementGroup)
        {
            elementGroup.CreateFancySliderInt ("Quality Level", 0, QualityLevelNames.Length - 1, 1,
                QualitySettings.SetQualityLevel,
                 QualitySettings.GetQualityLevel
                );
        }
    }
}