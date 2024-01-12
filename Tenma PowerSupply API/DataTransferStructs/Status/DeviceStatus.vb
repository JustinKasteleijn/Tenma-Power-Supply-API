Namespace Tenma
    Namespace Status
        Public Structure GetDeviceStatus
            Implements TenmaSerializable

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return "STATUS?"
            End Function
        End Structure
        Public Structure DeviceStatus
            Public ChannelOneMode As ChannelMode
            Public ChannelTwoMode As ChannelMode
            Public Tracking As Tracking
            Public Beep As PowerState
            Public Lock As Lock
            Public Output As PowerState
        End Structure

        Public Enum ChannelMode As Byte
            CC = 0
            CV = 1
        End Enum

        Public Enum Tracking As UShort
            Independent = 0
            TrackingSeries = 1
            TrackingParallel = 11
        End Enum

        Public Enum PowerState As Byte
            OFF = 0
            [ON] = 1
        End Enum

        Public Enum Lock As Byte
            Lock = 0
            Unlock = 1
        End Enum
    End Namespace
End Namespace