Namespace Tenma
    Namespace Commands
        Friend Structure ReadVoltageActualCommand
            Implements ITenmaSerializable
            Public Channel As Channels

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"VOUT{CInt(Channel)}?"
            End Function
        End Structure
    End Namespace
End Namespace