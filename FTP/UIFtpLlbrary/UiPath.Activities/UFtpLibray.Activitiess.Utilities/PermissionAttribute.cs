using System;
using UiPath.Library;
namespace FtpActivities.Utilities
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class PermissionAttribute : System.Attribute
	{
		private static bool? isRegistred;
		private static bool[,] skuHasPermission;
		private static bool[] hasPermission;
		private static Skus sku;
		public bool HasPermission
		{
			get
			{
				if (this.RequiredPermission.HasValue)
				{
					return PermissionAttribute.hasPermission[(int)this.RequiredPermission.Value];
				}
				return !this.RequiredSku.HasValue || this.RequiredSku <= PermissionAttribute.sku;
			}
		}
		public Permissions? RequiredPermission
		{
			get;
			private set;
		}
		public Skus? RequiredSku
		{
			get;
			private set;
		}
		public static bool IsRegistred
		{
			get
			{
				if (!PermissionAttribute.isRegistred.HasValue)
				{
					PermissionAttribute.isRegistred = new bool?(PermissionAttribute.InitRegistrationInfo());
				}
				return PermissionAttribute.isRegistred.Value;
			}
		}
		public PermissionAttribute(Permissions requiredPermission)
		{
			this.RequiredPermission = new Permissions?(requiredPermission);
		}
		public PermissionAttribute(Skus requiredSku)
		{
			this.RequiredSku = new Skus?(requiredSku);
		}
		public static bool InitRegistrationInfo()
		{
			Licences licences;
			bool registrationInfo = Setup.GetRegistrationInfo(out PermissionAttribute.sku, out licences);
			for (int i = 0; i < 11; i++)
			{
				PermissionAttribute.hasPermission[i] = PermissionAttribute.skuHasPermission[(int)PermissionAttribute.sku, i];
			}
			PermissionAttribute.hasPermission[0] = (PermissionAttribute.hasPermission[0] && licences == Licences.DevToolsRoyalty);
			PermissionAttribute.hasPermission[9] = (PermissionAttribute.hasPermission[9] && licences == Licences.RuntimeSite);
			return registrationInfo;
		}
		static PermissionAttribute()
		{
			PermissionAttribute.hasPermission = new bool[11];
			PermissionAttribute.skuHasPermission = new bool[7, 11];
			PermissionAttribute.skuHasPermission[0, 0] = true;
			PermissionAttribute.skuHasPermission[0, 1] = false;
			PermissionAttribute.skuHasPermission[0, 2] = false;
			PermissionAttribute.skuHasPermission[0, 3] = false;
			PermissionAttribute.skuHasPermission[0, 4] = false;
			PermissionAttribute.skuHasPermission[0, 5] = false;
			PermissionAttribute.skuHasPermission[0, 6] = false;
			PermissionAttribute.skuHasPermission[0, 7] = false;
			PermissionAttribute.skuHasPermission[0, 8] = false;
			PermissionAttribute.skuHasPermission[0, 9] = false;
			PermissionAttribute.skuHasPermission[0, 10] = false;
			PermissionAttribute.skuHasPermission[1, 0] = true;
			PermissionAttribute.skuHasPermission[1, 1] = true;
			PermissionAttribute.skuHasPermission[1, 2] = false;
			PermissionAttribute.skuHasPermission[1, 3] = false;
			PermissionAttribute.skuHasPermission[1, 4] = false;
			PermissionAttribute.skuHasPermission[1, 5] = false;
			PermissionAttribute.skuHasPermission[1, 6] = false;
			PermissionAttribute.skuHasPermission[1, 7] = false;
			PermissionAttribute.skuHasPermission[1, 8] = false;
			PermissionAttribute.skuHasPermission[1, 9] = false;
			PermissionAttribute.skuHasPermission[1, 10] = false;
			PermissionAttribute.skuHasPermission[2, 0] = true;
			PermissionAttribute.skuHasPermission[2, 1] = true;
			PermissionAttribute.skuHasPermission[2, 2] = true;
			PermissionAttribute.skuHasPermission[2, 3] = false;
			PermissionAttribute.skuHasPermission[2, 4] = false;
			PermissionAttribute.skuHasPermission[2, 5] = false;
			PermissionAttribute.skuHasPermission[2, 6] = false;
			PermissionAttribute.skuHasPermission[2, 7] = false;
			PermissionAttribute.skuHasPermission[2, 8] = false;
			PermissionAttribute.skuHasPermission[2, 9] = false;
			PermissionAttribute.skuHasPermission[2, 10] = false;
			PermissionAttribute.skuHasPermission[3, 0] = true;
			PermissionAttribute.skuHasPermission[3, 1] = true;
			PermissionAttribute.skuHasPermission[3, 2] = true;
			PermissionAttribute.skuHasPermission[3, 3] = true;
			PermissionAttribute.skuHasPermission[3, 4] = true;
			PermissionAttribute.skuHasPermission[3, 5] = true;
			PermissionAttribute.skuHasPermission[3, 6] = false;
			PermissionAttribute.skuHasPermission[3, 7] = false;
			PermissionAttribute.skuHasPermission[3, 8] = false;
			PermissionAttribute.skuHasPermission[3, 9] = false;
			PermissionAttribute.skuHasPermission[3, 10] = false;
			PermissionAttribute.skuHasPermission[4, 0] = true;
			PermissionAttribute.skuHasPermission[4, 1] = true;
			PermissionAttribute.skuHasPermission[4, 2] = true;
			PermissionAttribute.skuHasPermission[4, 3] = true;
			PermissionAttribute.skuHasPermission[4, 4] = true;
			PermissionAttribute.skuHasPermission[4, 5] = true;
			PermissionAttribute.skuHasPermission[4, 6] = true;
			PermissionAttribute.skuHasPermission[4, 7] = true;
			PermissionAttribute.skuHasPermission[4, 8] = true;
			PermissionAttribute.skuHasPermission[4, 9] = false;
			PermissionAttribute.skuHasPermission[4, 10] = true;
			PermissionAttribute.skuHasPermission[5, 0] = true;
			PermissionAttribute.skuHasPermission[5, 1] = true;
			PermissionAttribute.skuHasPermission[5, 2] = true;
			PermissionAttribute.skuHasPermission[5, 3] = true;
			PermissionAttribute.skuHasPermission[5, 4] = true;
			PermissionAttribute.skuHasPermission[5, 5] = true;
			PermissionAttribute.skuHasPermission[5, 6] = true;
			PermissionAttribute.skuHasPermission[5, 7] = true;
			PermissionAttribute.skuHasPermission[5, 8] = true;
			PermissionAttribute.skuHasPermission[5, 9] = true;
			PermissionAttribute.skuHasPermission[5, 10] = true;
			PermissionAttribute.skuHasPermission[6, 0] = true;
			PermissionAttribute.skuHasPermission[6, 1] = true;
			PermissionAttribute.skuHasPermission[6, 2] = true;
			PermissionAttribute.skuHasPermission[6, 3] = true;
			PermissionAttribute.skuHasPermission[6, 4] = true;
			PermissionAttribute.skuHasPermission[6, 5] = true;
			PermissionAttribute.skuHasPermission[6, 6] = true;
			PermissionAttribute.skuHasPermission[6, 7] = true;
			PermissionAttribute.skuHasPermission[6, 8] = true;
			PermissionAttribute.skuHasPermission[6, 9] = true;
			PermissionAttribute.skuHasPermission[6, 10] = true;
		}
	}
}
