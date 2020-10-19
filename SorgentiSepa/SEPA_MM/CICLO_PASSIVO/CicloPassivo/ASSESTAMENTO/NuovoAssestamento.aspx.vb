
Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_NuovoAssestamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        
        'CONTROLLO UTENTE ABILITATO ALLA CREAZIONE DI UN NUOVO ASSESTAMENTO
        If Session.Item("MOD_ASS_NUOVO") <> 1 Then
            Response.Write("<script>alert('Operatore non abilitato alla creazione di un nuovo assestamento!');location.href='../../pagina_home.aspx';</script>")
            Exit Sub
        End If
        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            caricaEserciziFinanziari()
            controlloCreaAssestamento()
        End If
    End Sub
    Protected Sub controlloCreaAssestamento()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN, SISCOM_MI.PF_ASSESTAMENTO WHERE PF_MAIN.ID=PF_ASSESTAMENTO.ID_PF_MAIN " _
                & "AND PF_ASSESTAMENTO.ID_STATO<>5 AND PF_MAIN.ID_STATO=5 AND PF_MAIN.ID='" & ddlanno.SelectedValue & "'"
            Dim LettoreAss As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreAss.Read Then
                caric.Value = "0"
            Else
                caric.Value = "1"
            End If
            LettoreAss.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Protected Sub controlloCreaPianoFinanziario()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN, SISCOM_MI.PF_ASSESTAMENTO WHERE PF_MAIN.ID=PF_ASSESTAMENTO.ID_PF_MAIN " _
                & "AND PF_ASSESTAMENTO.ID_STATO<>5 AND PF_MAIN.ID_STATO=5 AND PF_MAIN.ID='" & ddlanno.SelectedValue & "'"
            Dim LettoreAss As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreAss.Read Then
                Response.Write("<script>alert('Un assestamento è già in fase di elaborazione! Impossibile creare un nuovo assestamento.');</script>")
            Else
                creaNuovoAssestamento()
            End If
            LettoreAss.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblerrore.Text = "Errore durante la creazione di un nuovo assestamento!"
            lblerrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub
    Protected Sub controlloCreaPianoFinanziarioInElaborazione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN, SISCOM_MI.PF_ASSESTAMENTO WHERE PF_MAIN.ID=PF_ASSESTAMENTO.ID_PF_MAIN " _
                & "AND PF_ASSESTAMENTO.ID_STATO<>5 AND PF_MAIN.ID_STATO=5 AND PF_MAIN.ID='" & ddlanno.SelectedValue & "'"
            Dim LettoreAss As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreAss.Read Then
                Response.Write("<script>alert('Un assestamento è già in fase di elaborazione! Impossibile creare un nuovo assestamento.');</script>")
            End If
            LettoreAss.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblerrore.Text = "Errore durante la creazione di un nuovo assestamento!"
            lblerrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub
    Protected Sub creaNuovoAssestamento()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim Ididnuovoassestamentoamento As Integer = 0

            par.cmd.CommandText = "select siscom_mi.seq_pf_assestamento.nextval from dual"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Ididnuovoassestamentoamento = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            idnuovoAssestamento.Value = Ididnuovoassestamentoamento
            par.cmd.CommandText = "insert into siscom_mi.pf_assestamento (id,id_pf_main,data_inserimento,id_stato) " _
                                & "values(" & Ididnuovoassestamentoamento & " ," & ddlanno.SelectedValue & ",'" & Format(Now, "yyyyMMdd") & "',1)"

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "insert into siscom_mi.pf_assestamento_voci (id_assestamento,id_voce,id_struttura) " _
                                & "select " & Ididnuovoassestamentoamento & ",id_voce,id_struttura from siscom_mi.pf_voci_struttura " _
                                & "where id_voce in (select id from siscom_mi.pf_voci where id_piano_finanziario = " & ddlanno.SelectedValue & " )"

            par.cmd.ExecuteNonQuery()
            WriteEvent("F55", "CREAZIONE NUOVO ASSESTAMENTO")

            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Nuovo Assestamento creato con successo!');</script>")
            Response.Write("<script>alert('Lo stato dell\'assestamento appena creato viene settato su: CARICAMENTO IMPORTI.');</script>")
            Response.Write("<script>location.href='GestioneAssestamento.aspx?id=" & idnuovoAssestamento.Value & "';</script>")
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Visible = True
            lblerrore.Text = ex.Message
            lblerrore.Text = "Nuovo Assestamento - " & ex.Message

        End Try
    End Sub
    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '########## CONDIZIONE DI RICERCA DEI PIANI FINANZIARI ##########
            'SELEZIONO TUTTI I PIANI FINANZIARI CHE SONO STATI APPROVATI
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                    & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                    & "AND ID_STATO='5'"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                Dim ANNOINIZIO As String = par.IfNull(myReader1("INIZIO"), "")
                Dim ANNOFINE As String = par.IfNull(myReader1("FINE"), "")
                If Len(ANNOINIZIO) = 8 And Len(ANNOFINE) = 8 Then
                    ANNOINIZIO = ConvertiData(ANNOINIZIO)
                    ANNOFINE = ConvertiData(ANNOFINE)
                    ddlanno.Items.Add(New ListItem(ANNOINIZIO & " - " & ANNOFINE, par.IfNull(myReader1("ID"), 0)))
                    If par.IfNull(myReader1("ID_STATO"), 0) = 5 Then
                        ddlanno.SelectedValue = par.IfNull(myReader1("ID"), 0)
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
            lblerrore.Text = "Errore nel caricamento degli esercizi finanziari!"
            lblerrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function
    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovo.Click
        If prosegui.Value = "1" Then
            controlloCreaPianoFinanziario()
        Else
            controlloCreaPianoFinanziarioInElaborazione()
        End If
    End Sub
    Protected Sub WriteEvent(ByVal CodEvento As String, Optional ByVal Motivazione As String = "")
        Dim idPadre As String = "null"
        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_ASSESTAMENTO (ID_ASSESTAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA,IMPORTO) VALUES " _
                & "(" & idnuovoAssestamento.Value & ", " & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', '" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "',NULL,NULL,'0' )"
            par.cmd.ExecuteNonQuery()
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("../../pagina_home.aspx")
    End Sub
End Class
