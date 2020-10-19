Imports System.IO
Imports SubSystems.RP

Partial Class ANAUT_VisModello
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
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

                Dim BaseFile As String = "MODELLO_" & Format(Now, "yyyyMMddHHmmss")
                Dim file1 As String = BaseFile & ".RTF"
                Dim fileName As String = Server.MapPath("..\FileTemp\") & file1
                Dim fileNamePDF As String = Server.MapPath("..\FileTemp\") & BaseFile & ".pdf"
                Dim trovato As Boolean = False

                par.cmd.CommandText = "SELECT * FROM UTENZA_TIPO_DOC WHERE id=" & Request.QueryString("ID")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Dim bw As BinaryWriter
                    If par.IfNull(myReader("MODELLO"), "").LENGTH > 0 Then
                        Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)
                        bw = New BinaryWriter(fs)
                        bw.Write(myReader("MODELLO"))
                        bw.Flush()
                        bw.Close()
                        trovato = True
                    End If
                End If
                myReader.Close()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If trovato = True Then
                    Dim rp As New Rpn
                    Dim i As Boolean
                    Dim K As Integer = 0

                    'Dim result As Integer = Rpn.RpsSetLicenseInfo("G927S-F6R7A-7VH31", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                    Dim result As Int64 = Rpn.RpsSetLicenseInfo("8RWQS-6Y9UC-HA2L1-91017", "srab35887-1", "S&S SISTEMI E SOLUZIONI S.R.L.")
                    If result = 0 Then
                        rp.InWebServer = True
                        rp.EmbedFonts = True
                        rp.ExactTextPlacement = True

                        i = rp.RpsConvertFile(fileName, fileNamePDF)
                        For K = 0 To 2000

                        Next
                        File.Delete(fileName)
                        Response.Write("<script>window.location.href='../FileTemp/" & BaseFile & ".pdf';</script>")
                    Else
                        Response.Write("<script>alert('Impossibile procedere. Motivo: " & result & "');self.close();</script>")
                    End If
                Else
                    Response.Write("<script>alert('Impossibile visualizzare il modello.');self.close();</script>")
                End If
            End If


        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Redirect("../Errore.aspx", False)
            Response.Write("<script>window.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
