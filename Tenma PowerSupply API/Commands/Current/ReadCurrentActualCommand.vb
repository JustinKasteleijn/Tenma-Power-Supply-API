Namespace Tenma
    Namespace Current
        Public Structure ReadCurrentActualCommand
            Implements TenmaSerializable
            Public Channel As Channels

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"IOUT{CInt(Channel)}?"
            End Function
        End Structure
    End Namespace
End Namespace