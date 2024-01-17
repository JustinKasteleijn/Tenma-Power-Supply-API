Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Friend Module RemoteControlFunctions
        Friend Function SetVoltage(conn As SerialPort, voltageSetting As WriteVoltageCommand, partNumber As String) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                And(WriteVoltageCommand.CheckVoltageBetweenMinMax(partNumber, voltageSetting.Voltage)).
                AndThen(Function(unused) SendData(conn, voltageSetting)).
                AndThen(Function(unused) ReadVoltageFromSettings(
                            conn,
                            New ReadVoltageFromSettingsCommand With {
                                .Channel = voltageSetting.Channel
                            }
                        )
                    )
        End Function

        Private Function ReadVoltage(Of T As ITenmaSerializable)(conn As SerialPort, voltageSetting As T) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                    AndThen(Function(unused) SendData(conn, voltageSetting)).
                    AndThen(Function(unused) ReadDataWithTimeout(
                        conn,
                        New Timeout With {
                            .Interval = 100,
                            .TotalMilliseconds = 1000
                        }
                    )).
                    Apply(Sub(unused) conn.Close()).
                    AndThen(Function(data) Utils.FromBytesToDecimal(data))

        End Function

        Friend Function ReadVoltageFromSettings(conn As SerialPort, voltageSetting As ReadVoltageFromSettingsCommand) As Result(Of Decimal, String)
            Return ReadVoltage(conn, voltageSetting)
        End Function

        Friend Function ReadActualVoltage(conn As SerialPort, voltageSetting As ReadVoltageActualCommand) As Result(Of Decimal, String)
            Return ReadVoltage(conn, voltageSetting)
        End Function
    End Module
End Namespace