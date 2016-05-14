using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magic
{
	public class UIEventArgsCardClick : EventArgs
	{
		public UICard ClickedCard;
		public UIEventArgsCardClick() { }
	}

	public class UIEventArgsChatBox : EventArgs
	{
		public Player Sender;
		public string Text;
		public Key LastKey;
	}
}
