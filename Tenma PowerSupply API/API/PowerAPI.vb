Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Private Shared Function WritePowerState(conn As SerialPort, state As WriteDevicePowerStateCommand) As Result(Of State, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, state)).
                        Apply(Sub(unused) Threading.Thread.Sleep(20)).
                        AndThen(Function(unused) ReadPowerstate(conn))
        End Function

        Private Shared Function ReadPowerstate(conn As SerialPort) As Result(Of State, String)
            Return ReadStatus(conn).
                Map(Function(status) status.Output)
        End Function
    End Class
End Namespace