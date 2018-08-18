using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TechnicalFounders.Behaviors
{
    public class EntryValidationBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EntryValidationBehavior), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            this.BindingContext = bindable.BindingContext;

            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= Bindable_TextChanged;
        }

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Command?.Execute(e.NewTextValue);
        }
    }
}
