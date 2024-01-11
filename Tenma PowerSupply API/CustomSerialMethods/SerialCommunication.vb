Imports FunctionalExtensions.Functional
Imports System.IO.Ports
Imports System.Threading

Namespace Tenma
    Public Module SerialCommunication
        Public Structure Timeout
            Public TotalMilliseconds As Integer
            Public Interval As Integer
        End Structure

        Public Function ReadDataWithTimeout(conn As SerialPort, timeout As Timeout) As Result(Of Byte(), String)
            While (timeout.TotalMilliseconds - timeout.Interval) > 0
                If conn.DataAvailable() Then
                    Dim buffer(conn.BytesToRead - 1) As Byte
                    conn.Read(buffer, 0, conn.BytesToRead)
                    Return Result(Of Byte(), String).Ok(buffer)
                End If
                Thread.Sleep(timeout.Interval)
                timeout.TotalMilliseconds -= timeout.Interval
            End While
            Return Result(Of Byte(), String).Err($"Timeout occurred while reading data from port {conn.PortName}. Please ensure that the peripheral device is powered on and connected. Additionally, consider increasing the timeout value to allow the peripheral more time to process the data!")
        End Function

        Public Function SendData(ByRef conn As SerialPort, data As TenmaSerializable) As Result(Of SerialPort, String)
            Try
                conn.Write(data.ToCommand())
                Return Result(Of SerialPort, String).Ok(conn)
            Catch ex As InvalidOperationException
                Return Result(Of SerialPort, String).Err($"Com Port is closed {vbNewLine}{ex.Message}")
            Catch ex As ArgumentNullException
                Return Result(Of SerialPort, String).Err($"Data provided is null: {vbNewLine}{ex.Message}")
            Catch ex As TimeoutException
                Return Result(Of SerialPort, String).Err($"Process has timed out: {vbNewLine}{ex.Message}")
            End Try
        End Function

        Public Function OpenConnection(ByRef connection As SerialPort) As Result(Of SerialPort, String)
            If connection.IsOpen Then
                Return Result(Of SerialPort, String).Ok(connection)
            End If

            Try
                connection.Open()
            Catch ex As IO.IOException
                Return Result(Of SerialPort, String).Err($"Port is an invalid state: {vbNewLine}{ex.Message}")
            Catch ex As UnauthorizedAccessException
                Return Result(Of SerialPort, String).Err($"Your access is denied or used by another program: {vbNewLine}{ex.Message}")
            Catch ex As ArgumentOutOfRangeException
                Return Result(Of SerialPort, String).Err($"One or more properties are invalid (o.a., Parity, DataBits, Handshake, Baudrate or Read/Write timeout): {vbNewLine}{ex.Message}")
            Catch ex As InvalidOperationException
                Return Result(Of SerialPort, String).Err($"The specified port on the current instance of the serialpor is already open: {vbNewLine}{ex.Message}")
            End Try

            Return Result(Of SerialPort, String).Ok(connection)
        End Function
    End Module
End Namespace