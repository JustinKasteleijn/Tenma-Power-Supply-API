Namespace Tenma
    Namespace Commands
        Friend Structure ReadVoltageActualCommand
            Implements TenmaSerializable
            Public Channel As Channels

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"VOUT{CInt(Channel)}?"
            End Function
        End Structure
    End Namespace
End Namespace