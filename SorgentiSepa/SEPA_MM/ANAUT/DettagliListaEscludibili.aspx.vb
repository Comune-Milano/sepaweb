Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_DettagliListaEscludibili
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable
    Dim INDICEBANDO As Long = 0
    Dim DataInzio As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CaricaDatiLista()
            CaricaDati()
        End If
    End Sub

    Private Function CaricaDatiLista()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_LISTE_CONV WHERE id=" & Request.QueryString("ID")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                Label1.Text = PAR.IfNull(myReader("criteri"), "")
                Label3.Text = "LISTA " & PAR.IfNull(myReader("DESCRIZIONE"), "")
            End If
            myReader.Close()

            PAR.RiempiDList(Me, PAR.OracleConn, "cmbMotivi", "SELECT * FROM UTENZA_LISTE_MOT_ESC ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "ID")


            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Function

    Private Function CaricaDati()
        Try
            Dim S As String = ""
            Dim S1 As String = ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim S2 As String = ""



            Tabella = "SELECT * FROM UTENZA_LISTE_CDETT WHERE ID_LISTA=" & Request.QueryString("ID") & " and MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV IS NULL ORDER BY FILIALE,SEDE,INTESTATARIO"
            PAR.cmd.CommandText = Tabella
            da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
            da.Fill(DT)


            DataGridRateEmesse.DataSource = DT
            DataGridRateEmesse.DataBind()
            Session.Add("MIADT", DT)
            Label2.Text = DT.Rows.Count & " nella lista"
        Catch ex As Exception

        End Try
    End Function

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            Dim dt As New System.Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim I As Long = 0
            Dim Trovato As Boolean = False

            For Each oDataGridItem In Me.DataGridRateEmesse.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    Trovato = True
                    PAR.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET MOTIVAZIONE='" & PAR.PulisciStrSql(cmbMotivi.SelectedItem.Text) & "'  WHERE ID_LISTA=" & PAR.IfNull(dt.Rows(I).Item("ID_LISTA"), "-1") & " AND ID_CONTRATTO=" & PAR.IfNull(dt.Rows(I).Item("ID_CONTRATTO"), "-1")
                    PAR.cmd.ExecuteNonQuery()

                End If
                I = I + 1
            Next



            PAR.myTrans.Commit()
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If Trovato = True Then
                Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")
            Else
                Response.Write("<script>alert('Selezionare almeno un contratto dalla lista!');</script>")
            End If
        Catch ex As Exception
            PAR.myTrans.Rollback()
            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Sub

    Protected Sub btnSalva0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva0.Click
        Try
            Dim errore As Boolean = False
            Dim sErrore As String = ""
            Dim sContenutoRiga As String = ""
            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim i As Integer = 0

            


            If FileUpload1.HasFile = True Then
                If UCase(Mid(FileUpload1.FileName, Len(FileUpload1.FileName) - 2, 3)) <> "TXT" Then
                    sErrore = sErrore & "Errore: Tipo file non valido! E' richiesto un file .txt"
                    errore = True
                End If

                If errore = False Then
                    Dim dt As New System.Data.DataTable
                    dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

                    Dim NomeFile As String = Server.MapPath("..\FileTemp\" & FileUpload1.FileName)
                    FileUpload1.SaveAs(NomeFile)
                    Dim sr1 As StreamReader = New StreamReader(NomeFile, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    Do While sr1.Peek() >= 0
                        sContenutoRiga = sr1.ReadLine()
                        i = 0
                        If sContenutoRiga <> "" Then
                            For Each oDataGridItem In Me.DataGridRateEmesse.Items
                                If UCase(PAR.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "-1")) = UCase(sContenutoRiga) Then
                                    chkExport = oDataGridItem.FindControl("ChSelezionato")
                                    chkExport.Checked = True
                                    Exit For
                                End If
                                i = i + 1
                            Next
                        End If
                    Loop
                    sr1.Close()
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = sErrore
                End If
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
