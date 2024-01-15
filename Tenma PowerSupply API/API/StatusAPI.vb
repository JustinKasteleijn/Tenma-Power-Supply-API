Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API

        Private Shared Function ReadStatus(conn As SerialPort) As Result(Of DeviceStatus, String)
            Return OpenConnection(conn).
                AndThen(Function(unused) SendData(conn, New ReadDeviceStatusCommand())).
                Apply(Sub(unused) Threading.Thread.Sleep(20)).
                AndThen(Function(unused) ReadDataWithTimeout(
                            conn,
                            New Timeout With {
                                .TotalMilliseconds = 250,
                                .Interval = 50
                            }
                        )
                 ).
                 Apply(Sub(unused) conn.Close()).
                 Assert(
                    Function(data) data.Length = 1,
                    Function(data) $"The data has unexpected length of {data.Length}, expected length: 1"
                 ).
                 Map(Function(data) data.First()).
                 Map(Function(data) New DeviceStatus With {
                    .ChannelOneMode = data And &B1,
                    .ChannelTwoMode = Convert.ToInt32((data And &B10) = &B10),
                    .Tracking = Convert.ToInt32((data And &B1100) = &B1100),
                    .Beep = Convert.ToInt32((data And &B10000) = &B10000),
                    .Lock = Convert.ToInt32((data And &B100000) = &B1000000),
                    .Output = Convert.ToInt32((data And &B1000000) = &B1000000)
                 })
        End Function
    End Class
End Namespace