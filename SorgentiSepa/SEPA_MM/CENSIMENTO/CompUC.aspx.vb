
Partial Class CENSIMENTO_CompUC
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Dim vIndirizzo As String
    Dim vCivico As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                vEdificio = Request.QueryString("E")
                RicPer = Request.QueryString("PAS")
                'LBLID.Text = "-1"
                If Session("PED2_SOLOLETTURA") = "1" Then
                    Me.btnNuovo.Visible = False
                End If

            End If
            '*******in caso di problemi rimettere sotto il postback
            If RicPer = "ED" Or RicPer = "" Then
                cerca()
            ElseIf RicPer = "COM" Then
                CercaPerComple()
            End If
            If Session.Item("SLE") = 1 Then
                Me.btnNuovo.Visible = False
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Public Property vEdificio() As Long
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
    Public Property RicPer() As String
        Get
            If Not (ViewState("par_ricPer") Is Nothing) Then
                Return CStr(ViewState("par_ricPer"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As string)
            ViewState("par_ricPer") = value
        End Set

    End Property

    Private Sub CercaPerComple()
        Try
            Dim bTrovato As Boolean

            'Dim sValore As String
            Dim condizione As String = ""

            bTrovato = False
            sStringaSql = ""
            sStringaSql = "SELECT ROWNUM, SISCOM_MI.UNITA_COMUNI.ID, UNITA_COMUNI.COD_UNITA_COMUNE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME, (TIPO_UNITA_COMUNE.DESCRIZIONE) AS TIPOLOGIA FROM SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_COMUNI.COD_TIPOLOGIA = TIPO_UNITA_COMUNE.COD AND ID_COMPLESSO =" & vEdificio


            sStringaSql = sStringaSql & " ORDER BY ROWNUM ASC"

            QUERY = sStringaSql
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub cerca()
        Try
            Dim bTrovato As Boolean
            'Dim sValore As String
            Dim condizione As String = ""

            bTrovato = False
            sStringaSql = ""
            sStringaSql = "SELECT ROWNUM, SISCOM_MI.UNITA_COMUNI.ID, UNITA_COMUNI.COD_UNITA_COMUNE, EDIFICI.DENOMINAZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME, (TIPO_UNITA_COMUNE.DESCRIZIONE) AS TIPOLOGIA FROM SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI where SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID(+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_COMUNI.COD_TIPOLOGIA = TIPO_UNITA_COMUNE.COD  and ID_EDIFICIO = " & vEdificio
            sStringaSql = sStringaSql & " ORDER BY ROWNUM ASC"
            QUERY = sStringaSql
            BindGrid()
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
            da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
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

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If


    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Response.Redirect("UnitàComEdifici.aspx?C=CompUC&ID=" & txtid.Text & "&COMPEDI=" & Request.QueryString("E") & "&RICPER=" & Request.QueryString("PAS") & "&COMPLESSO=" & Request.QueryString("COMPLESSO"))
        End If
    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovo.Click
        If RicPer = "COM" Then
            Response.Redirect("UnitàComEdifici.aspx?C=CompUC&RICPER=" & RicPer & "&IDC=" & vEdificio & "&COMPEDI=" & Request.QueryString("E"))
        Else
            Response.Redirect("UnitàComEdifici.aspx?C=CompUC&RICPER=" & RicPer & "&IDED=" & vEdificio & "&COMPEDI=" & Request.QueryString("E"))

        End If

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
        If RicPer = "COM" Then
            Response.Redirect("InserimentoComplessi.aspx?C=CompUC&ID=" & vEdificio & "&E=" & Request.QueryString("COMPEDI") & "&PAS=" & Request.QueryString("RICPER"))

        Else
            Response.Redirect("InserimentoEdifici.aspx?C=CompUC&ID=" & vEdificio & "&E=" & Request.QueryString("COMPEDI") & "&PAS=" & Request.QueryString("RICPER") & "&COMPLESSO=" & Request.QueryString("COMPLESSO"))

        End If

    End Sub
End Class
