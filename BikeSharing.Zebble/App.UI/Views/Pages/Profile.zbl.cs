﻿namespace UI.Pages
{
    using Domain;
    using Domain.Entities;
    using Domain.Services;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Zebble;
    using static Domain.Services.Api;

    partial class ProfilePage
    {
        UserProfile Item;
        public override async Task OnInitializing()
        {
            Item = Settings.UserProfile;

            await base.OnInitializing();
            await InitializeComponents();
        }


        async Task UserImageTapped()
        {
            var tempImage = new FileInfo("Images/profile_placeholder.png");
            string base64Str = null;
            // try
            // {

            if (Device.Permissions.Check(DevicePermission.Camera).Result == PermissionResult.Granted)
                tempImage = await Device.Media.TakePhoto();

            if (tempImage == null || tempImage.FullName.Contains("profile_placeholder"))
                tempImage = await Device.Media.PickPhoto();

            if (tempImage != null)
            {
                userImageView.Path = tempImage.FullName;
                using (Stream mediaStream = tempImage.OpenRead())
                using (MemoryStream memStream = new MemoryStream())
                {
                    await mediaStream.CopyToAsync(memStream);
                    base64Str = Convert.ToBase64String(memStream.ToArray());
                }

                if (base64Str.HasValue())
                {
                    await ProfileService.UploadUserImageAsync(base64Str, Item);
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    await Alert.Show(ex.Message);
            //    return;
            //}
        }
    }
}