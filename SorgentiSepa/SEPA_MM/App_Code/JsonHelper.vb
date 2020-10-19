Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class JsonHelper(Of t)
    Public Shared Function JsonSerializer(Of x)() As String
        Dim ser As New DataContractJsonSerializer(GetType(x))
        Dim ms As New MemoryStream()
        ser.WriteObject(ms, GetType(x))
        Dim jsonString As String = Encoding.UTF8.GetString(ms.ToArray())
        ms.Close()
        Dim p As String = "\\/Date\((\d+)\+\d+\)\\/"
        Dim matchEvaluator As New RegularExpressions.MatchEvaluator(AddressOf ConvertJsonDateToDateString)
        Dim reg As New Regex(p)
        jsonString = reg.Replace(jsonString, matchEvaluator)
        Return jsonString
    End Function
    Private Shared Function ConvertJsonDateToDateString(m As Match) As String
        Dim result As String = String.Empty
        Dim dt As New DateTime(1970, 1, 1)
        dt = dt.AddMilliseconds(Long.Parse(m.Groups(1).Value))
        dt = dt.ToLocalTime()
        result = dt.ToString("yyyy-MM-dd HH:mm:ss")
        Return result
    End Function
    Private Shared Function ConvertDateStringToJsonDate(m As Match) As String
        Dim result As String = String.Empty
        Dim dt As DateTime = DateTime.Parse(m.Groups(0).Value)
        dt = dt.ToUniversalTime()
        Dim ts As TimeSpan = dt - DateTime.Parse("1970-01-01")
        result = String.Format("\/Date({0}+0800)\/", ts.TotalMilliseconds)
        Return result
    End Function
    Public Shared Function JsonDeserialize(jsonString As String) As t
        Dim p As String = "\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}"
        Dim matchEvaluator As New MatchEvaluator(AddressOf ConvertDateStringToJsonDate)
        Dim reg As New Regex(p)
        jsonString = reg.Replace(jsonString, matchEvaluator)
        Dim ser As New DataContractJsonSerializer(GetType(t))
        Dim ms As New MemoryStream(Encoding.UTF8.GetBytes(jsonString))
        Dim obj As t = DirectCast(ser.ReadObject(ms), t)
        Return obj
    End Function
End Class
