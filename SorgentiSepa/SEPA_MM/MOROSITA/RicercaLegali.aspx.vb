'*** RICERCA LEGALI MOROSITA'

Partial Class MOROSITA_RicercaLegali
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then

            Response.Flush()

            'If Session.Item("LIVELLO") <> "1" Then
            '    sFiliale = Session.Item("ID_STRUTTURA")
            'End If

            Me.cmbComune.Items.Clear()
            Me.cmbComune.Items.Add(New ListItem(" ", -1))

            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            Me.cmbCivico.Items.Clear()
            Me.cmbCivico.Items.Add(New ListItem(" ", -1))


            Me.cmbTribunali.Items.Clear()
            Me.cmbTribunali.Items.Add(New ListItem(" ", -1))

            CaricaComuni()
            CaricaIndirizzi()
            CaricaTribunali()


        End If

    End Sub



    'CARICO COMBO COMUNI
    Private Sub CaricaComuni()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            Me.cmbComune.Items.Clear()
            Me.cmbComune.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select COD,trim(NOME) as NOME from SEPA.COMUNI_NAZIONI " _
                                & " where COD IS NOT NULL " _
                                & "   and COD in (select COD_COMUNE from SISCOM_MI.MOROSITA_LEGALI where COD_COMUNE is not null or COD_COMUNE<>'')" _
                                & " order by NOME ASC"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmbComune.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            'Me.cmbComplesso.SelectedValue = "-1"


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO EDIFICI
    Private Sub CaricaIndirizzi()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO L'ELENCO EDIFICI (FABBRICATI)
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select ID,trim(INDIRIZZO) as INDIRIZZO from SISCOM_MI.MOROSITA_LEGALI " _
                                           & " where INDIRIZZO is not null or INDIRIZZO<>''" _
                                           & " order by INDIRIZZO ASC"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("INDIRIZZO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            'Me.cmbEdificio.SelectedValue = "-1"

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub


    'CARICO COMBO TRIBUNALI
    Private Sub CaricaTribunali()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            Me.cmbTribunali.Items.Clear()
            Me.cmbTribunali.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select TAB_TRIBUNALI_COMPETENTI.ID, T_COMUNI.NOME as COMUNE,TAB_TRIBUNALI_COMPETENTI.COMPETENZA  " _
                               & " from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI,SEPA.T_COMUNI " _
                               & " where TAB_TRIBUNALI_COMPETENTI.COD_COMUNE=T_COMUNI.COD " _
                               & " order by COMPETENZA,COMUNE ASC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbTribunali.Items.Add(New ListItem(par.IfNull(myReader1("COMPETENZA"), "") & " - " & par.IfNull(myReader1("COMUNE"), ""), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean

        Try

            ControlloCampi = True


            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiLegali.aspx?CO=" & Me.cmbComune.SelectedValue.ToString _
                                                                         & "&IN=" & par.VaroleDaPassare(Me.cmbIndirizzo.SelectedItem.Text) _
                                                                         & "&CI=" & par.VaroleDaPassare(Me.cmbCivico.SelectedItem.Text) _
                                                                         & "&CG=" & par.VaroleDaPassare(Me.txtCognome.Text) _
                                                                         & "&NM=" & par.VaroleDaPassare(Me.txtNome.Text) _
                                                                         & "&TR=" & par.VaroleDaPassare(Me.cmbTribunali.SelectedValue) _
                                                                        & "&ORD=" & Me.RBList1.Text _
                                                                             & "');</script>")
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Protected Sub cmbComune_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComune.SelectedIndexChanged
        FiltraIndirizzi()
        FiltraCivici()
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        FiltraCivici()
    End Sub


    Private Sub FiltraIndirizzi()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If



            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))


            If Me.cmbComune.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select  ID,trim(INDIRIZZO) as INDIRIZZO from SISCOM_MI.MOROSITA_LEGALI " _
                                    & " where INDIRIZZO is not null or INDIRIZZO<>''" _
                                    & "  and COD_COMUNE='" & Me.cmbComune.SelectedValue & "'" _
                                    & " order by INDIRIZZO asc"

            Else
                par.cmd.CommandText = " select  ID,trim(INDIRIZZO) as INDIRIZZO from SISCOM_MI.MOROSITA_LEGALI " _
                                    & " where INDIRIZZO is not null or INDIRIZZO<>''" _
                                    & " order by INDIRIZZO asc"


            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("INDIRIZZO"), " "), par.IfNull(myReader1("ID"), -1)))
                i = i + 1
            End While
            myReader1.Close()

            Me.cmbIndirizzo.SelectedValue = "-1"
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraCivici()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If



            '**** CARICO I CIVICI PER L'INDIRIZZO SELEZIONATO
            Me.cmbCivico.Items.Clear()
            Me.cmbCivico.Items.Add(New ListItem(" ", -1))


            If Me.cmbIndirizzo.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select ID, CIVICO from SISCOM_MI.MOROSITA_LEGALI " _
                                    & " where INDIRIZZO='" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "'" _
                                    & " order by CIVICO asc"



                i = 0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("CIVICO"), " "), i))
                    i = i + 1
                End While
                myReader1.Close()

                Me.cmbCivico.SelectedValue = "-1"
                '**************************

            End If

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            'If i = 1 Then
            '    Me.cmbCivico.Items(1).Selected = True
            'End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


End Class
