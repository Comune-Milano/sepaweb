Imports System.Collections

Partial Class Tab_Antincendio_Dettagli
    Inherits UserControlSetIdMode

    Dim PAR As New CM.Global

    Dim lstSerbatoiAccumulo As System.Collections.Generic.List(Of Epifani.SerbatoiAccumulo)
    Dim lstMotopompe As System.Collections.Generic.List(Of Epifani.Motopompe)
    Dim lstScaleSel As System.Collections.Generic.List(Of Epifani.Scale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstSerbatoiAccumulo = CType(HttpContext.Current.Session.Item("LSTSERBATOIACCUMULO"), System.Collections.Generic.List(Of Epifani.SerbatoiAccumulo))
        lstMotopompe = CType(HttpContext.Current.Session.Item("LSTMOTOPOMPE"), System.Collections.Generic.List(Of Epifani.Motopompe))
        lstScaleSel = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

        Try
            If Not IsPostBack Then

                lstSerbatoiAccumulo.Clear()
                lstMotopompe.Clear()
                lstScaleSel.Clear()

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

                BindGrid_Serbatoi()
                BindGrid_Motopompe()

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

    'SERBATOI ACCUMULO I_ANT_SERBATOI  DataGridSerbatoi
    Private Sub BindGrid_Serbatoi()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.I_ANT_SERBATOI.ID,SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_SCALA," _
                            & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                            & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA"",SISCOM_MI.I_ANT_SERBATOI.CAPACITA" _
              & " from  SISCOM_MI.I_ANT_SERBATOI,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
              & " where SISCOM_MI.I_ANT_SERBATOI.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
              & " and   SISCOM_MI.I_ANT_SERBATOI.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
              & " order by SISCOM_MI.I_ANT_SERBATOI.ID "


        PAR.cmd.CommandText = StringaSql


        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ANT_SERBATOI")

        DataGridSerbatoio.DataSource = ds
        DataGridSerbatoio.DataBind()

        ds.Dispose()
    End Sub

    Function ControlloCampiSerbatoio() As Boolean

        ControlloCampiSerbatoio = True

        If PAR.IfEmpty(Me.cmbEdificioSerbatoio.Text, "Null") = "Null" Or Me.cmbEdificioSerbatoio.Text = "-1" Then
            Response.Write("<script>alert('Selezionare l\'ubicazione del serbatoio!');</script>")
            ControlloCampiSerbatoio = False
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbScalaSerbatoio.Text, "Null") = "Null" Or Me.cmbScalaSerbatoio.Text = "-1" Then
            Response.Write("<script>alert('Selezionare la scala!');</script>")
            ControlloCampiSerbatoio = False
            Exit Function
        End If

    End Function

    Protected Sub btn_Inserisci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci.Click
        If ControlloCampiSerbatoio() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtID.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaSerbatoio()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateSerbatoio()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelSerbatoio.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_Chiudi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelSerbatoio.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaSerbatoio()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.SerbatoiAccumulo

                gen = New Epifani.SerbatoiAccumulo(lstSerbatoiAccumulo.Count, Convert.ToInt32(Me.cmbEdificioSerbatoio.SelectedValue), RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaSerbatoio.SelectedValue, -1))), Me.cmbEdificioSerbatoio.SelectedItem.Text, Me.cmbScalaSerbatoio.SelectedItem.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtCapacita.Text, 0)))

                DataGridSerbatoio.DataSource = Nothing
                lstSerbatoiAccumulo.Add(gen)
                gen = Nothing

                DataGridSerbatoio.DataSource = lstSerbatoiAccumulo
                DataGridSerbatoio.DataBind()


            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SERBATOI  " _
                                            & " (ID,ID_IMPIANTO,ID_UBICAZIONE_EDIFICIO,ID_UBICAZIONE_SCALA,CAPACITA)" _
                                    & "values (SISCOM_MI.SEQ_I_ANT_SERBATOI.NEXTVAL,:id_impianto,:id_edificio,:id_scala,:capacita) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbEdificioSerbatoio.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaSerbatoio.SelectedValue, -1)))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("capacita", strToNumber(Me.txtCapacita.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Serbatoi()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Serbatoio Accumulo")

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

    Private Sub UpdateSerbatoio()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstSerbatoiAccumulo(txtIdComponente.Text).ID_UBICAZIONE_EDIFICIO = Convert.ToInt32(Me.cmbEdificioSerbatoio.SelectedValue)
                lstSerbatoiAccumulo(txtIdComponente.Text).ID_UBICAZIONE_SCALA = RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaSerbatoio.SelectedValue, -1)))
                lstSerbatoiAccumulo(txtIdComponente.Text).EDIFICIO = Me.cmbEdificioSerbatoio.SelectedItem.Text
                lstSerbatoiAccumulo(txtIdComponente.Text).SCALA = Me.cmbScalaSerbatoio.SelectedItem.Text
                lstSerbatoiAccumulo(txtIdComponente.Text).CAPACITA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtCapacita.Text, 0))

                DataGridSerbatoio.DataSource = lstSerbatoiAccumulo
                DataGridSerbatoio.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_ANT_SERBATOI  set " _
                                            & " ID_UBICAZIONE_EDIFICIO=:id_edificio,ID_UBICAZIONE_SCALA=:id_scala," _
                                            & "CAPACITA=:capacita" _
                                    & " where ID=" & Me.txtID.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbEdificioSerbatoio.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaSerbatoio.SelectedValue, -1)))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("capacita", strToNumber(Me.txtCapacita.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Serbatoi()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Serbatoio Accumulo")

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

    Protected Sub btnApriSerbatoio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriSerbatoio.Click

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtID.Text = lstSerbatoiAccumulo(txtIdComponente.Text).ID

                    Me.cmbEdificioSerbatoio.SelectedValue = PAR.IfNull(lstSerbatoiAccumulo(txtIdComponente.Text).ID_UBICAZIONE_EDIFICIO, "")
                    FiltraScale()
                    Me.cmbScalaSerbatoio.SelectedValue = PAR.IfNull(lstSerbatoiAccumulo(txtIdComponente.Text).ID_UBICAZIONE_SCALA, "-1")

                    Me.txtCapacita.Text = PAR.IfNull(lstSerbatoiAccumulo(txtIdComponente.Text).CAPACITA, "")

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

                    '*** I_ANT_SERBATOI
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ANT_SERBATOI  where ID=" & txtIdComponente.Text
                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtID.Text = myReader1("ID")

                        Me.cmbEdificioSerbatoio.SelectedValue = PAR.IfNull(myReader1("ID_UBICAZIONE_EDIFICIO"), "")
                        FiltraScale()
                        Me.cmbScalaSerbatoio.SelectedValue = PAR.IfNull(myReader1("ID_UBICAZIONE_SCALA"), "-1")

                        Me.txtCapacita.Text = PAR.IfNull(myReader1("CAPACITA"), "")

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
                    txtAppare.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstSerbatoiAccumulo.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.SerbatoiAccumulo In lstSerbatoiAccumulo
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridSerbatoio.DataSource = lstSerbatoiAccumulo
                        DataGridSerbatoio.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_SERBATOI  where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Serbatoi()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Serbatoio Accumulo")

                        End If
                    End If

                    txtSelSerbatoio.Text = ""
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
            Me.txtID.Text = -1

            Me.cmbEdificioSerbatoio.SelectedIndex = -1

            Me.cmbScalaSerbatoio.Items.Clear()
            cmbScalaSerbatoio.Items.Add(New ListItem(" ", -1))
            Me.cmbScalaSerbatoio.SelectedIndex = -1

            Me.txtCapacita.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub cmbEdificioSerbatoio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificioSerbatoio.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbEdificioSerbatoio.SelectedValue <> "-1" Then
            FiltraScale()
        Else
            Me.cmbScalaSerbatoio.Items.Clear()
            cmbScalaSerbatoio.Items.Add(New ListItem(" ", -1))
        End If

    End Sub

    Private Sub FiltraScale()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.cmbEdificioSerbatoio.SelectedValue <> "-1" Then

                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.cmbScalaSerbatoio.Items.Clear()
                cmbScalaSerbatoio.Items.Add(New ListItem(" ", -1))

                PAR.cmd.CommandText = "select  ID, DESCRIZIONE from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO =" & Me.cmbEdificioSerbatoio.SelectedValue.ToString & " order by DESCRIZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    cmbScalaSerbatoio.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()

                If FlagConnessione = True Then
                    PAR.OracleConn.Close()
                End If
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub DataGridSerbatoio_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSerbatoio.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Dettagli_txtSelSerbatoio').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " - SCALA: " & e.Item.Cells(4).Text & "';document.getElementById('Tab_Antincendio_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Dettagli_txtSelSerbatoio').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " - SCALA: " & e.Item.Cells(4).Text & "';document.getElementById('Tab_Antincendio_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    '##############################################################à

    'MOTOPOMPE UNI 70 I_ANT_MOTOPOMPE  DataGridMotopompe

    Private Sub BindGrid_Motopompe()
        Dim StringaSql As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.I_ANT_MOTOPOMPE.ID,SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_SCALA," _
                            & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                            & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA""," _
                            & " (select count(*) from SISCOM_MI.I_ANT_MOTOPOMPE_SCALE where  SISCOM_MI.I_ANT_MOTOPOMPE_SCALE.ID_I_ANT_MOTOPOMPE=SISCOM_MI.I_ANT_MOTOPOMPE.ID) AS ""SCALE_SERVITE"" " _
              & " from  SISCOM_MI.I_ANT_MOTOPOMPE,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
              & " where SISCOM_MI.I_ANT_MOTOPOMPE.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
              & " and   SISCOM_MI.I_ANT_MOTOPOMPE.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
              & " order by SISCOM_MI.I_ANT_MOTOPOMPE.ID "


        PAR.cmd.CommandText = StringaSql

        myReader1 = PAR.cmd.ExecuteReader()
        While myReader1.Read
            If PAR.IfNull(myReader1("SCALE_SERVITE"), 0) > 0 Then
                CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
            End If
        End While
        myReader1.Close()


        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ANT_MOTOPOMPE")

        DataGridMotopompa.DataSource = ds
        DataGridMotopompa.DataBind()

        ds.Dispose()
    End Sub

    Function ControlloCampiMotopompe() As Boolean

        ControlloCampiMotopompe = True

        If PAR.IfEmpty(Me.cmbEdificioMotopompa.Text, "Null") = "Null" Or Me.cmbEdificioMotopompa.Text = "-1" Then
            Response.Write("<script>alert('Selezionare l\'ubicazione della motopompa!');</script>")
            ControlloCampiMotopompe = False
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbScalaMotopompa.Text, "Null") = "Null" Or Me.cmbScalaMotopompa.Text = "-1" Then
            Response.Write("<script>alert('Selezionare la scala!');</script>")
            ControlloCampiMotopompe = False
            Exit Function
        End If

    End Function

    Protected Sub btn_InserisciM_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciM.Click
        If ControlloCampiMotopompe() = False Then
            txtAppareM.Text = "1"
            Exit Sub
        End If

        If Me.txtIDM.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaMotopompa()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateMotopompa()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelMotopompa.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiM_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiM.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelMotopompa.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Private Sub SalvaMotopompa()
        Dim i As Integer
        Dim RigaMotopompa As Integer
        Dim vIdMotopompa As Integer
        Dim ContaScaleServite As Integer
        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Motopompe
                RigaMotopompa = lstMotopompe.Count

                ContaScaleServite = 0
                '***********SCALE SERVITE
                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then

                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel.Count, Str(RigaMotopompa), Str(CheckBoxScale.Items(i).Value))
                        lstScaleSel.Add(genS)
                        genS = Nothing
                        ContaScaleServite = ContaScaleServite + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next

                gen = New Epifani.Motopompe(lstMotopompe.Count, Convert.ToInt32(Me.cmbEdificioMotopompa.SelectedValue), RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaMotopompa.SelectedValue, -1))), Me.cmbEdificioMotopompa.SelectedItem.Text, Me.cmbScalaMotopompa.SelectedItem.Text, ContaScaleServite)

                DataGridMotopompa.DataSource = Nothing
                lstMotopompe.Add(gen)
                gen = Nothing

                DataGridMotopompa.DataSource = lstMotopompe
                DataGridMotopompa.DataBind()


            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_MOTOPOMPE  " _
                                            & " (ID,ID_IMPIANTO,ID_UBICAZIONE_EDIFICIO,ID_UBICAZIONE_SCALA)" _
                                    & "values (SISCOM_MI.SEQ_I_ANT_MOTOPOMPE.NEXTVAL,:id_impianto,:id_edificio,:id_scala) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbEdificioMotopompa.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaMotopompa.SelectedValue, -1)))))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Motopompa UNI 70")


                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_MOTOPOMPE.CURRVAL FROM dual "
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdMotopompa = myReader1(0)
                End If

                myReader1.Close()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_MOTOPOMPE_SCALE  (ID_I_ANT_MOTOPOMPE,ID_SCALA) values " _
                                   & "(" & vIdMotopompa & "," & CheckBoxScale.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next

                BindGrid_Motopompe()

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

    Private Sub UpdateMotopompa()
        Dim i As Integer
        Dim ContaScaleServite As Integer

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                ContaScaleServite = 0
                '***********SCALE
