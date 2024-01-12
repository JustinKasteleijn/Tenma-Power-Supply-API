Namespace Tenma
    Namespace Device
        Public Structure ReadDeviceIDCommand
            Implements TenmaSerializable
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return "*IDN?"
            End Function
        End Structure
    End Namespace
End Namespace