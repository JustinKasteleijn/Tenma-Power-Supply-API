Namespace Tenma
    Namespace Commands
        Friend Structure WriteBeepStateCommand
            Implements ITenmaSerializable
            Public State As State

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"OCP{CInt(State)}"
            End Function
        End Structure
    End Namespace
End Namespace