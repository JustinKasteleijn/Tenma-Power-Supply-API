Namespace Tenma
    Namespace Voltage
        Public Structure VoltageWrite
            Implements TenmaSerializable
            Public Channel As Channels
            Public Voltage As Decimal

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Const VSetCommand As String = "VSET"
                Return $"{VSetCommand}{CInt(Channel)}:{Voltage:F2}"
            End Function
        End Structure
    End Namespace
End Namespace

