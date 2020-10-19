'TAB ELENCO IMPIANTO TUTELA ALLOGGI
Imports System.Collections


Partial Class Tab_Tutela_Alloggi
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstAlloggi As System.Collections.Generic.List(Of Epifani.Alloggi)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstAlloggi = CType(HttpContext.Current.Session.Item("LSTALLOGGI"), System.Collections.Generic.List(Of Epifani.Alloggi))
       
        Try
            If Not IsPostBack Then

                lstAlloggi.Clear()

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

                BindGrid_Alloggi()

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

    Private Sub FrmSolaLettura()
        Try

            Me.btnApriA.Visible = False


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



    Private Sub BindGrid_Alloggi()
        Dim StringaSql As String

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_TUT_ALLOGGI.ID,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO, " _
                                    & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO, " _
                                    & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO,SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.ANTINTRUSIONE,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_ANTINTR,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_ANTINTR," _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_ANTINTR,'YYYYmmdd'),'DD/MM/YYYY') as DATA_RIMOZIONE_ANTINTR, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.BLINDATA,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_BLINDATA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_BLINDATA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.LASTRATURA,TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_LASTRATURA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSTALLA_LASTRATURA, " _
                                    & "TO_CHAR(TO_DATE(SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_LASTRATURA,'YYYYmmdd'),'DD/MM/YYYY') as DATA_RIMOZIONE_LASTRATURA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI " _
                                & "from SISCOM_MI.I_TUT_ALLOGGI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                & "where SISCOM_MI.I_TUT_ALLOGGI.ID_IMPIANTO = " & vIdImpianto & " and " _
                                 & "      SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI=SISCOM_MI.UNITA_IMMOBILIARI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD and " _
                                 & "      SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE " _
                                & "order by SISCOM_MI.I_TUT_ALLOGGI.ID"

        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "SISCOM_MI.I_TUT_ALLOGGI")

        DataGridA.DataSource = ds
        DataGridA.DataBind()

        ds.Dispose()
    End Sub



    Protected Sub DataGridA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridA.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Tutela_Alloggi_txtSelA').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " - " & Replace(e.Item.Cells(2).Text, "'", "\'") & " - " & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('Tab_Tutela_Alloggi_txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Tutela_Alloggi_txtUnita_Immobiliare').value='" & e.Item.Cells(14).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Tutela_Alloggi_txtSelA').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " - " & Replace(e.Item.Cells(2).Text, "'", "\'") & " - " & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('Tab_Tutela_Alloggi_txtIdComponente').value='" & e.Item.Cells(0).Text & "';document.getElementById('Tab_Tutela_Alloggi_txtUnita_Immobiliare').value='" & e.Item.Cells(14).Text & "'")
        End If
    End Sub



    Protected Sub btn_InserisciA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciA.Click
        Me.UpdateA()
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelA.Text = ""
        txtUnita_Immobiliare.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Protected Sub btn_ChiudiA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiA.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelA.Text = ""
        txtUnita_Immobiliare.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub UpdateA()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstAlloggi(txtIdComponente.Text).ID_UNITA_IMMOBILIARI = txtUnita_Immobiliare.Text

                lstAlloggi(txtIdComponente.Text).EDIFICIO = txtEdificio.Text
                lstAlloggi(txtIdComponente.Text).SCALA = txtScala.Text
                lstAlloggi(txtIdComponente.Text).PIANO = txtPiano.Text
                lstAlloggi(txtIdComponente.Text).INTERNO = txtInterno.Text
                lstAlloggi(txtIdComponente.Text).NOME_SUB = txtSUB.Text

                lstAlloggi(txtIdComponente.Text).ANTINTRUSIONE = Me.cmbAnti.SelectedValue.ToString
                lstAlloggi(txtIdComponente.Text).DATA_INSTALLA_ANTINTR = txtDataAnt1.Text
                lstAlloggi(txtIdComponente.Text).DATA_RIMOZIONE_ANTINTR = txtDataAnt2.Text

                lstAlloggi(txtIdComponente.Text).BLINDATA = Me.cmbBlindata.SelectedValue.ToString
                lstAlloggi(txtIdComponente.Text).DATA_INSTALLA_BLINDATA = txtDataBlind1.Text

                lstAlloggi(txtIdComponente.Text).LASTRATURA = Me.cmbLastratura.SelectedValue.ToString
                lstAlloggi(txtIdComponente.Text).DATA_INSTALLA_LASTRATURA = txtDataLast1.Text
                lstAlloggi(txtIdComponente.Text).DATA_RIMOZIONE_LASTRATURA = txtDataLast2.Text


                DataGridA.DataSource = lstAlloggi
                DataGridA.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_TUT_ALLOGGI set " _
                                         & "ANTINTRUSIONE=:anti,DATA_INSTALLA_ANTINTR=:data1_anti,DATA_RIMOZIONE_ANTINTR=:data2_anti," _
                                         & "BLINDATA=:blindatura,DATA_INSTALLA_BLINDATA=:data1_blindatura," _
                                         & "LASTRATURA=:lastratura,DATA_INSTALLA_LASTRATURA=:data1_lastra,DATA_RIMOZIONE_LASTRATURA=:data2_lastra " _
                                         & " where  SISCOM_MI.I_TUT_ALLOGGI.ID=" & Me.txtIDA.Text


                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anti", Me.cmbAnti.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data1_anti", PAR.AggiustaData(Me.txtDataAnt1.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data2_anti", PAR.AggiustaData(Me.txtDataAnt2.Text)))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("blindatura", Me.cmbBlindata.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data1_blindatura", PAR.AggiustaData(Me.txtDataBlind1.Text)))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("lastratura", Me.cmbLastratura.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data1_lastra", PAR.AggiustaData(Me.txtDataLast1.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data2_lastra", PAR.AggiustaData(Me.txtDataLast2.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Alloggi()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Tutela Alloggi")

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


    Protected Sub btnApriA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriA.Click

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDA.Text = lstAlloggi(txtIdComponente.Text).ID
                    Me.txtUnita_Immobiliare.Text = lstAlloggi(txtIdComponente.Text).ID_UNITA_IMMOBILIARI


                    Me.txtEdificio.Text = lstAlloggi(txtIdComponente.Text).EDIFICIO
                    Me.txtScala.Text = lstAlloggi(txtIdComponente.Text).SCALA
                    Me.txtPiano.Text = lstAlloggi(txtIdComponente.Text).PIANO
                    Me.txtInterno.Text = lstAlloggi(txtIdComponente.Text).INTERNO
                    Me.txtSUB.Text = lstAlloggi(txtIdComponente.Text).NOME_SUB

                    Me.cmbAnti.SelectedValue = PAR.IfNull(lstAlloggi(txtIdComponente.Text).ANTINTRUSIONE, "")
                    Me.txtDataAnt1.Text = PAR.FormattaData(PAR.IfNull(lstAlloggi(txtIdComponente.Text).DATA_INSTALLA_ANTINTR, ""))
                    Me.txtDataAnt2.Text = PAR.FormattaData(PAR.IfNull(lstAlloggi(txtIdComponente.Text).DATA_RIMOZIONE_ANTINTR, ""))

                    Me.cmbBlindata.SelectedValue = PAR.IfNull(lstAlloggi(txtIdComponente.Text).BLINDATA, "")
                    Me.txtDataBlind1.Text = PAR.FormattaData(PAR.IfNull(lstAlloggi(txtIdComponente.Text).DATA_INSTALLA_BLINDATA, ""))

                    Me.cmbLastratura.SelectedValue = PAR.IfNull(lstAlloggi(txtIdComponente.Text).LASTRATURA, "")
                    Me.txtDataLast1.Text = PAR.FormattaData(PAR.IfNull(lstAlloggi(txtIdComponente.Text).DATA_INSTALLA_LASTRATURA, ""))
                    Me.txtDataLast2.Text = PAR.FormattaData(PAR.IfNull(lstAlloggi(txtIdComponente.Text).DATA_RIMOZIONE_LASTRATURA, ""))

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

                    PAR.cmd.CommandText = "select SISCOM_MI.I_TUT_ALLOGGI.ID,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO, " _
                                    & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA,SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO, " _
                                    & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO,SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.ANTINTRUSIONE,SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_ANTINTR," _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_ANTINTR, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.BLINDATA,SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_BLINDATA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.LASTRATURA,SISCOM_MI.I_TUT_ALLOGGI.DATA_INSTALLA_LASTRATURA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.DATA_RIMOZIONE_LASTRATURA, " _
                                    & "SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI " _
                                & "from SISCOM_MI.I_TUT_ALLOGGI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_LIVELLO_PIANO, SISCOM_MI.IDENTIFICATIVI_CATASTALI " _
                                & "where SISCOM_MI.I_TUT_ALLOGGI.ID =" & txtIdComponente.Text & " And " _
                                 & "      SISCOM_MI.I_TUT_ALLOGGI.ID_UNITA_IMMOBILIARI=SISCOM_MI.UNITA_IMMOBILIARI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and " _
                                 & "      SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD and " _
                                 & "      SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE "

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDA.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtUnita_Immobiliare.Text = PAR.IfNull(myReader1("ID_UNITA_IMMOBILIARI"), -1)

                        Me.txtEdificio.Text = myReader1("EDIFICIO")
                        Me.txtScala.Text = myReader1("SCALA")
                        Me.txtPiano.Text = myReader1("PIANO")
                        Me.txtInterno.Text = myReader1("INTERNO")
                        Me.txtSUB.Text = myReader1("NOME_SUB")

                        Me.cmbAnti.SelectedValue = PAR.IfNull(myReader1("ANTINTRUSIONE"), "")
                        Me.txtDataAnt1.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_INSTALLA_ANTINTR"), ""))
                        Me.txtDataAnt2.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_RIMOZIONE_ANTINTR"), ""))

                        Me.cmbBlindata.SelectedValue = PAR.IfNull(myReader1("BLINDATA"), "")
                        Me.txtDataBlind1.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_INSTALLA_BLINDATA"), ""))

                        Me.cmbLastratura.SelectedValue = PAR.IfNull(myReader1("LASTRATURA"), "")
                        Me.txtDataLast1.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_INSTALLA_LASTRATURA"), ""))
                        Me.txtDataLast2.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_RIMOZIONE_LASTRATURA"), ""))

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



End Class
