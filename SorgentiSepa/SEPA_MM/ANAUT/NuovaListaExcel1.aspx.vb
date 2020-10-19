Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_NuovaListaExcel1
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim DT As New Data.DataTable

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

    Private Sub BindGrid()

        Try




            'Dim Str As String = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''DettagliListaConv.aspx?ID='||UTENZA_LISTE.ID||''',''Dettagli'','''');£>'||'DETTAGLI'||'</a>','$','&'),'£','" & Chr(34) & "') as VISUALIZZA,UTENZA_LISTE.* FROM UTENZA_LISTE ORDER BY UTENZA_LISTE.DATA_ORA DESC"
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Str, par.OracleConn)

            'Dim ds As New Data.DataSet()

            Dim dt As New System.Data.DataTable
            dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)

            'da.Fill(ds, "UTENZA_LISTE")

            DataGridCapitoli.DataSource = dt
            DataGridCapitoli.DataBind()
            Label1.Text = dt.Rows.Count & " nella lista"
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        Dim dt As New System.Data.DataTable
        dt = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "insert into UTENZA_LISTE values (SEQ_UTENZA_LISTE.NEXTVAL,NULL,'LISTA CONV. DEL " & Format(Now, "yyyy/MM/dd") & " " & Format(Now, "HH:mm") & "','" & Format(Now, "yyyyMMddHHmm") & "','" & par.PulisciStrSql(Session.Item("OPERATORE")) & "','IMPORT DA FILE " & par.DeCripta(Request.QueryString("F")) & "',0)"
                par.cmd.ExecuteNonQuery()

                Dim S As String = ""
                par.cmd.CommandText = "SELECT SEQ_UTENZA_LISTE.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    S = myReader(0)
                End If
                myReader.Close()

                Dim I As Long = 0
                Dim trovato As Boolean = False
                For Each row In dt.Rows
                    If par.IfNull(dt.Rows(I).Item("ID_LISTA"), "-1") <> "-1" Then
                        par.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET ID_LISTA_CONV=" & S & " where ID_LISTA_CONV IS NULL AND ID_LISTA=" & par.IfNull(dt.Rows(I).Item("ID_LISTA"), "-1") & " AND COD_CONTRATTO='" & par.IfNull(dt.Rows(I).Item("RU"), "-1") & "'"
                        par.cmd.ExecuteNonQuery()
                        trovato = True
                    End If
                    I = I + 1

                Next
                If trovato = True Then
                    par.myTrans.Commit()
                    Response.Write("<script>alert('Operazione effettuata!');location.href = 'GestListeConv.aspx';</script>")
                Else
                    par.myTrans.Rollback()
                    Response.Write("<script>alert('Nessun contratto inserito in lista!');location.href = 'GestListeConv.aspx';</script>")
                End If
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try

        End If

        
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            DT = CType(HttpContext.Current.Session.Item("AA"), Data.DataTable)

            If DT.Rows.Count > 0 Then

                'DT.Columns.Remove("IDC")
                'DT.Columns.Remove("ID_FILIALE")
                'DT.Columns.Remove("ID_SPORTELLO")

                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(DT, DataGridCapitoli, "ExportListaDaXLS", , False, , False)


                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
