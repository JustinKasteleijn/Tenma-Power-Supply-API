Namespace Tenma
    Partial Public Module DataTransferObjects
        Public Structure DeviceID
            Implements IEquatable(Of DeviceID)

            Public Manufacturer As String
            Public Model As String
            Public Version As Decimal
            Public SerialNumber As UInt64

            Public Overrides Function Equals(obj As Object) As Boolean
                If TypeOf obj IsNot DeviceID Then Return False

                Return Me.Equals(DirectCast(obj, DeviceID))
            End Function

            Public Overloads Function Equals(other As DeviceID) As Boolean Implements IEquatable(Of DeviceID).Equals
                If Me.Manufacturer Is Nothing AndAlso other.Manufacturer IsNot Nothing Then Return False
                If Me.Model Is Nothing AndAlso other.Model IsNot Nothing Then Return False

                Return Equals(Me.Manufacturer, other.Manufacturer) AndAlso
                       Equals(Me.Model, other.Model) AndAlso
                       Me.Version = other.Version AndAlso
                       Me.SerialNumber = other.SerialNumber
            End Function
        End Structure
    End Module
End Namespace
