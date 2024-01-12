Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Voltage

Namespace Tenma
    Partial Public Class Commands
        Public Shared Function SetVoltage(conn As SerialPort, voltageSetting As WriteVoltageCommand) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                Assert(
                    Function(unused) voltageSetting.CheckVoltageBetweenMinMax(),
                    Function(unused) $"Voltage {voltageSetting.Voltage}V not between min: {WriteVoltageCommand.MIN}V max: {WriteVoltageCommand.MAX}V"
                ).
                AndThen(Function(unused) SendData(conn, voltageSetting)).
                Apply(Sub(unused) Threading.Thread.Sleep(20)).
                AndThen(Function(innerConn) ReadVoltageFromSettings(
                            innerConn,
                            New ReadVoltageFromSettingsCommand With {
                                .Channel = voltageSetting.Channel
                            }
                        )
                    )
        End Function

        Private Shared Function ReadVoltage(Of T As TenmaSerializable)(conn As SerialPort, voltageSetting As T) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                    AndThen(Function(unused) SendData(conn, voltageSetting)).
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

        Public Shared Function ReadVoltageFromSettings(conn As SerialPort, voltageSetting As ReadVoltageFromSettingsCommand) As Result(Of Decimal, String)
            Return ReadVoltage(conn, voltageSetting)
        End Function

        Public Shared Function ReadActualVoltage(conn As SerialPort, voltageSetting As ReadVoltageActualCommand) As Result(Of Decimal, String)
            Return ReadVoltage(conn, voltageSetting)

        End Function
    End Class
End Namespace