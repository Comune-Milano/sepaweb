Imports System.Collections

Partial Class TabDettagliIDRICO
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstPreSerbatoi As System.Collections.Generic.List(Of Epifani.Serbatoi)
    Dim lstSerbatoi As System.Collections.Generic.List(Of Epifani.Serbatoi)
    Dim lstPompeS As System.Collections.Generic.List(Of Epifani.PompeS)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstPreSerbatoi = CType(HttpContext.Current.Session.Item("LSTSERBATOI"), System.Collections.Generic.List(Of Epifani.Serbatoi))
        lstSerbatoi = CType(HttpContext.Current.Session.Item("LSTPRESERBATOI"), System.Collections.Generic.List(Of Epifani.Serbatoi))
        lstPompeS = CType(HttpContext.Current.Session.Item("LSTPOMPES"), System.Collections.Generic.List(Of Epifani.PompeS))


        Try
            If Not IsPostBack Then

                lstPreSerbatoi.Clear()
                lstSerbatoi.Clear()
                lstPompeS.Clear()

                vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘‘par.cmd.Transaction = par.myTrans
                End If
                ''''''''''''''''''''''''''

                BindGrid_PreSerbatoi()
                BindGrid_Serbatoi()
                BindGrid_Pompe()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Property vIdImpianto() As Long
        Get
            If Not (ViewState("par_idImpianto") Is Nothing) Then
                Return CLng(ViewState("par_idImpianto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idImpianto") = value
        End Set

    End Property

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property

    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property


    'SERBATOI PRE-AUTOCLAVE GRID1
    Private Sub BindGrid_PreSerbatoi()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE " _
              & " from SISCOM_MI.SERBATOI_IDRICI " _
              & " where SISCOM_MI.SERBATOI_IDRICI.TIPO_SERBATOIO='PRE-AUTOCLAVE' and " _
                    & " SISCOM_MI.SERBATOI_IDRICI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.SERBATOI_IDRICI.MODELLO "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "SERBATOI_IDRICI")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        ds.Dispose()

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliIDRICO_txtSelPreSerbatoi').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliIDRICO_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliIDRICO_txtSelPreSerbatoi').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliIDRICO_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiSerbatoi() As Boolean

        ControlloCampiSerbatoi = True

        If PAR.IfEmpty(Me.txtModelloS.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiSerbatoi = False
            txtModelloS.Focus()
            Exit Function
        End If

        If Me.txtAnnoRealizzazioneS.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazioneS.Text = ""
        End If

    End Function

    Protected Sub btn_InserisciSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciSerbatoio.Click

        If ControlloCampiSerbatoi() = False Then
            txtAppareS.Text = "1"
            Exit Sub
        End If

        If Me.txtIDS.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaSerbatoi()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateSerbatoi()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelPreSerbatoi.Text = ""
        txtSelSerbatoi.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiSerbatoio.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelPreSerbatoi.Text = ""
        txtSelSerbatoi.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaSerbatoi()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                If Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE" Then
                    Dim gen As Epifani.Serbatoi

                    gen = New Epifani.Serbatoi(lstPreSerbatoi.Count, PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200), Me.txtMatricolaS.Text, Me.txtAnnoRealizzazioneS.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtVolumeS.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneBollaS.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneEsercizioS.Text, 0)), PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300))

                    DataGrid1.DataSource = Nothing
                    lstPreSerbatoi.Add(gen)
                    gen = Nothing

                    DataGrid1.DataSource = lstPreSerbatoi
                    DataGrid1.DataBind()
                Else
                    Dim gen As Epifani.Serbatoi

                    gen = New Epifani.Serbatoi(lstSerbatoi.Count, PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200), Me.txtMatricolaS.Text, Me.txtAnnoRealizzazioneS.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtVolumeS.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneBollaS.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneEsercizioS.Text, 0)), PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300))

                    DataGrid2.DataSource = Nothing
                    lstSerbatoi.Add(gen)
                    gen = Nothing

                    DataGrid2.DataSource = lstSerbatoi
                    DataGrid2.DataBind()
                End If

               
            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                If Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE" Then
                    PAR.cmd.CommandText = " insert into SISCOM_MI.SERBATOI_IDRICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE,TIPO_SERBATOIO) " _
                                        & " values (SISCOM_MI.SEQ_SERBATOI_IDRICI.NEXTVAL,:id_impianto,:modello,:matricola,:anno,:volume,:pressione_b,:pressione_e,:note,:tipo_serbatoio) "

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", Strings.Left(Me.txtMatricolaS.Text, 30)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Me.txtAnnoRealizzazioneS.Text))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("volume", strToNumber(Me.txtVolumeS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_b", strToNumber(Me.txtPressioneBollaS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_e", strToNumber(Me.txtPressioneEsercizioS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_serbatoio", Me.txtTipoSerbatoi.Text))


                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.Parameters.Clear()

                    BindGrid_PreSerbatoi()

                    '*** EVENTI_IMPIANTI
                    PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Serbatoio Pre-Autoclave")

                Else

                    PAR.cmd.CommandText = " insert into SISCOM_MI.SERBATOI_IDRICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE,TIPO_SERBATOIO) " _
                                        & " values (SISCOM_MI.SEQ_SERBATOI_IDRICI.NEXTVAL,:id_impianto,:modello,:matricola,:anno,:volume,:pressione_b,:pressione_e,:note,:tipo_serbatoio) "

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", Strings.Left(Me.txtMatricolaS.Text, 30)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Me.txtAnnoRealizzazioneS.Text))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("volume", strToNumber(Me.txtVolumeS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_b", strToNumber(Me.txtPressioneBollaS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_e", strToNumber(Me.txtPressioneEsercizioS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_serbatoio", Me.txtTipoSerbatoi.Text))

                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.Parameters.Clear()

                    BindGrid_Serbatoi()

                    '*** EVENTI_IMPIANTI
                    PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Serbatoio Autoclave")

                End If

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdateSerbatoi()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                If Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE" Then
                    lstPreSerbatoi(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)
                    lstPreSerbatoi(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaS.Text
                    lstPreSerbatoi(txtIdComponente.Text).NOTE = PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300)

                    lstPreSerbatoi(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneS.Text
                    lstPreSerbatoi(txtIdComponente.Text).VOLUME = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtVolumeS.Text, 0))
                    lstPreSerbatoi(txtIdComponente.Text).PRESSIONE_BOLLA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneBollaS.Text, 0))
                    lstPreSerbatoi(txtIdComponente.Text).PRESSIONE_ESERCIZIO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneEsercizioS.Text, 0))

                    DataGrid1.DataSource = lstPreSerbatoi
                    DataGrid1.DataBind()
                Else
                    lstSerbatoi(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)
                    lstSerbatoi(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaS.Text
                    lstSerbatoi(txtIdComponente.Text).NOTE = PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300)

                    lstSerbatoi(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneS.Text
                    lstSerbatoi(txtIdComponente.Text).VOLUME = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtVolumeS.Text, 0))
                    lstSerbatoi(txtIdComponente.Text).PRESSIONE_BOLLA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneBollaS.Text, 0))
                    lstSerbatoi(txtIdComponente.Text).PRESSIONE_ESERCIZIO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPressioneEsercizioS.Text, 0))

                    DataGrid2.DataSource = lstSerbatoi
                    DataGrid2.DataBind()
                End If

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                If Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE" Then
                    PAR.cmd.CommandText = "update SISCOM_MI.SERBATOI_IDRICI set ID_IMPIANTO=:id_impianto, MODELLO=:modello, MATRICOLA=:matricola,ANNO_COSTRUZIONE=:anno," _
                                                                        & "  VOLUME=:volume,PRESSIONE_BOLLA=:pressione_b,PRESSIONE_ESERCIZIO=:pressione_e,NOTE=:note,TIPO_SERBATOIO=:tipo_serbatoio " _
                                       & " where ID=" & Me.txtIDS.Text

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", Strings.Left(Me.txtMatricolaS.Text, 30)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Me.txtAnnoRealizzazioneS.Text))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("volume", strToNumber(Me.txtVolumeS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_b", strToNumber(Me.txtPressioneBollaS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_e", strToNumber(Me.txtPressioneEsercizioS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_serbatoio", Me.txtTipoSerbatoi.Text))

                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.Parameters.Clear()

                    BindGrid_PreSerbatoi()

                    '*** EVENTI_IMPIANTI
                    PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Serbatoio Pre-Autoclave")

                Else
                    PAR.cmd.CommandText = "update SISCOM_MI.SERBATOI_IDRICI set ID_IMPIANTO=:id_impianto, MODELLO=:modello, MATRICOLA=:matricola,ANNO_COSTRUZIONE=:anno," _
                                                                        & "  VOLUME=:volume,PRESSIONE_BOLLA=:pressione_b,PRESSIONE_ESERCIZIO=:pressione_e,NOTE=:note,TIPO_SERBATOIO=:tipo_serbatoio " _
                                       & " where ID=" & Me.txtIDS.Text

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("matricola", Strings.Left(Me.txtMatricolaS.Text, 30)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Me.txtAnnoRealizzazioneS.Text))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("volume", strToNumber(Me.txtVolumeS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_b", strToNumber(Me.txtPressioneBollaS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pressione_e", strToNumber(Me.txtPressioneEsercizioS.Text)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", PAR.PulisciStringaInvio(Me.txtNoteS.Text, 300)))
                    PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_serbatoio", Me.txtTipoSerbatoi.Text))


                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.Parameters.Clear()

                    BindGrid_Serbatoi()

                    '*** EVENTI_IMPIANTI
                    PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Serbatoio Autoclave")

                End If

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnApriPreSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriPreSerbatoio.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareS.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                    Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE"
                    Me.lblTitolo.Text = "Gestione Serbatoi Pre-Autoclave"

                    Me.txtIDS.Text = lstPreSerbatoi(txtIdComponente.Text).ID
                    Me.txtModelloS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).MATRICOLA, "")
                    Me.txtNoteS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazioneS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtVolumeS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).VOLUME, "")
                    Me.txtPressioneBollaS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).PRESSIONE_BOLLA, "")
                    Me.txtPressioneEsercizioS.Text = PAR.IfNull(lstPreSerbatoi(txtIdComponente.Text).PRESSIONE_ESERCIZIO, "")

                Else
                    '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                    If PAR.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    End If

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.SERBATOI_IDRICI where TIPO_SERBATOIO='PRE-AUTOCLAVE' and ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE"

                        Me.txtIDS.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloS.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaS.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
                        Me.txtNoteS.Text = PAR.IfNull(myReader1("NOTE"), "")

                        Me.txtAnnoRealizzazioneS.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtVolumeS.Text = PAR.IfNull(myReader1("VOLUME"), "")
                        Me.txtPressioneBollaS.Text = PAR.IfNull(myReader1("PRESSIONE_BOLLA"), "")
                        Me.txtPressioneEsercizioS.Text = PAR.IfNull(myReader1("PRESSIONE_ESERCIZIO"), "")

                    End If
                    myReader1.Close()

                End If
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnEliminaPreSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaPreSerbatoio.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareS.Text = "0"
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstPreSerbatoi.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Serbatoi In lstPreSerbatoi
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid1.DataSource = lstPreSerbatoi
                        DataGrid1.DataBind()

                    Else
                        '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                        If PAR.OracleConn.State = Data.ConnectionState.Open Then
                            Response.Write("IMPOSSIBILE VISUALIZZARE")
                            Exit Sub
                        Else
                            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            ‘‘par.cmd.Transaction = par.myTrans

                            PAR.cmd.CommandText = "delete from SISCOM_MI.SERBATOI_IDRICI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_PreSerbatoi()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Serbatoio Pre-Autoclave")

                        End If
                    End If

                    txtSelPreSerbatoi.Text = ""
                    txtSelSerbatoi.Text = ""
                    txtIdComponente.Text = ""

                End If
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAggPreSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggPreSerbatoio.Click
        Try

            Me.txtTipoSerbatoi.Text = "PRE-AUTOCLAVE"
            Me.lblTitolo.Text = "Gestione Serbatoi Pre-Autoclave"


            Me.txtIDS.Text = -1

            Me.txtModelloS.Text = ""
            Me.txtMatricolaS.Text = ""
            Me.txtNoteS.Text = ""
            Me.txtAnnoRealizzazioneS.Text = ""

            Me.txtVolumeS.Text = ""
            Me.txtPressioneBollaS.Text = ""
            Me.txtPressioneEsercizioS.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub



    'SERBATOI AUTOCLAVE GRID2
    Private Sub BindGrid_Serbatoi()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,VOLUME,PRESSIONE_BOLLA,PRESSIONE_ESERCIZIO,NOTE " _
              & " from SISCOM_MI.SERBATOI_IDRICI " _
              & " where SISCOM_MI.SERBATOI_IDRICI.TIPO_SERBATOIO='AUTOCLAVE' and " _
                    & " SISCOM_MI.SERBATOI_IDRICI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.SERBATOI_IDRICI.MODELLO "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "SERBATOI_IDRICI")

        DataGrid2.DataSource = ds
        DataGrid2.DataBind()

        ds.Dispose()

    End Sub

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliIDRICO_txtSelSerbatoi').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliIDRICO_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliIDRICO_txtSelSerbatoi').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliIDRICO_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub btnApriSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriSerbatoio.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareS.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                    Me.txtTipoSerbatoi.Text = "AUTOCLAVE"
                    Me.lblTitolo.Text = "Gestione Serbatoi Autoclave"

                    Me.txtIDS.Text = lstSerbatoi(txtIdComponente.Text).ID
                    Me.txtModelloS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).MATRICOLA, "")
                    Me.txtNoteS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazioneS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtVolumeS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).VOLUME, "")
                    Me.txtPressioneBollaS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).PRESSIONE_BOLLA, "")
                    Me.txtPressioneEsercizioS.Text = PAR.IfNull(lstSerbatoi(txtIdComponente.Text).PRESSIONE_ESERCIZIO, "")

                Else
                    '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                    If PAR.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    End If

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.SERBATOI_IDRICI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtTipoSerbatoi.Text = "AUTOCLAVE"

                        Me.txtIDS.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloS.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaS.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
                        Me.txtNoteS.Text = PAR.IfNull(myReader1("NOTE"), "")

                        Me.txtAnnoRealizzazioneS.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtVolumeS.Text = PAR.IfNull(myReader1("VOLUME"), "")
                        Me.txtPressioneBollaS.Text = PAR.IfNull(myReader1("PRESSIONE_BOLLA"), "")
                        Me.txtPressioneEsercizioS.Text = PAR.IfNull(myReader1("PRESSIONE_ESERCIZIO"), "")

                    End If
                    myReader1.Close()

                End If
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnEliminaSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaSerbatoio.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareS.Text = "0"
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstSerbatoi.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Serbatoi In lstSerbatoi
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid2.DataSource = lstSerbatoi
                        DataGrid2.DataBind()

                    Else
                        '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                        If PAR.OracleConn.State = Data.ConnectionState.Open Then
                            Response.Write("IMPOSSIBILE VISUALIZZARE")
                            Exit Sub
                        Else
                            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            ‘‘par.cmd.Transaction = par.myTrans


                            PAR.cmd.CommandText = "delete from SISCOM_MI.SERBATOI_IDRICI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Serbatoi()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Serbatoio Autoclave")

                        End If
                    End If

                    txtSelPreSerbatoi.Text = ""
                    txtIdComponente.Text = ""

                End If
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAggSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggSerbatoio.Click
        Try

            Me.txtTipoSerbatoi.Text = "AUTOCLAVE"
            Me.lblTitolo.Text = "Gestione Serbatoi Autoclave"

            Me.txtIDS.Text = -1

            Me.txtModelloS.Text = ""
            Me.txtMatricolaS.Text = ""
            Me.txtNoteS.Text = ""
            Me.txtAnnoRealizzazioneS.Text = ""

            Me.txtVolumeS.Text = ""
            Me.txtPressioneBollaS.Text = ""
            Me.txtPressioneEsercizioS.Text = ""



        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub



    'POMPE GRID3
    Private Sub BindGrid_Pompe()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "  select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA,DISCONNETTORE" _
                    & " from SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI " _
                    & " where SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI.ID_IMPIANTO = " & vIdImpianto _
                    & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI.MODELLO "


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "POMPE_CIRCOLAZIONE_IDRICI")


        DataGrid3.DataSource = ds
        DataGrid3.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliIDRICO_txtSelPompe').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliIDRICO_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliIDRICO_txtSelPompe').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliIDRICO_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Function ControlloCampiPompe() As Boolean

        ControlloCampiPompe = True


        If PAR.IfEmpty(Me.txtModelloP.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiPompe = False
            txtModelloP.Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazioneP.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazioneP.Text = ""
        End If

    End Function

    Protected Sub btn_InserisciPompe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciPompe.Click
        If ControlloCampiPompe() = False Then
            txtAppareP.Text = "1"
            Exit Sub
        End If

        If Me.txtIDP.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaPompe()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdatePompe()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelPompe.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiPompe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiPompe.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelPompe.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Private Sub SalvaPompe()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.PompeS

                gen = New Epifani.PompeS(lstPompeS.Count, PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200), Me.txtMatricolaP.Text, Me.txtAnnoRealizzazioneP.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataP.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaP.Text, 0)), Me.cmbDisconnettore.SelectedValue.ToString)

                DataGrid3.DataSource = Nothing
                lstPompeS.Add(gen)
                gen = Nothing

                DataGrid3.DataSource = lstPompeS
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI(ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA,DISCONNETTORE) " _
                                    & "values (SISCOM_MI.SEQ_POMPE_CIRCOLAZIONE_IDRICI.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricolaP.Text) & "','" & Me.txtAnnoRealizzazioneP.Text & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaP.Text, "Null")) & "," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPortataP.Text, "Null")) & "," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPrevalenzaP.Text, "Null")) & ",'" _
                                        & Me.cmbDisconnettore.SelectedValue.ToString & "')"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Pompe()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdatePompe()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstPompeS(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)
                lstPompeS(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaP.Text
                'lstPompeS(txtIdComponente.Text).NOTE = PAR.PulisciStrSql(Me.txtNoteP.Text)

                lstPompeS(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneP.Text
                lstPompeS(txtIdComponente.Text).POTENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0))
                lstPompeS(txtIdComponente.Text).PORTATA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataP.Text, 0))
                lstPompeS(txtIdComponente.Text).PREVALENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaP.Text, 0))

                lstPompeS(txtIdComponente.Text).DISCONNETTORE = Me.cmbDisconnettore.SelectedValue.ToString

                DataGrid3.DataSource = lstPompeS
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "update SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaP.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneP.Text & "'," _
                                            & "POTENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaP.Text, "Null")) & "," _
                                            & "PORTATA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPortataP.Text, "Null")) & "," _
                                            & "PREVALENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPrevalenzaP.Text, "Null")) & "," _
                                            & "DISCONNETTORE='" & Me.cmbDisconnettore.SelectedValue.ToString & "' " _
                                            & " where ID=" & Me.txtIDP.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Pompe()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnApriPompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriPompa.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareP.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDP.Text = lstPompeS(txtIdComponente.Text).ID
                    Me.txtModelloP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).MATRICOLA, "")
                    'Me.txtNoteP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazioneP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtPotenzaP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).POTENZA, "")
                    Me.txtPortataP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).PORTATA, "")
                    Me.txtPrevalenzaP.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).PREVALENZA, "")
                    Me.cmbDisconnettore.Items.FindByValue(PAR.IfNull(lstPompeS(txtIdComponente.Text).DISCONNETTORE, "")).Selected = True


                Else
                    '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                    If PAR.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    End If

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDP.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloP.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaP.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
                        'Me.txtNoteP.Text = PAR.IfNull(myReader1("NOTE"), "")

                        Me.txtAnnoRealizzazioneP.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtPotenzaP.Text = PAR.IfNull(myReader1("POTENZA"), "")
                        Me.txtPortataP.Text = PAR.IfNull(myReader1("PORTATA"), "")
                        Me.txtPrevalenzaP.Text = PAR.IfNull(myReader1("PREVALENZA"), "")
                        Me.cmbDisconnettore.SelectedValue = PAR.IfNull(myReader1("DISCONNETTORE"), "")


                    End If
                    myReader1.Close()

                End If
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub btnEliminaPompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaPompa.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareP.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else
                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstPompeS.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.PompeS In lstPompeS
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid3.DataSource = lstPompeS
                        DataGrid3.DataBind()

                    Else
                        '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO
                        If PAR.OracleConn.State = Data.ConnectionState.Open Then
                            Response.Write("IMPOSSIBILE VISUALIZZARE")
                            Exit Sub
                        Else
                            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            ‘‘par.cmd.Transaction = par.myTrans


                            PAR.cmd.CommandText = "delete from SISCOM_MI.POMPE_CIRCOLAZIONE_IDRICI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Pompe()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

                        End If
                    End If
                    txtSelPompe.Text = ""
                    txtIdComponente.Text = ""

                End If
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnAggPompe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggPompe.Click
        Try

            Me.txtIDP.Text = -1

            Me.txtModelloP.Text = ""
            Me.txtMatricolaP.Text = ""
            'Me.txtNoteP.Text = ""
            Me.txtAnnoRealizzazioneP.Text = ""
            Me.txtPotenzaP.Text = ""
            Me.txtPortataP.Text = ""
            Me.txtPrevalenzaP.Text = ""
            Me.cmbDisconnettore.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggPreSerbatoio.Visible = False
            Me.btnEliminaPreSerbatoio.Visible = False
            Me.btnApriPreSerbatoio.Visible = False

            Me.btnAggSerbatoio.Visible = False
            Me.btnEliminaSerbatoio.Visible = False
            Me.btnApriSerbatoio.Visible = False

            Me.btnAggPompe.Visible = False
            Me.btnEliminaPompa.Visible = False
            Me.btnApriPompa.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub


   
End Class
