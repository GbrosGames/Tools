using UnityEngine;

namespace Gbros.Watchers
{
    public static class Utilities
    {
        public static Color Color(string hex) => ColorUtility.TryParseHtmlString(hex, out var color) ? color : UnityEngine.Color.white;
    }
}
