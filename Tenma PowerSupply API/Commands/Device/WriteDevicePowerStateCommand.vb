Namespace Tenma
    Partial Friend Module Commands
        Public Structure WriteDevicePowerStateCommand
            Implements ITenmaSerializable
            Public State As State

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"OUT{CInt(State)}"
            End Function
        End Structure
    End Module
End Namespace