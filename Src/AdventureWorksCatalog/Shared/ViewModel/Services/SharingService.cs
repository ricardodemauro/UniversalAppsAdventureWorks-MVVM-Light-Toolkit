using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AdventureWorksCatalog.ViewModel.Services
{
    public class SharingService
    {
        private readonly DataTransferManager transferManager;

        private DataPackage _requestData;

        public SharingService()
        {
            transferManager = DataTransferManager.GetForCurrentView();
            transferManager.DataRequested += OnDataRequested;
            transferManager.TargetApplicationChosen += transferManager_TargetApplicationChosen;
        }

        private void transferManager_TargetApplicationChosen(DataTransferManager sender, TargetApplicationChosenEventArgs args)
        {
            try
            {
                if (!(args.ApplicationName == "Email" || args.ApplicationName == "Mail"))
                {
                    var view = this.GetCurrentView();
                    if (view == null)
                        return;

                    var supportSharing = view.DataContext as ISupportSharing;
                    if (supportSharing == null)
                        return;

                    _requestData.SetWebLink(supportSharing.GetUriToShare());
                }
            }
            catch { }
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var view = this.GetCurrentView();
            if (view == null)
                return;

            var supportSharing = view.DataContext as ISupportSharing;
            if (supportSharing == null)
                return;

            supportSharing.OnShareRequested(args.Request);
            
            _requestData = args.Request.Data;
        }

        private FrameworkElement GetCurrentView()
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
                return frame.Content as FrameworkElement;

            return Window.Current.Content as FrameworkElement;
        }
    }
}
