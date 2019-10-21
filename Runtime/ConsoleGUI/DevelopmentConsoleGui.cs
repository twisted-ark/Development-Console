using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    [AddComponentMenu (null)]
    public partial class DevelopmentConsoleGui : MonoBehaviour
    {
        internal DevelopmentConsole console;
        
        private int currentTab;
        
        private void Update ()
        {
            if (Input.GetKeyDown (console.ToggleGuiKey))
                DevelopmentConsole.IsActive = !DevelopmentConsole.IsActive;
        }

        private void OnGUI ()
        {
            if (!DevelopmentConsole.IsActive)
                return;
            
            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.BackgroundColor);

            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            
            var headerHeight = DevelopmentConsole.Instance.HeaderHeight;
            var anchorsMin = DevelopmentConsole.Instance.anchorsMin;
            var anchorsMax = DevelopmentConsole.Instance.anchorsMax;
            var contentOffset = DevelopmentConsole.Instance.HeaderSpacing + headerHeight;

            var paddingX = DevelopmentConsole.Instance.PaddingX;
            var paddingY = DevelopmentConsole.Instance.PaddingY;
            var totalPaddingX = paddingX.x + paddingX.y;
            var totalPaddingY = paddingY.x + paddingY.y;

            var startLeft = screenWidth * anchorsMin.x;
            var startUp = screenHeight * anchorsMin.y;

            var panelWidth = screenWidth * anchorsMax.x - startLeft;
            var panelHeight = screenHeight * anchorsMax.y - startUp;
            
            var fullRect = new Rect (startLeft, startUp, 
                panelWidth, panelHeight);
            
            var headerRect = new Rect(
                startLeft + paddingX.x, 
                startUp + paddingY.y, 
                panelWidth - totalPaddingX, 
                headerHeight);

            var contentRect = new Rect (
                startLeft + paddingX.x, 
                startUp + contentOffset + paddingY.y, 
                panelWidth - totalPaddingX, 
                panelHeight - contentOffset - totalPaddingY);


            var skin = DevelopmentConsole.Instance.Skin;
            var unitySkin = DevelopmentConsole.Instance.UnityGuiSkin;
            
            GUI.Box (fullRect, GUIContent.none, skin.GetOrCreateStyle ("Box", unitySkin.box));
            
            currentTab = math.min (currentTab, DevelopmentConsole.TabCount);

            DrawHeader (in headerRect, skin);
            DrawOpenTab (in contentRect, skin);

            GuiColors.PopBackgroundColor ();
            
            if (Event.current.type != EventType.Repaint && Event.current.type != EventType.Layout)
                Event.current.Use ();
        }

    }

}