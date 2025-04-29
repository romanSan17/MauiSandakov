using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiSandakov
{

public class CarouselItem
	{
		public string Title { get; set; }
		public string ImageUrl { get; set; }
	}
	public partial class carousel : ContentPage
	{
		private CarouselView carouselView;
		private List<CarouselItem> items;
		private int position = 0;
		public carousel ()
		{
			Title = "Karusselli näide";

			var items = new List<CarouselItem>
			{
				new CarouselItem { Title = "Päkesetõus", ImageUrl = ""},
				new CarouselItem { Title = "Metsavaikus", ImageUrl = ""},
				new CarouselItem { Title = "Järvepeegel", ImageUrl = ""}
			};

			var CarouselView = new CarouselView
			{
				ItemsSourse = items,
				HeightRequest = 300,
				IsBounceEnabled = true
			};

			carouselView.ItemTemplate = new DataTemplate(() =>
			{
				var frame = new Frame
				{
					CornerRadius = 20,
					HasShadow = true,
					Padding = 0,
					Margin = new Thickness(10),
					BackgroundColor = Colors.Transparent
				};
				var grid = new Grid();
				var image = new Image
				{
					Aspect = Aspect.AspectFill
				};
				image.SetBinding(Image.SourceProperty, "ImageUrl");
				var gradient = new BoxView
				{
					Background = new LinearGradientBrush
					{
						StartPoint = new Point(0, 1),
						EndPoint = new Point(0, 0),
						GradientStops = new GradientStopCollection
						{
							new GradientStop(Colors.Black.WithAlpha(0.6f), 0),
							new GradientStop(Colors.Transparent, 1)
						}
					},
					Opacity = 0.7
				};
				var label = new Label
				{
					TextColor = Colors.White,
					FontSize = 24,
					Margin = new Thickness(20),
					VerticalOptions = LayoutOptions.End,
					HorizontalOptions = LayoutOptions.Start
				};
				label.SetBinding(Label.TextProperty, "Title");
				var tap = new TapGestureRecognizer();
				tap.Tapped += async (s, e) =>
				{
					var tappedItem = ((Frame)s).BindingContext as CarouselItem;
					await DisplayAlert("Valisid:", tappedItem?.Title ?? "Tundmatu", "OK");
				};
				frame.GestureRecognizers.Add(tap);

				grid.Children.Add(image);
				grid.Children.Add(gradient);
				grid.Children.Add(label);

				frame.Content = grid;
				return frame;
			});

			var indicatorView = new IndicatorView
			{
				IndicatorColor = Colors.Gray,
				SelectedindicatorColor = Color.Blue,
				Horizontaloptions.Center,
				Margin = new Thickness(0, 10)
			};

			carouselView.IndicatorView = indicatorView;

			Device.StartTimer(TimeSpan.FromSeconds(4), () =>
			{
				if (items.Count == 0)
					return false;
				position = (position + 1) % items.Count;
				carouselView.Position = position;

				return true;
			});

			Content = new StackLayout
			{
				Padding = 20,
				ChildrenReordered =
				{
					carouselView,
					indicatorView
				}
			};
		}
	}
}