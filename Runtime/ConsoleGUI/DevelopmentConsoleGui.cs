using Unity.Mathematics;
using UnityEngine;

namespace TwistedArk.Development.Console
{
    [AddComponentMenu (null)]
    public partial class DevelopmentConsoleGui : MonoBehaviour
    {
        internal DevelopmentConsole console;
        
        private int currentTabIndex;
        
        private void Update ()
        {
            if (Input.GetKeyDown (console.ToggleGuiKey))
                DevelopmentConsole.IsActive = !DevelopmentConsole.IsActive;
        }

        private void OnGUI ()
        {
            if (!DevelopmentConsole.IsActive)
                return;

            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            var reference = DevelopmentConsole.Instance.ReferenceResolution;
            
            var oldMatrix = GUI.matrix;
            var scaler = new Vector3 (
                (float) screenWidth / reference.x, 
                (float) screenHeight / reference.y, 
                1);
            
            GUI.matrix = Matrix4x4.Scale (scaler);
            
            GuiColors.PushGuiColor (DevelopmentConsole.Instance.ForegroundColor);
            GuiColors.PushBackgroundColor (DevelopmentConsole.Instance.BackgroundColor);

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
            panelWidth /= scaler.x;
            
            var panelHeight = screenHeight * anchorsMax.y - startUp;
            panelHeight /= scaler.y;
            
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
                panelWidth - totalPaddingX - 120, 
                panelHeight - contentOffset - totalPaddingY);

            var scrollbarRect = contentRect;
            scrollbarRect.x = contentRect.width + 50;
            scrollbarRect.width = panelWidth - totalPaddingX - contentRect.width - 30;

            var skin = DevelopmentConsole.Instance.Skin;
            
            GUI.Box (fullRect, GUIContent.none, skin.GetOrCreateStyle ("Box", GUI.skin.box));
            
            currentTabIndex = math.min (currentTabIndex, DevelopmentConsole.TabCount);

            DrawHeader (in headerRect, skin);
            
            DrawOpenTab (contentRect, skin);
            DrawScrollbar (in scrollbarRect, skin);

            GuiColors.PopGuiColor ();
            GuiColors.PopBackgroundColor ();
            
            if (Event.current.type != EventType.Repaint && Event.current.type != EventType.Layout)
                Event.current.Use ();

            GUI.matrix = oldMatrix;
        }

    }

}