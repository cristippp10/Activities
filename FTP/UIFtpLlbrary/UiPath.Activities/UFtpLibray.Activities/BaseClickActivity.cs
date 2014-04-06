using System;
using System.Activities;
using System.Activities.Validation;
using System.ComponentModel;
using System.Threading;
using FtpActivities.Properties;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseClickActivity : BaseElementActivity
	{
		[Category("Mouse")]
		public int OffsetX
		{
			get;
			set;
		}
		[Category("Mouse")]
		public int OffsetY
		{
			get;
			set;
		}
		[Category("Mouse")]
		public MouseButton MouseButton
		{
			get;
			set;
		}
		[Category("Mouse")]
		public ClickType ClickType
		{
			get;
			set;
		}
		[Browsable(false), Category("Mouse"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), System.Obsolete("Use Position")]
		public OffsetDirection OffsetDirection
		{
			get;
			set;
		}
		[Category("Mouse")]
		public Position Position
		{
			get;
			set;
		}
		[Category("Common")]
		public InArgument<int> DelayMS
		{
			get;
			set;
		}
		[Category("Key Modifiers")]
		public bool Alt
		{
			get;
			set;
		}
		[Category("Key Modifiers")]
		public bool Ctrl
		{
			get;
			set;
		}
		[Category("Key Modifiers")]
		public bool Shift
		{
			get;
			set;
		}
		[Category("Key Modifiers")]
		public bool Win
		{
			get;
			set;
		}
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("DelayMS", typeof(int), ArgumentDirection.In, false));
			if (this.OffsetDirection != OffsetDirection.None)
			{
				metadata.AddValidationError(new ValidationError(Resources.DeprecateOffsetDirection, true, "OffsetDirection"));
			}
			base.CacheMetadata(metadata);
		}
		protected void Click(UiElement node, InputMethod method)
		{
			int offsetX = this.OffsetX;
			int offsetY = this.OffsetY;
			Position mousePlacement = Position.TopLeft;
			this.PressReleaseKeys(node, method, true);
			try
			{
				if (this.OffsetDirection == OffsetDirection.None)
				{
					mousePlacement = this.Position;
				}
				else
				{
					if (this.OffsetDirection == OffsetDirection.Normal && offsetX == 0 && offsetY == 0)
					{
						mousePlacement = Position.Center;
					}
					else
					{
						this.SetOffsetsWithDirection(ref offsetX, ref offsetY, node);
					}
				}
				node.Click(offsetX, offsetY, this.ClickType, this.MouseButton, method, mousePlacement);
				this.PressReleaseKeys(node, method, false);
			}
			catch
			{
				try
				{
					this.PressReleaseKeys(node, method, false);
				}
				catch
				{
				}
				throw;
			}
		}
		private void SetOffsetsWithDirection(ref int offsetX, ref int offsetY, UiElement node)
		{
			switch (this.OffsetDirection)
			{
			case OffsetDirection.Left:
				offsetX = -offsetX;
				return;
			case OffsetDirection.Top:
				offsetY = -offsetY;
				return;
			case OffsetDirection.Right:
				if (base.ClippingRegion != null)
				{
					if (base.ClippingRegion.Rectangle.HasValue)
					{
						goto IL_8C;
					}
				}
				try
				{
					Region position = node.GetPosition();
					if (position != null && position.Rectangle.HasValue)
					{
						offsetX = position.Rectangle.Value.Width + offsetX;
					}
					break;
				}
				catch
				{
					break;
				}
				IL_8C:
				offsetX = base.ClippingRegion.Rectangle.Value.Width + offsetX;
				return;
			case OffsetDirection.Bottom:
				if (base.ClippingRegion != null)
				{
					if (base.ClippingRegion.Rectangle.HasValue)
					{
						goto IL_10A;
					}
				}
				try
				{
					Region position2 = node.GetPosition();
					if (position2 != null && position2.Rectangle.HasValue)
					{
						offsetY = position2.Rectangle.Value.Height + offsetY;
					}
					break;
				}
				catch
				{
					break;
				}
				IL_10A:
				offsetY = base.ClippingRegion.Rectangle.Value.Height + offsetY;
				break;
			default:
				return;
			}
		}
		private void PressReleaseKeys(UiElement node, InputMethod method, bool press)
		{
			string text = "";
			char c = press ? 'd' : 'u';
			if (this.Alt)
			{
				text = text + c + "(alt)";
			}
			if (this.Ctrl)
			{
				text = text + c + "(ctrl)";
			}
			if (this.Shift)
			{
				text = text + c + "(shift)";
			}
			if (this.Win)
			{
				text = text + c + "(lwin)";
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				text = "[" + text + "]";
				if (method == InputMethod.API)
				{
					method = InputMethod.SYNTHESIZE_INPUT;
				}
				if (press)
				{
					node.WriteText(text, method);
					System.Threading.Thread.Sleep(100);
					return;
				}
				System.Threading.Thread.Sleep(100);
				node.WriteText(text, method);
			}
		}
	}
}
