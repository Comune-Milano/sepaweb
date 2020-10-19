Imports System
Imports System.Collections
Imports System.Linq
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Linq
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Web.Script.Serialization

<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class SepacomAutoComplete
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetListaComuni(context As RadAutoCompleteContext) As AutoCompleteBoxData
        Dim par As New CM.Global
        par.cmd.CommandText = "SELECT COD, NOME FROM COMUNI_NAZIONI WHERE SIGLA <> 'I' AND UPPER(NOME) LIKE '" & par.PulisciStrSql(context.Text.ToUpper) & "%' " _
                            & "ORDER BY NOME ASC"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        Dim result As New List(Of AutoCompleteBoxItemData)()
        Dim dropDownData As New AutoCompleteBoxData()
        result = New List(Of AutoCompleteBoxItemData)()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim itemData As New AutoCompleteBoxItemData()
            itemData.Text = dt.Rows(i)("NOME").ToString()
            itemData.Value = dt.Rows(i)("COD").ToString()
            result.Add(itemData)
        Next
        dropDownData.Items = result.ToArray()
        Return dropDownData
    End Function
    <WebMethod()>
    Public Function GetListaComuniIndirizzo(context As RadAutoCompleteContext) As AutoCompleteBoxData
        Dim par As New CM.Global
        par.cmd.CommandText = "SELECT COD, NOME FROM COMUNI_NAZIONI WHERE LENGTH(SIGLA) = 2 AND UPPER(NOME) LIKE '" & par.PulisciStrSql(context.Text.ToUpper) & "%' " _
                            & "ORDER BY NOME ASC"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        Dim result As New List(Of AutoCompleteBoxItemData)()
        Dim dropDownData As New AutoCompleteBoxData()
        result = New List(Of AutoCompleteBoxItemData)()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim itemData As New AutoCompleteBoxItemData()
            itemData.Text = dt.Rows(i)("NOME").ToString()
            itemData.Value = dt.Rows(i)("COD").ToString()
            result.Add(itemData)
        Next
        dropDownData.Items = result.ToArray()
        Return dropDownData
    End Function
    <WebMethod(EnableSession:=True)>
    Public Function GetCodComune(ByVal comune As String) As String
        Dim par As New CM.Global
        Dim connData As CM.datiConnessione = Nothing
        connData = New CM.datiConnessione(par, False, False)
        connData.apri(False)
        par.cmd.CommandText = "SELECT COD FROM COMUNI_NAZIONI WHERE UPPER(NOME) = '" & par.PulisciStrSql(Trim(comune).ToUpper) & "'"
        GetCodComune = par.IfNull(par.cmd.ExecuteScalar, System.Web.HttpContext.Current.Session.Item("CODCOMUNEPREDEFINITO"))
        connData.chiudi(False)
        Dim JSON As New JavaScriptSerializer()
        Return JSON.Serialize(GetCodComune)
    End Function
    <WebMethod(EnableSession:=True)>
    Public Function GetProvinciaComune(ByVal comune As String) As String
        Dim par As New CM.Global
        Dim connData As CM.datiConnessione = Nothing
        connData = New CM.datiConnessione(par, False, False)
        connData.apri(False)
        par.cmd.CommandText = "SELECT UPPER(SIGLA) AS SIGLA FROM COMUNI_NAZIONI WHERE UPPER(NOME) = '" & par.PulisciStrSql(Trim(comune).ToUpper) & "'"
        GetProvinciaComune = par.IfNull(par.cmd.ExecuteScalar, System.Web.HttpContext.Current.Session.Item("PROVINCIAPREDEFINITO"))
        connData.chiudi(False)
        Dim JSON As New JavaScriptSerializer()
        Return JSON.Serialize(GetProvinciaComune)
    End Function
    
End Class