
Partial Class CENSIMENTO_ListaUtenze
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
                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If

            End If
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub FrmSolaLettura()
        Me.ImageButton1.Visible = False
        Me.BtnDeleteConsistenza.Visible = False

        'Dim CTRL As Control = Nothing
        'For Each CTRL In Me.form1.Controls
        '    If TypeOf CTRL Is TextBox Then
        '        DirectCast(CTRL, TextBox).Enabled = False
        '    ElseIf TypeOf CTRL Is DropDownList Then
        '        DirectCast(CTRL, DropDownList).Enabled = False
        '    End If
        'Next
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
    Private Sub BindGrid()
        Try

            If vId <> 0 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Dim StringaSql As String
                Select Case Passato
                    Case "COMP"

                        StringaSql = " SELECT  DISTINCT utenze.id, utenze.cod_tipologia,  ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE  FROM SISCOM_MI.ANAGRAFICA_FORNITORI, SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID  AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE AND TABELLE_MILLESIMALI.ID_COMPLESSO = " & vId & ""
                        par.cmd.CommandText = StringaSql
                        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                        Dim ds As New Data.DataSet()
                        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

                        DatGridUtenzaMillesim.DataSource = ds
                        DatGridUtenzaMillesim.DataBind()

                    Case "ED"

                        StringaSql = " SELECT DISTINCT utenze.id, utenze.cod_tipologia, ANAGRAFICA_FORNITORI.DESCRIZIONE AS FORNITORE, utenze.contatore, UTENZE.CONTRATTO,UTENZE.DESCRIZIONE FROM SISCOM_MI.ANAGRAFICA_FORNITORI,SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.UTENZE WHERE UTENZE_TABELLE_MILLESIMALI.ID_TABELLA_MILLESIMALE = TABELLE_MILLESIMALI.ID  AND UTENZE.ID = UTENZE_TABELLE_MILLESIMALI.ID_UTENZA AND ANAGRAFICA_FORNITORI.ID= UTENZE.ID_FORNITORE AND TABELLE_MILLESIMALI.ID_EDIFICIO = " & vId & " "
                        par.cmd.CommandText = StringaSql

                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                        Dim ds As New Data.DataSet()
                        da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

                        DatGridUtenzaMillesim.DataSource = ds
                        DatGridUtenzaMillesim.DataBind()

                End Select
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub DatGridUtenzaMillesim_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DatGridUtenzaMillesim.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(2).Text
        LBLID.Text = e.Item.Cells(0).Text
        Label6.Text = "Hai selezionato l'utenza con il contratto: " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DatGridUtenzaMillesim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DatGridUtenzaMillesim.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il contratto " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il contratto " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(2).Text & "'")

        End If

    End Sub

    Protected Sub DatGridUtenzaMillesim_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DatGridUtenzaMillesim.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DatGridUtenzaMillesim.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub ImButEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImButEsci.Click
        'Response.Write("<script language='javascript'> { self.close() }</script>")
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script>window.open('UtenzeMillesimali.aspx?ID=" & vId & ",&Pas=" & Passato & "','DETTUTMILLES', 'resizable=no, width=630, height=500');</script>")

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text <> "Label" And txtid.Text <> "" Then
            Dim IDUTENZA As Integer
            IDUTENZA = txtid.Text
            Response.Write("<script>window.open('UtenzeMillesimali.aspx?ID=" & vId & ",&IDUTENZA=" & IDUTENZA & ",&Pas=" & Passato & "&APERTURA=UPDATE','DETTUTMILLES', 'resizable=no, width=630, height=500');</script>")
        Else
            Response.Write("<SCRIPT>alert('Selezionare una utenza!');</SCRIPT>")

        End If

    End Sub

    Protected Sub BtnDeleteConsistenza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnDeleteConsistenza.Click
        Try
            If Me.txtid.Text <> "Label" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "delete from SISCOM_MI.utenze where id = " & txtid.Text
                par.cmd.ExecuteNonQuery()
                Session.Item("MODIFICASOTTOFORM") = 1
                Me.txtmia.Text = ""
                txtid.Text = ""
                BindGrid()
            Else
                Response.Write("<SCRIPT>alert('Selezionare una utenza!');</SCRIPT>")

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub

End Class
