
Partial Class CENSIMENTO_Tab_ValoriMilleismalil
    Inherits UserControlSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global

    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                vId = CType(Me.Page, Object).vId
                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If
                If Session("SLE") = "1" Then
                    FrmSolaLettura()
                End If
                RiempiCampi()
                txtValore.Attributes.Add("onkeyUp", "javascript:valid(this,'notnumbers');")
                txtValore.Attributes.Add("onBlur", "javascript:AutoDecimal(this);")

            End If
            '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
            If vId = 0 Then
                vId = CType(Me.Page, Object).vId
            End If
            '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************

            cerca()


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub
    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
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
    Private Sub FrmSolaLettura()
        Try
            Me.BtnElimina.Visible = False
            Me.btnModifica.Visible = False
            'Me.BtnADD.Visible = False
            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Enabled = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub cerca()
        Try


            'Select Case Me.Page.Title
            '    Case "Inserimento Unita Comuni"
            '        If CType(Me.Page, Object).DaPassare = "UNCOM" Then
            '            sStringaSql = "Select rownum, ID_TABELLA as ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIONE, VALORE_MILLESIMO FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE TABELLE_MILLESIMALI.id = VALORI_MILLESIMALI.id_TABELLA and VALORI_MILLESIMALI.ID_UNITA_COMUNE =" & vId & " ORDER BY ROWNUM ASC"
            '        Else

            '            sStringaSql = "Select rownum, ID_TABELLA as ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIONE, VALORE_MILLESIMO FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE TABELLE_MILLESIMALI.id = VALORI_MILLESIMALI.id_TABELLA and VALORI_MILLESIMALI.ID_UNITA_COMUNE =" & vId & " ORDER BY ROWNUM ASC"
            '        End If

            '        QUERY = sStringaSql
            '        BindGrid()

            If Me.Page.Title = "Unita Immobiliari" Then

                sStringaSql = "Select rownum, ID_TABELLA as ID,(DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIONE, VALORE_MILLESIMO FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE TABELLE_MILLESIMALI.id = VALORI_MILLESIMALI.id_TABELLA and VALORI_MILLESIMALI.ID_UNITA_IMMOBILIARE =" & vId & " ORDER BY ROWNUM ASC"
                QUERY = sStringaSql
                BindGrid()
            End If

            'End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()
        Try

            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If
            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "INTERV_ADEG_NORM")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub
    Private Sub RiempiCampi()
        Try
            If Me.Page.Title = "Unita Immobiliari" Then

                Dim apertoOra As Boolean = False

                If Session("SLE") = "1" Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    apertoOra = True
                Else

                    If Not IsNothing(HttpContext.Current.Session.Item("CONNESSIONE")) AndAlso Not IsNothing(HttpContext.Current.Session.Item("TRANSAZIONE")) Then

                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    Else
                        'Apro la connsessione con il DB
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                        apertoOra = True
                    End If
                End If

                par.cmd.CommandText = "SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO = " & CType(Me.Page, Object).vIdEdificio
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Me.DrLMillesimi.Items.Add(New ListItem(" ", -1))
                While myReader.Read
                    DrLMillesimi.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIO"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()

                par.cmd.CommandText = "SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO from SISCOM_MI.TABELLE_MILLESIMALI WHERE  id_complesso = (select id_complesso from siscom_mi.edifici where id = " & CType(Me.Page, Object).vIdEdificio & ")"
                myReader = par.cmd.ExecuteReader()
                Me.DrLMillesimiComp.Items.Add(New ListItem(" ", -1))
                While myReader.Read
                    DrLMillesimiComp.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIO"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()

                If apertoOra = True Then
                    par.OracleConn.Close()
                End If

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_ValoriMillesimali_SubRimepicampi" & ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ValoriMilleismalil1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ValoriMilleismalil1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ValoriMilleismalil1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ValoriMilleismalil1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Me.HFtxtId.Value = 0
        TextBox2.Value = 0
    End Sub

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click

        If Me.txtValore.Text <> "" AndAlso Not String.IsNullOrEmpty(Me.txtValore.Text) AndAlso CLng(Me.txtValore.Text) <= 1000 Then

            If Me.HFtxtId.Value = 0 Then
                If Me.DrLMillesimi.SelectedValue <> -1 And Me.DrLMillesimiComp.SelectedValue <> -1 Then
                    Response.Write("<script>alert('E\' possibile selezionare una tabella alla volta!');</script>")
                    Exit Sub
                Else
                    Salva()

                End If
            Else
                Update()
            End If
            cerca()
            Me.txtValore.Text = ""
            Me.DrLMillesimi.SelectedValue = "-1"
            Me.DrLMillesimiComp.SelectedValue = "-1"
            Me.TextBox2.Value = 0
        Else
            Response.Write("<script>alert('Inserire un valore coerente!');</script>")

        End If

    End Sub
    Private Sub Salva()
        Try
            Dim apertoOra As Boolean = False
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim Tabella As String

            If Me.DrLMillesimi.SelectedValue <> -1 Then
                Tabella = Me.DrLMillesimi.SelectedValue
            Else
                Tabella = Me.DrLMillesimiComp.SelectedValue
            End If

            If par.IfEmpty(Me.txtValore.Text, "null") <> "null" AndAlso (Tabella <> "-1" Or Tabella <> "") Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.VALORI_MILLESIMALI WHERE ID_TABELLA = " & Tabella & " AND ID_UNITA_IMMOBILIARE = " & vId
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Response.Write("<SCRIPT>alert('Esiste già un valore millesimale con questa tabella!');</SCRIPT>")
                    Exit Sub
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.VALORI_MILLESIMALI (ID_TABELLA, VALORE_MILLESIMO, ID_UNITA_IMMOBILIARE) VALUES (" & Tabella & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtValore.Text), "Null") & ", " & vId & ")"
                par.cmd.ExecuteNonQuery()
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1

            Else
                Response.Write("<SCRIPT>alert('Inserire il valore!');</SCRIPT>")
            End If
            cerca()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_ValoriMillesimali_SubRimepicampi" & ex.Message
        End Try
    End Sub
    Private Sub Update()
        Dim nuovoVal As String
        If Me.DrLMillesimi.enabled = True Then
            nuovoVal = Me.DrLMillesimi.SelectedValue
        Else
            nuovoVal = Me.DrLMillesimiComp.SelectedValue

        End If
        If nuovoVal <> vIdTipologia Then
            par.cmd.CommandText = "SELECT * FROM VALORI_MILLESIMALI WHERE ID_TABELLA = " & nuovoVal & " AND ID_UNITA_IMMOBILIARE = " & vId
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Response.Write("<SCRIPT>alert('Esiste già un valore millesimale con questa tabella!');</SCRIPT>")
                Exit Sub
            End If
        Else

            par.cmd.CommandText = "UPDATE SISCOM_MI.VALORI_MILLESIMALI SET VALORE_MILLESIMO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtValore.Text), "Null") & ", ID_TABELLA= " & nuovoVal & " WHERE ID_TABELLA = " & nuovoVal & " AND ID_UNITA_IMMOBILIARE = " & vId
            par.cmd.ExecuteNonQuery()
            DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If

    End Sub

    Protected Sub btnModifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModifica.Click
        If Me.HFtxtId.Value <> 0 Then
            ApriVisualizzazione()
        Else
            Me.TextBox2.Value = 0
            Response.Write("<script>alert('Selezionare una riga!');</script>")
        End If
    End Sub
    Public Property vIdTipologia() As String
        Get
            If Not (ViewState("par_IdTipologia") Is Nothing) Then
                Return CStr(ViewState("par_IdTipologia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdTipologia") = value
        End Set
    End Property


    Private Sub ApriVisualizzazione()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT Valori_millesimali.*,id_edificio,id_complesso FROM SISCOM_MI.VALORI_MILLESIMALI,siscom_mi.tabelle_millesimali WHERE ID_TABELLA = " & HFtxtId.Value & " AND ID_UNITA_IMMOBILIARE = " & vId & " and tabelle_millesimali.id = id_tabella"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Try


                    If par.IfNull(myReader("ID_EDIFICIO"), 0) <> 0 Then
                        Me.DrLMillesimi.SelectedValue = par.IfNull(myReader("ID_TABELLA"), "-1")
                        Me.DrLMillesimiComp.Enabled = False
                        vIdTipologia = Me.DrLMillesimi.SelectedValue
                    Else
                        Me.DrLMillesimiComp.SelectedValue = par.IfNull(myReader("ID_TABELLA"), "-1")
                        Me.DrLMillesimi.Enabled = False
                        vIdTipologia = Me.DrLMillesimiComp.SelectedValue
                    End If
                Catch ex As Exception
                    Response.Write("<SCRIPT>alert('Il millesimo risulta legato ad una tabella di un altro Edificio o Complesso!Verificare la coerenza dei dati!');</SCRIPT>")
                    Me.txtValore.Text = ""
                    Me.DrLMillesimi.SelectedValue = "-1"
                    Me.DrLMillesimiComp.SelectedValue = "-1"
                    Me.TextBox2.Value = 0
                    Exit Sub
                End Try

                Me.txtValore.Text = par.PuntiInVirgole(par.IfNull((myReader("VALORE_MILLESIMO")), ""))
                TextBox2.Value = 2

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubApriVisualizzazione" & ex.Message
        End Try

    End Sub

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If Me.HFtxtId.Value <> 0 Then
                If txtConfElimina.Value = 1 Then

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.VALORI_MILLESIMALI WHERE ID_TABELLA = " & Me.HFtxtId.Value & " AND ID_UNITA_IMMOBILIARE = " & vId
                    par.cmd.ExecuteNonQuery()
                    cerca()
                    Me.DrLMillesimiComp.SelectedValue = "-1"
                    Me.DrLMillesimi.SelectedValue = "-1"
                    Me.txtValore.Text = ""
                    Me.TextBox2.Value = 0
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")
                    DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                Else
                    Me.HFtxtId.Value = 0
                    txtConfElimina.Value = 0
                End If

            Else
                Response.Write("<script>alert('Selezionare una riga!');</script>")
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = "Tab_Millesimali_SubDelete" & ex.Message
        End Try
    End Sub
End Class
