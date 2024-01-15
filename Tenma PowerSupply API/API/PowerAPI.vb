Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Friend Module RemoteControlFunctions
        Friend Function WritePowerState(conn As SerialPort, state As WriteDevicePowerStateCommand) As Result(Of State, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, state)).
                        AndThen(Function(unused) ReadPowerstate(conn))
        End Function

        Friend Function ReadPowerstate(conn As SerialPort) As Result(Of State, String)
            Return ReadStatus(conn).
                Map(Function(status) status.Output)
        End Function
    End Module
End Namespace