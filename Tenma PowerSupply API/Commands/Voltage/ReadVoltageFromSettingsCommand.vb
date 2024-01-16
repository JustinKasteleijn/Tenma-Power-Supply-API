Namespace Tenma
    Partial Friend Module Commands
        Friend Structure ReadVoltageFromSettingsCommand
            Implements ITenmaSerializable
            Public Channel As Channels
            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"VSET{CInt(Channel)}?"
            End Function
        End Structure
    End Module
End Namespace
