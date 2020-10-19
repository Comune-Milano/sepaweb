Imports System.IO

Partial Class AMMSEPA_OperatoreSUA_AggRUAbusivoDaFile
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader

    Public Property DTADAFILE() As Data.DataTable
        Get
            If Not (ViewState("DTADAFILE") Is Nothing) Then
                Return ViewState("DTADAFILE")
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("DTADAFILE") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
    End Sub
    Protected Sub ImgEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub
    Protected Sub btnElabora_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElabora.Click
        Dim nFile As String = ""
        DTADAFILE = New Data.DataTable
        Try
            If FileUploadAbusivi.HasFile = True Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                nFile = Server.MapPath("..\..\FileTemp\") & FileUploadAbusivi.FileName
                FileUploadAbusivi.SaveAs(nFile)
                Dim sr As New StreamReader(nFile)
                CreaDT()
                Dim row As Data.DataRow
                Dim riga As String = sr.ReadLine
                While Not riga Is Nothing
                    row = DTADAFILE.NewRow()
                    row.Item("CODICE") = riga
                    If Len(riga) <> 19 Then
                        row.Item("STATO") = "Codice Contratto non valido."
                        row.Item("SALVA") = 0
                    Else
                        par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO = '" & riga & "'"
                        myReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            row.Item("ID") = par.IfNull(myReader("ID"), 0)
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO = " & par.IfNull(myReader("ID"), 0)
                            myReader2 = par.cmd.ExecuteReader
                            If myReader2.Read Then
                                row.Item("STATO") = "Il contratto è già presente nella lista abusivi."
                                row.Item("SALVA") = 0
                            Else
                                row.Item("STATO") = "OK. Il contratto può essere inserito nella lista."
                                row.Item("SALVA") = 1
                            End If
                            myReader2.Close()
                            par.cmd.CommandText = "SELECT ID_ANAGRAFICA FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND ID_CONTRATTO = " & par.IfNull(myReader("ID"), 0)
                            myReader2 = par.cmd.ExecuteReader
                            If myReader2.Read Then
                                par.cmd.CommandText = "SELECT (CASE WHEN siscom_mi.anagrafica.cognome IS NULL AND siscom_mi.anagrafica.NOME IS NULL THEN siscom_mi.anagrafica.RAGIONE_SOCIALE ELSE siscom_mi.anagrafica.cognome || ' ' ||siscom_mi.anagrafica.NOME END) AS INTESTATARIO FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & par.IfNull(myReader2("ID_ANAGRAFICA"), 0)
                                myReader3 = par.cmd.ExecuteReader
                                If myReader3.Read Then
                                    row.Item("INTESTATARIO") = par.IfNull(myReader3("INTESTATARIO"), "")
                                End If
                                myReader3.Close()
                            End If
                            myReader2.Close()
                        Else
                            row.Item("STATO") = "Il Codice Contratto non esiste."
                            row.Item("SALVA") = 0
                        End If
                        myReader.Close()
                    End If
                    DTADAFILE.Rows.Add(row)
                    riga = sr.ReadLine()
                End While
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                BindGrid()
                dgvruabusividafile.Visible = True
                sr.Close()
            Else
                Response.Write("<script>alert('Selezionare un file da elaborare!');</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvruabusividafile.DataSource = DTADAFILE
            dgvruabusividafile.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CreaDT()
        '######### SVUOTA E CREA COLONNE DATATABLE #########
        DTADAFILE.Clear()
        DTADAFILE.Columns.Clear()
        DTADAFILE.Rows.Clear()
        DTADAFILE.Columns.Add("ID")
        DTADAFILE.Columns.Add("CODICE")
        DTADAFILE.Columns.Add("INTESTATARIO")
        DTADAFILE.Columns.Add("STATO")
        DTADAFILE.Columns.Add("SALVA")
    End Sub
    Protected Sub dgvruabusividafile_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvruabusividafile.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvruabusividafile.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Dim codice As String = ""
        Dim conta As Boolean = False
        If conferma.Value = 1 Then
            If dgvruabusividafile.Visible = False Then
                Response.Write("<script>alert('Elaborare un file prima di procedere con il salvataggio!');</script>")
                Exit Sub
            End If
            Try
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                For Each row As Data.DataRow In DTADAFILE.Rows
                    If CInt(row.Item(4)) = 1 Then
                        codice = CInt(row.Item(0))
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO = " & codice
                        myReader = par.cmd.ExecuteReader
                        If Not myReader.Read Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI (ID_CONTRATTO) VALUES (" & codice & ")"
                            par.cmd.ExecuteNonQuery()
                            conta = True
                        End If
                        myReader.Close()
                    End If
                Next
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                If conta = True Then
                    Response.Write("<script>alert('Operazione Completata!');</script>")
                Else
                    Response.Write("<script>alert('Nessun dato salvato!');</script>")
                End If
                conferma.Value = 0
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)
            End Try
        End If
    End Sub
End Class
