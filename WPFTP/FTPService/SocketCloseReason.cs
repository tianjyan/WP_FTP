﻿namespace WPFTPService.FtpService
{
    public enum SocketCloseReason : byte
    {
        None = 0,
        ClosedFromLocalHost = 1,
        ClosedByRemoteHost = 2,
        UnknownReason = 3
    }
}