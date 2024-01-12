Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Public Class Utils
        Public Shared Function FromBytesToDecimal(data As Byte()) As Result(Of Decimal, String)
            Return StringToDecimal(
                data.Aggregate("", Function(acc, b) acc & ChrW(b))
            )
        End Function
    End Class
End Namespace
