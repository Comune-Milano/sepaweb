'TAB ELENCO IMPIANTO TUTELA DEI CANCELLI
Imports System.Collections

Partial Class Tab_Tutela_Dettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstCancelli As System.Collections.Generic.List(Of Epifani.Cancelli)
    'Dim lstCitofoni As System.Collections.Generic.List(Of CM.Citofoni)
    'Dim lstScaleSel As System.Collections.Generic.List(Of CM.Scale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstCancelli = CType(HttpContext.Current.Session.Item("LSTCANCELLI"), System.Collections.Generic.List(Of Epifani.Cancelli))


        Try
            If Not IsPostBack Then

                lstCancelli.Clear()

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

                BindGrid_Cancelli()

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


 

    '*** CANCELLI
    Private Sub BindGrid_Cancelli()
        Dim StringaSql As String

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_TUT_CANCELLI.ID,SISCOM_MI.I_TUT_CANCELLI.CARRABILE,SISCOM_MI.I_TUT_CANCELLI.AUTOMATIZZATO," _
                                   & "SISCOM_MI.I_TUT_CANCELLI.MARCA,SISCOM_MI.I_TUT_CANCELLI.MODELLO,SISCOM_MI.I_TUT_CANCELLI.DITTA_MANUTENZIONE " _
                           & " from SISCOM_MI.I_TUT_CANCELLI " _
                           & " where SISCOM_MI.I_TUT_CANCELLI.ID_IMPIANTO = " & vIdImpianto _
                           & " order by SISCOM_MI.I_TUT_CANCELLI.MODELLO "

        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_TUT_CANCELLI")

        DataGridCA.DataSource = ds
        DataGridCA.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGridCA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCA.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Tutela_Dettagli_txtSelCA').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('Tab_Tutela_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Tutela_Dettagli_txtSelCA').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & "';document.getElementById('Tab_Tutela_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiCA() As Boolean

        ControlloCampiCA = True

        If PAR.IfEmpty(Me.txtMarca.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca del cancello!');</script>")
            ControlloCampiCA = False
            Me.txtMarca.Focus()
            Exit Function
        End If

    End Function

    Protected Sub btn_InserisciCA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciCA.Click
        If ControlloCampiCA() = False Then
            txtAppareCA.Text = "1"
            Exit Sub
        End If

        If Me.txtIDCA.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaCA()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateCA()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelCA.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Protected Sub btn_ChiudiCA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiCA.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelCA.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaCA()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Cancelli

                gen = New Epifani.Cancelli(lstCancelli.Count, Me.cmbCarrabile.SelectedValue.ToString, Me.cmbAutomatizzato.SelectedValue.ToString, PAR.PulisciStringaInvio(Me.txtMarca.Text, 200), Me.txtModello.Text, Me.txtDitta.Text)

                DataGridCA.DataSource = Nothing
                lstCancelli.Add(gen)
                gen = Nothing

                DataGridCA.DataSource = lstCancelli
                DataGridCA.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_TUT_CANCELLI (ID, ID_IMPIANTO,MARCA,MODELLO,AUTOMATIZZATO,CARRABILE,DITTA_MANUTENZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_TUT_CANCELLI.NEXTVAL,:id_impianto," _
                                    & "       :marca,:modello,:auto,:carrabile,:ditta) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("marca", PAR.PulisciStringaInvio(Me.txtMarca.Text, 200)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", Strings.Left(Me.txtModello.Text, 200)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("auto", Me.cmbAutomatizzato.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("carrabile", Me.cmbCarrabile.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(Me.txtDitta.Text, 200)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Cancelli()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Cancelli")

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

    Private Sub UpdateCA()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstCancelli(txtIdComponente.Text).MARCA = PAR.PulisciStringaInvio(Me.txtMarca.Text, 200)
                lstCancelli(txtIdComponente.Text).MODELLO = Me.txtModello.Text
                lstCancelli(txtIdComponente.Text).DITTA_MANUTENZIONE = Me.txtDitta.Text

                lstCancelli(txtIdComponente.Text).AUTOMATIZZATO = Me.cmbAutomatizzato.SelectedValue.ToString
                lstCancelli(txtIdComponente.Text).CARRABILE = Me.cmbCarrabile.SelectedValue.ToString

                DataGridCA.DataSource = lstCancelli
                DataGridCA.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_TUT_CANCELLI set " _
                                            & "MARCA=:marca,MODELLO=:modello,AUTOMATIZZATO=:auto,CARRABILE=:carrabile,DITTA_MANUTENZIONE=:ditta " _
                                   & " where ID=" & Me.txtIDCA.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("marca", PAR.PulisciStringaInvio(Me.txtMarca.Text, 200)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("modello", Strings.Left(Me.txtModello.Text, 200)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("auto", Me.cmbAutomatizzato.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("carrabile", Me.cmbCarrabile.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(Me.txtDitta.Text, 200)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Cancelli()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Cancelli")

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

    Protected Sub btnApriCA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriCA.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareCA.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDCA.Text = lstCancelli(txtIdComponente.Text).ID

                    Me.txtMarca.Text = PAR.IfNull(lstCancelli(txtIdComponente.Text).MARCA, "")
                    Me.txtModello.Text = PAR.IfNull(lstCancelli(txtIdComponente.Text).MODELLO, "")
                    Me.txtDitta.Text = PAR.IfNull(lstCancelli(txtIdComponente.Text).DITTA_MANUTENZIONE, "")

                    Me.cmbAutomatizzato.SelectedValue = PAR.IfNull(lstCancelli(txtIdComponente.Text).AUTOMATIZZATO, "")
                    Me.cmbCarrabile.SelectedValue = PAR.IfNull(lstCancelli(txtIdComponente.Text).CARRABILE, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_TUT_CANCELLI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDCA.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.txtMarca.Text = PAR.IfNull(myReader1("MARCA"), "")
                        Me.txtModello.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtDitta.Text = PAR.IfNull(myReader1("DITTA_MANUTENZIONE"), "")

                        Me.cmbAutomatizzato.SelectedValue = PAR.IfNull(myReader1("AUTOMATIZZATO"), "")
                        Me.cmbCarrabile.SelectedValue = PAR.IfNull(myReader1("CARRABILE"), "")

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

    Protected Sub btnEliminaCA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaCA.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareCA.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstCancelli.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Cancelli In lstCancelli
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridCA.DataSource = lstCancelli
                        DataGridCA.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_TUT_CANCELLI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Cancelli()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Cancelli")

                        End If
                    End If

                    txtSelCA.Text = ""
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

    Protected Sub btnAggCA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggCA.Click
        Try
            Me.txtIDCA.Text = -1

            Me.txtMarca.Text = ""
            Me.txtModello.Text = ""
            Me.txtDitta.Text = ""

            Me.cmbAutomatizzato.Text = ""
            Me.cmbCarrabile.Text = ""


        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub
    '*************


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

    Private Sub FrmSolaLettura()
        Try


            Me.btnAggCA.Visible = False
            Me.btnEliminaCA.Visible = False
            Me.btnApriCA.Visible = False

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
