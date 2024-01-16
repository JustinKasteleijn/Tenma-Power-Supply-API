Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Friend Module Commands
        Friend Structure WriteCurrentCommand
            Implements ITenmaSerializable
            Public Channel As Channels
            Public Current As Decimal

            Public Shared Function CheckVoltageBetweenMinMax(partNumber As String, current As Decimal) As Result(Of Range, String)
                Return SupportedPowerSupplies.
                    GetVoltageRange(partNumber).
                    Assert(
                        Function(range) current >= range.Min And current <= range.Max,
                        Function(range) $"Current of {current} is not between {range.Min} / {range.Max}"
                    )
            End Function

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"ISET{CInt(Channel)}:{Utils.FormatDecimalAsString(Current, 3)}"
            End Function
        End Structure
    End Module
End Namespace

