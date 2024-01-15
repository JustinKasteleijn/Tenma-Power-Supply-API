Namespace Tenma
    Namespace Commands
        Friend Structure ReadDeviceStatusCommand
            Implements TenmaSerializable

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return "STATUS?"
            End Function
        End Structure

    End Namespace
End Namespace