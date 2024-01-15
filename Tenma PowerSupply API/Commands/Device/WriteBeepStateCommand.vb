Namespace Tenma
    Namespace Commands
        Public Structure WriteBeepStateCommand
            Implements TenmaSerializable
            Public State As Output

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"OCP{CInt(State)}"
            End Function
        End Structure
    End Namespace
End Namespace