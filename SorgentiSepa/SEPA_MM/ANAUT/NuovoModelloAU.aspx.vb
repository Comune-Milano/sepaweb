Imports System.IO

Partial Class ANAUT_NuovoModelloAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If IsPostBack = False Then
            Response.Flush()
            CaricaAU()
        End If

    End Sub


    Private Function CaricaAU()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.RiempiDList(Me, par.OracleConn, "cmbAU", "select * from utenza_bandi order by id desc", "DESCRIZIONE", "ID")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            If txtTitolo.Text <> "" Then
                If FileUpload1.HasFile = True Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "select * from UTENZA_TIPO_DOC where UPPER(DESCRIZIONE) = '" & UCase(txtTitolo.Text) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        MessJQuery("Esiste già un modello con questo titolo! Cambiare titolo o cancellare il precedente modello prima di inserirne uno nuovo.", 0, "Attenzione")
                    Else
                        Dim nFile As String = Server.MapPath("..\FileTemp\") & Format(Now, "HHmmss") & "_" & FileUpload1.FileName
                        FileUpload1.SaveAs(nFile)

                        par.cmd.CommandText = "INSERT INTO UTENZA_TIPO_DOC (ID,ID_BANDO,DESCRIZIONE,TIPO_DOC,DATA_INS,NOTE,MODELLO) VALUES (SEQ_UTENZA_TIPO_DOC.NEXTVAL," & cmbAU.SelectedItem.Value & ",'" & UCase(par.PulisciStrSql(txtTitolo.Text)) & "','RTF','" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(txtNote.Text) & "',:TESTO)"
                        Dim objStream As Stream = File.Open(nFile, FileMode.Open)
                        Dim buffer(objStream.Length) As Byte
                        objStream.Read(buffer, 0, objStream.Length)
                        objStream.Close()

                        Dim parmData As New Oracle.DataAccess.Client.OracleParameter
                        With parmData
                            .Direction = Data.ParameterDirection.Input
                            .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                            .ParameterName = "TESTO"
                            .Value = buffer
                        End With

                        par.cmd.Parameters.Add(parmData)
                        par.cmd.ExecuteNonQuery()
                        System.IO.File.Delete(nFile)
                        par.cmd.Parameters.Remove(parmData)
                        buffer = Nothing
                        objStream = Nothing
                        'MessJQuery("Operazione effettuata!", 1, "Info...")
                        Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

                    End If
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else
                    MessJQuery("Inserire un file RTF come modello prima di procedere!", 0, "Attenzione")
                End If
            Else
                MessJQuery("Inserire il titolo del modello!", 0, "Attenzione")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub


    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            'lblErrore.Text = ex.Message
            'lblErrore.Visible = True
        End Try
    End Sub
    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub SceltaJQuery(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "")
        Try
            Dim sc As String = ScriptScelta(Messaggio, Funzione, Titolo, Funzione2)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptScelta", sc, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function ScriptScelta(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptScelta').text('" & Messaggio & "');")
            sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
