Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Friend Module RemoteControlFunctions
        Friend Function CompatibilityTest(conn As SerialPort, id As DeviceID) As Result(Of Boolean, String)
            Return OpenConnection(conn).
                AndThen(Function(unused) ReadDeviceID(conn)).
                Assert(
                    Function(data) data.Equals(id),
                    Function(data) $"Data {data} is not identical to {id}, please ensure the function is working properly and the entered data is correct."
                ).
                Map(Function(unused) True)
        End Function

        Friend Function TestConnection(conn As SerialPort) As Result(Of DeviceStatus, String)
            Return OpenConnection(conn).
                    AndThen(Function(unused) ReadStatus(conn)).
                    Apply(Sub(unused) conn.Close())
        End Function
    End Module
End Namespace