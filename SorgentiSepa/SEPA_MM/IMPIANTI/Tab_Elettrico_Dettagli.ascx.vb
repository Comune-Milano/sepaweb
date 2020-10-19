Imports System.Collections

Partial Class TabElettricoDettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstQuadroSE As System.Collections.Generic.List(Of Epifani.Quadro)
    Dim lstQuadroSC As System.Collections.Generic.List(Of Epifani.Quadro)

    Dim lstScaleSel As System.Collections.Generic.List(Of Epifani.Scale)
    Dim lstScaleSel2 As System.Collections.Generic.List(Of Epifani.Scale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstQuadroSE = CType(HttpContext.Current.Session.Item("LSTQUADRO_SE"), System.Collections.Generic.List(Of Epifani.Quadro))
        lstQuadroSC = CType(HttpContext.Current.Session.Item("LSTQUADRO_SC"), System.Collections.Generic.List(Of Epifani.Quadro))

        lstScaleSel = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL"), System.Collections.Generic.List(Of Epifani.Scale))
        lstScaleSel2 = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL2"), System.Collections.Generic.List(Of Epifani.Scale))


        Try
            If Not IsPostBack Then

                lstQuadroSE.Clear()
                lstQuadroSC.Clear()

                lstScaleSel.Clear()
                lstScaleSel2.Clear()

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

                BindGrid_Servizio()
                BindGrid_Scale()

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


    'QUADRO SERVIZI GRID SERVIZIO
    Private Sub BindGrid_Servizio()
        Dim StringaSql As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID,SISCOM_MI.I_ELE_QUADRO_SERVIZI.QUANTITA,SISCOM_MI.I_ELE_QUADRO_SERVIZI.DIFFERENZIALE,SISCOM_MI.I_ELE_QUADRO_SERVIZI.NORMA,SISCOM_MI.I_ELE_QUADRO_SERVIZI.UBICAZIONE," _
                      & " (select count(*) from SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI where  SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI.ID_QUADRO_SERVIZI=SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID) AS ""SCALE_SERVITE"" " _
                      & " from SISCOM_MI.I_ELE_QUADRO_SERVIZI " _
                      & " where SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID_IMPIANTO = " & vIdImpianto _
                      & " order by SISCOM_MI.I_ELE_QUADRO_SERVIZI.ID "

        PAR.cmd.CommandText = StringaSql

        myReader1 = PAR.cmd.ExecuteReader()
        While myReader1.Read
            If PAR.IfNull(myReader1("SCALE_SERVITE"), 0) > 0 Then
                CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                CType(Me.Page.FindControl("DrLEdificio"), DropDownList).Enabled = False
            End If
        End While
        myReader1.Close()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ELE_QUADRO_SERVIZI")

        DataGridServizio.DataSource = ds
        DataGridServizio.DataBind()

        ds.Dispose()

    End Sub

    'QUADRO SCALE GRID SCALE
    Private Sub BindGrid_Scale()
        Dim StringaSql As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_ELE_QUADRO_SCALA.ID,SISCOM_MI.I_ELE_QUADRO_SCALA.QUANTITA,SISCOM_MI.I_ELE_QUADRO_SCALA.DIFFERENZIALE,SISCOM_MI.I_ELE_QUADRO_SCALA.NORMA,SISCOM_MI.I_ELE_QUADRO_SCALA.UBICAZIONE," _
                    & " (select count(*) from SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI where  SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI.ID_QUADRO_SCALA=SISCOM_MI.I_ELE_QUADRO_SCALA.ID) AS ""SCALE_SERVITE"" " _
              & " from SISCOM_MI.I_ELE_QUADRO_SCALA " _
              & " where SISCOM_MI.I_ELE_QUADRO_SCALA.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.I_ELE_QUADRO_SCALA.ID "

        PAR.cmd.CommandText = StringaSql

        myReader1 = PAR.cmd.ExecuteReader()
        While myReader1.Read
            If PAR.IfNull(myReader1("SCALE_SERVITE"), 0) > 0 Then
                CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                CType(Me.Page.FindControl("DrLEdificio"), DropDownList).Enabled = False
            End If
        End While
        myReader1.Close()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ELE_QUADRO_SCALA")

        DataGridScala.DataSource = ds
        DataGridScala.DataBind()

        ds.Dispose()

    End Sub



    Protected Sub DataGridServizio_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridServizio.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoDettagli_txtSelServizio').value='Hai selezionato: " & e.Item.Cells(2).Text & "';document.getElementById('TabElettricoDettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoDettagli_txtSelServizio').value='Hai selezionato: " & e.Item.Cells(2).Text & "';document.getElementById('TabElettricoDettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGridScala_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridScala.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoDettagli_txtSelScale').value='Hai selezionato: " & e.Item.Cells(2).Text & "';document.getElementById('TabElettricoDettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoDettagli_txtSelScale').value='Hai selezionato: " & e.Item.Cells(2).Text & "';document.getElementById('TabElettricoDettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    Function ControlloCampiServizio() As Boolean

        ControlloCampiServizio = True

        If PAR.IfEmpty(Me.txtQuantitaSE.Text, 0) <= 0 Then
            Response.Write("<script>alert('Inserire la quantità!');</script>")
            ControlloCampiServizio = False
            Me.txtQuantitaSE.Focus()
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbDifferenzialeSE.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Protezione Differenziale!');</script>")
            ControlloCampiServizio = False
            Me.cmbDifferenzialeSE.Focus()
            Exit Function
        End If

    End Function

    Function ControlloCampiScale() As Boolean

        ControlloCampiScale = True

        If PAR.IfEmpty(Me.txtQuantitaSC.Text, 0) <= 0 Then
            Response.Write("<script>alert('Inserire la quantità!');</script>")
            ControlloCampiScale = False
            Me.txtQuantitaSC.Focus()
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbDifferenzialeSC.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Protezione Differenziale!');</script>")
            ControlloCampiScale = False
            Me.cmbDifferenzialeSC.Focus()
            Exit Function
        End If

    End Function


    Protected Sub btn_InserisciServizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciServizio.Click
        If ControlloCampiServizio() = False Then
            txtAppareSE.Text = "1"
            Exit Sub
        End If

        If Me.txtIDSE.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaServizio()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateServizio()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelServizio.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_InserisciScala_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciScala.Click
        If ControlloCampiScale() = False Then
            txtAppareSC.Text = "1"
            Exit Sub
        End If

        If Me.txtIDSC.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaScala()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateScala()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelScale.Text = ""
        txtIdComponente.Text = ""

    End Sub


    Protected Sub btn_ChiudiServizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiServizio.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelServizio.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Protected Sub btn_ChiudiScala_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiScala.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelScale.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Private Sub SalvaServizio()
        Dim i, RigaQuadro As Integer
        Dim vIdQuadro As Integer
        Dim ContaElementiServiti As Integer


        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim genSE As Epifani.Quadro
                RigaQuadro = lstQuadroSE.Count

                ContaElementiServiti = 0
                '***********ELEMENTI SERVITE
                For i = 0 To CheckBoxScaleSE.Items.Count - 1
                    If CheckBoxScaleSE.Items(i).Selected = True Then

                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel.Count, Str(RigaQuadro), Str(CheckBoxScaleSE.Items(i).Value))
                        lstScaleSel.Add(genS)
                        genS = Nothing
                        ContaElementiServiti = ContaElementiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                        CType(Me.Page.FindControl("DrLEdificio"), DropDownList).Enabled = False
                    End If
                Next

                genSE = New Epifani.Quadro(lstQuadroSE.Count, Me.txtQuantitaSE.Text, Me.cmbDifferenzialeSE.SelectedItem.Text, Me.cmbNormaSE.SelectedValue.ToString, PAR.PulisciStrSql(Me.txtUbicazioneSE.Text), ContaElementiServiti)

                DataGridServizio.DataSource = Nothing
                lstQuadroSE.Add(genSE)
                genSE = Nothing

                DataGridServizio.DataSource = lstQuadroSE
                DataGridServizio.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SERVIZI (ID, ID_IMPIANTO,QUANTITA,DIFFERENZIALE,NORMA,UBICAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_QUADRO_SERVIZI.NEXTVAL,:id_impianto,:quantita,:differenziale,:norma,:ubicazione) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", Me.txtQuantitaSE.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeSE.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", Me.cmbNormaSE.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Strings.Left(Me.txtUbicazioneSE.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()


                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Quadro Servizi")

                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_ELE_QUADRO_SERVIZI.CURRVAL FROM dual "
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdQuadro = myReader1(0)
                End If

                myReader1.Close()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScaleSE.Items.Count - 1
                    If CheckBoxScaleSE.Items(i).Selected = True Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI  (ID_QUADRO_SERVIZI,ID_ELEMENTO) values " _
                                   & "(" & vIdQuadro & "," & CheckBoxScaleSE.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        'PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next

                BindGrid_Servizio()

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

    Private Sub UpdateServizio()
        Dim i As Integer
        Dim ContaElementiServiti As Integer

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                ContaElementiServiti = 0
                '***********SCALE
SCALE:
                For i = 0 To lstScaleSel.Count - 1
                    If lstScaleSel(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstScaleSel.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next

                For i = 0 To CheckBoxScaleSE.Items.Count - 1
                    If CheckBoxScaleSE.Items(i).Selected = True Then
                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel.Count, Str(txtIdComponente.Text), Str(CheckBoxScaleSE.Items(i).Value))
                        lstScaleSel.Add(genS)
                        genS = Nothing
                        ContaElementiServiti = ContaElementiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                        CType(Me.Page.FindControl("DrLEdificio"), DropDownList).Enabled = False
                    End If
                Next
                '*************************

                lstQuadroSE(txtIdComponente.Text).QUANTITA = Me.txtQuantitaSE.Text
                lstQuadroSE(txtIdComponente.Text).DIFFERENZIALE = Me.cmbDifferenzialeSE.SelectedItem.Text
                lstQuadroSE(txtIdComponente.Text).NORMA = Me.cmbNormaSE.SelectedValue.ToString
                lstQuadroSE(txtIdComponente.Text).UBICAZIONE = PAR.PulisciStrSql(Me.txtUbicazioneSE.Text)
                lstQuadroSE(txtIdComponente.Text).SCALE_SERVITE = ContaElementiServiti

                DataGridServizio.DataSource = lstQuadroSE
                DataGridServizio.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_ELE_QUADRO_SERVIZI set " _
                                            & "QUANTITA=:quantita,DIFFERENZIALE=:differenziale,NORMA=:norma,UBICAZIONE=:ubicazione " _
                                   & " where ID=" & Me.txtIDSE.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", Me.txtQuantitaSE.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeSE.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", Me.cmbNormaSE.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Strings.Left(Me.txtUbicazioneSE.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()


                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Quadro Servizi")

                PAR.cmd.CommandText = "delete from SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI where ID_QUADRO_SERVIZI= " & Me.txtIDSE.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScaleSE.Items.Count - 1
                    If CheckBoxScaleSE.Items(i).Selected = True Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI  (ID_QUADRO_SERVIZI,ID_ELEMENTO) values " _
                                   & "(" & Me.txtIDSE.Text & "," & CheckBoxScaleSE.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        'PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next

                BindGrid_Servizio()


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


    Private Sub SalvaScala()
        Dim i, RigaQuadro As Integer
        Dim vIdQuadro As Integer
        Dim ContaElementiServiti As Integer

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim genSC As Epifani.Quadro
                RigaQuadro = lstQuadroSC.Count

                ContaElementiServiti = 0
                '***********ELEMENTI SERVITE
                For i = 0 To CheckBoxScaleSC.Items.Count - 1
                    If CheckBoxScaleSC.Items(i).Selected = True Then

                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel2.Count, Str(RigaQuadro), Str(CheckBoxScaleSC.Items(i).Value))
                        lstScaleSel2.Add(genS)
                        genS = Nothing
                        ContaElementiServiti = ContaElementiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                        CType(Me.Page.FindControl("DrLEdificio"), DropDownList).Enabled = False
                    End If
                Next

                genSC = New Epifani.Quadro(lstQuadroSC.Count, Me.txtQuantitaSC.Text, Me.cmbDifferenzialeSC.SelectedItem.Text, Me.cmbNormaSC.SelectedValue.ToString, PAR.PulisciStrSql(Me.txtUbicazioneSC.Text), ContaElementiServiti)

                DataGridScala.DataSource = Nothing
                lstQuadroSC.Add(genSC)
                genSC = Nothing

                DataGridScala.DataSource = lstQuadroSC
                DataGridScala.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SCALA (ID, ID_IMPIANTO,QUANTITA,DIFFERENZIALE,NORMA,UBICAZIONE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_QUADRO_SCALA.NEXTVAL,:id_impianto,:quantita,:differenziale,:norma,:ubicazione) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", Me.txtQuantitaSC.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeSC.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", Me.cmbNormaSC.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Strings.Left(Me.txtUbicazioneSC.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_ELE_QUADRO_SCALA.CURRVAL FROM dual "
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdQuadro = myReader1(0)
                End If

                myReader1.Close()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScaleSC.Items.Count - 1
                    If CheckBoxScaleSC.Items(i).Selected = True Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI  (ID_QUADRO_SCALA,ID_ELEMENTO) values " _
                                   & "(" & vIdQuadro & "," & CheckBoxScaleSC.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        'PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next

                BindGrid_Scale()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Quadro Scala")

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

    Private Sub UpdateScala()
        Dim i As Integer
        Dim ContaElementiServiti As Integer

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                ContaElementiServiti = 0
                '***********SCALE
SCALE:
                For i = 0 To lstScaleSel2.Count - 1
                    If lstScaleSel2(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstScaleSel2.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next

                For i = 0 To CheckBoxScaleSC.Items.Count - 1
                    If CheckBoxScaleSC.Items(i).Selected = True Then
                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel2.Count, Str(txtIdComponente.Text), Str(CheckBoxScaleSC.Items(i).Value))
                        lstScaleSel2.Add(genS)
                        genS = Nothing
                        ContaElementiServiti = ContaElementiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                        CType(Me.Page.FindControl("DrLEdificio"), DropDownList).Enabled = False
                    End If
                Next
                '*************************


                lstQuadroSC(txtIdComponente.Text).QUANTITA = Me.txtQuantitaSC.Text
                lstQuadroSC(txtIdComponente.Text).DIFFERENZIALE = Me.cmbDifferenzialeSC.SelectedItem.Text
                lstQuadroSC(txtIdComponente.Text).NORMA = Me.cmbNormaSC.SelectedValue.ToString
                lstQuadroSC(txtIdComponente.Text).UBICAZIONE = PAR.PulisciStrSql(Me.txtUbicazioneSC.Text)
                lstQuadroSC(txtIdComponente.Text).SCALE_SERVITE = ContaElementiServiti

                DataGridScala.DataSource = lstQuadroSC
                DataGridScala.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_ELE_QUADRO_SCALA set " _
                                            & "QUANTITA=:quantita,DIFFERENZIALE=:differenziale,NORMA=:norma,UBICAZIONE=:ubicazione " _
                                   & " where ID=" & Me.txtIDSC.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", Me.txtQuantitaSC.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeSC.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", Me.cmbNormaSC.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Strings.Left(Me.txtUbicazioneSC.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "delete from SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI where ID_QUADRO_SCALA= " & Me.txtIDSC.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScaleSC.Items.Count - 1
                    If CheckBoxScaleSC.Items(i).Selected = True Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI  (ID_QUADRO_SCALA,ID_ELEMENTO) values " _
                                   & "(" & Me.txtIDSC.Text & "," & CheckBoxScaleSC.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        'PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Scale Servite della Motopompa UNI 70")

                    End If
                Next


                BindGrid_Scale()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Quadro Scala")

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


    Protected Sub btnApriServizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriServizio.Click
        Dim i, j As Integer

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareSE.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDSE.Text = lstQuadroSE(txtIdComponente.Text).ID

                    Me.txtQuantitaSE.Text = PAR.IfNull(lstQuadroSE(txtIdComponente.Text).QUANTITA, "")
                    Me.cmbDifferenzialeSE.SelectedValue = PAR.IfNull(lstQuadroSE(txtIdComponente.Text).DIFFERENZIALE, "")
                    Me.cmbNormaSE.SelectedValue = PAR.IfNull(lstQuadroSE(txtIdComponente.Text).NORMA, "")
                    Me.txtUbicazioneSE.Text = PAR.IfNull(lstQuadroSE(txtIdComponente.Text).UBICAZIONE, "")

                    For i = 0 To CheckBoxScaleSE.Items.Count - 1
                        CheckBoxScaleSE.Items(i).Selected = False
                    Next

                    For i = 0 To CheckBoxScaleSE.Items.Count - 1
                        For j = 0 To lstScaleSel.Count - 1
                            If Val(lstScaleSel(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxScaleSE.Items(i).Value = Val(lstScaleSel(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxScaleSE.Items(i).Selected = True
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

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ELE_QUADRO_SERVIZI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDSE.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.txtQuantitaSE.Text = PAR.IfNull(myReader1("QUANTITA"), "")
                        Me.cmbDifferenzialeSE.Text = PAR.IfNull(myReader1("DIFFERENZIALE"), "")
                        Me.cmbNormaSE.SelectedValue = PAR.IfNull(myReader1("NORMA"), "")
                        Me.txtUbicazioneSE.Text = PAR.IfNull(myReader1("UBICAZIONE"), "")

                    End If
                    myReader1.Close()

                    '*** ELEMENTI SERVITI I_ELE_QUADRO_SE_ELEMENTI
                    '***Azzero la lista delle scale ed elementi serviti
                    For i = 0 To CheckBoxScaleSE.Items.Count - 1
                        CheckBoxScaleSE.Items(i).Selected = False
                    Next

                    '*** check della scala/elemento salvata
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select ID_ELEMENTO from SISCOM_MI.I_ELE_QUADRO_SE_ELEMENTI where  ID_QUADRO_SERVIZI= " & Me.txtIDSE.Text

                    myReader2 = PAR.cmd.ExecuteReader()

                    While myReader2.Read
                        For i = 0 To CheckBoxScaleSE.Items.Count - 1
                            If CheckBoxScaleSE.Items(i).Value = PAR.IfNull(myReader2("ID_ELEMENTO"), "-1") Then
                                CheckBoxScaleSE.Items(i).Selected = True
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

    Protected Sub btnApriScale_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriScale.Click
        Dim i, j As Integer

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareSE.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDSC.Text = lstQuadroSC(txtIdComponente.Text).ID

                    Me.txtQuantitaSC.Text = PAR.IfNull(lstQuadroSC(txtIdComponente.Text).QUANTITA, "")
                    Me.cmbDifferenzialeSC.SelectedValue = PAR.IfNull(lstQuadroSC(txtIdComponente.Text).DIFFERENZIALE, "")
                    Me.cmbNormaSC.SelectedValue = PAR.IfNull(lstQuadroSC(txtIdComponente.Text).NORMA, "")
                    Me.txtUbicazioneSC.Text = PAR.IfNull(lstQuadroSC(txtIdComponente.Text).UBICAZIONE, "")

                    For i = 0 To CheckBoxScaleSC.Items.Count - 1
                        CheckBoxScaleSC.Items(i).Selected = False
                    Next

                    For i = 0 To CheckBoxScaleSC.Items.Count - 1
                        For j = 0 To lstScaleSel2.Count - 1
                            If Val(lstScaleSel2(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxScaleSC.Items(i).Value = Val(lstScaleSel2(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxScaleSC.Items(i).Selected = True
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

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ELE_QUADRO_SCALA where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDSC.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.txtQuantitaSC.Text = PAR.IfNull(myReader1("QUANTITA"), "")
                        Me.cmbDifferenzialeSC.Text = PAR.IfNull(myReader1("DIFFERENZIALE"), "")
                        Me.cmbNormaSC.SelectedValue = PAR.IfNull(myReader1("NORMA"), "")
                        Me.txtUbicazioneSC.Text = PAR.IfNull(myReader1("UBICAZIONE"), "")

                    End If
                    myReader1.Close()

                    '*** ELEMENTI SERVITI I_ELE_QUADRO_SC_ELEMENTI
                    '***Azzero la lista delle scale ed elementi serviti
                    For i = 0 To CheckBoxScaleSC.Items.Count - 1
                        CheckBoxScaleSC.Items(i).Selected = False
                    Next

                    '*** check della scala/elemento salvata
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select ID_ELEMENTO from SISCOM_MI.I_ELE_QUADRO_SC_ELEMENTI where  ID_QUADRO_SCALA= " & Me.txtIDSC.Text

                    myReader2 = PAR.cmd.ExecuteReader()

                    While myReader2.Read
                        For i = 0 To CheckBoxScaleSC.Items.Count - 1
                            If CheckBoxScaleSC.Items(i).Value = PAR.IfNull(myReader2("ID_ELEMENTO"), "-1") Then
                                CheckBoxScaleSC.Items(i).Selected = True
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

            If vIdImpianto <> -1 Then
                PAR.myTrans.Rollback()
            End If

            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub btnEliminaServizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaServizio.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareSE.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstQuadroSE.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Quadro In lstQuadroSE
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridServizio.DataSource = lstQuadroSE
                        DataGridServizio.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ELE_QUADRO_SERVIZI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Servizio()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Quadro Servizi")

                        End If
                    End If

                    txtSelServizio.Text = ""
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

    Protected Sub btnEliminaScale_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaScale.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareSC.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstQuadroSC.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Quadro In lstQuadroSC
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridScala.DataSource = lstQuadroSC
                        DataGridScala.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ELE_QUADRO_SCALA where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Scale()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Quadro Scala")

                        End If
                    End If

                    txtSelScale.Text = ""
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


    Protected Sub btnAggServizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggServizio.Click
        Dim i As Integer

        Try
            Me.txtIDSE.Text = -1

            Me.txtQuantitaSE.Text = 1
            Me.cmbDifferenzialeSE.Text = ""
            Me.cmbNormaSE.Text = ""
            Me.txtUbicazioneSE.Text = ""

            For i = 0 To CheckBoxScaleSE.Items.Count - 1
                CheckBoxScaleSE.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnAggScale_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggScale.Click
        Dim i As Integer

        Try
            Me.txtIDSC.Text = -1

            Me.txtQuantitaSC.Text = 1
            Me.cmbDifferenzialeSC.Text = ""
            Me.cmbNormaSC.Text = ""
            Me.txtUbicazioneSC.Text = ""

            For i = 0 To CheckBoxScaleSC.Items.Count - 1
                CheckBoxScaleSC.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggServizio.Visible = False
            Me.btnEliminaServizio.Visible = False
            Me.btnApriServizio.Visible = False

            Me.btnAggScale.Visible = False
            Me.btnEliminaScale.Visible = False
            Me.btnApriScale.Visible = False

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

End Class
