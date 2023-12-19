namespace ContactBookMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private List<string> _items = [];

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Add_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Entry_Name.Text))
            {
                _items.Add(Entry_Name.Text);
                Entry_Name.Text = string.Empty;
            }
        }
    }

}
