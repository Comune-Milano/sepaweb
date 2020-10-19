'TAB ELENCO SPRINKLER del'IMPIANTO ANTINCENDIO
Imports System.Collections


Partial Class Tab_Antincendio_Sprinkler
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstSprinkler As System.Collections.Generic.List(Of Epifani.Sprinkler)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstSprinkler = CType(HttpContext.Current.Session.Item("LSTSPRINKLER"), System.Collections.Generic.List(Of Epifani.Sprinkler))

        Try
            If Not IsPostBack Then

                lstSprinkler.Clear()

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

                BindGrid_S()

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




    '*** SPRINKLER
    Private Sub BindGrid_S()
        Dim StringaSql As String

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_ANT_SPRINKLER.ID,SISCOM_MI.I_ANT_SPRINKLER.ALLACCIAMENTO," _
                            & " SISCOM_MI.TIPOLOGIA_SPRINKLER.DESCRIZIONE AS ""SPRINKLER"",SISCOM_MI.I_ANT_SPRINKLER.CERTIFICAZIONI,SISCOM_MI.I_ANT_SPRINKLER.ID_TIPOLOGIA_SPRINKLER " _
                  & " from SISCOM_MI.I_ANT_SPRINKLER,SISCOM_MI.TIPOLOGIA_SPRINKLER " _
                  & " where SISCOM_MI.I_ANT_SPRINKLER.ID_IMPIANTO = " & vIdImpianto _
                  & " and SISCOM_MI.I_ANT_SPRINKLER.ID_TIPOLOGIA_SPRINKLER=SISCOM_MI.TIPOLOGIA_SPRINKLER.ID (+) " _
                  & " order by SISCOM_MI.I_ANT_SPRINKLER.ALLACCIAMENTO "

        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ANT_SPRINKLER")

        DataGridS.DataSource = ds
        DataGridS.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGridS_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridS.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Sprinkler_txtSelS').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Antincendio_Sprinkler_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Sprinkler_txtSelS').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Antincendio_Sprinkler_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiS() As Boolean

        ControlloCampiS = True

        If PAR.IfEmpty(Me.cmbAllacciamento.SelectedItem.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il tipo di allacciamento!');</script>")
            ControlloCampiS = False
            Me.cmbAllacciamento.Focus()
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbSprinkler.SelectedItem.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire il tipo di sprinkler!');</script>")
            ControlloCampiS = False
            Me.cmbSprinkler.Focus()
            Exit Function
        End If

    End Function

    Protected Sub btn_InserisciS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciS.Click
        If ControlloCampiS() = False Then
            txtAppareS.Text = "1"
            Exit Sub
        End If

        If Me.txtIDS.Text = "-1" Then
            Me.SalvaSprinkler()
        Else
            Me.UpdateSprinkler()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelS.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Protected Sub btn_ChiudiS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiS.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelS.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaSprinkler()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Sprinkler

                gen = New Epifani.Sprinkler(lstSprinkler.Count, Me.cmbAllacciamento.SelectedItem.Text, Me.cmbSprinkler.SelectedItem.Text, Me.cmbCertificazioni.SelectedValue.ToString, Me.cmbSprinkler.SelectedValue)

                DataGridS.DataSource = Nothing
                lstSprinkler.Add(gen)
                gen = Nothing

                DataGridS.DataSource = lstSprinkler
                DataGridS.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_SPRINKLER (ID, ID_IMPIANTO,ALLACCIAMENTO,ID_TIPOLOGIA_SPRINKLER,CERTIFICAZIONI) " _
                                    & "values (SISCOM_MI.SEQ_I_ANT_SPRINKLER.NEXTVAL,:id_impianto,:allacciamento,:sprinkler,:cert) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("allacciamento", Me.cmbAllacciamento.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("sprinkler", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbSprinkler.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cert", Me.cmbCertificazioni.SelectedValue.ToString))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_S()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Sprinkler")

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

    Private Sub UpdateSprinkler()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstSprinkler(txtIdComponente.Text).ALLACCIAMENTO = Me.cmbAllacciamento.SelectedItem.Text
                lstSprinkler(txtIdComponente.Text).SPRINKLER = Me.cmbSprinkler.SelectedItem.Text
                lstSprinkler(txtIdComponente.Text).CERTIFICAZIONI = Me.cmbCertificazioni.SelectedValue.ToString

                lstSprinkler(txtIdComponente.Text).ID_TIPOLOGIA_SPRINKLER = Me.cmbSprinkler.SelectedValue

                DataGridS.DataSource = lstSprinkler
                DataGridS.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_ANT_SPRINKLER set " _
                                            & "ALLACCIAMENTO=:allacciamento,ID_TIPOLOGIA_SPRINKLER=:sprinkler,CERTIFICAZIONI=:cert " _
                                   & " where ID=" & Me.txtIDS.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("allacciamento", Me.cmbAllacciamento.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("sprinkler", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbSprinkler.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cert", Me.cmbCertificazioni.SelectedValue.ToString))


                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_S()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Sprinkler")

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

    Protected Sub btnApriS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriS.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareS.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDS.Text = lstSprinkler(txtIdComponente.Text).ID

                    Me.cmbAllacciamento.SelectedValue = PAR.IfNull(lstSprinkler(txtIdComponente.Text).ALLACCIAMENTO, "")
                    Me.cmbCertificazioni.SelectedValue = PAR.IfNull(lstSprinkler(txtIdComponente.Text).CERTIFICAZIONI, "")

                    Me.cmbSprinkler.SelectedValue = PAR.IfNull(lstSprinkler(txtIdComponente.Text).ID_TIPOLOGIA_SPRINKLER, -1)

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ANT_SPRINKLER where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDS.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.cmbAllacciamento.Text = PAR.IfNull(myReader1("ALLACCIAMENTO"), "")
                        Me.cmbSprinkler.SelectedValue = PAR.IfNull(myReader1("ID_TIPOLOGIA_SPRINKLER"), -1)
                        Me.cmbCertificazioni.SelectedValue = PAR.IfNull(myReader1("CERTIFICAZIONI"), "")

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

    Protected Sub btnEliminaS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaS.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareS.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstSprinkler.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Sprinkler In lstSprinkler
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridS.DataSource = lstSprinkler
                        DataGridS.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_SPRINKLER where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_S()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Sprinkler")

                        End If
                    End If

                    txtSelS.Text = ""
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

    Protected Sub btnAggS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggS.Click
        Try
            Me.txtIDS.Text = -1

            Me.cmbAllacciamento.Text = ""
            Me.cmbSprinkler.SelectedValue = -1
            Me.cmbCertificazioni.Text = ""


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


            Me.btnAggS.Visible = False
            Me.btnEliminaS.Visible = False
            Me.btnApriS.Visible = False

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
