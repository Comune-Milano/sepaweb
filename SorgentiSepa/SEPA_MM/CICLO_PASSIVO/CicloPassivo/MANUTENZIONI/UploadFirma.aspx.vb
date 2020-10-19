Imports System.IO

Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_UploadFirma
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
            connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                Try
                    connData.apri()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FIRME_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE")
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim firma As String = ""
                    If lettore.Read Then
                        firma = par.IfNull(lettore("firma"), "")
                    End If
                    lettore.Close()
                    If firma <> "" Then
                        If IO.File.Exists(Server.MapPath("../../../ALLEGATI/FIRME/" & firma)) Then
                            Immagine.Text = "<img src=""../../../ALLEGATI/FIRME/" & firma & """ alt="""" width=""300"" height=""120"">"
                        Else
                            Immagine.Text = "Firma non presente"
                        End If
                    Else
                        Immagine.Text = "Firma non presente"
                    End If
                    connData.chiudi()
                Catch ex As Exception
                    connData.chiudi()
                End Try
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ERJSCR", "<script>alert('Operatore non abilitato ad eseguire questa operazione!');location.href='../../pagina_home.aspx';</script>", False)
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.EventArgs) Handles ImageButton1.Click
        Try
            If FileUpload1.HasFile = True Then
                connData.apri()
                Dim estensione As String = System.IO.Path.GetExtension(FileUpload1.FileName).ToString.ToLower()
                If estensione = ".jpg" Or estensione = ".jpeg" Or estensione = ".png" Or estensione = ".bmp" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FIRME_OPERATORI WHERE ID_OPERATORE=" & Session.Item("id_operatore")
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.FIRME_OPERATORI SET FIRMA='" & Session.Item("ID_OPERATORE") & estensione & "' WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE")
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.FIRME_OPERATORI(ID_OPERATORE,FIRMA) VALUES (" & Session.Item("ID_OPERATORE") & ",'" & Session.Item("ID_OPERATORE") & estensione & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lettore.Close()
                    If Not Directory.Exists(Server.MapPath("~/ALLEGATI/FIRME")) Then
                        Directory.CreateDirectory(Server.MapPath("~/ALLEGATI/FIRME"))
                    End If
                    FileUpload1.SaveAs(Server.MapPath("~/ALLEGATI/FIRME/" & Session.Item("ID_OPERATORE") & estensione))
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ERJSCR", "<script>alert('Firma impostata correttamente!');location.href='UploadFirma.aspx'</script>", False)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ERJSCR", "<script>alert('L\'estensione del file non è consentita!');</script>", False)
                End If
                connData.chiudi()
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ERJSCR", "<script>alert('Selezionare il file da allegare!');</script>", False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ImageButton1_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ImageButton2_Click(sender As Object, e As EventArgs) Handles ImageButton2.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Private Function ControllaDati() As Boolean
        ControllaDati = True
    End Function

End Class
