
Partial Class Contratti_AU_abusivi_ScegliAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            CodContratto.Value = Request.QueryString("COD")
            CaricaBandi()
        End If
    End Sub

    Private Sub CaricaBandi()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            '"SELECT * FROM UTENZA_BANDI ORDER BY ID ASC"

            par.cmd.CommandText = "SELECT DESCRIZIONE,ID,(SELECT '- AU ART.15 già inserita' FROM UTENZA_DICHIARAZIONI WHERE ART_15=1 AND RAPPORTO='" & CodContratto.Value & "' AND ID_BANDO=UTENZA_BANDI.ID AND ID_STATO<>2) AS ART_15_FATTO FROM UTENZA_BANDI ORDER BY ID ASC"
            Dim I As Integer = 0

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                rdbListaAU.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), "") & par.IfNull(myReader("ART_15_FATTO"), ""), myReader("ID")))
                If par.IfNull(myReader("ART_15_FATTO"), "") <> "" Then
                    rdbListaAU.Items(I).Enabled = False
                End If
                I = I + 1
            End While
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Response.Redirect("CreaAU.aspx?T=" + Request.QueryString("T") + "&IDC=" + Request.QueryString("IDC") + "&COD=" + CodContratto.Value + "&IDB=" + rdbListaAU.SelectedValue)
    End Sub
End Class
