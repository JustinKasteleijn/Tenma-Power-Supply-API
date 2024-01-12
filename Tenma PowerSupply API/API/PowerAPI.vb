Imports System.IO.Ports
Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Public Class Commands
        Public Shared Function SetPowerState(conn As SerialPort, state As Device.WriteDevicePowerStateCommand) As Result(Of Output, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, state)).
                        Apply(Sub(unused) Threading.Thread.Sleep(20)).
                        AndThen(Function(unused) ReadStatus(conn)).
                        Map(Function(deviceStatus) deviceStatus.Output)
        End Function

        Public Shared Function GetPowerstate(conn As SerialPort) As Result(Of Output, String)
            Return ReadStatus(conn).
                Map(Function(status) status.Output)
        End Function
    End Class
End Namespace