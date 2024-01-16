Namespace Tenma
    Partial Friend Module Commands
        Friend Structure WriteOVPStateCommand
            Implements ITenmaSerializable
            Public State As State

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"OVP{CInt(State)}"
            End Function
        End Structure
    End Module
End Namespace