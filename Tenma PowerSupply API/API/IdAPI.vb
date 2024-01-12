Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Public Shared Function ReadDeviceID(conn As SerialPort) As Result(Of DeviceID, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, New ReadDeviceIDCommand())).
                        AndThen(Function(unused) ReadDataWithTimeout(
                                conn,
                                New Timeout With {
                                    .TotalMilliseconds = 250,
                                    .Interval = 50
                                })
                        ).
                        Apply(Sub(unused) conn.Close()).
                        AndThen(Function(data) Utils.BytesToString(data)).
                        Map(Function(data)
                                Dim seperated As String() = data.Split(" ")
                                Return New DeviceID With {
                                    .Manufacturer = seperated(0),
                                    .Model = seperated(1),
                                    .Version = Utils.StringToDecimal(
                                        seperated(2).Replace("V", "")
                                    ).Match(
                                        Function(d) d,
                                        Function(err) 0
                                    ),
                                    .SerialNumber = Utils.StringToInt(
                                        String.Concat(seperated(3).Where(Function(c) Char.IsDigit(c)))
                                    ).Match(
                                        Function(d) d,
                                        Function(err) 0
                                    )
                                }
                            End Function)
        End Function
    End Class
End Namespace