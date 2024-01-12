Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Current

Namespace Tenma
    Partial Public Class Commands
        Public Shared Function SetCurrent(conn As SerialPort, currentSetting As CurrentWrite) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                Assert(
                    Function(unused) currentSetting.CheckVoltageBetweenMinMax(),
                    Function(unused) $"Voltage {currentSetting.Current}A not between min: {CurrentWrite.MIN}A max: {CurrentWrite.MAX}A"
                ).
                AndThen(Function(unused) SendData(conn, currentSetting)).
                Apply(Sub(unused) Threading.Thread.Sleep(20)).
                AndThen(Function(innerConn) ReadCurrentFromSettings(
                            innerConn,
                            New CurrentReadFromSettings With {
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
                    Map(Function(data) New Tuple(Of SerialPort, Result(Of Decimal, String))(conn, ParseData(data))).
                    Apply(Sub(connAndData) connAndData.Item1.Close()).
                    AndThen(Function(connAndData) connAndData.Item2)

        End Function

        Public Shared Function ReadCurrentFromSettings(conn As SerialPort, currentSetting As CurrentReadFromSettings) As Result(Of Decimal, String)
            Return ReadVoltage(conn, currentSetting)

        End Function

        Public Shared Function ReadActualCurrent(conn As SerialPort, currentSetting As CurrentReadActual) As Result(Of Decimal, String)
            Return ReadVoltage(conn, currentSetting)

        End Function
    End Class
End Namespace