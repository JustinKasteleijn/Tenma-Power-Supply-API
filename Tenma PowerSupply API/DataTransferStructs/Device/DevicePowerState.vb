Namespace Tenma
    Namespace Device
        Public Structure DevicePowerState
            Implements TenmaSerializable
            Public State As PowerState

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"OUT{CInt(State)}"
            End Function
        End Structure
    End Namespace
End Namespace