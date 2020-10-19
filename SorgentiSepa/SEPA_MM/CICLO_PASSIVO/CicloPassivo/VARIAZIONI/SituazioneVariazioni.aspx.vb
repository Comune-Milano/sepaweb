Partial Class SituazioneVariazioni

    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_VARIAZIONI_SL") <> 1 And Session.Item("BP_VARIAZIONI") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        lblErrore.Visible = False
        Try

            If Not IsPostBack Then
                caricaEserciziFinanziari()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub caricaEserciziFinanziari()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                & "AND ID_STATO>='5'"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader.Read
                Dim ANNOINIZIO As String = par.IfNull(myReader("INIZIO"), "")
                Dim ANNOFINE As String = par.IfNull(myReader("FINE"), "")
                If Len(ANNOINIZIO) = 8 And Len(ANNOFINE) = 8 Then
                    ANNOINIZIO = ConvertiData(ANNOINIZIO)
                    ANNOFINE = ConvertiData(ANNOFINE)
                    ddlAnno.Items.Add(New ListItem(ANNOINIZIO & "   -   " & ANNOFINE, par.IfNull(myReader("ID"), 0)))
                    If par.IfNull(myReader("ID_STATO"), 0) = 5 Then
                        ddlAnno.SelectedValue = par.IfNull(myReader("ID"), 0)
                        AnnoSelezionato.Value = ddlAnno.SelectedValue
                    End If
                Else
                    'ERRORE USCIRE DALLA PAGINA
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento degli esercizi finanziari.');location.replace('../../pagina_home.aspx');</script>")
                End If


            End While

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Text = "Si è verificato un errore durante il caricamento degli esercizi finanziari!"
            lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub ddlAnno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnno.SelectedIndexChanged
        AnnoSelezionato.Value = ddlAnno.SelectedValue
    End Sub

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

    Protected Sub T1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles T1.SelectedNodeChanged
        Select Case T1.SelectedValue
            Case "RiepGen"
                Response.Write("<script>window.open('RiepilogoVariazioni.aspx?AN=' + " & ddlAnno.SelectedValue & ", 'Variazioni', '');</script>")
            Case "RiepStrutt"
                Response.Write("<script>window.open('RiepilogoPerStrutture.aspx?AN=' + " & ddlAnno.SelectedValue & ", 'Variazioni', '');</script>")
            Case "RiepDett"
                Response.Write("<script>window.open('DettaglioVariazioni.aspx?AN=' + " & ddlAnno.SelectedValue & ", 'Variazioni', '');</script>")
        End Select
        T1.SelectedNode.Selected = False

    End Sub
End Class