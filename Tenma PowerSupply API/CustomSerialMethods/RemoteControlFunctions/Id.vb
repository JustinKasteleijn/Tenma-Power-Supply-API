﻿Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Friend Module RemoteControlFunctions
        Friend Function ReadDeviceID(conn As SerialPort) As Result(Of DeviceID, String)
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
                                    .PartNumber = seperated(1),
                                    .Version = Utils.StringToDecimal(
                                        seperated(2).Replace("V", "")
                                    ).UnwrapOr(0),
                                    .SerialNumber = Utils.StringToInt(
                                        String.Concat(seperated(3).Where(Function(c) Char.IsDigit(c)))
                                    ).UnwrapOr(0)
                                }
                            End Function)
        End Function
    End Module
End Namespace