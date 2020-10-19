
Partial Class SATISFACTION_RicercaSchede
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        erroreDate.Visible = False

        If Not IsPostBack Then
            txtData1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtData2.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CaricaDomande("0")
            CaricaOperatori()
            CaricaIndirizzi()
            Session.Remove("QUERY_ESPORTAZIONE_QUESTIONARI")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Redirect("pagina_home.aspx")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Dim controlloData1 = False
        Dim controlloData2 = False

        If Trim(txtData1.Text) = "" Then
            controlloData1 = True
        Else
            controlloData1 = par.ControllaData(txtData1)
        End If

        If Trim(txtData2.Text) = "" Then
            controlloData2 = True
        Else
            controlloData2 = par.ControllaData(txtData2)
        End If


        If Not controlloData1 Or Not controlloData2 Then
            erroreDate.Visible = True

        Else
            Dim codiceUI As String = Trim(txt_codUI.Text)

            Dim id_unita As Long



            If Not codiceUI = "" Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    If Right(codiceUI, 1) = "*" Then
                        Dim codiceUIModificato As String = Mid(codiceUI, 1, Len(codiceUI) - 1)
                        par.cmd.CommandText = "SELECT SISCOM_MI.UNITA_IMMOBILIARI.* FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & par.PulisciStrSql(codiceUIModificato) & "%'"
                        'MsgBox("SELECT SISCOM_MI.UNITA_IMMOBILIARI.* FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE LIKE '" & par.PulisciStrSql(codiceUIModificato) & "%'")
                    Else
                        par.cmd.CommandText = "SELECT SISCOM_MI.UNITA_IMMOBILIARI.* FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ='" & par.PulisciStrSql(codiceUI) & "'"
                        'MsgBox("SELECT SISCOM_MI.UNITA_IMMOBILIARI.* FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ='" & par.PulisciStrSql(codiceUI) & "'")
                    End If

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If Not myReader.Read Then
                        id_unita = -1
                        Response.Write("<script>alert('Codice unità immobiliare non corretto!');</script>")
                    Else
                        id_unita = myReader("ID")
                    End If

                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch ex As Exception

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write(ex.Message)

                End Try
            End If

            Dim ParametriRicerca As String = ""
            ParametriRicerca = ParametriRicerca & "SERV=" & ddlServizi.SelectedValue
            ParametriRicerca = ParametriRicerca & "&DOM=" & ddlDomande.SelectedValue
            ParametriRicerca = ParametriRicerca & "&RISP=" & ddlRisposta.SelectedValue
            ParametriRicerca = ParametriRicerca & "&VAL=" & ddlValore.SelectedValue
            ParametriRicerca = ParametriRicerca & "&CODUI=" & codiceUI
            ParametriRicerca = ParametriRicerca & "&CIV=" & ddlCivico.SelectedValue
            ParametriRicerca = ParametriRicerca & "&GIU=" & ddlGiudizio.SelectedValue
            If opID.Value = "-1" Then
                ParametriRicerca = ParametriRicerca & "&OP=" & ddlOperatori.SelectedValue
            Else


                par.OracleConn.Open()
                par.SettaCommand(par)
                ddlOperatori.Items.Clear()
                ddlOperatori.Items.Add("---")
                par.cmd.CommandText = "SELECT ID FROM OPERATORI WHERE OPERATORE='" & Session.Item("OPERATORE") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim IDoperatore As Long = -1


                If myReader.Read Then
                    IDoperatore = myReader(0)
                End If
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                ParametriRicerca = ParametriRicerca & "&OP=" & IDoperatore

            End If

            ParametriRicerca = ParametriRicerca & "&DATAI=" & txtData1.Text
            ParametriRicerca = ParametriRicerca & "&DATAF=" & txtData2.Text



            'Apice carattere non consentito...
            '----------------------------------------------------------------------------------------------------
            ParametriRicerca = ParametriRicerca & "&IND=" & Trim(Replace(ddlIndirizzi.SelectedValue, "'", "@@@"))
            '----------------------------------------------------------------------------------------------------

            If id_unita <> -1 Then
                Response.Redirect("RisultatoSchede.aspx?" & ParametriRicerca)
            End If

        End If





    End Sub

    Protected Sub ddlServizi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlServizi.SelectedIndexChanged
        CaricaDomande(ddlServizi.SelectedValue)
    End Sub

    Protected Sub CaricaIndirizzi()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            ddlIndirizzi.Items.Clear()
            ddlIndirizzi.Items.Add("---")
            par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.INDIRIZZI.DESCRIZIONE) FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID GROUP BY SISCOM_MI.INDIRIZZI.DESCRIZIONE ORDER BY SISCOM_MI.INDIRIZZI.DESCRIZIONE"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                ddlIndirizzi.Items.Add(myReader("DESCRIZIONE"))
            End While

            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try

    End Sub

    Protected Sub CaricaOperatori()
        opID.Value = "-1"
        Try

            If Session.Item("MOD_SATISFACTION_SV") = "1" Then
                'If 1 Then
                ddlOperatori.Visible = True
                lblOperatore.Visible = True


                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT ID FROM OPERATORI WHERE MOD_SATISFACTION_SV='1' AND OPERATORE='" & Session.Item("OPERATORE") & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader


                If myReader.Read Then
                    'supervisore
                    
                    ddlOperatori.Items.Clear()
                    ddlOperatori.Items.Add("---")
                    par.cmd.CommandText = "SELECT ID,OPERATORE FROM OPERATORI WHERE MOD_SATISFACTION='1'"
                    myReader2 = par.cmd.ExecuteReader()
                    While myReader2.Read
                        ddlOperatori.Items.Add(New ListItem(myReader2("OPERATORE"), myReader2("ID")))
                    End While
                    
                End If
                
                myReader.Close()
                myReader2.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                'non supervisore
                ddlOperatori.Visible = False
                lblOperatore.Visible = False
                opID.Value = "1"

            End If


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try

    End Sub

    Protected Sub CaricaCivici()
        Try
            
            If ddlIndirizzi.SelectedValue = "---" Then
                ddlCivico.Items.Clear()
                ddlCivico.Items.Add("---")
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                ddlCivico.Items.Clear()
                ddlCivico.Items.Add("---")
                Dim indirizzo As String = ddlIndirizzi.SelectedValue
                indirizzo = Trim(Replace(indirizzo, "'", "''"))
                par.cmd.CommandText = "SELECT DISTINCT (SISCOM_MI.INDIRIZZI.CIVICO) FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.CUSTOMER_SATISFACTION,SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO=SISCOM_MI.INDIRIZZI.ID AND SISCOM_MI.CUSTOMER_SATISFACTION.ID_UNITA=SISCOM_MI.UNITA_IMMOBILIARI.ID AND SISCOM_MI.INDIRIZZI.DESCRIZIONE='" & indirizzo & "' ORDER BY SISCOM_MI.INDIRIZZI.CIVICO"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    ddlCivico.Items.Add(myReader("CIVICO"))
                End While

                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            

        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)

        End Try

    End Sub


    Protected Sub CaricaDomande(ByVal Tipo As String)
        'Carica la ddl delle domande
        Select Case Tipo
            Case "0"
                'Caso generico
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "25"))
                AggiungiServiziGenerici(0)
                AggiungiServiziPulizia()
                AggiungiServiziPortierato()
                AggiungiServiziRiscaldamento()
                AggiungiServiziManutenzione()
            Case "1"
                'Servizi di pulizia
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "26"))
                AggiungiServiziGenerici(1)
                AggiungiServiziPulizia()
            Case "2"
                'Servizi di portierato
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "27"))
                AggiungiServiziGenerici(2)
                AggiungiServiziPortierato()
            Case "3"
                'Servizi di riscaldamento
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "28"))
                AggiungiServiziGenerici(3)
                AggiungiServiziRiscaldamento()
            Case "4"
                'Servizi di manutenzione del verde
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "29"))
                AggiungiServiziGenerici(4)
                AggiungiServiziManutenzione()
            Case Else
                'Caso generico
                ddlDomande.Items.Clear()
                ddlDomande.Items.Add(New ListItem("---", "25"))
                AggiungiServiziGenerici(0)
                AggiungiServiziPulizia()
                AggiungiServiziPortierato()
                AggiungiServiziRiscaldamento()
                AggiungiServiziManutenzione()
        End Select
    End Sub

    Protected Sub AggiungiServiziGenerici(ByVal indice As Integer)
        ddlDomande.Items.Add(New ListItem("Ritiene che il servizio sia svolto con REGOLARITA'?", CStr(indice * 3)))
        ddlDomande.Items.Add(New ListItem("Ritiene che il servizio sia svolto con QUALITA'?", CStr(indice * 3 + 1)))
        ddlDomande.Items.Add(New ListItem("Ritiene che il personale sia CORTESE?", CStr(indice * 3 + 2)))
    End Sub

    Protected Sub AggiungiServiziPulizia()
        ddlDomande.Items.Add(New ListItem("Ritiene che la condizione dei punti di raccolta rifiuti sia IGIENICAMENTE soddisfacente?", "32"))
        ddlDomande.Items.Add(New ListItem("Ritiene che la PULIZIA dei PIAZZALI E DELLE PARTI COMUNI sia adeguata?", "33"))
        ddlDomande.Items.Add(New ListItem("Ritiene tempestiva la rimozione di masserizie e RIFIUTI INGOMBRANTI?", "34"))
    End Sub

    Protected Sub AggiungiServiziPortierato()
        ddlDomande.Items.Add(New ListItem("Ritiene che il personale offra INFORMAZIONI COMPLETE?", "17"))
        ddlDomande.Items.Add(New ListItem("Ritiene che la GESTIONE DELLA POSTA sia soddisfacente?", "18"))
    End Sub

    Protected Sub AggiungiServiziRiscaldamento()
        ddlDomande.Items.Add(New ListItem("Ritiene che la TEMPERATURA sia adeguata nei mesi invernali?", "19"))
        ddlDomande.Items.Add(New ListItem("Ritiene che sia facile contattare il pronto intervento in caso di GUASTI?", "20"))
        ddlDomande.Items.Add(New ListItem("Ritiene che i GUASTI siano risolti con tempestività?", "21"))
    End Sub

    Protected Sub AggiungiServiziManutenzione()
        ddlDomande.Items.Add(New ListItem("Ritiene che l’intervento per risolvere i potenziali rischi (es: rami pendenti), sia tempestivo?", "22"))
        ddlDomande.Items.Add(New ListItem("Ritiene che i macchinari utilizzati siano troppo rumorosi?", "23"))
        ddlDomande.Items.Add(New ListItem("Ritiene che i rifiuti prodotti (es. taglio erba...) vengano smaltiti in modo soddisfacente?", "24"))
    End Sub

    Protected Sub ddlIndirizzi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIndirizzi.SelectedIndexChanged
        CaricaCivici()
    End Sub
End Class
