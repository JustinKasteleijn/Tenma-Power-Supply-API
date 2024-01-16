Namespace Tenma
    Partial Friend Module Commands
        Friend Structure ReadVoltageActualCommand
            Implements ITenmaSerializable
            Public Channel As Channels

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"VOUT{CInt(Channel)}?"
            End Function
        End Structure
    End Module
End Namespace