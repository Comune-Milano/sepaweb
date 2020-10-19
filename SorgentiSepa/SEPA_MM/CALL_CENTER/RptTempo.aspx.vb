﻿Imports System.IO
Partial Class CALL_CENTER_RptTempo
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Ricerca()
        End If

    End Sub
    Private Sub Ricerca()
        Try

            Dim whereCond As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            Dim swhere As Boolean = True
            Dim e As String = ""

            If Not String.IsNullOrEmpty(Request.QueryString("DAL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_ORA_RICHIESTA,1,8),'30000000') >='" & Request.QueryString("DAL") & "'"
                Me.lblFiltri.Text = lblFiltri.Text & " Data apertura dal: " & par.FormattaData(Request.QueryString("DAL"))

            End If

            If Not String.IsNullOrEmpty(Request.QueryString("AL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_CHIUSURA,1,8),'10000000') <= '" & Request.QueryString("AL") & "'"
                Me.lblFiltri.Text = lblFiltri.Text & " Data chiusura al: " & par.FormattaData(Request.QueryString("AL"))

            End If

            If par.IfEmpty(Request.QueryString("TIPO"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " ID_TIPOLOGIE =" & Request.QueryString("TIPO")
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE ID = " & Request.QueryString("TIPO")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Tipologia: " & par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                lettore.Close()
            End If

            If par.IfEmpty(Request.QueryString("STR"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " segnalazioni.ID_STRUTTURA  = " & Request.QueryString("STR")
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & Request.QueryString("STR")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Filiale: " & par.IfNull(lettore("NOME"), "")
                End If
                lettore.Close()
            End If

            If par.IfEmpty(Request.QueryString("OP"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True

                End If
                whereCond = whereCond & " " & e & " ID_OPERATORE_INS  = " & Request.QueryString("OP")
                par.cmd.CommandText = "SELECT OPERATORE FROM OPERATORI WHERE ID = " & Request.QueryString("OP")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Operatore: " & par.IfNull(lettore("OPERATORE"), "")
                End If
                lettore.Close()
            End If



            '& "TO_CHAR(NVL(TRUNC(((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi')-TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24)),0),'99') AS ore,

            par.cmd.CommandText = "SELECT SEGNALAZIONI.ID,id_tipologie,TIPOLOGIE_GUASTI.descrizione  AS tipo, " _
                                & "TO_CHAR(ABS(NVL (  TO_DATE (SUBSTR (data_chiusura, 0, 8), 'yyyymmdd')- TO_DATE (SUBSTR (data_ora_richiesta, 0, 8), 'yyyymmdd'),0))) AS GIORNI," _
                                & "TO_CHAR(abs(NVL(SUBSTR (data_chiusura, 9, 2)-SUBSTR (data_ora_richiesta, 9, 2),0)),'99') AS ore, " _
                                & "TO_CHAR(abs(NVL((((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi')-TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24*60))- " _
                                & "TRUNC(((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi')-TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24))*60,0)),'99') AS minuti ," _
                                & "SEGNALAZIONI.descrizione_ric " _
                                & "FROM siscom_mi.SEGNALAZIONI,siscom_mi.TIPOLOGIE_GUASTI " _
                                & "WHERE TIPOLOGIE_GUASTI.ID = SEGNALAZIONI.id_tipologie AND SEGNALAZIONI.ID_STATO = 10 " & whereCond _
                                & "ORDER BY TIPOLOGIE_GUASTI.descrizione ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable

            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim dtCopia As New Data.DataTable
                dtCopia = dt.Clone
                Dim riga As Data.DataRow

                Dim sommaPerTipoGG As Integer = 0
                Dim sommaPerTipoHH As Integer = 0
                Dim sommaPerTipoMM As Integer = 0

                Dim idTipologie As String = dt.Rows(0).Item("id_tipologie")
                For Each row As Data.DataRow In dt.Rows
                    If row.Item("id_tipologie") <> idTipologie Then
                        riga = dtCopia.NewRow
                        'riga.Item("descrizione_RIC") = MediaPerTipologia(idTipologie, dtCopia, whereCond, sommaPerTipo)
                        dtCopia.Rows.Add(riga)


                        sommaPerTipoGG = 0
                        sommaPerTipoHH = 0
                        sommaPerTipoMM = 0
                        sommaPerTipoGG = sommaPerTipoGG + row.Item("GIORNI")
                        sommaPerTipoHH = sommaPerTipoHH + row.Item("ORE")
                        sommaPerTipoMM = sommaPerTipoMM + row.Item("MINUTI")
                        idTipologie = row.Item("id_tipologie")
                        dtCopia.Rows.Add(row.ItemArray)
                    Else
                        sommaPerTipoGG = sommaPerTipoGG + row.Item("GIORNI")
                        sommaPerTipoHH = sommaPerTipoHH + row.Item("ORE")
                        sommaPerTipoMM = sommaPerTipoMM + row.Item("MINUTI")
                        dtCopia.Rows.Add(row.ItemArray)
                    End If
                Next
                riga = dtCopia.NewRow
                riga.Item("descrizione_RIC") = MediaPerTipologia(idTipologie, dtCopia, whereCond, sommaPerTipoGG, sommaPerTipoHH, sommaPerTipoMM)
                dtCopia.Rows.Add(riga)

                Me.DataGridRptTempo.DataSource = dtCopia
                Me.DataGridRptTempo.DataBind()


            End If

            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try


    End Sub
    Private Function MediaPerTipologia(ByVal idtipo As String, ByVal dtAggiunta As Data.DataTable, ByVal where As String, ByVal sommaXtipoGG As Integer, ByVal sommaXtipoHH As Integer, ByVal sommaXtipoMM As Integer) As String
        Dim testoRiga As String = "TOTALI CHIUSE NEL PERIODO SCELTO "
        Dim media As Integer
        Dim giorni As Integer
        Dim ore As Integer
        Dim minuti As Integer
        If Not String.IsNullOrEmpty(Request.QueryString("DAL")) Then
            testoRiga = testoRiga & " dal " & par.FormattaData(Request.QueryString("DAL"))

        End If

        If Not String.IsNullOrEmpty(Request.QueryString("AL")) Then
            testoRiga = testoRiga & " al " & par.FormattaData(Request.QueryString("AL"))
        End If

        par.cmd.CommandText = "SELECT SEGNALAZIONI.ID," _
                            & "id_tipologie,TIPOLOGIE_GUASTI.descrizione  AS tipo, " _
                            & "TO_CHAR(NVL (  TO_DATE (SUBSTR (data_chiusura, 0, 8), 'yyyymmdd')- TO_DATE (SUBSTR (data_ora_richiesta, 0, 8), 'yyyymmdd'),0)) AS GIORNI," _
                            & "TO_CHAR(ABS(NVL(SUBSTR (data_chiusura, 9, 2)-SUBSTR (data_ora_richiesta, 9, 2),0)),'99') AS ore,  " _
                            & "TO_CHAR(ABS(NVL((((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi')-" _
                            & "TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24*60))- TRUNC(((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi')-" _
                            & "TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24))*60,0)),'99') AS minuti , " _
                            & "TO_NUMBER(TO_CHAR(ROUND(NVL(TO_DATE (SUBSTR (data_chiusura, 0, 8), 'yyyymmdd')- TO_DATE (SUBSTR (data_ora_richiesta, 0, 8), 'yyyymmdd'),0)*24*60 + ABS(NVL(SUBSTR (data_chiusura, 9, 2)-SUBSTR (data_ora_richiesta, 9, 2),0))*60" _
                            & "+ABS(NVL((((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi') - TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24*60))- TRUNC(((TO_DATE(SUBSTR (data_chiusura, 0, 12),'yyyymmddhh24mi') - TO_DATE(SUBSTR (data_ora_richiesta, 0, 12),'yyyymmddhh24mi')) *24))*60,0))))) AS somma, " _
                            & "SEGNALAZIONI.descrizione_ric " _
                            & "FROM siscom_mi.SEGNALAZIONI,siscom_mi.TIPOLOGIE_GUASTI " _
                            & "WHERE TIPOLOGIE_GUASTI.ID = SEGNALAZIONI.id_tipologie AND SEGNALAZIONI.ID_STATO = 10 " & where _
                            & "AND ID_TIPOLOGIE = " & idtipo


        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        Dim r As Data.DataRow
        r = dt.Select("SOMMA = MIN (SOMMA)")(0)
        testoRiga = testoRiga & ", Tempo minimo = " & Math.Max(CInt(Fix(r.Item("giorni"))), 0) & " giorni " & Math.Max(CInt(Fix(r.Item("ore"))), 0) & " ore " & Math.Max(CInt(Fix(r.Item("minuti"))), 0) & " minuti"
        r = dt.Select("SOMMA = MAX (SOMMA)")(0)
        testoRiga = testoRiga & ", Tempo massimo = " & Math.Max(CInt(Fix(r.Item("giorni"))), 0) & " giorni " & Math.Max(CInt(Fix(r.Item("ore"))), 0) & " ore " & Math.Max(CInt(Fix(r.Item("minuti"))), 0) & " minuti"

        media = ((sommaXtipoGG * 1440) + (sommaXtipoHH * 60) + (sommaXtipoMM)) / dt.Rows.Count

        giorni = Math.Floor(Fix(media) / 1440)

        media = media - (1440 * giorni)

        ore = Math.Floor(Fix(media) / 60)
        media = media - (60 * ore)

        minuti = media

        testoRiga = testoRiga & ", Tempo medio = " & giorni & " giorni " & ore & " ore " & minuti & " minuti"


        MediaPerTipologia = testoRiga
    End Function
    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRptTempo, "RptSegn", False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub
End Class
