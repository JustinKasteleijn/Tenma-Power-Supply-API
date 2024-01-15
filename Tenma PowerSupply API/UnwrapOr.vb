
Imports FunctionalExtensions.Functional


Imports System.Runtime.CompilerServices

﻿Imports System.Runtime.CompilerServices

Namespace Functional
    Public Module UnwrapOr
        <Extension()>
        Public Function UnwrapOr(Of T, E)(res As Result(Of T, E), _default As T) As T
            If res.IsErr() Then
                Return _default
            End If

            Return res.Unwrap()
        End Function
    End Module
End Namespace
