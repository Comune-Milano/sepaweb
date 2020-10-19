
Partial Class CENSIMENTO_UtenzeMillesimali
    Inherits PageSetIdMode
    Dim par As New CM.Global
  


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then

                vId = Request.QueryString("ID")
                Passato = Request.QueryString("Pas")
                vIdUtenza = Request.QueryString("IDUTENZA")
                vTipoApertura = Request.QueryString("APERTURA")



                'Apro la connsessione con il DB
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UTENZA"
                myReader1 = par.cmd.ExecuteReader()
                cmbTipologiaUtenza.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbTipologiaUtenza.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA_FORNITORI"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                cmbFornitore.Items.Add(New ListItem(" ", -1))

                While myReader.Read
                    cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If

                If vIdUtenza <> 0 Then
                    ApriVisualizzazione()
                End If


            End If
            If vIdUtenza <> 0 Then
                BindGrid()
            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try


    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.ImageButton1.Visible = False
            Me.btnAddUtMill.Visible = False
            Me.BtnDeleteUtMil.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
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
    Private Property vIdUtenza() As Long
        Get
            If Not (ViewState("par_vIdUtenza") Is Nothing) Then
                Return CLng(ViewState("par_vIdUtenza"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdUtenza") = value
        End Set

    End Property
    Private Property vTipoApertura() As String
        Get
            If Not (ViewState("par_vTipoApertura") Is Nothing) Then
                Return CStr(ViewState("par_vTipoApertura"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vTipoApertura") = value
        End Set

    End Property


    Protected Sub btnAddUtMill_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAddUtMill.Click
        If Me.cmbTipologiaUtenza.SelectedValue <> "-1" AndAlso Me.txtContratto.Text <> "" AndAlso Me.txtDescrizione.Text <> "" AndAlso Me.cmbFornitore.SelectedValue.ToString <> "-1" AndAlso Me.txtContratto.Text <> "" Then
            Try

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans




                Dim IDUTENZA As String
                If vIdUtenza = 0 Then
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    IDUTENZA = Mid(vId, 1, 1)
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_UTENZE.NEXTVAL FROM DUAL"
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then

                        IDUTENZA = IDUTENZA & myReader1(0)
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.UTENZE (ID, COD_TIPOLOGIA, ID_FORNITORE, CONTATORE, CONTRATTO, DESCRIZIONE) VALUES (" & IDUTENZA & ", '" & Me.cmbTipologiaUtenza.SelectedValue.ToString & "', '" & Me.cmbFornitore.SelectedValue.ToString & "', '" & par.PulisciStrSql(Me.txtContatore.Text) & "', '" & par.PulisciStrSql(Me.txtContratto.Text) & "', '" & par.PulisciStrSql(Me.txtDescrizione.Text) & "')"
                        par.cmd.ExecuteNonQuery()

                        Select Case Passato
                            Case "CO"
                                '****************MYEVENT*****************
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_COMPLESSI (ID_COMPLESSO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','INSERIMENTO UTENZE MILLESIMALI SULLE TABELLE MILLESIMALI DEL COMPLESSO')"
                                par.cmd.ExecuteNonQuery()
                                '*******************************FINE****************************************
                                '***************************************************************************

                            Case "ED"
                                '****************MYEVENT*****************
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIo,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','INSERIMENTO UTENZE MILLESIMALI SULLE TABELLE MILLESIMALI DEL COMPLESSO')"
                                par.cmd.ExecuteNonQuery()
                                '*******************************FINE****************************************
                                '***************************************************************************

                        End Select

                        vIdUtenza = IDUTENZA
                    End If
                Else
                    IDUTENZA = vIdUtenza

                End If
                If vTipoApertura <> "UPDATE" Then


                    Me.cmbTipologiaUtenza.Enabled = False
                    Me.cmbFornitore.Enabled = False
                    Me.txtContatore.Enabled = False
                    Me.txtContratto.Enabled = False
                    Me.txtDescrizione.Enabled = False

                End If
                Response.Write("<script>window.open('DatiUTMillesimale.aspx?ID=" & vId & ",&IDUTENZA=" & IDUTENZA & ",&Pas=" & Passato & "','DatiUtMilles', 'resizable=no, width=630, height=200');</script>")

            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            End Try

        Else
            Response.Write("<SCRIPT>alert('E\' necessario definire prima l\' utenza millesimale, e poi i dati relativi all\'utenza!');</SCRIPT>")

        End If

    End Sub


    Private Sub BindGrid()

        Try
            'par.OracleConn.Open()
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim StringaSql As String

            StringaSql = "SELECT ROWNUM, id_utenza, TABELLE_MILLESIMALI.DESCRIZIONE_TABELLA ,   TIPOLOGIA_COSTO.descrizione AS Descrizione, perc_ripartizione_costi, ID_TABELLA_MILLESIMALE, COD_TIPOLOGIA_COSTO FROM SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_COSTO  WHERE ID_UTENZA = " & vIdUtenza & " AND TIPOLOGIA_COSTO.cod=UTENZE_TABELLE_MILLESIMALI.cod_Tipologia_costo AND ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID ORDER BY ROWNUM ASC"

            par.cmd.CommandText = StringaSql

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

            DatGridUtenzaMillesim.DataSource = ds
            DatGridUtenzaMillesim.DataBind()
            'par.OracleConn.Close()




        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub ImButEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImButEsci.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans
        par.myTrans.Rollback()
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub
    Protected Sub DatGridUtenzaMillesim_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DatGridUtenzaMillesim.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(1).Text
        LBLID.Text = e.Item.Cells(2).Text
        Label6.Text = "Hai selezionato la riga N°: " & e.Item.Cells(0).Text

    End Sub


    Protected Sub DatGridUtenzaMillesim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DatGridUtenzaMillesim.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            'e.Item.Attributes.Add("onclick", "alert('" & e.Item.Cells(1).Text & "')")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtdesccost').value='" & e.Item.Cells(3).Text & "'")

            'e.Item.Attributes.Add("onclick", MsgBox("ciao"))
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         +
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(0).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtdesccost').value='" & e.Item.Cells(3).Text & "'")

        End If
    End Sub

    Protected Sub DatGridUtenzaMillesim_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DatGridUtenzaMillesim.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DatGridUtenzaMillesim.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub
    Private Sub ApriVisualizzazione()
        Try


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            If vIdUtenza <> 0 Then
                Dim StringaSql As String
                StringaSql = "SELECT * FROM SISCOM_MI.UTENZE WHERE UTENZE.ID = " & vIdUtenza
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = StringaSql
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    cmbTipologiaUtenza.SelectedValue = myReader1("COD_TIPOLOGIA")
                    Me.cmbFornitore.SelectedValue = par.IfNull(myReader1("ID_FORNITORE"), " ")
                    Me.txtDescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), " ")
                    Me.txtContratto.Text = par.IfNull(myReader1("CONTRATTO"), "")
                    Me.txtContatore.Text = par.IfNull(myReader1("CONTATORE"), "")
                End If
                myReader1.Close()


            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try
        'N()


    End Sub
    'Private Sub ApriVisualizzazione()
    '    If vIdUtenza <> 0 Then
    '        Try

    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '            par.cmd.CommandText = "SELECT COD_TIPOLOGI, FORNITOR, CONTATORE, CONTRATTO, DESCRIZIONE WHERE ID= " & vIdUtenza




    '            par.OracleConn.Close()

    '        Catch ex As Exception
    '            Response.Write(ex.Message)

    '        End Try

    '    End If
    'End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        If vTipoApertura = "UPDATE" AndAlso Me.DatGridUtenzaMillesim.Items.Count > 0 Then
            par.cmd.CommandText = "UPDATE SISCOM_MI.UTENZE SET COD_TIPOLOGIA = '" & Me.cmbTipologiaUtenza.SelectedValue.ToString & "', ID_FORNITORE = '" & Me.cmbFornitore.SelectedValue.ToString & "',CONTATORE =  '" & par.PulisciStrSql(Me.txtContatore.Text) & "', CONTRATTO = '" & par.PulisciStrSql(Me.txtContratto.Text) & "', DESCRIZIONE ='" & par.PulisciStrSql(Me.txtDescrizione.Text) & "' WHERE ID = " & vIdUtenza
            par.cmd.ExecuteNonQuery()
            Select Case Passato
                Case "CO"
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_COMPLESSI (ID_COMPLESSO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','MODIFICA UTENZE MILLESIMALI')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************
                Case "ED"
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','MODIFICA UTENZE MILLESIMALI')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

            End Select
            Session.Item("MODIFICASOTTOFORM") = 1
        ElseIf Me.DatGridUtenzaMillesim.Items.Count = 0 Then
            par.cmd.CommandText = "delete from SISCOM_MI.utenze where id = " & vIdUtenza
            par.cmd.ExecuteNonQuery()
            Response.Write("<SCRIPT>alert('Nono sono stati inseriti i dati relativi all\'utenza millesimale!\r\nNon verrà salvata alcuna informazione.');</SCRIPT>")
            Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
            Exit Sub
        Else
            Session.Item("MODIFICASOTTOFORM") = 1

        End If
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub

    Protected Sub BtnDeleteUtMil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnDeleteUtMil.Click
        If txtid.Text <> " " Then
            Try

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "DELETE SISCOM_MI.UTENZE_TABELLE_MILLESIMALI WHERE ID_UTENZA = " & txtid.Text & " AND ID_TABELLA_MILLESIMALE = " & txtdesc.Text & "and cod_tipologia_costo = '" & txtdesccost.Text & "'"
                par.cmd.ExecuteNonQuery()
                Select Case Passato
                    Case "CO"
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_COMPLESSI (ID_COMPLESSO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLAZIONE DATI UTENZA MILLESIMALE')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************
                    Case "ED"
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI (ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLAZIONE DATI UTENZA MILLESIMALE')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************

                End Select
                BindGrid()
                txtmia.Text = ""
                txtid.Text = ""
            Catch ex As Exception
                Me.LblErrore.Visible = True
                LblErrore.Text = ex.Message
                par.myTrans.Rollback()
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

            End Try

        End If
    End Sub


End Class
