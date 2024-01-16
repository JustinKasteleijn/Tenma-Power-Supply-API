Namespace Tenma
    Partial Friend Module Commands
        Friend Structure WriteOCPStateCommand
            Implements ITenmaSerializable
            Public State As State

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"OCP{CInt(State)}"
            End Function
        End Structure
    End Module
End Namespace