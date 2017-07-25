﻿namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Framework;
    using Domain;
    using Domain.Services;
    using System.Net;
    using System.Net.Http;

    partial class ReportIncidentPage
    {


        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
        }
        

        async Task OpenBot()
        {
            try
            {
                await Device.OS.OpenBrowser(GlobalSettings.SkypeBotAccount);
            }
            catch (Exception ex)
            {

                await Alert.Show("Error", "Unable to launch Skype.", KeyValuePair.Of("Ok", true));
            }
        }

        async Task HandlebarTapped()
        {
            SetReportType(ReportedIssueType.Handlebar);
         //   handlebarImageView.Set(x => x.Style.BackgroundColor = "Red"); 
        }
        async Task ChainTapped()
        {
            SetReportType(ReportedIssueType.Chain);
        }
        async Task FlatTireTapped()
        {
            SetReportType(ReportedIssueType.FlatTire);
        }
        async Task ForkTapped()
        {
            SetReportType(ReportedIssueType.Fork);
        }
        async Task PedalsTapped()
        {
            SetReportType(ReportedIssueType.Pedals);
        }
        async Task LossTapped()
        {
            SetReportType(ReportedIssueType.Stolen);
        }
        private void SetReportType(ReportedIssueType type)
        {
            chainImageView.BackgroundImagePath = "Images/ic_report_chain.png";
            Chain = false;
            flat_tireImageView.BackgroundImagePath = "Images/ic_report_flat_tire.png";
            FlatTire = false;
            forkImageView.BackgroundImagePath = "Images/ic_report_fork.png";
            Fork = false;
            handlebarImageView.BackgroundImagePath = "Images/ic_report_handlebar.png";
            Handlebar = false;
            lossImageView.BackgroundImagePath = "Images/ic_report_loss.png";
            Loss = false;
            pedalsImageView.BackgroundImagePath = "Images/ic_report_pedals.png";
            Pedals = false;

            switch (type)
            {
                case ReportedIssueType.Chain:
                    chainImageView.BackgroundImagePath = "Images/ic_report_chain_selec.png";
                    Chain = true;
                    break;
                case ReportedIssueType.FlatTire:
                    FlatTire = true;
                    flat_tireImageView.BackgroundImagePath = "Images/ic_report_flat_tire_selec.png";
                    break;
                case ReportedIssueType.Fork:
                    forkImageView.BackgroundImagePath = "Images/ic_report_fork_selec.png";
                    Fork = true;
                    break;
                case ReportedIssueType.Handlebar:
                    Handlebar = true;
                    handlebarImageView.BackgroundImagePath = "Images/ic_report_handlebar_selec.png";
                    break;
                case ReportedIssueType.Stolen:
                    lossImageView.BackgroundImagePath = "Images/ic_report_loss_selec.png";
                    Loss = true;
                    break;
                case ReportedIssueType.Pedals:
                    Pedals = true;
                    pedalsImageView.BackgroundImagePath = "Images/ic_report_pedals_selec.png";
                    break;
            }

            _reportIncidentType = type;
        }

        async Task ReportButtonTapped()
        {
            // IsBusy = true;
            FillData();
            try
            {
                IsValid = true;

                bool isValid = Validate();

                if (isValid)
                {
                    var incident = new ReportedIssue
                    {
                        Type = _reportIncidentType,
                        Title = Title,
                        Description = Description
                    };

                    var _feedbackService = new FeedbackService();
                    if (await _feedbackService.SendIssueAsync(incident))
                    {
                        await Alert.Toast("Received");
                        ResetData();
                    }
                    else
                        await Alert.Toast("Error on save");

                }
                else
                {
                    IsValid = false;
                }
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await Alert.Show("Error", "Communication error");
            }
            catch (Exception ex)
            {
                //  Debug.WriteLine($"Error reporting incident in: {ex}");
            }

            // IsBusy = false;
        }

        private void FillData()
        {
            Title = titleInput.Text;
            Description = descriptionInput.Text;
        }

        private void ResetData()
        {
            SetReportType(ReportedIssueType.Unknown);
            Title = titleInput.Text = Description = descriptionInput.Text = "";
        }

    }
}