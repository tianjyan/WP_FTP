namespace WPFTPService.FtpService
{
    public enum FtpPassiveOperation: byte
    {
        None,
        FileUpload,
        FileDownload,
		ListDirectory
    }
}