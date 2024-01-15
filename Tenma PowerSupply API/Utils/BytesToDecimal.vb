Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Friend Module Utils
        Friend Function FromBytesToDecimal(data As Byte()) As Result(Of Decimal, String)
            Return StringToDecimal(
                data.Aggregate("", Function(acc, b) acc & ChrW(b))
            )
        End Function
    End Module
End Namespace
