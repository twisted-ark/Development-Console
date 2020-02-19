using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class DevelopmentConsoleController : MonoBehaviour
    {
        [SerializeField] private bool disableUnityEventSystemWhenOpen;
        
        [SerializeField] private UnityEvent consoleVisible;
        [SerializeField] private UnityEvent consoleHidden;

        private EventSystem eventSystem;
        
        private void Start ()
        {
            DevelopmentConsole.VisibilityChanged += OnDevConsoleVisibilityChanged;
        }

        private void OnDestroy ()
        {
            DevelopmentConsole.VisibilityChanged -= OnDevConsoleVisibilityChanged;
        }

        public void ShowConsole ()
        {
            DevelopmentConsole.IsActive = true;
        }

        public void HideConsole ()
        {
            DevelopmentConsole.IsActive = false;
        }

        private void OnDevConsoleVisibilityChanged (bool status)
        {
            if (!this || enabled == false)
                return;
            
            if (eventSystem == false)
                eventSystem = EventSystem.current;

            if (eventSystem && disableUnityEventSystemWhenOpen)
                eventSystem.enabled = status;
            
            if (status)
                consoleVisible.Invoke ();
            else
                consoleHidden.Invoke ();
        }
    }

}
