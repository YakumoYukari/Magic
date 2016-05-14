using DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Magic
{
	public class UICard : Image
	{
		public float AspectRatio = 12.0f/17.0f;
		public bool IsTweening = false;
		public bool UpdateCardInspector = true;
		public Card TargetCard;

		public CardPile Pile;
		public int PileIndex;

		public bool Dragging = false;

		public UICard(Card target)
		{
			TargetCard = target;
			Initialize();
		}

		public void Initialize()
		{
			RenderTransform = new RotateTransform();
			RenderTransformOrigin = new Point(0.5, 0.5);
			VisualBitmapScalingMode = System.Windows.Media.BitmapScalingMode.Fant;
		}

		public void InsertIntoCardPile(CardPile pile)
		{
			pile.AddCard(this);
		}

		public void Scale(double height)
		{
			this.Height = height;
			this.Width = (int)(AspectRatio * height);
		}

		public void SetPos(double x, double y)
		{
			Thickness m = this.Margin;
			m.Left = x;
			m.Top = y;
			this.Margin = m;
		}

		#region Overrides
		protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);

			EventManager.NotifyEvent(Events.UI_CARD_CLICKED, this, new UIEventArgsCardClick());
		}
		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			Mouse.Capture(this);
		}
		protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.LeftButton == MouseButtonState.Pressed)
			{
				var pos = Mouse.GetPosition(this);

				this.SetPos(pos.X, pos.Y);
			}
			
		}
		protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			if (UpdateCardInspector)
				UIMain.Instance.SetCardInspector(TargetCard);
		}
		#endregion
	}
}
