Namespace Tenma
    Partial Friend Module Commands
        Friend Structure ReadCurrentFromSettingsCommand
            Implements ITenmaSerializable
            Public Channel As Channels
            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"ISET{CInt(Channel)}?"
            End Function
        End Structure
    End Module
End Namespace
