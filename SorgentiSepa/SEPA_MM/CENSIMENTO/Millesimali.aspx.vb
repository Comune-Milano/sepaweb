
Partial Class CENSIMENTO_Millesimali
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                vIdEdif = Request.QueryString("IDED")
                vId = Request.QueryString("IDUNI")
                Passato = Request.QueryString("Pas")
                'riempicombo()
                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If
                If Session("LE") = "1" Then
                    FrmSolaLettura()
                End If

            End If
            cerca()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub FrmSolaLettura()
        Try
            Me.BtnADD.Visible = False
            Me.ImageButton1.Visible = False
            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Enabled = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

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
    Public Property vIdEdif() As Long
        Get
            If Not (ViewState("par_lIdEdificio") Is Nothing) Then
                Return CLng(ViewState("par_lIdEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdEdificio") = value
        End Set

    End Property

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
    Private Sub cerca()

        Try


            Select Case Passato
                Case "UNCOM"
                    sStringaSql = "Select rownum, ID_TABELLA as ID, DESCRIZIONE, VALORE_MILLESIMO FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE TABELLE_MILLESIMALI.id = VALORI_MILLESIMALI.id_TABELLA and VALORI_MILLESIMALI.ID_UNITA_COMUNE =" & vId & " ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()
                Case "UNCOMED"
                    sStringaSql = "Select rownum, ID_TABELLA as ID, DESCRIZIONE, VALORE_MILLESIMO FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE TABELLE_MILLESIMALI.id = VALORI_MILLESIMALI.id_TABELLA and VALORI_MILLESIMALI.ID_UNITA_COMUNE =" & vId & " ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

                Case "UI"
                    sStringaSql = "Select rownum, ID_TABELLA as ID, DESCRIZIONE, VALORE_MILLESIMO FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE TABELLE_MILLESIMALI.id = VALORI_MILLESIMALI.id_TABELLA and VALORI_MILLESIMALI.ID_UNITA_IMMOBILIARE =" & vId & " ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()
            End Select
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()

        Try

            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "INTERV_ADEG_NORM")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            par.OracleConn.Close()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()

        End Try
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(0).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(2).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtdesc').value='" & e.Item.Cells(2).Text & "'")

        End If


    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub
    'Private Function riempicombo()
    '    Dim ds As New Data.DataSet
    '    Dim da As Oracle.DataAccess.Client.OracleDataAdapter

    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Response.Write("IMPOSSIBILE VISUALIZZARE")
    '        Exit Function
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    Try
    '        Select Case Passato
    '            Case "UNICOM"
    '                da = New Oracle.DataAccess.Client.OracleDataAdapter(" SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_COMPLESSO = " & vIdEdif, par.OracleConn)
    '                da.Fill(ds)
    '                'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
    '                DrlTabMillesim.DataSource = ds
    '                DrlTabMillesim.DataTextField = "DESCRIZIO"
    '                DrlTabMillesim.DataValueField = "ID"
    '                DrlTabMillesim.DataBind()
    '            Case "UI"
    '                da = New Oracle.DataAccess.Client.OracleDataAdapter(" SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO = " & vIdEdif, par.OracleConn)
    '                da.Fill(ds)
    '                'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
    '                DrlTabMillesim.DataSource = ds
    '                DrlTabMillesim.DataTextField = "DESCRIZIO"
    '                DrlTabMillesim.DataValueField = "ID"
    '                DrlTabMillesim.DataBind()
    '        End Select
    '    Catch ex As Exception
    '        par.OracleConn.Close()
    '    End Try
    '    par.OracleConn.Close()
    'End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")

    End Sub



    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If txtid.Text <> "" Then
            Me.elimina()
            BindGrid()
        Else
            Response.Write("<SCRIPT>alert('Selezionare l'oggetto da eliminare!');</SCRIPT>")
        End If
    End Sub
    Private Function elimina()
        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Function
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If txtid.Text <> "" Then

                Select Case Passato
                    Case "UNICOM"
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.VALORI_MILLESIMALI WHERE ID_TABELLA = " & txtid.Text & " AND ID_UNITA_COMUNE = " & vId & " AND VALORE_MILLESIMO = " & par.VirgoleInPunti(txtdesc.Text)
                        par.cmd.ExecuteNonQuery()
                        'Me.txtmia.Text = ""
                        'Me.txtid.Text = ""
                    Case "UI"
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.VALORI_MILLESIMALI WHERE ID_TABELLA = " & txtid.Text & " AND ID_UNITA_IMMOBILIARE = " & vId & " AND VALORE_MILLESIMO = " & par.VirgoleInPunti(txtdesc.Text)
                        par.cmd.ExecuteNonQuery()
                        'Me.txtmia.Text = ""
                        'Me.txtid.Text = ""

                End Select

            End If
            par.OracleConn.Close()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Function

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        Response.Write("<script>window.open('NumMillesimi.aspx?IDUNI=" & vId & ",&IDED=" & vIdEdif & ",&Pas=" & Passato & "','DATMILLESIMALI', 'resizable=yes, width=620, height=230');</script>")

    End Sub
End Class
