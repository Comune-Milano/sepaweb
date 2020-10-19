
Partial Class VSA_ElencoEliminati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                BindGrid()

                If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                    sololettura.Value = "1"
                    'ImgModifica.Visible = False
                Else
                    sololettura.Value = "0"
                    'ImgModifica.Visible = True
                End If

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Private Sub BindGrid()
        Try
            Dim codContratto As String = ""
            Dim idDich As Long = 0
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID=" & Request.QueryString("IDDOM")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                codContratto = par.IfNull(myReader("CONTRATTO_NUM"), "")
                idDich = par.IfNull(myReader("ID_DICHIARAZIONE"), "")
            End If
            myReader.Close()


            Dim Str As String = ""
            Str = "select comp_nucleo_cancell.*,(CASE WHEN comp_nucleo_cancell.ID_MOTIVO = 1 THEN 'DECESSO' WHEN comp_nucleo_cancell.ID_MOTIVO = 2 THEN 'SEPARAZIONE, NULLITA'', SCIOGLIMENTO MATRIMONIO' WHEN comp_nucleo_cancell.ID_MOTIVO = 3 THEN 'TRASFERIMENTO' WHEN comp_nucleo_cancell.ID_MOTIVO = 4 THEN 'ALTRO' END) AS MOTIVO_USC,TO_CHAR(TO_DATE(COMP_NUCLEO_CANCELL.DATA_USCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_USC from comp_nucleo_cancell,dichiarazioni_vsa " _
                & "where dichiarazioni_vsa.id = comp_nucleo_cancell.id_dichiarazione and id_dichiarazione=" & idDich & " ORDER BY ID_DICHIARAZIONE ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridIntLegali.DataSource = dt
            DataGridIntLegali.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub



    'Protected Sub DataGridIntLegali_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIntLegali.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    'End Sub

    
End Class
