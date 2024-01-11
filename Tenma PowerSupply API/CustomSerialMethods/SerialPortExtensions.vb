Imports System.IO.Ports

Namespace Tenma
    Module SerialPortExtensions
        <System.Runtime.CompilerServices.Extension()>
        Public Function DataAvailable(ByVal connection As SerialPort) As Boolean
            Return connection.BytesToRead > 0
        End Function
    End Module
End Namespace