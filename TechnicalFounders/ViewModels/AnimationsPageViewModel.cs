using System;
using System.Collections.ObjectModel;
using TechnicalFounders.Models;
using Xamarin.Forms;

namespace TechnicalFounders.ViewModels
{
    public class AnimationsPageViewModel : BaseViewModel
    {
        public AnimationsPageViewModel()
        {
            Cards = new ObservableCollection<CarouselCard>
            {
                new CarouselCard
                {
                    ImageUrl = "tab_feed.png",
                    Name = "First Card"
                },
                new CarouselCard
                {
                    ImageUrl = "tab_chat.png",
                    Name = "Second Card"
                },
                new CarouselCard
                {
                    ImageUrl = "tab_about.png",
                    Name = "Third Card"
                }
            };
        }

        private ObservableCollection<CarouselCard> cards;
        public ObservableCollection<CarouselCard> Cards 
        {
            get => cards;
            set
            {
                if (SetProperty(ref cards, value))
                {

                }
            }
        }

        private double rotationAngle;
        public double RotationAngle
        {
            get => rotationAngle;
            set
            {
                if (SetProperty(ref rotationAngle, value))
                {

                }
            }
        }
    }
}
