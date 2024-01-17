Imports FunctionalExtensions.Functional
Imports System.Runtime.CompilerServices

Namespace Tenma
    Public Module WaitResult
        <Extension()>
        Public Function Wait(Of T, E)(res As Result(Of T, E), milliseconds As Integer) As Result(Of T, E)
            If res.IsErr() Then
                Return res
            End If

            Threading.Thread.Sleep(milliseconds)

            Return res
        End Function
    End Module
End Namespace