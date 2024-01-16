Namespace Tenma
    Partial Friend Module Commands
        Friend Structure ReadDeviceStatusCommand
            Implements ITenmaSerializable

            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return "STATUS?"
            End Function
        End Structure

    End Module
End Namespace