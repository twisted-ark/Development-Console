using UnityEngine;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class DevelopmentConsoleController : MonoBehaviour
    {
        public void ShowConsole ()
        {
            DevelopmentConsole.IsActive = true;
        }

        public void HideConsole ()
        {
            DevelopmentConsole.IsActive = false;
        }
    }

}
