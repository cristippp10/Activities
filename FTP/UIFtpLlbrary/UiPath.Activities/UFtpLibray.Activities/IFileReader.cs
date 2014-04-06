using System;
using System.Collections.Generic;
using UiPath.Library;
namespace FtpActivities
{
	internal interface IFileReader
	{
		int NumberOfPages
		{
			get;
		}
		string GetPageText(int pageNo);
		System.Collections.Generic.List<Image> GetPageImages(int pageNo);
	}
}
