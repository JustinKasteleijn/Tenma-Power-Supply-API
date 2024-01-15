Imports System.Text
Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Friend Module Utils
        Friend Function BytesToString(data As Byte()) As Result(Of String, String)
            Return Result(Of Decimal, String).Try(
                Function() Encoding.ASCII.GetString(data),
                Function(err) err.Message
            )
        End Function
    End Module
End Namespace
