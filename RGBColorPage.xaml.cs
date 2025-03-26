namespace MauiSandakov;

public partial class RGBColorPage : ContentPage
{
    Label lbl;
    Slider redSlider, greenSlider, blueSlider;
    Stepper sizeStepper;
    Button randomColorButton;
    AbsoluteLayout abs;

    public RGBColorPage()
    {
        Title = "RGB Color Control";

        lbl = new Label
        {
            Text = "RGB Color Picker",
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgb(255, 255, 255)
        };

        redSlider = CreateSlider();
        greenSlider = CreateSlider();
        blueSlider = CreateSlider();

        redSlider.ValueChanged += UpdateColor;
        greenSlider.ValueChanged += UpdateColor;
        blueSlider.ValueChanged += UpdateColor;

        sizeStepper = new Stepper
        {
            Minimum = 10,
            Maximum = 100,
            Increment = 5,
            Value = 20
        };
        sizeStepper.ValueChanged += UpdateSize;

        randomColorButton = new Button
        {
            Text = "Random Color",
            BackgroundColor = Colors.Gray,
            TextColor = Colors.White
        };
        randomColorButton.Clicked += RandomColorButton_Clicked;

        abs = new AbsoluteLayout { Children = { lbl, redSlider, greenSlider, blueSlider, sizeStepper, randomColorButton } };

        AbsoluteLayout.SetLayoutBounds(lbl, new Rect(10, 10, 300, 50));
        AbsoluteLayout.SetLayoutBounds(redSlider, new Rect(10, 80, 300, 50));
        AbsoluteLayout.SetLayoutBounds(greenSlider, new Rect(10, 140, 300, 50));
        AbsoluteLayout.SetLayoutBounds(blueSlider, new Rect(10, 200, 300, 50));
        AbsoluteLayout.SetLayoutBounds(sizeStepper, new Rect(10, 320, 300, 50));
        AbsoluteLayout.SetLayoutBounds(randomColorButton, new Rect(10, 260, 300, 50));

        Content = abs;
    }

    private Slider CreateSlider()
    {
        return new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 128,
            MinimumTrackColor = Colors.Gray,
            MaximumTrackColor = Colors.Black,
            ThumbColor = Colors.White
        };
    }

    private void UpdateColor(object sender, ValueChangedEventArgs e)
    {
        int red = Convert.ToInt32(redSlider.Value);
        int green = Convert.ToInt32(greenSlider.Value);
        int blue = Convert.ToInt32(blueSlider.Value);

        lbl.BackgroundColor = Color.FromRgb(red, green, blue);
        lbl.Text = $"RGB({red}, {green}, {blue})";
    }

    private void UpdateSize(object sender, ValueChangedEventArgs e)
    {
        lbl.FontSize = Convert.ToInt32(e.NewValue);
    }

    private async void RandomColorButton_Clicked(object sender, EventArgs e)
    {
        Random rnd = new Random();
        await AnimateSlider(redSlider, rnd.Next(0, 256));
        await AnimateSlider(greenSlider, rnd.Next(0, 256));
        await AnimateSlider(blueSlider, rnd.Next(0, 256));
    }

    private async Task AnimateSlider(Slider slider, int targetValue)
    {
        int currentValue = Convert.ToInt32(slider.Value);
        int step = (currentValue < targetValue) ? 1 : -1;

        while (currentValue != targetValue)
        {
            currentValue += step;
            slider.Value = currentValue;
            await Task.Delay(5);
        }
    }
}
