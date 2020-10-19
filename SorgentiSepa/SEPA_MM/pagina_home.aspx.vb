Namespace CM

Partial Class pagina_home
        Inherits PageSetIdMode
        Dim par As New CM.Global
        Dim scriptblock As String
        Dim nGiorno As String
        Dim nGiornoRif As String
        Dim GiorniAp As String
        Dim ID_BANDO As Long



#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()

    End Sub

#End Region


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If Not IsPostBack Then
                'imgContatti.Attributes.Add("Onclick", "javascript:ApriContatti();")
                Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")

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
                '    Response.Write("<script>top.location.href=""Portale.aspx""</script>")
                '    Exit Sub
                'End If

                'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) > Val(System.Configuration.ConfigurationManager.AppSettings("OraCh") & "00") Then
                '    scriptblock = "<script language='javascript' type='text/javascript'>" _
                '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
                '                & "</script>"
                '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
                '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
                '    End If
                '    Response.Write("<script>top.location.href=""Portale.aspx""</script>")
                '    Exit Sub
                'End If

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "select ID from BANDI where STATO=1"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    ID_BANDO = par.IfNull(myReader(0), -2)
                End If
                myReader.Close()
                par.cmd.CommandText = "select count(DOMANDE_BANDO.ID) from DOMANDE_BANDO,DICHIARAZIONI where DICHIARAZIONI.ID_CAF=" & Session.Item("ID_CAF") & " and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.FL_RINNOVO='1' AND DOMANDE_BANDO.ID_STATO='2' and DICHIARAZIONI.ID_STATO<>2 AND DOMANDE_BANDO.ID_BANDO=" & ID_BANDO
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If myReader(0) > 0 Then
                        label33.Visible = True
                        label33.Text = "Premi qui per visualizzare l'elenco delle Domande da rielaborare"
                        lblComunicazioni.Visible = True
                        label33.Attributes.Add("OnClick", "javascript:window.open('ListaDomande.aspx?IDBANDO=" & ID_BANDO & "','Lista','top=0,left=0,width=480,height=400');")
                    Else
                        label33.Text = ""
                        lblComunicazioni.Visible = False
                    End If
                End If
                myReader.Close()

                'par.cmd.CommandText = "select count(WEB_REL_NEWS_ENTI.ID_NEWS) from WEB_REL_NEWS_ENTI,WEB_NEWS_ENTI where WEB_REL_NEWS_ENTI.ID_ENTE=" & Session.Item("ID_CAF") & " and WEB_NEWS_ENTI.ID=WEB_REL_NEWS_ENTI.ID_NEWS  AND WEB_NEWS_ENTI.DATA_V<=" & Format(Now, "yyyyMMdd") & " and WEB_NEWS_ENTI.DATA_F>=" & Format(Now, "yyyyMMdd")
                'myReader = par.cmd.ExecuteReader()
                'If myReader.Read Then
                '    If myReader(0) > 0 Then
                '        lblComunicazioni.Text = "Premi qui per visualizzare le news e gli avvisi"
                '        If myReader(0) > Session.Item("NEWS") Then
                '            If Session.Item("NEWS") = 0 Then
                '                lblComunicazioni.Text = "CI SONO NEWS o AVVISI! Premi qui per visualizzarle."
                '            Else
                '                lblComunicazioni.Text = "CI SONO NUOVE NEWS o AVVISI! Premi qui per visualizzarle."
                '            End If
                '        End If
                '        Image3.Visible = True
                '        lblComunicazioni.Visible = True
                '        lblComunicazioni.Attributes.Add("OnClick", "javascript:window.open('ListaNews.aspx','Lista','top=0,left=0,width=480,height=400');")
                '    Else
                '        lblComunicazioni.Visible = False
                '        Image3.Visible = False
                '    End If
                'End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label3.Text = Session.Item("ORARIO")
                txtmessaggio.Value = Session.Item("ORARIO")

            End If

            'par.OracleConn.Dispose()

        End Sub


    End Class

End Namespace