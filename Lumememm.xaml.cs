using Microsoft.Maui.Layouts;
using Microsoft.Maui.Graphics;

namespace MauiSandakov;

public partial class Lumememm : ContentPage
{
    AbsoluteLayout absoluteLayout;
    BoxView bucket;
    Frame head, body; 
    Label actionLabel;
    Button hideButton, showButton, colorButton, meltButton;
    Slider sizeSlider;
    Stepper sizeStepper;

    public Lumememm()
    {
        absoluteLayout = new AbsoluteLayout();

        bucket = new BoxView { Color = Colors.Brown };
        head = new Frame { BackgroundColor = Colors.White, CornerRadius = 50, WidthRequest = 50, HeightRequest = 50 }; 
        body = new Frame { BackgroundColor = Colors.White, CornerRadius = 80, WidthRequest = 80, HeightRequest = 80 }; 

        absoluteLayout.Children.Add(bucket);
        AbsoluteLayout.SetLayoutBounds(bucket, new Rect(0.5, 0.1, 60, 30));
        AbsoluteLayout.SetLayoutFlags(bucket, AbsoluteLayoutFlags.PositionProportional);

        absoluteLayout.Children.Add(body);
        AbsoluteLayout.SetLayoutBounds(body, new Rect(0.5, 6.9, 220, 220));
        AbsoluteLayout.SetLayoutFlags(body, AbsoluteLayoutFlags.PositionProportional);

        absoluteLayout.Children.Add(head);
        AbsoluteLayout.SetLayoutBounds(head, new Rect(0.5, 0.35, 60, 60));
        AbsoluteLayout.SetLayoutFlags(head, AbsoluteLayoutFlags.PositionProportional);

        head.CornerRadius = 30;
        body.CornerRadius = 45;

        actionLabel = new Label { Text = "Valige tegevus", HorizontalOptions = LayoutOptions.Center };

        hideButton = new Button { Text = "Peida lumeinimene" };
        hideButton.Clicked += (s, e) => { SetVisibility(false); actionLabel.Text = "Lumemees on peidetud"; };

        showButton = new Button { Text = "Näita lumeinimest" };
        showButton.Clicked += (s, e) => { SetVisibility(true); actionLabel.Text = "Lumeinimene kuvatakse"; };

        colorButton = new Button { Text = "Värvimine" };
        colorButton.Clicked += (s, e) => { SetRandomColor(); actionLabel.Text = "Lumemees on värviline"; };

        meltButton = new Button { Text = "Sulata lumeinimene" };
        meltButton.Clicked += async (s, e) => { await MeltSnowman(); actionLabel.Text = "Lumemees sulas"; };

        sizeSlider = new Slider(50, 150, 80);
        sizeSlider.ValueChanged += (s, e) => ResizeSnowman(e.NewValue);

        sizeStepper = new Stepper(50, 150, 80, 5);
        sizeStepper.ValueChanged += (s, e) => ResizeSnowman(e.NewValue);

        StackLayout controls = new StackLayout
        {
            Children = { hideButton, showButton, colorButton, meltButton, sizeSlider, sizeStepper, actionLabel },
            Padding = new Thickness(10),
            Spacing = 10
        };

        Content = new StackLayout { Children = { absoluteLayout, controls } };
    }

    void SetVisibility(bool isVisible)
    {
        bucket.IsVisible = isVisible;
        head.IsVisible = isVisible;
        body.IsVisible = isVisible;
    }

    void SetRandomColor()
    {
        Random random = new Random();
        head.BackgroundColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
        body.BackgroundColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
    }

    async Task MeltSnowman()
    {
        for (double i = 1; i >= 0; i -= 0.1)
        {
            head.Opacity = i;
            body.Opacity = i;
            bucket.Opacity = i;
            await Task.Delay(200);
        }
        SetVisibility(false);
    }

    void ResizeSnowman(double size)
    {

        double headSize = size * 0.6;  
        double bodySize = size;  
        double bucketWidth = size * 0.5;  
        double bucketHeight = size * 0.3;  

       
        AbsoluteLayout.SetLayoutBounds(body, new Rect(0.5, 6.9, bodySize, bodySize));  
        AbsoluteLayout.SetLayoutBounds(head, new Rect(0.5, 0.35, headSize, headSize));  
        AbsoluteLayout.SetLayoutBounds(bucket, new Rect(0.5, 0.1, bucketWidth, bucketHeight));  

        
        AbsoluteLayout.SetLayoutFlags(body, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutFlags(head, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutFlags(bucket, AbsoluteLayoutFlags.PositionProportional);
    }
}