namespace MauiSandakov;

public partial class StartPage : ContentPage
{
    public List<ContentPage> lehed = new List<ContentPage>()
    {
        new TextPage(0),
        new FigurePage(1),
        new ValgusfoorPage(),
        new RGBColorPage()
    };

    public List<string> tekstid = new List<string>
    {
        "Tee lahti TekstPage",
        "Tee lahti FigurePage",
        "Tee lahti ValgusfoorPage",
        "Tee lahti RGB"
    };

    ScrollView sv;
    VerticalStackLayout vsl;


    public StartPage()
    {
        Title = "Avaleht";
        vsl = new VerticalStackLayout { BackgroundColor = Colors.Brown };

        for (int i = 0; i < tekstid.Count; i++)
        {
            Button nupp = new Button
            {
                Text = tekstid[i],
                BackgroundColor = Colors.Brown,
                TextColor = Colors.Teal,
                BorderWidth = 2,
                FontFamily = "Arial",
                ZIndex = i
            };

            vsl.Add(nupp);
            nupp.Clicked += Nupp_CLicked;
        }

        sv = new ScrollView { Content = vsl };
        Content = sv;

    }

    private async void Nupp_CLicked(object? sender, EventArgs e)
    {
        Button btn = sender as Button;
        await Navigation.PushAsync(lehed[btn.ZIndex]);
    }
}