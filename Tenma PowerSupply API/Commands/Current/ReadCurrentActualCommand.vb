Namespace Tenma
    Namespace Commands
        Friend Structure ReadCurrentActualCommand
            Implements ITenmaSerializable
            Public Channel As Channels

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"IOUT{CInt(Channel)}?"
            End Function
        End Structure
    End Namespace
End Namespace