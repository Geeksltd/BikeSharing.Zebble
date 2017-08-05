﻿namespace UI.Pages
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Zebble;

    partial class AccountPage
    {
        string _email = string.Empty;
        string _skype = string.Empty;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;

            }
        }

        public string Skype
        {
            get
            {
                return _skype;
            }
            set
            {
                _skype = value;

            }
        }
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            foregroundStack.Y.Set(10);


            await sunBox.Animate(new Animation
            {
                Duration = 100000.Milliseconds(),
                Change = () => sunBox.Rotation(360),
                Repeats = 60
            });
        }

        public SignUpPage signupPage => FindParent<SignUpPage>();

        async Task NextButtonTapped()
        {
            if (EmailValidation())
            {
                Email = emailInput.Text;
                Skype = skypeInput.Text;
                await signupPage.NextPage();
            }
            else
                await Alert.Toast("Email is incorrect");
        }

        async Task TextChanged()
        {
            if (skypeInput.Text.HasValue() && EmailValidation())
                nextButton1.Set(rec => rec.Enabled = true).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_normal.png");
            else
                nextButton1.Set(rec => rec.Enabled = false).Set(rec => rec.BackgroundImagePath = "Images/SignUp/floating_action_button_disable.png");
        }


        bool EmailValidation()
        {
            if (emailInput.Text.HasValue())
            {
                string patternEmail = @"(?<email>\w+@\w+\.[a-z]{0,3})";
                Regex regexEmail = new Regex(patternEmail);
                if (regexEmail.IsMatch(emailInput.Text))
                    return true;
            }
            return false;
        }
    }
}