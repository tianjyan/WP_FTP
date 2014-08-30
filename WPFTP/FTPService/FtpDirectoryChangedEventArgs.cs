using System;

namespace WPFTPService.FtpService
{
	public class FtpDirectoryChangedEventArgs
	{
		internal FtpDirectoryChangedEventArgs(String RemoteDirectory)
		{
			this.RemoteDirectory = RemoteDirectory;
		}

		public String RemoteDirectory
		{
			get;
			private set;
		}
	}
}