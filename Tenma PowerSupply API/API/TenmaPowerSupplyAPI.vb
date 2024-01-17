Imports System.IO.Ports
Imports FunctionalExtensions.Functional
Imports Tenma_PowerSupply_API.Tenma.Commands

Namespace Tenma
    Public Class TenmaPowerSupply
        Private ReadOnly Connection As Lazy(Of SerialPort)
        Private ReadOnly PartNumber As String

        Public Shared Function Create(partNumber As String, portname As String) As Result(Of TenmaPowerSupply, String)
            Return Result(Of TenmaPowerSupply, String).Ok(
                New TenmaPowerSupply(portname, partNumber)
            ).Assert(
                Function(unused) SupportedPowerSupplies.IsSupported(partNumber),
                Function() $"Device with partnumber: {partNumber} is not supported. Please refrence to readme.md or https://www.farnell.com/datasheets/3217055.pdf"
            ).AndThen(
                Function(tenma) tenma.
                                    GetIdentification().
                                    Assert(
                                        Function(res) res.result.PartNumber = partNumber,
                                        Function(res) $"Device returned unexcepted part number, Expected {partNumber}, Actual {res.result.PartNumber}."
                                    ).Map(Function(x) tenma)
            )
        End Function

        Private Sub New(portname As String, partNumber As String)
            Me.Connection = New Lazy(Of SerialPort)(
                            Function()
                                Return New SerialPort With {
                                    .PortName = portname,
                                    .BaudRate = 9600,
                                    .DataBits = 8,
                                    .StopBits = StopBits.One,
                                    .Parity = Parity.None,
                                    .Handshake = Handshake.None,
                                    .ReadTimeout = 10000,
                                    .WriteTimeout = 10000
                                }
                            End Function)
            Me.PartNumber = partNumber
        End Sub

        Public Function SetOutputCurrent(current As Decimal, channel As Channels) As Result(Of TenmaResponse(Of Decimal), String)
            Return SetCurrent(Connection.Value,
                              New WriteCurrentCommand With {
                                  .Current = current,
                                  .Channel = channel
                                  },
                              PartNumber
                   ).Map(Function(result) New TenmaResponse(Of Decimal) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function GetOutputCurrentFromSettings(channel As Channels) As Result(Of TenmaResponse(Of Decimal), String)
            Return ReadCurrentFromSettings(Connection.Value, New ReadCurrentFromSettingsCommand With {.Channel = channel}).
                Map(Function(result) New TenmaResponse(Of Decimal) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function GetActualOutputCurrent(channel As Channels) As Result(Of TenmaResponse(Of Decimal), String)
            Return ReadActualCurrent(Connection.Value, New ReadCurrentActualCommand With {.Channel = channel}).
                Map(Function(result) New TenmaResponse(Of Decimal) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function SetOutputVoltage(voltage As Decimal, channel As Channels) As Result(Of TenmaResponse(Of Decimal), String)
            Return SetVoltage(Connection.Value,
                              New WriteVoltageCommand With {
                                    .Voltage = voltage,
                                    .Channel = channel
                              },
                              PartNumber
                    ).Map(Function(result) New TenmaResponse(Of Decimal) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function GetOutputVoltageFromSettings(channel As Channels) As Result(Of TenmaResponse(Of Decimal), String)
            Return ReadVoltageFromSettings(Connection.Value, New ReadVoltageFromSettingsCommand With {.Channel = channel}).
                Map(Function(result) New TenmaResponse(Of Decimal) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function GetActualOutputVoltage(channel As Channels) As Result(Of TenmaResponse(Of Decimal), String)
            Return ReadActualVoltage(Connection.Value, New ReadVoltageActualCommand With {.Channel = channel}).
                Map(Function(result) New TenmaResponse(Of Decimal) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function SetBeep(state As State) As Result(Of TenmaResponse(Of State), String)
            Return WriteBeep(Connection.Value, New WriteBeepStateCommand With {.State = state}).
                Map(Function(result) New TenmaResponse(Of State) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function TurnOn() As Result(Of TenmaResponse(Of State), String)
            Return WritePowerState(
                Connection.Value,
                New WriteDevicePowerStateCommand With {.State = State.ON}
            ).Map(Function(result) New TenmaResponse(Of State) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function TurnOff() As Result(Of TenmaResponse(Of State), String)
            Return WritePowerState(
                Connection.Value,
                New WriteDevicePowerStateCommand With {.State = State.OFF}
            ).Map(Function(result) New TenmaResponse(Of State) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function GetStatus() As Result(Of TenmaResponse(Of DeviceStatus), String)
            Return ReadStatus(Connection.Value).
                Map(Function(result) New TenmaResponse(Of DeviceStatus) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function GetIdentification() As Result(Of TenmaResponse(Of DeviceID), String)
            Return ReadDeviceID(Connection.Value).
                Map(Function(result) New TenmaResponse(Of DeviceID) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function SwitchMemoryState(memoryNumber As MemoryNumber) As Result(Of TenmaResponse(Of MemoryNumber), String)
            Return _SwitchMemoryState(Connection.Value, New SwitchMemoryStateCommand With {.MemoryNumber = memoryNumber}).
                Map(Function(result) New TenmaResponse(Of MemoryNumber) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function SaveMemorySettings(memoryNumber As MemoryNumber) As Result(Of TenmaResponse(Of Boolean), String)
            Return _SaveMemorySettings(Connection.Value, New SaveMemorySettingCommand With {.MemoryNumber = memoryNumber}).
                Map(Function(result) New TenmaResponse(Of Boolean) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function SetOPC(state As State) As Result(Of TenmaResponse(Of State), String)
            Return WriteOCP(Connection.Value, New WriteOCPStateCommand With {.State = state}).
                Map(Function(result) New TenmaResponse(Of State) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

        Public Function SetOVP(state As State) As Result(Of TenmaResponse(Of State), String)
            Return WriteOVP(Connection.Value, New WriteOVPStateCommand With {.State = state}).
                Map(Function(result) New TenmaResponse(Of State) With {
                        .result = result,
                        .tenmaPowerSupply = Me
                   })
        End Function

    End Class
End Namespace
