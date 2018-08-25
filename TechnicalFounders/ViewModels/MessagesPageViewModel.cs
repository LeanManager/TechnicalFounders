using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechnicalFounders.Utilities;
using Xamarin.Forms;

namespace TechnicalFounders.ViewModels
{
    public class MessagesPageViewModel : BaseViewModel
    {
        public ICommand SendCommand { get; set; }

        private ObservableCollection<MessageViewModel> messages;
        public ObservableCollection<MessageViewModel> Messages
        {
            get => messages;
            set
            {
                if (SetProperty(ref messages, value))
                {

                }
            }
        }

        private string outgoingText;
        public string OutgoingText
        {
            get => outgoingText;
            set
            {
                if (SetProperty(ref outgoingText, value))
                {

                }
            }
        }

        public MessagesPageViewModel()
        {
            // Initialize with default values

            Messages = new ObservableCollection<MessageViewModel>
            {
                // Blushy smile    Cool smile
                // \uD83D\uDE0A    \uD83D\uDE0E

                new MessageViewModel 
                { 
                    Text = "Welcome to Technical Founders!", 
                    IsIncoming = true, 
                    MessageDateTime = DateTime.Now.AddMinutes(-25)
                },
                new MessageViewModel 
                { 
                    Text = "Hi Carlos. How are you? \uD83D\uDE0A", 
                    IsIncoming = false, 
                    MessageDateTime = DateTime.Now.AddMinutes(-24)
                },
                new MessageViewModel 
                { 
                    Text = "I'm doing great! How are you?", 
                    IsIncoming = true, 
                    MessageDateTime = DateTime.Now.AddMinutes(-23)
                },
                new MessageViewModel 
                { 
                    Text = "How can I help you today?", 
                    IsIncoming = true, 
                    MessageDateTime = DateTime.Now.AddMinutes(-23)
                },
                new MessageViewModel 
                { 
                    Text = "I have a question. How do you write a custom renderer to display rounded images in Xamarin.Forms?", 
                    IsIncoming = false, 
                    MessageDateTime = DateTime.Now.AddMinutes(-23)
                },
                new MessageViewModel 
                { 
                    Text = "This is for Android only. On iOS we can interact directly with the Layer.", 
                    IsIncoming = false, 
                    MessageDateTime = DateTime.Now.AddMinutes(-23)
                }
            };

            OutgoingText = null;

            SendCommand = new Command(() =>
            {
                if (String.IsNullOrEmpty(OutgoingText))
                    return;

                Messages.Add(new MessageViewModel { Text = OutgoingText, IsIncoming = false, MessageDateTime = DateTime.Now });

                OutgoingText = null;

                MessagingCenter.Send(new ScrollListViewMessage(), "ScrollToBottom");
            });
        }
        // public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}