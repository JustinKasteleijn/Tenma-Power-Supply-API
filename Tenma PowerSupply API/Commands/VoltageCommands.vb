Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Functional
Imports Tenma_PowerSupply_API.Tenma.Voltage

Namespace Tenma
    Partial Public Class Commands
        Public Shared Function SetVoltage(conn As SerialPort, voltageSetting As VoltageWrite) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                Assert(
                    Function(unused) voltageSetting.CheckVoltageBetweenMinMax(),
                    Function(unused) $"Voltage {voltageSetting.Voltage}V not between min max"
                ).
                AndThen(Function(unused) SendData(conn, voltageSetting)).
                Apply(Sub(unused) Threading.Thread.Sleep(20)).
                AndThen(Function(innerConn) ReadVoltage(
                            innerConn,
                            New VoltageRead With {
                                .Channel = voltageSetting.Channel
                            }
                        )
                    )
        End Function

        Public Shared Function ReadVoltage(conn As SerialPort, voltageSetting As VoltageRead) As Result(Of Decimal, String)
            Return OpenConnection(conn).
                    AndThen(Function(unused) SendData(conn, voltageSetting)).
                    AndThen(Function(innerConn) ReadDataWithTimeout(
                        innerConn,
                        New Timeout With {
                            .Interval = 50,
                            .TotalMilliseconds = 250
                        }
                    )).
                    Map(Function(data) New Tuple(Of SerialPort, Result(Of Decimal, String))(conn, ParseData(data, conn))).
                    Apply(Sub(connAndData) connAndData.Item1.Close()).
                    AndThen(Function(connAndData) connAndData.Item2)

        End Function

        Private Shared Function ParseData(data As Byte(), conn As SerialPort) As Result(Of Decimal, String)
            Return Utils.StringToDecimal(
                data.Aggregate("", Function(acc, b) acc & ChrW(b))
            )
        End Function
    End Class
End Namespace