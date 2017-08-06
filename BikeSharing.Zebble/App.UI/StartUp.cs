﻿namespace UI
{
    using Domain.Services;
    using System;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Services;
    public partial class StartUp : Zebble.StartUp
    {
        public override async Task Run()
        {
            ApplicationName = "BikeSharing";

            await InstallIfNeeded();

            //  RegisterDataProvider(typeof(AppData.AdoDotNetDataProviderFactory));

            CssStyles.LoadAll();
            ImageService.MemoryCacheFolder("Images");

            //Device.System.ReceivedMemoryWarning.Handle(() => Alert.Toast("There is a shortage of memory. The application may crash."));

            Services.PushNotificationListener.Setup();

            LoadFirstPageAsync().RunInParallel();
        }

        public static async Task LoadFirstPageAsync()
        {
            if (await new ProfileService().GetCurrentProfileAsync() == null)
                await Nav.Go<Pages.LoginPage>();
            else
                await Nav.Go(new Pages.HomePage());
        }
    }
}