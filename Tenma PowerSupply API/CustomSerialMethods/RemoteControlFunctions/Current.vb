Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Friend Module RemoteControlFunctions
        Friend Function SetCurrent(conn As SerialPort, currentSetting As WriteCurrentCommand, partNumber As String) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                And(WriteCurrentCommand.CheckVoltageBetweenMinMax(partNumber, currentSetting.Current)).
                AndThen(Function(unused) SendData(conn, currentSetting)).
                Apply(Sub(unused) conn.Close()).
                AndThen(Function(unused) ReadCurrentFromSettings(
                            conn,
                            currentSetting:=New ReadCurrentFromSettingsCommand With {
                                .Channel = currentSetting.Channel
                            }
                        )
                    )
        End Function

        Private Function ReadCurrent(Of T As ITenmaSerializable)(conn As SerialPort, currentSetting As T) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                    AndThen(Function(unused) SendData(conn, currentSetting)).
                    AndThen(Function(unused) ReadDataWithTimeout(
                        conn,
                        New Timeout With {
                            .Interval = 50,
                            .TotalMilliseconds = 250
                        }
                    )).
                    Apply(Sub(unused) conn.Close()).
                    AndThen(Function(data) Utils.FromBytesToDecimal(data))

        End Function

        Friend Function ReadCurrentFromSettings(conn As SerialPort, currentSetting As ReadCurrentFromSettingsCommand) As Result(Of Decimal, String)
            Return ReadCurrent(conn, currentSetting)

        End Function

        Friend Function ReadActualCurrent(conn As SerialPort, currentSetting As ReadCurrentActualCommand) As Result(Of Decimal, String)
            Return ReadCurrent(conn, currentSetting)
        End Function
    End Module
End Namespace