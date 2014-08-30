﻿using System;

namespace WPFTPService.FtpService
{
	public class FtpDirectoryListedEventArgs : EventArgs
	{
		private String[] Filenames = null;
		private String[] Directories = null;

		internal FtpDirectoryListedEventArgs(String[] Directories, String[] Filenames)
		{			
			this.Directories = Directories;
            this.Filenames = Filenames;
		}

		public String[] GetDirectories()
		{
			return this.Directories;
		}

		public String[] GetFilenames()
		{
			return this.Filenames;
		}
	}
}