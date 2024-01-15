Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Public Class TenmaPowerSupply
        Private ReadOnly Connection As Lazy(Of SerialPort)

        Public Sub New(portname As String, baudrate As Integer)
            Connection = New Lazy(Of SerialPort)(
                            Function()
                                Return New SerialPort With {
                                    .PortName = portname,
                                    .BaudRate = baudrate,
                                    .DataBits = 8,
                                    .StopBits = StopBits.One,
                                    .Parity = Parity.None,
                                    .Handshake = Handshake.None,
                                    .ReadTimeout = 10000,
                                    .WriteTimeout = 10000
                                }
                            End Function)
        End Sub

        Public Function SetOutputCurrent(current As Decimal, channel As Channels) As Result(Of Decimal, String)
            Return SetCurrent(Connection.Value, New WriteCurrentCommand With {
                                  .Current = current,
                                  .Channel = channel
                                  })
        End Function

        Public Function GetOutputCurrentFromSettings(channel As Channels) As Result(Of Decimal, String)
            Return ReadCurrentFromSettings(Connection.Value, New ReadCurrentFromSettingsCommand With {.Channel = channel})
        End Function

        Public Function GetActualOutputCurrent(channel As Channels) As Result(Of Decimal, String)
            Return ReadActualCurrent(Connection.Value, New ReadCurrentActualCommand With {.Channel = channel})
        End Function

        Public Function SetOutputVoltage(voltage As Decimal, channel As Channels) As Result(Of Decimal, String)
            Return SetVoltage(Connection.Value, New WriteVoltageCommand With {
                .Voltage = voltage,
                .Channel = channel
            })
        End Function

        Public Function GetOutputVoltageFromSettings(channel As Channels) As Result(Of Decimal, String)
            Return ReadVoltageFromSettings(Connection.Value, New ReadVoltageFromSettingsCommand With {.Channel = channel})
        End Function

        Public Function GetActualOutputVoltage(channel As Channels) As Result(Of Decimal, String)
            Return ReadActualVoltage(Connection.Value, New ReadVoltageActualCommand With {.Channel = channel})
        End Function

        Public Function SetBeep(state As State) As Result(Of State, String)
            Return WriteBeep(Connection.Value, New WriteBeepStateCommand With {.State = state})
        End Function

        Public Function TurnOn() As Result(Of State, String)
            Return WritePowerState(
                Connection.Value,
                New WriteDevicePowerStateCommand With {.State = State.ON}
            )
        End Function

        Public Function TurnOff() As Result(Of State, String)
            Return WritePowerState(
                Connection.Value,
                New WriteDevicePowerStateCommand With {.State = State.OFF}
            )
        End Function

        Public Function GetStatus() As Result(Of DeviceStatus, String)
            Return ReadStatus(Connection.Value)
        End Function

        Public Function GetIdentification() As Result(Of DeviceID, String)
            Return ReadDeviceID(Connection.Value)
        End Function

        Public Function SwitchMemoryState(memoryNumber As MemoryNumber) As Result(Of Boolean, String)
            Return _SwitchMemoryState(Connection.Value, New SwitchMemoryStateCommand With {.MemoryNumber = memoryNumber})
        End Function

        Public Function SaveMemorySettings(memoryNumber As MemoryNumber) As Result(Of Boolean, String)
            Return _SaveMemorySettings(Connection.Value, New SaveMemorySettingCommand With {.MemoryNumber = memoryNumber})
        End Function

        Public Function SetOPC(state As State) As Result(Of State, String)
            Return WriteOCP(Connection.Value, New WriteOCPStateCommand With {.State = state})
        End Function

        Public Function SetOVP(state As State) As Result(Of State, String)
            Return WriteOVP(Connection.Value, New WriteOVPStateCommand With {.State = state})
        End Function

    End Class
End Namespace