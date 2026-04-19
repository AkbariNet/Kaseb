using System;
using System.Collections.Generic;
using System.Text;

namespace KasebCore.Services
{
    public class Animation
    {

        #region -------------------------------- Fade Animations --------------------------------
        //----------------------------------------------------------------

        /// <summary>
        /// Shows a view with a fade‑in (and upward) animation.
        /// </summary>
        public static async Task ShowWithFade(View view)
        {
            view.TranslationY = 0; // Ensure view starts at its normal Y
            view.Opacity = 0;
            view.IsVisible = true;

            await view.FadeTo(1, 400, Easing.CubicInOut);
        }

        /// <summary>
        /// Shows a view with fade‑in while fading out its parent.
        /// The parent is temporarily disabled.
        /// </summary>
        public static async Task ShowWithFade(View view, View Parent)
        {
            Parent.IsEnabled = false;

            view.TranslationY = 0;
            view.Opacity = 0;
            view.IsVisible = true;

            await Task.WhenAll(
                view.FadeTo(1, 400, Easing.CubicInOut),
                Parent.FadeTo(0.3, 400, Easing.CubicInOut));
        }

        /// <summary>
        /// Hides a view with a fade‑out animation.
        /// </summary>
        public static async Task HideWithFade(View view)
        {
            await view.FadeTo(0, 400, Easing.CubicInOut);
            view.IsVisible = false;
        }

        /// <summary>
        /// Hides a view while restoring the parent’s opacity and enabled state.
        /// </summary>
        public static async Task HideWithFade(View view, View Parent)
        {
            Parent.IsEnabled = true;

            await Task.WhenAll(
                view.FadeTo(0, 400, Easing.CubicInOut),
                Parent.FadeTo(1, 400, Easing.CubicInOut));

            view.IsVisible = false;
        }
        #endregion
    }
}
