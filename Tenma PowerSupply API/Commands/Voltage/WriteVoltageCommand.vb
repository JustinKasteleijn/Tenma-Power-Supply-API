Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Friend Module Commands
        Friend Structure WriteVoltageCommand
            Implements ITenmaSerializable
            Public Channel As Channels
            Public Voltage As Decimal

            Public Shared Function CheckVoltageBetweenMinMax(partNumber As String, voltage As Decimal) As Result(Of Range, String)
                Return SupportedPowerSupplies.
                    GetVoltageRange(partNumber).
                    Assert(
                        Function(range) voltage >= range.Min And voltage <= range.Max,
                        Function(range) $"Voltage of {voltage} not between {range.Min} / {range.Max}"
                    )
            End Function

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"VSET{CInt(Channel)}:{Utils.FormatDecimalAsString(Voltage, 2)}"
            End Function
        End Structure
    End Module
End Namespace

