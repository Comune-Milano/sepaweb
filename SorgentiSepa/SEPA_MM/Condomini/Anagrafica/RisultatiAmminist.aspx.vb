
Partial Class Condomini_Anagrafica_RisultatiAmminist
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

        Try
            If Not IsPostBack Then

                Cerca()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub Cerca()
        Try
            Dim bTrovato As Boolean = False
            Dim condizione As String = ""
            Dim sValore As String = ""
            Dim sCompara As String
            Dim Nome As String = Request.QueryString("NOME")
            Dim Cognome As String = Request.QueryString("COGNOME")
            Dim CF As String = Request.QueryString("CF")
            Dim PIVA As String = Request.QueryString("PIVA")
            Dim sStringaSql As String = ""

            If Nome <> "" Then
                sValore = Nome
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & "COND_AMMINISTRATORI.NOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True
            End If

            If Cognome <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = Cognome
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)

                Else
                    sCompara = " = "
                End If
                sStringaSql = sStringaSql & "COND_AMMINISTRATORI.COGNOME" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True

            End If

            If CF <> "" Then
                If CF.Length = 16 Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                    sValore = CF
                    sCompara = " = "
                    sStringaSql = sStringaSql & "COND_AMMINISTRATORI.CF" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                    bTrovato = True

                End If
            End If

            If PIVA <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = PIVA
                sCompara = " = "
                sStringaSql = sStringaSql & "COND_AMMINISTRATORI.PARTITA_IVA" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
                bTrovato = True

            End If

            If bTrovato = True Then
                sStringaSql = "SELECT ID,COGNOME,NOME,CF, PARTITA_IVA FROM SISCOM_MI.COND_AMMINISTRATORI WHERE " & sStringaSql & " ORDER BY COGNOME ASC"
            Else
                sStringaSql = "SELECT ID,COGNOME,NOME,CF, PARTITA_IVA FROM SISCOM_MI.COND_AMMINISTRATORI ORDER BY COGNOME ASC"
            End If


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
            End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(1).Text & "';")

        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If

    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Me.txtid.Value <> "" Then
            Response.Redirect("Inserimento.aspx?ID=" & Me.txtid.Value.ToUpper)
        Else
            Response.Write("<script>alert('Nessun Amministratore Selezionato!');</script>")

        End If

    End Sub
End Class
