﻿using GSMA.MobileConnect.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GSMA.MobileConnect.Demo.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly MobileConnectInterface _mobileConnect;
        private readonly MobileConnectConfig _config;
        private string _state;
        private string _nonce;
        private Discovery.DiscoveryResponse _discoveryResponse;
        private RequestTokenResponseData _token;
        private MobileConnectRequestOptions _authOptions;

        public MainPage()
        {
            _mobileConnect = MobileConnectFactory.MobileConnect;
            _config = MobileConnectFactory.Config;

            this.InitializeComponent();
        }

        #region MobileConnect Methods

        private async Task HandleRedirect(Uri url)
        {
            var response = await _mobileConnect.HandleUrlRedirectAsync(url, _discoveryResponse, _state, _nonce, _authOptions);
            await HandleResponse(response);
        }

        private async Task HandleResponse(MobileConnectStatus response)
        {
            System.Diagnostics.Debug.WriteLine(response.ResponseType);
            switch (response.ResponseType)
            {
                case MobileConnectResponseType.Error:
                    HandleError(response);
                    break;
                case MobileConnectResponseType.OperatorSelection:
                case MobileConnectResponseType.Authentication:
                    web.Navigate(new Uri(response.Url));
                    break;
                case MobileConnectResponseType.StartDiscovery:
                    await StartDiscovery(null);
                    break;
                case MobileConnectResponseType.StartAuthentication:
                    await StartAuthentication(response);
                    break;
                case MobileConnectResponseType.Complete:
                    Complete(response);
                    break;
                case MobileConnectResponseType.TokenRevoked:
                    validationResult.Text = "Token Revoked";
                    identityButton.IsEnabled = false;
                    userInfoButton.IsEnabled = false;
                    break;
                case MobileConnectResponseType.UserInfo:
                case MobileConnectResponseType.Identity:
                    ShowIdentity(response);
                    break;
            }
        }

        private void HandleError(MobileConnectStatus response)
        {
            errorText.Text = response.ErrorMessage;
            progress.Visibility = Visibility.Collapsed;
        }

        private async Task StartAuthentication(MobileConnectStatus response)
        {
            _state = Utils.Security.GenerateSecureNonce();
            _nonce = Utils.Security.GenerateSecureNonce();
            _discoveryResponse = response.DiscoveryResponse;
            _authOptions = new MobileConnectRequestOptions
            {
                Scope = GetScope(),
                Context = "demo",
                BindingMessage = "demo auth",
                // Accept valid results and results indicating validation was skipped due to missing support on the provider
                AcceptedValidationResults = TokenValidationResult.Valid | TokenValidationResult.IdTokenValidationSkipped,
            };

            var newResponse = _mobileConnect.StartAuthentication(_discoveryResponse,
                response.DiscoveryResponse.ResponseData.subscriber_id, _state, _nonce, _authOptions);

            await HandleResponse(newResponse);
        }

        private async Task StartDiscovery(string msisdn)
        {
            var response = await _mobileConnect.AttemptDiscoveryAsync(msisdn, null, null, new MobileConnectRequestOptions());
            await HandleResponse(response);
        }

        private void Complete(MobileConnectStatus response)
        {
            _state = null;
            _nonce = null;

            _token = response.TokenResponse.ResponseData;
            accessToken.Text = _token.AccessToken;
            idToken.Text = _token.IdToken;
            timeReceived.Text = _token.TimeReceived.ToString("u");
            applicationName.Text = _discoveryResponse.ApplicationShortName ?? "";
            validationResult.Text = response.TokenResponse.ValidationResult.ToString();

            loginPanel.Visibility = Visibility.Collapsed;
            loggedPanel.Visibility = Visibility.Visible;
        }

        private void ShowIdentity(MobileConnectStatus status)
        {
            loggedPanel.Visibility = Visibility.Collapsed;
            identityPanel.Visibility = Visibility.Visible;

            var json = new Newtonsoft.Json.Linq.JRaw(status.IdentityResponse.ResponseJson);
            identity.Text = json.ToString(Newtonsoft.Json.Formatting.Indented);
        }

        #endregion

        #region Helper Methods

        private string GetScope()
        {
            var scopes = new List<string> { };

            //Create scope from checked checkboxes
            var elements = authScopes.Children.Concat(userInfoScopes.Children.Concat(identityScopes.Children));

            foreach (var element in elements)
            {
                var check = element as Windows.UI.Xaml.Controls.Primitives.ToggleButton;
                if (check != null && check.Tag != null && check.IsChecked == true)
                {
                    scopes.Add(check.Tag.ToString());
                }
            }

            return string.Join(" ", scopes);
        }

        #endregion

        #region Event Handlers

        private async void MobileConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string msisdnVal = null;
            if (toggle.IsChecked == true)
            {
                msisdnVal = msisdn.Text;
            }

            progress.Visibility = Visibility.Visible;

            await StartDiscovery(msisdnVal);
        }

        private async void UserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var response = await _mobileConnect.RequestUserInfoAsync(_discoveryResponse, _token.AccessToken, new MobileConnectRequestOptions());
            await HandleResponse(response);
        }

        private async void IdentityButton_Click(object sender, RoutedEventArgs e)
        {
            var response = await _mobileConnect.RequestIdentityAsync(_discoveryResponse, _token.AccessToken, new MobileConnectRequestOptions());
            await HandleResponse(response);
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(_token.RefreshToken))
            {
                return;
            }

            var response = await _mobileConnect.RefreshTokenAsync(_token.RefreshToken, _discoveryResponse);
            await HandleResponse(response);
        }

        private async void RevokeButton_Click(object sender, RoutedEventArgs e)
        {
            var response = await _mobileConnect.RevokeTokenAsync(_token.AccessToken, "access_token", _discoveryResponse);
            await HandleResponse(response);
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            loggedPanel.Visibility = Visibility.Visible;
            identityPanel.Visibility = Visibility.Collapsed;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void MSISDNCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            msisdn.Visibility = Visibility.Visible;
        }

        private void MSISDNCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            msisdn.Visibility = Visibility.Collapsed;
        }

        private async void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            // Hide the webview before loading the page, once the page is loaded if it needs to be seen by the user
            // it will be made visible
            Debug.WriteLine(args.Uri.AbsoluteUri);
            web.Visibility = Visibility.Collapsed;

            if (args.Uri.AbsoluteUri.StartsWith(_config.RedirectUrl))
            {
                //cancel navigation to prevent final redirect from loading, navigate to blank to prevent previous redirect reloading
                args.Cancel = true;
                sender.Source = new Uri("about:blank");
                await HandleRedirect(args.Uri);
            }
        }

        private void web_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // If a page loads that has a hostname and isn't the redirect url then ensure the webview is visible to the user
            if (!string.IsNullOrEmpty(e.Uri.Host) && !e.Uri.AbsoluteUri.StartsWith(_config.RedirectUrl))
            {
                web.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}
