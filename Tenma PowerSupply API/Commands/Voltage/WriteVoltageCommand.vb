Namespace Tenma
    Namespace Commands
        Public Structure WriteVoltageCommand
            Implements TenmaSerializable
            Public Channel As Channels
            Public Voltage As Decimal

            Public Const MIN As Decimal = 0.00
            Public Const MAX As Decimal = 61.0

            Public Function CheckVoltageBetweenMinMax() As Boolean
                Return Voltage >= MIN And Voltage <= MAX
            End Function

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"VSET{CInt(Channel)}:{Utils.FormatDecimalAsString(Voltage, 2)}"
            End Function
        End Structure
    End Namespace
End Namespace

