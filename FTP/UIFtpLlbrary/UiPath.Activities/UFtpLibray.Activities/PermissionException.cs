using System;
using System.Linq;
using FtpActivities.Properties;
using FtpActivities.Utilities;
using UiPath.Library.EnumExtensions;
namespace FtpActivities
{
	[System.Serializable]
	public class PermissionException : System.Exception
	{
		public PermissionException(params PermissionAttribute[] permissions) : base(PermissionException.GetPermissionMessage(permissions))
		{
		}
		public static string GetPermissionMessage(params PermissionAttribute[] permissions)
		{
			return string.Format((permissions.Length == 1) ? Resources.OneMissingPermission : Resources.MultipleMissingPermissions, string.Join(",", permissions.Select(delegate(PermissionAttribute p)
			{
				if (p.RequiredPermission.HasValue)
				{
					return p.RequiredPermission.Value.FriendlyName();
				}
				return p.RequiredSku.Value.FriendlyName();
			})));
		}
	}
}
