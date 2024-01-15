Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Public Shared Function Beep(conn As SerialPort, command As WriteBeepStateCommand) As Result(Of Output, String)
            Return OpenConnection(conn).
                AndThen(Function(unused) SendData(conn, command)).
                AndThen(Function(unused) ReadStatus(conn)).
                Map(Function(id) id.Beep)
        End Function
    End Class
End Namespace