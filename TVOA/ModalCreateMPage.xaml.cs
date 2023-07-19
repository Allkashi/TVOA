using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TVOA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalCreateMPage : ContentPage
    {
        public ModalCreateMPage()
        {
            InitializeComponent();
        }

        private int _answerCount = 2;

        private void OnAddAnswerClicked(object sender, EventArgs e)
        {
            _answerCount++;
            var answerEntry = new Entry { Placeholder = $"Вариант {_answerCount}" };
            answerEntry.SetBinding(Entry.TextProperty, new Binding($"Answer{_answerCount}"));
            ((StackLayout)((Button)sender).Parent).Children.Insert(_answerCount + 2, answerEntry);
        }

        private async void btnUndo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}