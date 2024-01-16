Namespace Tenma
    Partial Public Module DataTransferObjects
        Public Structure DeviceID
            Implements IEquatable(Of DeviceID)

            Public Manufacturer As String
            Public PartNumber As String
            Public Version As Decimal
            Public SerialNumber As UInt64

            Public Overrides Function Equals(obj As Object) As Boolean
                If TypeOf obj IsNot DeviceID Then Return False

                Return Me.Equals(DirectCast(obj, DeviceID))
            End Function

            Public Overloads Function Equals(other As DeviceID) As Boolean Implements IEquatable(Of DeviceID).Equals
                If Me.Manufacturer Is Nothing AndAlso other.Manufacturer IsNot Nothing Then Return False
                If Me.PartNumber Is Nothing AndAlso other.PartNumber IsNot Nothing Then Return False

                Return Equals(Me.Manufacturer, other.Manufacturer) AndAlso
                       Equals(Me.PartNumber, other.PartNumber) AndAlso
                       Me.Version = other.Version AndAlso
                       Me.SerialNumber = other.SerialNumber
            End Function

            Public Overrides Function GetHashCode() As Integer
                Dim hash As Integer = 17
                hash = hash * 23 + If(Me.Manufacturer IsNot Nothing, Me.Manufacturer.GetHashCode(), 0)
                hash = hash * 23 + If(Me.PartNumber IsNot Nothing, Me.PartNumber.GetHashCode(), 0)
                hash = hash * 23 + Me.Version.GetHashCode()
                hash = hash * 23 + Me.SerialNumber.GetHashCode()
                Return hash
            End Function
        End Structure
    End Module
End Namespace
