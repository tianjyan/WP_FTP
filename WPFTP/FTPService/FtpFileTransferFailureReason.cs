namespace WPFTPService.FtpService
{
	public enum  FtpFileTransferFailureReason: byte
	{
		None,
		MemoryCardNotFound,
		FileDoesNotExist,
		InputOutputError
	}
}