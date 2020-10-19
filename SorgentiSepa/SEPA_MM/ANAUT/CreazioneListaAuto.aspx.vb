
Partial Class ANAUT_CreazioneListaAuto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
                  & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
                  & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
                  & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
                  & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
                  & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
                  & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
                  & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Protected Sub btnDeselezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = False
        Next
    End Sub

    Protected Sub btnSelezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = True
        Next
    End Sub

    Private Function Cerca()

        Try
            'Dim IDAU As Long

            'par.OracleConn.Open()
            'par.SettaCommand(par)

            sStringaSQL1 = "SELECT UTENZA_GRUPPI_CONV.ID,UTENZA_GRUPPI_CONV.DESCRIZIONE AS NOME_GRUPPO,UTENZA_BANDI.DESCRIZIONE AS ANAGRAFE,CRITERI,'' AS VISUALIZZA FROM UTENZA_GRUPPI_CONV,UTENZA_BANDI WHERE UTENZA_BANDI.ID=UTENZA_GRUPPI_CONV.ID_AU AND UTENZA_GRUPPI_CONV.ID IN (SELECT NVL(ID_GRUPPO_CONV,'0') FROM UTENZA_LISTE_CDETT WHERE ID_GRUPPO_CONV IS NOT NULL AND MOTIVAZIONE IS NULL AND ID_LISTA_CONV IS NULL)  ORDER BY UTENZA_GRUPPI_CONV.DESCRIZIONE ASC"
           
            BindGrid()

        Catch ex As Exception
            ' par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Sub BindGrid()



        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_GRUPPI_CONV")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub


    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim scriptblock As String = ""
        Dim Elenco As String = ""


        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            If chkExport.Checked Then
                Elenco = Elenco & oDataGridItem.Cells(1).Text & ","
            End If
        Next

        If Elenco <> "" Then
            Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"


            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "insert into UTENZA_LISTE values (SEQ_UTENZA_LISTE.NEXTVAL,NULL,'LISTA CONV. DEL " & Format(Now, "yyyy/MM/dd") & " " & Format(Now, "HH:mm") & "','" & Format(Now, "yyyyMMddHHmm") & "','" & par.PulisciStrSql(Session.Item("OPERATORE")) & "','AUTOMATICO',0)"
                par.cmd.ExecuteNonQuery()

                Dim S As String = ""
                par.cmd.CommandText = "SELECT SEQ_UTENZA_LISTE.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    S = myReader(0)
                End If
                myReader.Close()


                par.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET ID_LISTA_CONV=" & S & " where ID_LISTA_CONV IS NULL AND ID_GRUPPO_CONV IN " & Elenco
                par.cmd.ExecuteNonQuery()


                par.myTrans.Commit()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');location.href = 'GestListeConv.aspx';</script>")


            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try

        Else

            Response.Write("<script>alert('Selezionare almeno un gruppo!');</script>")
        End If
    End Sub
End Class
