Namespace Tenma
    Namespace Commands
        Friend Structure WriteCurrentCommand
            Implements ITenmaSerializable
            Public Channel As Channels
            Public Current As Decimal

            Const MIN As Decimal = 0.000
            Const MAX As Decimal = 3.0

            Public Function CheckVoltageBetweenMinMax() As Boolean
                Return Current >= MIN And Current <= MAX
            End Function

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"ISET{CInt(Channel)}:{Utils.FormatDecimalAsString(Current, 3)}"
            End Function
        End Structure
    End Namespace
End Namespace

