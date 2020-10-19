
Partial Class CENSIMENTO_RisultatiUC
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Dim vEdificio As String
    Dim vIndirizzo As String
    Dim vCivico As String
    Dim vTipologia As String
    Dim RicPer As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then

            Response.Flush()

            vEdificio = Request.QueryString("E")
            vIndirizzo = Request.QueryString("I")
            vCivico = Request.QueryString("CIV")
            RicPer = Request.QueryString("PAS")
            vTipologia = Request.QueryString("TIPOL")
            'LBLID.Text = "-1"
            If RicPer = "ED" Or RicPer = "" Then
                cerca()
            ElseIf RicPer = "COM" Then
                CercaPerComple()
            End If

        End If
    End Sub

    Private Sub CercaPerComple()
        Dim bTrovato As Boolean

        Dim sValore As String
        Dim condizione As String = ""
        Try
            bTrovato = False
            sStringaSql = ""
            sStringaSql = "SELECT ROWNUM, SISCOM_MI.UNITA_COMUNI.ID, UNITA_COMUNI.COD_UNITA_COMUNE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME, (TIPO_UNITA_COMUNE.DESCRIZIONE) AS TIPOLOGIA FROM SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_COMUNI.COD_TIPOLOGIA = TIPO_UNITA_COMUNE.COD "

            If vEdificio <> "" Then
                sValore = vEdificio
                'sValore = Mid(sValore, 1, InStr(sValore, "-") - 1)
                bTrovato = True
                condizione = "AND SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO = '" & par.PulisciStrSql(sValore) & "'"
            End If
            If vIndirizzo <> "" Then

                sValore = vIndirizzo

                condizione = condizione & "AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & par.PulisciStrSql(sValore) & "' "

                If vCivico <> "" Then
                    sValore = vCivico
                    condizione = condizione & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
                End If
                condizione = condizione & ")"


            End If

            If vTipologia <> "" And vTipologia <> "-1" Then
                sValore = vTipologia
                condizione = condizione & "AND UNITA_COMUNI.COD_TIPOLOGIA ='" & sValore & "'"
            End If
            If condizione <> "" Then
                sStringaSql = sStringaSql & condizione
            End If
            sStringaSql = sStringaSql & " ORDER BY ROWNUM ASC"

            QUERY = sStringaSql
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim condizione As String = ""
        Try
            bTrovato = False
            sStringaSql = ""
            sStringaSql = "SELECT ROWNUM, SISCOM_MI.UNITA_COMUNI.ID, UNITA_COMUNI.COD_UNITA_COMUNE, EDIFICI.DENOMINAZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME, (TIPO_UNITA_COMUNE.DESCRIZIONE) AS TIPOLOGIA FROM SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI where SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID(+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_COMUNI.COD_TIPOLOGIA = TIPO_UNITA_COMUNE.COD "


            If vEdificio <> "" Then
                sValore = vEdificio
                'sValore = Mid(sValore, 1, InStr(sValore, "-") - 1)
                bTrovato = True
                condizione = "AND SISCOM_MI.UNITA_COMUNI.ID_EDIFICIO= '" & par.PulisciStrSql(sValore) & "'"
            End If
            If vIndirizzo <> "" Then

                sValore = vIndirizzo

                condizione = condizione & "AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & par.PulisciStrSql(sValore) & "' "

                If vCivico <> "" Then
                    sValore = vCivico
                    condizione = condizione & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
                End If
                condizione = condizione & ")"


            End If
            If vTipologia <> "" And vTipologia <> "-1" Then
                sValore = vTipologia
                condizione = condizione & "AND UNITA_COMUNI.COD_TIPOLOGIA ='" & sValore & "'"
            End If

            If condizione <> "" Then
                sStringaSql = sStringaSql & condizione

            End If
            sStringaSql = sStringaSql & " ORDER BY ROWNUM ASC"
            QUERY = sStringaSql
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


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

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    LBLID.Text = e.Item.Cells(0).Text
    '    Label2.Text = "Hai selezionato: PG " & e.Item.Cells(0).Text

    'End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità comune COD: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità comune COD: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaUC.aspx""</script>")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Response.Redirect("UnitàComEdifici.aspx?C=RisultatiUC&ID=" & txtid.Text & "&COMPEDI=" & Request.QueryString("E") & "&RICPER=" & Request.QueryString("PAS") & "&TIPOL=" & Request.QueryString("TIPOL"))

        End If

    End Sub
End Class
