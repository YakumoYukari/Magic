using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artefact;
using Artefact.Animation;
using Artefact.Utilities;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace Magic
{
	public static class UITweens
	{
		public static void TapCard(Card card)
		{
			TapCard(card.UI);
		}
		public static void TapCard(UICard card)
		{
			//MainWindow.Instance.CardDisplayImage.RotateTo(90.0d, 1.0d, AnimationTransitions.QuadEaseInOut, 0);

			Storyboard storyboard = new Storyboard();
			storyboard.Duration = new Duration(TimeSpan.FromSeconds(0.2));
			DoubleAnimation rotateAnimation = new DoubleAnimation()
			{
				From = null,
				To = 90,
				Duration = storyboard.Duration
			};
			storyboard.Children.Add(rotateAnimation);

			Storyboard.SetTarget(rotateAnimation, card);
			Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

			storyboard.Begin();
			//card.RotateTo(-90.0d, 1.0d, AnimationTransitions.ElasticEaseIn, 0);
			//card.ScaleTo(500,500,1.0d, AnimationTransitions.QuadEaseInOut, 0.0d);
		}

		public static void UntapCard(Card card)
		{
			UntapCard(card.UI);
		}
		public static void UntapCard(UICard card)
		{
			//card.RotateTo(0.0d, 1.0d, AnimationTransitions.QuadEaseInOut, 0.0d);

			Storyboard storyboard = new Storyboard();
			storyboard.Duration = new Duration(TimeSpan.FromSeconds(0.2));
			DoubleAnimation rotateAnimation = new DoubleAnimation()
			{
				From = null,
				To = 0,
				Duration = storyboard.Duration
			};
			storyboard.Children.Add(rotateAnimation);

			Storyboard.SetTarget(rotateAnimation, card);
			Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

			storyboard.Begin();
		}
	}
}
