Namespace Tenma
    Namespace Device
        Public Structure GetDeviceStatus
            Implements TenmaSerializable

            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return "STATUS?"
            End Function
        End Structure

    End Namespace
End Namespace