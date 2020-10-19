
Partial Class CicloPassivo_CicloPassivo_LOTTI_SceltaLotto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Session.Add("IDA", 0)
            Dim a As String = par.DeCripta("4C564B4C09010B1C")
            x.Value = UCase(Request.QueryString("X")) 'serve per sapere se è aperto come finestra di dialogo
            idfornitore.Value = UCase(Request.QueryString("F")) 'id fornitore da passare agli appalti

            If x.Value = "1" Then
                tipo = "_self"

            Else
                tipo = ""

            End If

            CaricaEsercizio()
            CaricaFiliali()
            cmbTipoLotto.SelectedValue = "E"
            CaricaLotti()
            Me.rdbType.SelectedValue = 0
        End If
        NascondiCampi()
    End Sub

    Private Sub CaricaFiliali()
        '*****PEPPE MODIFY 30/09/2010*****
        '*****************FILIALI*********************************
        '************APERTURA CONNESSIONE**********
        If Me.cmbesercizio.SelectedValue <> "-1" Then
            Me.CmbFiliali.Items.Clear()
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim CondStruttura As String = ""
            'If par.IfNull(Session.Item("LIVELLO"), 0) <> 1 Then
            CondStruttura = " AND OPERATORI.ID = " & Session.Item("ID_OPERATORE") & " AND TAB_FILIALI.ID=OPERATORI.ID_UFFICIO"

            par.cmd.CommandText = "SELECT TAB_FILIALI.ID, TAB_FILIALI.NOME || ' - ' || INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '|| COMUNI_NAZIONI.NOME AS INDIRIZZO" _
                                & " FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,OPERATORI " _
                                & " WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD " & CondStruttura & " ORDER BY TAB_FILIALI.NOME ASC"
            'Else

            '    par.cmd.CommandText = "SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO" _
            '        & " FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI " _
            '        & " WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD ORDER BY NOME ASC"
            'End If

            par.caricaComboTelerik(par.cmd.CommandText, CmbFiliali, "ID", "INDIRIZZO", True)
            If CmbFiliali.Items.Count = 2 Then
                CmbFiliali.Items(1).Selected = True
            End If
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    CmbFiliali.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & " - " & par.IfNull(myReader1("INDIRIZZO"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '************CHIUSURA CONNESSIONE**********
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            '*****************FINE FILIALI*********************************
        Else
            Me.CmbFiliali.Items.Clear()
        End If

    End Sub
    Private Sub CaricaEsercizio()
        Try
            Dim i As Integer = 0
            Dim ID_ANNO_EF_CORRENTE As Long = -1
            '*****PEPPE MODIFY 30/09/2010*****
            '************APERTURA CONNESSIONE**********
            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE SISCOM_MI.PF_MAIN.ID_STATO=5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || PF_STATI.descrizione || ')' AS DESCRIZIONE " _
                                & "FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,siscom_mi.PF_STATI " _
                                & "WHERE id_stato > = 5 and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND id_stato = PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                '  Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "DESCRIZIONE", False)
            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                '  Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If
            End If


            '************CHIUSURA CONNESSIONE**********
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    

    Public Property tipo() As String
        Get
            If Not (ViewState("par_tipo") Is Nothing) Then
                Return CStr(ViewState("par_tipo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipo") = value
        End Set

    End Property

    Protected Sub cmbesercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbesercizio.SelectedIndexChanged

        CaricaFiliali()
        Me.cmblotto.Items.Clear()
        CaricaLotti()
    End Sub
    Private Sub CaricaLotti()
        Try
            If Me.CmbFiliali.SelectedValue <> "" Then

                par.OracleConn.Open()
                par.SettaCommand(par)
                Dim stringa As String = "select ID, DESCRIZIONE from siscom_mi.lotti where siscom_mi.lotti.id_esercizio_finanziario=" & cmbEsercizio.SelectedValue & " AND LOTTI.ID_FILIALE = " & Me.CmbFiliali.SelectedValue & " AND LOTTI.TIPO = '" & Me.cmbTipoLotto.SelectedValue.ToString & "' order by id asc"
                par.caricaComboTelerik(stringa, cmblotto, "ID", "DESCRIZIONE", True, , , , False)
                If cmblotto.Items.Count = 2 Then
                    cmblotto.Items(1).Selected = True
                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                Me.cmblotto.Items.Clear()
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Protected Sub CmbFiliali_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbFiliali.SelectedIndexChanged
        If Me.cmbTipoLotto.SelectedValue <> "-1" Then
            '*****PEPPE MODIFY 30/09/2010*****
            CaricaLotti()
        End If
    End Sub

    Protected Sub cmbTipoLotto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoLotto.SelectedIndexChanged
        If Me.cmbTipoLotto.SelectedValue <> "-1" Then
            '*****PEPPE MODIFY 30/09/2010*****
            CaricaLotti()
        Else
            Me.cmblotto.Items.Clear()
        End If

    End Sub
    Private Sub NascondiCampi()
        If Me.rdbType.SelectedValue = 1 Then
            Me.CmbFiliali.Visible = False
            Me.lblStruttura.Visible = False
            Me.lblTipoLotto.Visible = False
            Me.cmbTipoLotto.Visible = False
            Me.lblLotto.Visible = False
            Me.cmblotto.Visible = False
        ElseIf Me.rdbType.SelectedValue = 0 Then
            Me.CmbFiliali.Visible = True
            Me.lblStruttura.Visible = True
            Me.lblTipoLotto.Visible = True
            Me.cmbTipoLotto.Visible = True
            Me.lblLotto.Visible = True
            Me.cmblotto.Visible = True
        End If
    End Sub

    Protected Sub ImgProcedi_Click(sender As Object, e As System.EventArgs) Handles ImgProcedi.Click

        If Me.rdbType.SelectedValue = 0 Then


            If cmblotto.SelectedValue <> "" And Me.CmbFiliali.SelectedValue <> "" Then

                If x.Value = "1" Then
                    Response.Redirect("Appalti.aspx?IDL=" & cmblotto.SelectedValue & "&IDFILI=" & Me.CmbFiliali.SelectedValue & "&X=1&F=" & idfornitore.Value)
                Else
                    Response.Redirect("Appalti.aspx?IDL=" & cmblotto.SelectedValue & "&IDFILI=" & Me.CmbFiliali.SelectedValue)
                End If
            Else
                RadWindowManager1.RadAlert("Attenzione! Se non si seleziona un lotto ed una struttura non è possibile inserire appalti", 300, 150, "Attenzione", "", "null")

            End If
        Else
            If Me.cmbEsercizio.SelectedValue > 0 Then
                '*****PEPPE MODIFY 30/09/2010*****
                '************APERTURA CONNESSIONE**********
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim idStruttura As String = ""
                Dim StatoPf As Integer
                par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")

                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idStruttura = lettore("ID_UFFICIO")
                End If
                lettore.Close()
                par.cmd.CommandText = "select id_stato from siscom_mi.pf_main where id_esercizio_finanziario = " & Me.cmbEsercizio.SelectedValue
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    StatoPf = par.IfNull(lettore("id_stato"), 0)
                End If
                lettore.Close()
                '************CHIUSURA CONNESSIONE**********
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If idStruttura > 0 Then

                    Select Case StatoPf
                        Case 5
                            Response.Redirect("AppaltiNP.aspx?IDESERC=" & par.VaroleDaPassare(Me.cmbEsercizio.SelectedValue.ToString))
                        Case 6
                            If Session.Item("FL_COMI") = 1 Then
                                Response.Redirect("AppaltiNP.aspx?IDESERC=" & par.VaroleDaPassare(Me.cmbEsercizio.SelectedValue.ToString))
                            Else
                                RadWindowManager1.RadAlert("ATTENZIONE!<br />Impossibile inserire un appalto sulle voci dell'esercizio finanziario chiuso!", 300, 150, "Attenzione", "", "null")

                            End If
                        Case Else
                            RadWindowManager1.RadAlert("ATTENZIONE!<br />Impossibile inserire un appalto sulle voci dell'esercizio finanziario non approvato!", 300, 150, "Attenzione", "", "null")
                    End Select
                Else
                    RadWindowManager1.RadAlert("L'operatore che cerca di eseguire l'inserimento non è legato a nessuna struttra!Impossibile procedere con l'inserimento!", 300, 150, "Attenzione", "", "null")

                End If
                'Response.Write("<script>alert('Funzione in fase di sviluppo, non ancora disponibile!')</script>")
            Else
                RadWindowManager1.RadAlert("Selezionare l'esercizio finanziario per continuare!", 300, 150, "Attenzione", "", "null")

                Me.rdbType.SelectedValue = 0

            End If


        End If

    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        If x.Value = "1" Then
            Response.Write("<script language='javascript'> { self.close(); }</script>")
        Else

            Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
        End If
    End Sub

End Class