SCALE:
                For i = 0 To lstScaleSel.Count - 1
                    If lstScaleSel(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstScaleSel.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next


                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel.Count, Str(txtIdComponente.Text), Str(CheckBoxScale.Items(i).Value))
                        lstScaleSel.Add(genS)
                        genS = Nothing
                        ContaScaleServite = ContaScaleServite + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next
                '*************************

                lstMotopompe(txtIdComponente.Text).ID_UBICAZIONE_EDIFICIO = Convert.ToInt32(Me.cmbEdificioMotopompa.SelectedValue)
                lstMotopompe(txtIdComponente.Text).ID_UBICAZIONE_SCALA = RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaMotopompa.SelectedValue, -1)))
                lstMotopompe(txtIdComponente.Text).EDIFICIO = Me.cmbEdificioMotopompa.SelectedItem.Text
                lstMotopompe(txtIdComponente.Text).SCALA = Me.cmbScalaMotopompa.SelectedItem.Text

                lstMotopompe(txtIdComponente.Text).SCALE_SERVITE = ContaScaleServite

                DataGridMotopompa.DataSource = lstMotopompe
                DataGridMotopompa.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_ANT_MOTOPOMPE  set " _
                                            & " ID_UBICAZIONE_EDIFICIO=:id_edificio,ID_UBICAZIONE_SCALA=:id_scala " _
                                    & " where ID=" & Me.txtIDM.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbEdificioMotopompa.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaMotopompa.SelectedValue, -1)))))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Motopompa UNI 70")


                PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_MOTOPOMPE_SCALE where ID_I_ANT_MOTOPOMPE= " & Me.txtIDM.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_MOTOPOMPE_SCALE  (ID_I_ANT_MOTOPOMPE,ID_SCALA) values " _
                                   & "(" & Me.txtIDM.Text & "," & CheckBoxScale.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next

                BindGrid_Motopompe()

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


    Protected Sub btnApriMotopompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriMotopompa.Click
        Dim i, j As Integer

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareM.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDM.Text = lstMotopompe(txtIdComponente.Text).ID

                    Me.cmbEdificioMotopompa.SelectedValue = PAR.IfNull(lstMotopompe(txtIdComponente.Text).ID_UBICAZIONE_EDIFICIO, "")
                    FiltraScaleM()
                    Me.cmbScalaMotopompa.SelectedValue = PAR.IfNull(lstMotopompe(txtIdComponente.Text).ID_UBICAZIONE_SCALA, "-1")

                    For i = 0 To CheckBoxScale.Items.Count - 1
                        CheckBoxScale.Items(i).Selected = False
                    Next

                    For i = 0 To CheckBoxScale.Items.Count - 1
                        For j = 0 To lstScaleSel.Count - 1
                            If Val(lstScaleSel(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxScale.Items(i).Value = Val(lstScaleSel(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxScale.Items(i).Selected = True
                                End If
                            End If
                        Next
                    Next

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

                    '*** I_ANT_MOTOPOMPE
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ANT_MOTOPOMPE where ID=" & txtIdComponente.Text
                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDM.Text = myReader1("ID")

                        Me.cmbEdificioMotopompa.SelectedValue = PAR.IfNull(myReader1("ID_UBICAZIONE_EDIFICIO"), "")
                        FiltraScaleM()
                        Me.cmbScalaMotopompa.SelectedValue = PAR.IfNull(myReader1("ID_UBICAZIONE_SCALA"), "-1")
                    End If
                    myReader1.Close()

                    '*** SCALE I_ANT_EDIFICI
                    '***Azzero la lista delle scale
                    For i = 0 To CheckBoxScale.Items.Count - 1
                        CheckBoxScale.Items(i).Selected = False
                    Next

                    '*** check della scala salvata
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select ID_SCALA from SISCOM_MI.I_ANT_MOTOPOMPE_SCALE where  ID_I_ANT_MOTOPOMPE= " & Me.txtIDM.Text

                    myReader2 = PAR.cmd.ExecuteReader()

                    While myReader2.Read
                        For i = 0 To CheckBoxScale.Items.Count - 1
                            If CheckBoxScale.Items(i).Value = PAR.IfNull(myReader2("ID_SCALA"), "-1") Then
                                CheckBoxScale.Items(i).Selected = True
                            End If
                        Next
                    End While
                    myReader2.Close()
                    '**************************

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


    Protected Sub btnEliminaMotopompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaMotopompa.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareM.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstMotopompe.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Motopompe In lstMotopompe
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridMotopompa.DataSource = lstMotopompe
                        DataGridMotopompa.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_MOTOPOMPE where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Motopompe()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Motopompa UNI 70")

                        End If
                    End If

                    txtSelMotopompa.Text = ""
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


    Protected Sub btnAggMotopompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggMotopompa.Click
        Dim i As Integer

        Try
            Me.txtIDM.Text = -1

            Me.cmbEdificioMotopompa.SelectedIndex = -1

            Me.cmbScalaMotopompa.Items.Clear()
            cmbScalaMotopompa.Items.Add(New ListItem(" ", -1))
            Me.cmbScalaMotopompa.SelectedIndex = -1

            For i = 0 To CheckBoxScale.Items.Count - 1
                CheckBoxScale.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub cmbEdificioMotopompa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificioMotopompa.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbEdificioMotopompa.SelectedValue <> "-1" Then
            FiltraScaleM()
        Else
            Me.cmbScalaMotopompa.Items.Clear()
            cmbScalaMotopompa.Items.Add(New ListItem(" ", -1))
        End If
    End Sub


    Private Sub FiltraScaleM()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.cmbEdificioMotopompa.SelectedValue <> "-1" Then

                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.cmbScalaMotopompa.Items.Clear()
                cmbScalaMotopompa.Items.Add(New ListItem(" ", -1))

                PAR.cmd.CommandText = "select  ID, DESCRIZIONE from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO =" & Me.cmbEdificioMotopompa.SelectedValue.ToString & " order by DESCRIZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    cmbScalaMotopompa.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()

                If FlagConnessione = True Then
                    PAR.OracleConn.Close()
                End If
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub DataGridMotopompa_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridMotopompa.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Dettagli_txtSelMotopompa').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " - SCALA: " & e.Item.Cells(4).Text & "';document.getElementById('Tab_Antincendio_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Dettagli_txtSelMotopompa').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " - SCALA: " & e.Item.Cells(4).Text & "';document.getElementById('Tab_Antincendio_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    '###########################################à

    Private Sub FrmSolaLettura()
        Try

            Me.btnAggMotopompa.Visible = False
            Me.btnEliminaMotopompa.Visible = False
            Me.btnApriMotopompa.Visible = False

            Me.btnAggSerbatoio.Visible = False
            Me.btnEliminaSerbatoio.Visible = False
            Me.btnApriSerbatoio.Visible = False

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

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function



End Class
