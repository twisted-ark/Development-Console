using UnityEngine;
using UnityEngine.Events;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class DevelopmentConsoleController : MonoBehaviour
    {
        [SerializeField] private UnityEvent consoleVisible;
        [SerializeField] private UnityEvent consoleHidden;

        private void Start ()
        {
            DevelopmentConsole.VisibilityChanged += OnDevConsoleVisibilityChanged;
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
            if (enabled == false)
                return;

            if (status)
                consoleVisible.Invoke ();
            else
                consoleHidden.Invoke ();
        }
    }

}
