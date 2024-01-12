Namespace Tenma
    Namespace Current
        Public Structure ReadCurrentFromSettingsCommand
            Implements TenmaSerializable
            Public Channel As Channels
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"ISET{CInt(Channel)}?"
            End Function
        End Structure
    End Namespace
End Namespace
