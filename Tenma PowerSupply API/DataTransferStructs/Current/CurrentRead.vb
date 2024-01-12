Namespace Tenma
    Namespace Voltage
        Public Class CurrentRead
            Implements TenmaSerializable
            Public Channel As Channels
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Dim command As String = $"ISET{CInt(Channel)}?"
                Return command
            End Function
        End Class
    End Namespace
End Namespace
