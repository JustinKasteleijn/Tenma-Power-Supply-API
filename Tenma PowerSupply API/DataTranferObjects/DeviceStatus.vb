Namespace Tenma
    Partial Public Module DataTransferObjects
        Public Structure DeviceStatus
            Public ChannelOneMode As ChannelMode
            Public ChannelTwoMode As ChannelMode
            Public Tracking As Tracking
            Public Beep As Output
            Public Lock As Lock
            Public Output As Output
        End Structure

    End Module
End Namespace