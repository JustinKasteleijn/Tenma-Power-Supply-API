Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Public Shared Function SetCurrent(conn As SerialPort, currentSetting As WriteCurrentCommand) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                Assert(
                    Function(unused) currentSetting.CheckVoltageBetweenMinMax(),
                    Function(unused) $"Voltage {currentSetting.Current}A not between min: {WriteCurrentCommand.MIN}A max: {WriteCurrentCommand.MAX}A"
                ).
                AndThen(Function(unused) SendData(conn, currentSetting)).
                Apply(Sub(unused) Threading.Thread.Sleep(20)).
                AndThen(Function(innerConn) ReadCurrentFromSettings(
                            innerConn,
                            New ReadCurrentFromSettingsCommand With {
                                .Channel = currentSetting.Channel
                            }
                        )
                    )
        End Function

        Public Shared Function ReadCurrent(Of T As TenmaSerializable)(conn As SerialPort, currentSetting As T) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                    AndThen(Function(unused) SendData(conn, currentSetting)).
                    AndThen(Function(innerConn) ReadDataWithTimeout(
                        innerConn,
                        New Timeout With {
                            .Interval = 50,
                            .TotalMilliseconds = 250
                        }
                    )).
                    Apply(Sub(unused) conn.Close()).
                    AndThen(Function(data) Utils.FromBytesToDecimal(data))

        End Function

        Public Shared Function ReadCurrentFromSettings(conn As SerialPort, currentSetting As ReadCurrentFromSettingsCommand) As Result(Of Decimal, String)
            Return ReadVoltage(conn, currentSetting)

        End Function

        Public Shared Function ReadActualCurrent(conn As SerialPort, currentSetting As ReadCurrentActualCommand) As Result(Of Decimal, String)
            Return ReadVoltage(conn, currentSetting)

        End Function
    End Class
End Namespace