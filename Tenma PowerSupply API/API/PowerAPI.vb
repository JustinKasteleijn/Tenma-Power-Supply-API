Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Private Shared Function WritePowerState(conn As SerialPort, state As WriteDevicePowerStateCommand) As Result(Of Output, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, state)).
                        Apply(Sub(unused) Threading.Thread.Sleep(20)).
                        AndThen(Function(unused) ReadStatus(conn)).
                        Map(Function(deviceStatus) deviceStatus.Output)
        End Function

        Public Shared Function TurnOn(conn As SerialPort) As Result(Of Output, String)
            Return WritePowerState(
                conn,
                New WriteDevicePowerStateCommand With {.State = Output.ON}
            )
        End Function

        Public Shared Function TurnOff(conn As SerialPort) As Result(Of Output, String)
            Return WritePowerState(
                conn,
                New WriteDevicePowerStateCommand With {.State = Output.OFF}
            )
        End Function

        Public Shared Function ReadPowerstate(conn As SerialPort) As Result(Of Output, String)
            Return ReadStatus(conn).
                Map(Function(status) status.Output)
        End Function
    End Class
End Namespace