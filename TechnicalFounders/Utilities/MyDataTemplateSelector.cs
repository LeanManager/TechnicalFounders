using System;
using TechnicalFounders.CustomCells;
using TechnicalFounders.ViewModels;
using Xamarin.Forms;

namespace TechnicalFounders.Utilities
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public MyDataTemplateSelector()
        {
            // Retain instances
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageViewModel = item as MessageViewModel;

            if (messageViewModel == null)
                return null;
            
            return messageViewModel.IsIncoming ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }

        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
    }
}
