using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using MvvmCross.Platform;
using Xamarin.Forms.Platform.Android;
using Acr.UserDialogs;
using Bagahe.Helpers;
using Bagahe.Views;

namespace Bagahe.Droid
{
    [Activity(Theme = "@style/Theme.MainBackground", Label = "XFormsCross Template",
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MvxNavigationActivity
    : FormsApplicationActivity, IMvxPageNavigationProvider
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            Mvx.Resolve<IMvxPageNavigationHost>().NavigationProvider = this;
            Mvx.Resolve<IMvxAppStart>().Start();

        }

        public async void Push(Page page)
        {
            if (MvxNavigationActivity.NavigationPage != null)
            {
                await MvxNavigationActivity.NavigationPage.PushAsync(page);
                return;
            }

            MvxNavigationActivity.NavigationPage = new NavigationPage(page);
            SetPage(MvxNavigationActivity.NavigationPage);
        }

        public async void Pop()
        {
            if (MvxNavigationActivity.NavigationPage == null)
                return;

            await MvxNavigationActivity.NavigationPage.PopAsync();
        }

        public static NavigationPage NavigationPage { get; set; }

        //For overriding the back button
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 16908332) // xam forms nav bar back button id
            {
                var currentpage = (BackButtonHelper)Xamarin.Forms.Application.Current.
                     MainPage.Navigation.NavigationStack.LastOrDefault();
                
                if (currentpage?.CustomBackButtonAction != null)
                {
                    currentpage?.CustomBackButtonAction.Invoke();
                    return false;
                }
                return base.OnOptionsItemSelected(item);
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            var currentpage = (BackButtonHelper)Xamarin.Forms.Application.Current.
                MainPage.Navigation.NavigationStack.LastOrDefault();

            if (currentpage?.CustomBackButtonAction != null)
            {
                currentpage?.CustomBackButtonAction.Invoke();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }
}