Namespace Tenma
    Namespace Commands
        Public Structure WriteOVPStateCommand
            Implements TenmaSerializable
            Public State As Output

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"OUT{CInt(State)}"
            End Function
        End Structure
    End Namespace
End Namespace