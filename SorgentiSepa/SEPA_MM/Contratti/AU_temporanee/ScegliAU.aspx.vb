
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

            par.cmd.CommandText = "SELECT DESCRIZIONE,ID,(SELECT ' - scheda già inserita' FROM UTENZA_DICHIARAZIONI WHERE RAPPORTO='" & CodContratto.Value & "' AND ID_BANDO=UTENZA_BANDI.ID AND ID_STATO<>2) AS FATTO FROM UTENZA_BANDI ORDER BY ID ASC"
            Dim I As Integer = 0

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                rdbListaAU.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), "") & par.IfNull(myReader("FATTO"), ""), myReader("ID")))
                If par.IfNull(myReader("FATTO"), "") <> "" Then
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

    Private Sub btnProcedi_Click(sender As Object, e As EventArgs) Handles btnProcedi.Click
        If rdbListaAU.SelectedValue <> "" Then
            Response.Redirect("CreaAU.aspx?T=" + Request.QueryString("T") + "&IDC=" + Request.QueryString("IDC") + "&COD=" + CodContratto.Value + "&IDB=" + rdbListaAU.SelectedValue)
        Else
            Response.Write("<script>alert('Selezionare l\'AU che si desidera importare.');</script>")
        End If
    End Sub
End Class
