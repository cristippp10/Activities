using System;
using System.Activities;
namespace FtpActivities.Utilities
{
	public static class PermissionExtensions
	{
		public static void CheckPermissions(this Activity activity)
		{
			PermissionExtensions.CheckPermissions(activity.GetType().GetCustomAttributes(typeof(PermissionAttribute), true) as PermissionAttribute[]);
		}
		public static void CheckPermissions(params PermissionAttribute[] permissions)
		{
			if (!PermissionAttribute.IsRegistred)
			{
				throw new RegistrationException();
			}
			if (permissions.Length == 0)
			{
				return;
			}
			for (int i = 0; i < permissions.Length; i++)
			{
				PermissionAttribute permissionAttribute = permissions[i];
				if (permissionAttribute.HasPermission)
				{
					return;
				}
			}
			throw new PermissionException(permissions);
		}
	}
}
