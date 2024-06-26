﻿using LicenseHubWF._Repositories;
using LicenseHubWF.Views;
using LoggerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseHubWF.Presenters
{
    public class ConfigurationPresenter
    {
        private IConfigurationView _view;
        private IFileLogger _logger;

        public ConfigurationPresenter(IConfigurationView view, IFileLogger logger)
        {
            _view = view;
            _logger = logger;

            _view.SaveEvent += SaveBaseUrl;
            _view.TestEvent += TestServerConnection;

        }

        private async void TestServerConnection(object? sender, EventArgs e)
        {
            try
            {
                await BaseRepository.EstablishConnection(_logger);
                if (BaseRepository.Connectivity)
                {
                    _view.TestMessage = ApiRepository.GetSetting<string>("ConnectionTestSuccessMessage");
                }
                else
                {
                    _view.TestMessage = ApiRepository.GetSetting<string>("ConnectionTestFailMessage");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"TestServerConnection -> {ex.Message}");
                _logger.LogError($"TestServerConnection -> Exception: {ex}");

                BaseRepository.ShowMessage("Error", ex.Message);
            }
        }

        private void SaveBaseUrl(object? sender, EventArgs e)
        {
            try
            {
                BaseRepository.UpdateAppSettings("ApiBaseUrl", _view.BaseAdsress, _logger);
                _view.IsSaved = true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"SaveBaseUrl -> {ex.Message}");
                _logger.LogError($"SaveBaseUrl -> Exception: {ex}");

                BaseRepository.ShowMessage("Error", ex.Message);
            }
        }
    }
}
