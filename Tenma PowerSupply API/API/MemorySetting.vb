Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Partial Public Class API
        Private Shared Function SaveMemorySettings(conn As SerialPort, commnand As SaveMemorySettingCommand) As Result(Of Boolean, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, commnand)).
                        Apply(Sub(unused) conn.Close()).
                        Map(Function(unused) True)
        End Function

        Private Shared Function SwitchMemoryState(conn As SerialPort, command As SwitchMemoryStateCommand) As Result(Of Boolean, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, command)).
                        Apply(Sub(unused) conn.Close()).
                        Map(Function(unused) True)
        End Function

        Private Shared Function WriteOCP(conn As SerialPort, command As WriteOCPStateCommand) As Result(Of State, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, command)).
                        Apply(Sub(unused) conn.Close()).
                        Map(Function(unused) command.State)
        End Function

        Private Shared Function WriteOCP(conn As SerialPort, command As WriteOVPStateCommand) As Result(Of State, String)
            Return OpenConnection(conn).
                        AndThen(Function(unused) SendData(conn, command)).
                        Apply(Sub(unused) conn.Close()).
                        Map(Function(unused) command.State)
        End Function
    End Class
End Namespace