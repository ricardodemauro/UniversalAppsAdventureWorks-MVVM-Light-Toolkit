using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.DataTransfer;

namespace AdventureWorksCatalog.ViewModel.Messages
{
    public class ShareContentMessage
    {
        public DataTransferManager TransferManager { get; set; }

        public DataRequestedEventArgs TransferArgs { get; set; }

        public ShareContentMessage(DataTransferManager transferManager, DataRequestedEventArgs transferArgs)
        {
            this.TransferArgs = transferArgs;
            this.TransferManager = transferManager;
        }
    }
}
