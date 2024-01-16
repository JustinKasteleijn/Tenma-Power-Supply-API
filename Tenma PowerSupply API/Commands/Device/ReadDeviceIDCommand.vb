Namespace Tenma
    Partial Friend Module Commands
        Friend Structure ReadDeviceIDCommand
            Implements ITenmaSerializable
            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return "*IDN?"
            End Function
        End Structure
    End Module
End Namespace