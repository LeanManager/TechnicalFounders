using System;
namespace TechnicalFounders.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string text;
        public string Text
        {
            get => text;
            set
            {
                if (SetProperty(ref text, value))
                {

                }
            }
        }

        private DateTime messageDateTime;
        public DateTime MessageDateTime
        {
            get => messageDateTime;
            set
            {
                if (SetProperty(ref messageDateTime, value))
                {

                }
            }
        }

        private bool isIncoming;
        public bool IsIncoming
        {
            get => isIncoming;
            set
            {
                if (SetProperty(ref isIncoming, value))
                {

                }
            }
        }

        public bool HasAttachement => !string.IsNullOrEmpty(attachementUrl);

        private string attachementUrl;
        public string AttachementUrl
        {
            get => attachementUrl;
            set
            {
                if (SetProperty(ref attachementUrl, value))
                {

                }
            }
        }


    }
}
