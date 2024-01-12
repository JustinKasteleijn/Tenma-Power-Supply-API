Namespace Tenma
    Namespace Voltage
        Public Structure CurrentWrite
            Implements TenmaSerializable
            Public Channel As Channels
            Public Current As Decimal

            Const MIN As Decimal = 0.00
            Const MAX As Decimal = 3.1

            Public Function CheckVoltageBetweenMinMax() As Boolean
                Return Current >= MIN And Current <= MAX
            End Function

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Const VSetCommand As String = "ISET"
                Return $"{VSetCommand}{CInt(Channel)}:{Utils.FormatDecimalAsString(Current, 3)}"
            End Function
        End Structure
    End Namespace
End Namespace

