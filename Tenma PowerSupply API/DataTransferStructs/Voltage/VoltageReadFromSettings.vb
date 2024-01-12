Namespace Tenma
    Namespace Voltage
        Public Structure VoltageReadFromSettings
            Implements TenmaSerializable
            Public Channel As Channels
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"VSET{CInt(Channel)}?"
            End Function
        End Structure
    End Namespace
End Namespace
