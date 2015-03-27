using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.DataTransfer;

namespace AdventureWorksCatalog.ViewModel.Services
{
    public interface ISupportSharing
    {
        void OnShareRequested(DataRequest dataRequest);

        Uri GetUriToShare();
    }
}
