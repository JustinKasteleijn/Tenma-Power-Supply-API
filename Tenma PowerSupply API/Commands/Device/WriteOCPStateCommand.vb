Namespace Tenma
    Namespace Commands
        Public Structure WriteOCPStateCommand
            Implements TenmaSerializable
            Public State As State

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"OCP{CInt(State)}"
            End Function
        End Structure
    End Namespace
End Namespace