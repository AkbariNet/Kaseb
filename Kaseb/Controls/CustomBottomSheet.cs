using Kaseb.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Extensions;
using UraniumUI.Material;
using UraniumUI.Material.Attachments;
using UraniumUI.Material.Controls;

namespace Kaseb.Controls
{
    /// <summary>
    /// Custom Bottom Sheet View for using buttomsheet with secoud stop of movement
    /// +++
    /// clickable Header
    /// --------------------------------------------------------------------------
    /// </summary>


    public class MyCustomBottomSheetView : BottomSheetView
    {




        protected override void Init()
        {
            base.Init();
            if (Header != null)
            {
                var panGestureToRemove = Header.GestureRecognizers
                                              .FirstOrDefault(g => g is PanGestureRecognizer);
                if (panGestureToRemove != null)
                {
                    Header.GestureRecognizers.Remove(panGestureToRemove);
                }

                var customPanGestureRecognizer = new PanGestureRecognizer();
                customPanGestureRecognizer.PanUpdated += CustomPanGestureRecognizer_PanUpdated;
                Header.GestureRecognizers.Add(customPanGestureRecognizer);
            }
        }
        MessageBox MessageBox = new MessageBox();
        private void CustomPanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    var isApple = DeviceInfo.Current.Platform == DevicePlatform.iOS || DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst;

                    var y = TranslationY + (isApple ? e.TotalY * .05 : e.TotalY);

                    this.TranslationY = y.Clamp(-50, this.Height);

                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    bool NeedToClose = false;
                    if (this.TranslationY < this.Height * .2)
                    {
                        IsPresented = true;
                    }
                    else if (this.TranslationY > 300)
                    {
                        IsPresented = false;
                        NeedToClose = true;
                    }
                    else
                    {
                        IsPresented = false;
                    }

                    MyAlignBottomSheet(needToClose: NeedToClose);
                    break;
            }
        }

        // متد جدیدی که منطق AlignBottomSheet را پیاده‌سازی می‌کند
        private void MyAlignBottomSheet(bool animate = true,bool needToClose=false)
        {
            double y;
            if (!needToClose)
            {

                if (IsPresented)
                {
                    y = 0;
                    OnOpened(); // OnOpened یک متد protected virtual است و قابل فراخوانی است.
                }
                else
                {
                    y = 190;
                    //OnClosed(); // OnClosed یک متد protected virtual است و قابل فراخوانی است.

                }
            }
            else
            {
                y = this.Height - Header.Height;
                OnClosed();
            }

            if (animate)
            {
                this.TranslateToSafely(this.X, y, 50);
            }
            else
            {
                this.TranslationY = y;
            }

            UpdateDisabledStateOfPage(); // UpdateDisabledStateOfPage یک متد protected است.
        }
    }


}
