
Partial Class ANAUT_EmissioneConvocazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        If IsPostBack = False Then
            BindGrid()
        End If
    End Sub

    Public Property IndiceBando() As Long
        Get
            If Not (ViewState("par_IndiceBando") Is Nothing) Then
                Return CLng(ViewState("par_IndiceBando"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IndiceBando") = value
        End Set
    End Property

    Private Sub BindGrid()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT max(id) FROM UTENZA_BANDI WHERE stato=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IndiceBando = myReader(0)
            End If
            myReader.Close()


            Dim Str As String = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DettagliSimulazione.aspx?IDS='||CONVOCAZIONI_AU_GRUPPI.ID||''',''Dettagli'','''');£>'||'DETTAGLI'||'</a>','$','&'),'£','" & Chr(34) & "') as VISUALIZZA,TO_CHAR(TO_DATE(INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS INIZIO,TO_CHAR(TO_DATE(FINE,'yyyymmdd'),'dd/mm/yyyy') AS FINE,CONVOCAZIONI_AU_GRUPPI.*,UTENZA_BANDI.DESCRIZIONE AS NOME FROM UTENZA_BANDI,SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE UTENZA_BANDI.ID=CONVOCAZIONI_AU_GRUPPI.ID_AU AND CONVOCAZIONI_AU_GRUPPI.FL_CONFERMATA=1 AND CONVOCAZIONI_AU_GRUPPI.FL_STAMPATA=0 ORDER BY CONVOCAZIONI_AU_GRUPPI.ID DESC, CONVOCAZIONI_AU_GRUPPI.DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "CONVOCAZIONI_AU_GRUPPI")

            DataGridCapitoli.DataSource = ds
            DataGridCapitoli.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub DataGridCapitoli_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCapitoli.ItemDataBound
        If e.Item.Cells(1).Text <> "DESCRIZIONE" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGridCapitoli_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCapitoli.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridCapitoli.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub


    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        If txtid.Value <> "" Then
            Response.Write("<script>location.href='EmissioneConvocazioni1.aspx?ID=" & txtid.Value & "';</script>")
        Else
            Response.Write("<script>alert('Selezionare un elemento dalla lista!');</script>")
        End If
    End Sub

    Protected Sub ImgAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAnnulla.Click
        If eliminato.Value = "1" Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                Dim MIADATA As String = Format(Now, "yyyyMMddHHmm")
                Dim buono As Boolean = True


                par.cmd.CommandText = "update siscom_mi.CONVOCAZIONI_AU_GRUPPI set fl_confermata=0,ID_OPERATORE_CONFERMA=NULL WHERE ID=" & txtid.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.CONVOCAZIONI_AU_EVENTI WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & txtid.Value & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.AGENDA_APPUNTAMENTI WHERE ID_CONVOCAZIONE IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU WHERE ID_GRUPPO=" & txtid.Value & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.Dispose()
                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');</script>")
                txtid.Value = ""
                eliminato.Value = "0"
                txtmia.Text = "Nessuna Selezione"
                BindGrid()

            Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
        End If
    End Sub
End Class
