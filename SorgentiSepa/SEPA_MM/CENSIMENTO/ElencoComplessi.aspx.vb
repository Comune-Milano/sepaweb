
Partial Class NEW_CENSIMENTO_ElencoComplessi
    Inherits PageSetIdMode
    Dim par As New CM.Global

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
        Try
            If Not IsPostBack Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                If Session("PED2_ESTERNA") = "1" Then
                    par.cmd.CommandText = "SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,COMPLESSI_IMMOBILIARI.COD_COMPLESSO, COMPLESSI_IMMOBILIARI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND LOTTO > 3 AND complessi_immobiliari.ID <> 1 ORDER BY denominazione ASC "
                Else
                    par.cmd.CommandText = "SELECT SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,COMPLESSI_IMMOBILIARI.COD_COMPLESSO, COMPLESSI_IMMOBILIARI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, (COMUNI_NAZIONI.NOME) AS COMUNE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND complessi_immobiliari.ID<>1 ORDER BY denominazione ASC"
                End If
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
                DgvComplessi.DataSource = ds
                DgvComplessi.DataBind()


                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub DgvComplessi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DgvComplessi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Complesso: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtId').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Complesso: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtId').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Not String.IsNullOrEmpty(Me.txtId.Value.ToString) Then
            Session.Add("LE", 1)
            Response.Redirect("InserimentoComplessi.aspx?C=ElencoComplessi&ID=" & txtId.Value)

        End If
    End Sub
End Class
