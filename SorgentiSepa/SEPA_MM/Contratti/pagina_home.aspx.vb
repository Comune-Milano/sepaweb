
Partial Class ANAUT_pagina_home
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            Session.LCID = 1040

            'GiorniAp = ""
            'nGiorno = Format(Now, "dddd")
            'Select Case LCase(nGiorno)
            '    Case "lunedì", "monday"
            '        nGiorno = "1"
            '    Case "martedì", "tuesday"
            '        nGiorno = "2"
            '    Case "mercoledì", "wednesday"
            '        nGiorno = "3"
            '    Case "giovedì", "thursday"
            '        nGiorno = "4"
            '    Case "venerdì", "friday"
            '        nGiorno = "5"
            '    Case "sabato", "saturday"
            '        nGiorno = "6"
            '    Case "domenica", "sunday"
            '        nGiorno = "7"
            'End Select
            'nGiornoRif = System.Configuration.ConfigurationManager.AppSettings("Giorni")

            'If InStr(nGiornoRif, nGiorno) = 0 Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) < Val(System.Configuration.ConfigurationManager.AppSettings("OraAp") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) > Val(System.Configuration.ConfigurationManager.AppSettings("OraCh") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            
            txtmessaggio.Text = Session.Item("ORARIO")
            Label3.Text = Session.Item("ORARIO")

            imgDomScad.Attributes.Add("onclick", "ApriDomScadenza();")
            CercaDomScadenza()
            imgSospese.Attributes.Add("onclick", "ApriDomScadenzaSosp();")
            CercaDomScadenzaSosp()

            '//////////////////////////////
            'imgDomScad.Attributes.Add("onclick", "VisualizzaErroreDepCauz();")
            CercaErroreDepCauz()
            '//////////////////////////////
        End If

        'par.OracleConn.Dispose()

    End Sub

    Private Sub CercaErroreDepCauz()
        Try
            ImgDepCauz.Visible = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID=" & CStr(Session.Item("ID_OPERATORE"))
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Or Session.Item("ID_OPERATORE") = "72" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PROCEDURE_REST_INT WHERE VISTO=0"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        ImgDepCauz.Visible = True
                    Else
                        ImgDepCauz.Visible = False
                    End If
                    myReader1.Close()
                Else
                    If Session.Item("REST_INT_DEP_CAUS") = "1" Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PROCEDURE_REST_INT WHERE TIPO_ERRORE=1 AND ESITO=1 AND VISTO=0"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            ImgDepCauz.Visible = True
                        Else
                            ImgDepCauz.Visible = False
                        End If
                        myReader1.Close()
                    End If
                End If
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

           
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CercaDomScadenza()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE ((TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID)) - TO_DATE(SYSDATE) >= 0) " _
                & "AND ((TO_DATE(DATA_PRESENTAZIONE,'yyyymmdd') + (SELECT TEMPO_GG FROM TEMPI_PROCESSI_VSA WHERE ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID)) - TO_DATE(SYSDATE) <= 10) AND FL_AUTORIZZAZIONE = 0 AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID AND DICHIARAZIONI_VSA.ID_STATO<>2 AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                imgDomScad.Visible = True
            Else
                imgDomScad.Visible = False
            End If
            myReader1.Close()
            
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub CercaDomScadenzaSosp()
        Try
            Dim dataDocManc As String = ""
            Dim strSQL As String = ""
            Dim dtDomande As New Data.DataTable

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            strSQL = "SELECT * FROM EVENTI_BANDI_VSA,DOMANDE_BANDO_VSA,DICHIARAZIONI_VSA WHERE EVENTI_BANDI_VSA.ID_DOMANDA=DOMANDE_BANDO_VSA.ID AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID AND FL_AUTORIZZAZIONE=0 AND DICHIARAZIONI_VSA.ID_STATO<>2 AND nvl(FL_FINE_SOSPENSIONE,0)=0 AND COD_EVENTO = 'F193'"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(strSQL, par.OracleConn)
            da.Fill(dtDomande)
            If dtDomande.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtDomande.Rows
                    dataDocManc = par.IfNull(row.Item("DATA_ORA"), "").ToString.Substring(0, 8)
                    If dataDocManc <> "" Then
                        If DateDiff(DateInterval.Day, CDate(par.FormattaData(dataDocManc)), CDate(par.FormattaData(Format(Now, "yyyyMMdd")))) >= 0 And DateDiff(DateInterval.Day, CDate(par.FormattaData(dataDocManc)), CDate(par.FormattaData(Format(Now, "yyyyMMdd")))) <= 30 Then
                            imgSospese.Visible = True
                            Exit For
                        End If
                    End If
                Next
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
