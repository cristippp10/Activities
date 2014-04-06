using System;
using System.Activities;
using System.Activities.Validation;
using System.ComponentModel;
using FtpActivities.Properties;
using UiPath.Library;
namespace FtpActivities
{
	public abstract class BaseHoverActivity : BaseElementActivity
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
		protected override void CacheMetadata(NativeActivityMetadata metadata)
		{
			metadata.AddArgument(new RuntimeArgument("DelayMS", typeof(int), ArgumentDirection.In, false));
			if (this.OffsetDirection != OffsetDirection.None)
			{
				metadata.AddValidationError(new ValidationError(Resources.DeprecateOffsetDirection, true, "OffsetDirection"));
			}
			base.CacheMetadata(metadata);
		}
		protected void Hover(UiElement node, InputMethod method)
		{
			int offsetX = this.OffsetX;
			int offsetY = this.OffsetY;
			Position mousePlacement = Position.TopLeft;
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
			node.Hover(offsetX, offsetY, method, 1000, mousePlacement);
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
	}
}
