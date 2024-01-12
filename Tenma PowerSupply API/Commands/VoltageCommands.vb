Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Voltage

Namespace Tenma
    Partial Public Class Commands
        Public Shared Function SetVoltage(conn As SerialPort, voltageSetting As VoltageWrite) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                Assert(
                    Function(unused) voltageSetting.CheckVoltageBetweenMinMax(),
                    Function(unused) $"Voltage {voltageSetting.Voltage}V not between min: {VoltageWrite.MIN}V max: {VoltageWrite.MAX}V"
                ).
                AndThen(Function(unused) SendData(conn, voltageSetting)).
                Apply(Sub(unused) Threading.Thread.Sleep(20)).
                AndThen(Function(innerConn) ReadVoltageFromSettings(
                            innerConn,
                            New VoltageReadFromSettings With {
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
                    Map(Function(data) New Tuple(Of SerialPort, Result(Of Decimal, String))(conn, ParseData(data))).
                    Apply(Sub(connAndData) connAndData.Item1.Close()).
                    AndThen(Function(connAndData) connAndData.Item2)

        End Function

        Public Shared Function ReadVoltageFromSettings(conn As SerialPort, voltageSetting As VoltageReadFromSettings) As Result(Of Decimal, String)
            Return ReadVoltage(conn, voltageSetting)
        End Function

        Public Shared Function ReadActualVoltage(conn As SerialPort, voltageSetting As VoltageReadActual) As Result(Of Decimal, String)
            Return ReadVoltage(conn, voltageSetting)

        End Function

        Private Shared Function ParseData(data As Byte()) As Result(Of Decimal, String)
            Return Utils.StringToDecimal(
                data.Aggregate("", Function(acc, b) acc & ChrW(b))
            )
        End Function
    End Class
End Namespace