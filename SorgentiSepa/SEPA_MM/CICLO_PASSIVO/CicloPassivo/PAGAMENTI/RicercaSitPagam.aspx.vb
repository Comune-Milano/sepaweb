
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RicercaSitPagam
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String = "-1"
    Public sBP_Generale As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            If Session.Item("LIVELLO") <> "1" Then
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = Session.Item("ID_STRUTTURA")
                Else
                    sBP_Generale = Session.Item("ID_STRUTTURA")
                End If
            End If
            caricaStrutture()
            pianoFinanziario()
        End If
    End Sub

    Private Sub caricaStrutture()

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            cmbStrutture.Items.Add(New ListItem("--", "-1"))
            If sFiliale <> "-1" Then
                par.cmd.CommandText = "select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                par.cmd.CommandText = "select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                   & " where ID in (select distinct (ID_STRUTTURA) from SISCOM_MI.PF_VOCI_STRUTTURA) " _
                                   & " order by NOME asc"
            End If

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader.Read
                cmbStrutture.Items.Add(New ListItem(par.IfNull(myReader("NOME"), "--"), par.IfNull(myReader("ID"), -1)))
                If sFiliale <> "-1" Then
                    Me.cmbStrutture.SelectedValue = par.IfNull(myReader("ID"), -1)
                    Me.cmbStrutture.Enabled = False
                End If
            End While

            myReader.Close()

            If sFiliale <> "-1" Then
                Me.cmbStrutture.SelectedValue = sFiliale
                Me.cmbStrutture.Enabled = False

            Else
                If sBP_Generale <> "" Then
                    Me.cmbStrutture.SelectedValue = sBP_Generale
                Else
                    Me.cmbStrutture.SelectedValue = "-1"
                End If
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub pianoFinanziario()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT SISCOM_MI.PF_MAIN.id, TO_CHAR(TO_DATE(inizio,'yyyymmdd'),'dd/mm/yyyy') || ' - ' || TO_CHAR(TO_DATE(fine,'yyyymmdd'),'dd/mm/yyyy') AS eser_finanz FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO= SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID AND SISCOM_MI.PF_MAIN.ID_STATO=5"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                cmbEserFinanz.Items.Add(New ListItem(par.IfNull(myReader("eser_finanz"), "--"), par.IfNull(myReader("id"), -1)))
            End While

            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnAvanti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAvanti.Click
        Response.Write("<script>window.open('RisultatiSitPagam.aspx?Str=" & par.PulisciStrSql(cmbStrutture.SelectedItem.Value) & "&NomeStr=" & par.IfNull(cmbStrutture.SelectedItem.Text, "") & "&PF=" & par.IfNull(cmbEserFinanz.SelectedItem.Value, "") & "')</script>")
    End Sub
End Class
