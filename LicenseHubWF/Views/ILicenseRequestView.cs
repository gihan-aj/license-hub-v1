﻿using LicenseHubWF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseHubWF.Views
{
    public interface ILicenseRequestView
    {
        // Properties - Fields
        string Client { get; set; }
        string RequestKey { get; set; }
        string PCName { get; set; }
        string LicenseType { get; set; }
        List<string> Packages {  get; set; }
        bool IsAgreementAccepted { get; }

        // Events
        event EventHandler RequestLicenseEvent;
        event EventHandler ResetEvent;
        event EventHandler LicenseAgreementEvent;

        // Methods
        void SetClientList(IEnumerable<ClientModel> clientList);
        void SetPackageList(IEnumerable<PackageModel> packages);
        //void SetRequestLicenseBindingSource(BindingSource request);
        void Show();
    }
}
