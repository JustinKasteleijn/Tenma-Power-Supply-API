Namespace Tenma
    Namespace Voltage
        Public Structure VoltageWrite
            Implements TenmaSerializable
            Public Channel As Channels
            Public Voltage As Decimal

            Public Function CheckVoltageBetweenMinMax() As Boolean
                Const MAX As Decimal = 61
                Const MIN As Decimal = 0.00
                Return Voltage >= MIN And Voltage <= MAX
            End Function

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Const VSetCommand As String = "VSET"
                Return $"{VSetCommand}{CInt(Channel)}:{Voltage.ToString("F2", Globalization.CultureInfo.InvariantCulture)}"
            End Function
        End Structure
    End Namespace
End Namespace

