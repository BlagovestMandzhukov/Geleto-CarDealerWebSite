namespace GeletoCarDealer.Web.ViewModels.Administration.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllMessagesViewModel
    {
        public IEnumerable<MessagesViewModel> Messages { get; set; }
        
    }
}
