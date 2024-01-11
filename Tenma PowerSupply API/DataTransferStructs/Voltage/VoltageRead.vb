Namespace Tenma
    Namespace Voltage
        Public Class VoltageRead
            Implements TenmaSerializable
            Public Channel As Channels
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Dim command As String = $"VSET{CInt(Channel)}?"
                Return command
            End Function
        End Class
    End Namespace
End Namespace
