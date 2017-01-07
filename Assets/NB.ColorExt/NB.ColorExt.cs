using UnityEngine;
using System.Globalization;

namespace NB.ColorExt {
    /// <summary>
    /// Extension method class which adds additional methods to the Unity Color object
    /// 
    /// Ex.
    /// Color c = Color.white;
    /// c.FromHexStr("#FF0000"); //Change To Red
    /// c.FromHSV(240, 100, 100); //Change To Blue
    /// </summary>
    public static class NBColorExtensions {

        /// <summary>        
        /// Applies Hex string (ex. #FF00FF) to color object
        /// Hex Color String can be either 2, 3, 4 or 6 value arrangements
        /// Example 2 Values: #FF = Red = Only effects red channel and turns green/blue to 0
        /// Example 3 Values: #AAA = Grey = Takes first two values and uses it on r, g and b to make grey value
        /// Example 4 Values: #FFFF = Yellow = Only Effects Red/Green channels and turns blue to 0
        /// Example 6 Values: #FF00FF = Purple = Normal HTML Hex behavior
        /// </summary>    
        ///     
        /// <param name="HexStr">Hex Color String</param>
        /// <param name="alpha">Optional: Alpha override</param>
        /// <returns></returns>
        public static Color FromHexStr(this Color c, string HexStr, float alpha = -1f) {
            if (HexStr == "" || HexStr == "#") {
                Debug.LogError("NB.ColorExt FromHexStr :: Hex String is empty.");
                return c;
            }//if

            c.r = c.g = c.b = 0f;

            //Strip out the hash sign if it's there
            if (HexStr[0] == '#') {
                HexStr = HexStr.Substring(1, HexStr.Length - 1);
            }//if

            if (HexStr.Length > 6) {
                HexStr = HexStr.Substring(0, 6);
            }//if

            if (HexStr.Length % 2 != 0 && HexStr.Length != 3) {
                HexStr += "0";
            }//if	

            int RedChannel = 0;
            int GreenChannel = 0;
            int BlueChannel = 0;
            if (HexStr.Length == 2) {
                if (int.TryParse(HexStr, NumberStyles.HexNumber, null, out RedChannel) == false) { RedChannel = 0; }//if		
                c.r = (float)RedChannel / 255.0f;
                c.g = 0f;
                c.b = 0f;                
            }//if
            else if (HexStr.Length == 3) {
                if (!int.TryParse(HexStr.Substring(0, 2), NumberStyles.HexNumber, null, out RedChannel)) { RedChannel = 0; }//if
                c.r = (float)RedChannel / 255.0f;
                c.g = (float)RedChannel / 255.0f;
                c.b = (float)RedChannel / 255.0f;                
            }//else if
            else if (HexStr.Length == 4) {
                if (!int.TryParse(HexStr.Substring(0, 2), NumberStyles.HexNumber, null, out RedChannel)) { RedChannel = 0; }//if
                if (!int.TryParse(HexStr.Substring(2, 2), NumberStyles.HexNumber, null, out GreenChannel)) { GreenChannel = 0; }//if
                c.r = (float)RedChannel / 255.0f;
                c.g = (float)RedChannel / 255.0f;
                c.b = 0f;             
            }//if
            else if (HexStr.Length == 6) {
                if (!int.TryParse(HexStr.Substring(0, 2), NumberStyles.HexNumber, null, out RedChannel)) { RedChannel = 0; }//if
                if (!int.TryParse(HexStr.Substring(2, 2), NumberStyles.HexNumber, null, out GreenChannel)) { GreenChannel = 0; }//if		
                if (!int.TryParse(HexStr.Substring(4, 2), NumberStyles.HexNumber, null, out BlueChannel)) { BlueChannel = 0; }//if		
                c.r = (float)RedChannel / 255.0f;
                c.g = (float)GreenChannel / 255.0f;
                c.b = (float)BlueChannel / 255.0f;                
            }//if

            if(alpha > 0f)
                c.a = alpha;

            return c;
        }

        /// <summary>
        /// Returns RGB Color values as Hex String.
        /// </summary>
        /// 
        /// <param name="c"></param>
        /// <returns>HTML Hex String</returns>
        public static string ToHexStr(this Color c) {
            int r = Mathf.RoundToInt(c.r * 255.0f);
            int g = Mathf.RoundToInt(c.g * 255.0f);
            int b = Mathf.RoundToInt(c.b * 255.0f);

            return r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        /// <summary>        
        /// Applies Hue, Saturation and Vibrance values on color object
        /// </summary>  
        ///       
        /// <param name="H">Hue</param>
        /// <param name="S">Saturation</param>
        /// <param name="V">Vibrance</param>
        /// <param name="alpha">Optional: Alpha override</param>
        /// <returns></returns>
        public static Color FromHSV(this Color c, float H, float S, float V, float alpha = -1f) {
            float min, chroma, hDash, x;

            //Color c = Color.black;
            c.r = c.g = c.b = 0f;

            chroma = S * V;
            hDash = H / 60.0f;
            x = chroma * (1.0f - Mathf.Abs((hDash % 2.0f) - 1.0f));

            if (hDash < 1.0f) {
                c.r = chroma;
                c.g = x;
            }//if
            else if (hDash < 2.0f) {
                c.r = x;
                c.g = chroma;
            }//else if
            else if (hDash < 3.0f) {
                c.g = chroma;
                c.b = x;
            }//else if
            else if (hDash < 4.0f) {
                c.g = x;
                c.b = chroma;
            }//else if
            else if (hDash < 5.0f) {
                c.r = x;
                c.b = chroma;
            }//else if
            else if (hDash < 6.0f) {
                c.r = chroma;
                c.b = x;
            }//else if

            min = V - chroma;

            c.r += min;
            c.g += min;
            c.b += min;

            if (alpha > 0f)
                c.a = alpha;

            return c;
        }

        /// <summary>
        /// Unity Color to Hue, Saturation and Vibrance
        /// </summary>        
        /// <returns>float array with 3 values for Hue, Saturation and Vibrance.</returns>
        public static float[] HSV(this Color c) {
            
            float[] hsv = new float[3];

            float min = Mathf.Min(Mathf.Min(c.r, c.g), c.b);
            float max = Mathf.Max(Mathf.Max(c.r, c.g), c.b);
            float chroma = max - min;
            
            if (chroma != 0) {
                if (c.r == max) {
                    hsv[0] = (c.g - c.b) / chroma;
                    if (hsv[0] < 0) {
                        hsv[0] += 6.0f;
                    }//if
                }//if
                else if (c.g == max) {
                    hsv[0] = ((c.b - c.r) / chroma) + 2.0f;
                }//else if
                else {
                    hsv[0] = ((c.r - c.g) / chroma) + 4.0f;
                }//else

                hsv[0] *= 60.0f;
                hsv[1] = chroma / max;
            }//if
            else {
                hsv[0] = 0.0f;
                hsv[1] = 0.0f;
            }//else

            hsv[2] = max;

            return hsv;
        }

        public static float Hue(this Color c) {         return c.HSV()[0]; }
        public static float Saturation(this Color c) {  return c.HSV()[1]; }
        public static float Vibrance(this Color c) {    return c.HSV()[2]; }
    }
}