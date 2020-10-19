
Partial Class CENSIMENTO_Report_Ricerca
    Inherits PageSetIdMode
    Dim Tipo As String = "1"
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            Tipo = Request.QueryString("T")
            Select Case Tipo
                Case "1"
                    lblTipo.Text = "Report Tecnico"
                Case "2"
                    lblTipo.Text = "Report Amministrativo"
                Case "3"
                    lblTipo.Text = "Report Tecnico UI Disdettate"
                Case "4"
                    lblTipo.Text = "per Assegnazione Immediata"
            End Select
            tipologia.Value = Tipo
            Carica()
        End If
    End Sub

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            '***CARICAMENTO liste
            cmbTipo.Items.Add(New ListItem(" ", -1))
            cmbTipo.Items.Add(New ListItem("ALLOGGI", 1))
            cmbTipo.Items.Add(New ListItem("USI DIVERSI", 2))

            cmbFiliale.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "SELECT TAB_FILIALI.ID,TAB_FILIALI.NOME,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO ORDER BY TAB_FILIALI.NOME ASC"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader2.Read
                cmbFiliale.Items.Add(New ListItem(par.IfNull(myReader2("NOME"), " ") & " - " & par.IfNull(myReader2("descrizione"), " ") & " " & par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), -1)))
            End While
            myReader2.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Label2.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Select Case tipologia.Value
            Case "1"
                Response.Write("<script>window.open('UiDisponibili.aspx?T=" & cmbTipo.SelectedItem.Value & "&F=" & cmbFiliale.SelectedItem.Value & "','Tecnico','');</script>")
            Case "2"
                Response.Write("<script>window.open('UiDisponibiliAmm.aspx?T=" & cmbTipo.SelectedItem.Value & "&F=" & cmbFiliale.SelectedItem.Value & "','Amministrativo','');</script>")
            Case "3"
                Response.Write("<script>window.open('UiDisdettateTec.aspx?T=" & cmbTipo.SelectedItem.Value & "&F=" & cmbFiliale.SelectedItem.Value & "','Tecnico','');</script>")
            Case "4"
                Response.Write("<script>window.open('UiDisponNuovaAssegn.aspx?T=" & cmbTipo.SelectedItem.Value & "&F=" & cmbFiliale.SelectedItem.Value & "','Tecnico','');</script>")
        End Select

    End Sub
End Class
