Namespace Tenma
    Namespace Commands
        Public Structure WriteDevicePowerStateCommand
            Implements TenmaSerializable
            Public State As State

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"OUT{CInt(State)}"
            End Function
        End Structure
    End Namespace
End Namespace