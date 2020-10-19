
Partial Class CENSIMENTO_UtMillesimali
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            vId = Request.QueryString("ID")
            Passato = Request.QueryString("Pas")

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            Try
                If vId <> 0 Then
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    Select Case Passato
                        Case "COMP"


                            par.cmd.CommandText = " SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_COMPLESSO = " & vId
                            'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
                            myReader1 = par.cmd.ExecuteReader()
                            cmbTabMillesimale.Items.Add(New ListItem(" ", -1))
                            While myReader1.Read
                                cmbTabMillesimale.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIO"), " "), par.IfNull(myReader1("ID"), -1)))
                            End While
                            myReader1.Close()


                        Case "ED"
                            par.cmd.CommandText = " SELECT ID, (DESCRIZIONE || ' - '|| DESCRIZIONE_TABELLA) as DESCRIZIO  from SISCOM_MI.TABELLE_MILLESIMALI WHERE ID_EDIFICIO = " & vId
                            myReader1 = par.cmd.ExecuteReader()
                            cmbTabMillesimale.Items.Add(New ListItem(" ", -1))
                            While myReader1.Read
                                cmbTabMillesimale.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIO"), " "), par.IfNull(myReader1("ID"), -1)))
                            End While
                            myReader1.Close()
                    End Select
                    par.cmd.CommandText = "Select * from SISCOM_MI.TIPOLOGIA_COSTO"
                    myReader1 = par.cmd.ExecuteReader()
                    cmbTipolCatasto.Items.Add(New ListItem(" ", -1))
                    While myReader1.Read
                        cmbTipolCatasto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
                    End While
                    myReader1.Close()

                End If

                par.OracleConn.Close()

            Catch ex As Exception
                par.OracleConn.Close()

            End Try
        End If

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


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Me.salva()

    End Sub
    Private Sub salva()
        If Me.txtPercRipart.Text <> "" Then
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim inizioid As Integer
            par.OracleConn.Open()
            par.SettaCommand(par)
            inizioid = Mid(vId, 1, 6)
            par.cmd.CommandText = "SELECT COD_TIPOLOGIA_COSTO FROM SISCOM_MI.UTENZE_TABELLE_MILLESIMALI WHERE ID_TABELLA_MILLESIMALE =" & cmbTabMillesimale.SelectedValue & " AND COD_TIPOLOGIA_COSTO = '" & Me.cmbTipolCatasto.SelectedValue & "'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Response.Write("<SCRIPT>alert('Esiste costo già esistente!');</SCRIPT>")
            Else
                myReader1.Close()
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_UTENZE_TABELLE_MILLESIMALI.NEXTVAL FROM DUAL"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    inizioid = inizioid & myReader1(0)
                End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.UTENZE_TABELLE_MILLESIMALI (id_utenza, id_tabella_millesimale, cod_tipologia_costo, perc_ripartizione_costi) values (" & inizioid & ", " & cmbTabMillesimale.SelectedValue.ToString & ", '" & Me.cmbTipolCatasto.SelectedValue.ToString & "', " & par.VirgoleInPunti(Me.txtPercRipart.Text) & ")"
                par.cmd.ExecuteNonQuery()
                BindGrid()
            End If
            par.OracleConn.Close()

        End If
    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()
        Dim StringaSql As String
        Try
            Select Case Passato
                Case "ED"

                    StringaSql = "SELECT ROWNUM, id_utenza,  TIPOLOGIA_COSTO.descrizione AS Descrizione, perc_ripartizione_costi FROM SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_COSTO WHERE TIPOLOGIA_COSTO.cod = UTENZE_TABELLE_MILLESIMALI.cod_tipologia_costo AND id_tabella_millesimale in (select id from SISCOM_MI.tabelle_millesimali where id_edificio= " & vId & ") and id_tabella_millesimale = " & cmbTabMillesimale.SelectedValue & " ORDER BY ROWNUM ASC"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, par.OracleConn)

                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

                    DatGridUtenzaMillesim.DataSource = ds
                    DatGridUtenzaMillesim.DataBind()

                Case "COMP"
                    StringaSql = "SELECT ROWNUM, id_utenza,  TIPOLOGIA_COSTO.descrizione AS Descrizione, perc_ripartizione_costi FROM SISCOM_MI.UTENZE_TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_COSTO WHERE TIPOLOGIA_COSTO.cod = UTENZE_TABELLE_MILLESIMALI.cod_tipologia_costo AND id_tabella_millesimale in (select id from SISCOM_MI.tabelle_millesimali where id_edificio= " & vId & ") and id_tabella_millesimale = " & cmbTabMillesimale.SelectedValue & " ORDER BY ROWNUM ASC"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, par.OracleConn)

                    Dim ds As New Data.DataSet()
                    da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

                    DatGridUtenzaMillesim.DataSource = ds
                    DatGridUtenzaMillesim.DataBind()

            End Select
        Catch ex As Exception
            par.OracleConn.Close()

        End Try
        par.OracleConn.Close()

    End Sub

    Protected Sub DatGridUtenzaMillesim_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DatGridUtenzaMillesim.EditCommand
        LBLDESCRIZIONE.Text = e.Item.Cells(2).Text
        LBLID.Text = e.Item.Cells(1).Text
        Label6.Text = "Hai selezionato la riga N°: " & e.Item.Cells(0).Text

    End Sub

    Protected Sub DatGridUtenzaMillesim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DatGridUtenzaMillesim.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        End If

    End Sub

    Protected Sub DatGridUtenzaMillesim_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DatGridUtenzaMillesim.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DatGridUtenzaMillesim.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub ImButEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImButEsci.Click
        Response.Write("<script language='javascript'> { self.close() }</script>")
    End Sub

    Protected Sub BtnDeleteConsistenza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnDeleteConsistenza.Click
        Me.Cancella()
    End Sub
    Private Sub Cancella()
        Try


            If Me.LBLID.Text <> "" AndAlso Label6.Text <> "Nessuna selezione" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "delete SISCOM_MI.utenze_tabelle_millesimali where id_utenza =" & LBLID.Text
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.OracleConn.Close()
                BindGrid()
                Label6.Text = "Nessuna selezione"


            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmbTabMillesimale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTabMillesimale.SelectedIndexChanged
        BindGrid()

    End Sub
End Class
