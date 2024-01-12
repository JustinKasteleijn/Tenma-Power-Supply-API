Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Public Shared Function SaveMemorySettings(conn As SerialPort, commnand As SaveMemorySettingCommand) As Result(Of String, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, commnand)).
                        AndThen(Function(unused) ReadDataWithTimeout(
                                    conn,
                                    New Timeout With {
                                        .TotalMilliseconds = 250,
                                        .Interval = 50
                                    })
                        ).
                        Apply(Sub(unused) conn.Close()).
                        Map(Function(data) data.ToString())
        End Function
    End Class
End Namespace